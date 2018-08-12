import { Component, OnInit, Input, trigger, state, style, transition, animate } from '@angular/core';
import { ApplicationUser } from './application-user';
import { PerformanceEvaluation } from '../performanceevaluations/performance-evaluation';
import { PerformanceEvaluationService } from '../performanceevaluations/performance-evaluation.service';
import { ApplicationUserService } from './application-user.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';
import { NotificationService } from '../notifications/notification.service';

declare var swal: any;

@Component({
    templateUrl: 'app/Users/application-user-start-page.component.html',
    providers: [ApplicationUserService, PerformanceEvaluationService],
    animations: [
        trigger('animateIn', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateY(3%)'
                }),
                animate('0.2s ease-in')
            ]),
            transition('* => void', [
                animate('0.2s 10 ease-out', style({
                    opacity: 0,
                    transform: 'translateY(-3%)'
                }))
            ])
        ])
    ]
})

export class ApplicationUsersStartPageComponent implements OnInit {
    constructor(
        private applicationUserService: ApplicationUserService,
        private peService: PerformanceEvaluationService,
        private route: ActivatedRoute,
        private router: Router,
        private myGlobals: MyGlobals,
        private notificationService: NotificationService
    ) { }

    user: ApplicationUser;
    peConsultant: PerformanceEvaluation[];
    peHistoryConsultant: PerformanceEvaluation[];
    filteredPes: PerformanceEvaluation[];

    ngOnInit() {
        this.applicationUserService
            .getLoggedUser()
            .then(response => this.user = response)
            .catch((error) => this.myGlobals.showException(error, 'Error with getting logged user'));
        this.initializeForConsultant(); 
    }

    initializeForConsultant() {
        // get performance evaluations written for consultant
        this.peService.getMyPerformanceEvaluations()
            .then(response => this.peConsultant = response);

        // get performance evaluations written for consultant and signed by consultant
        this.peService.getMyPerformanceEvaluationsHistory()
            .then(response => {
                this.peHistoryConsultant = response;
                this.searchPerformanceEvaluationsHistory();
            });
    }

    acknowledgePe(id: number) {
        if (+localStorage.getItem('numberOfNotifications') > 0) {
            this.notificationService.getNotificationIdByPeId(id)
                .then(response => {
                    this.notificationService.setSeenStatus(response);
                });
        }     

        this.router.navigateByUrl('pe/acknowledge/' + id);
    }

    readPe(id: number) {
        this.router.navigateByUrl('pe/details/' + id);
    }

    createPe(userId: number) {
        let newPe = this.peService.createPerformanceEvaluation(userId)
            .then(response => this.router.navigateByUrl('pe/details/' + response.id));
    }

    searchPerformanceEvaluationsHistory() {
        this.filteredPes = Object.assign([], this.peHistoryConsultant);
    }

    filterItem(myInput: string) {
        if (!myInput) this.searchPerformanceEvaluationsHistory(); //when nothing has typed

        this.filteredPes = Object.assign([], this.peHistoryConsultant).filter(
            item => (item.reviewerFirstName + item.reviewerLast).toLowerCase()
                .indexOf(myInput.toLowerCase()) > -1
        )
    }
}

