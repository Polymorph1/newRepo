import { Component, OnInit, Input, trigger, state, style, transition, animate } from '@angular/core';
import { ApplicationUser } from './application-user';
import { PerformanceEvaluation } from '../performanceevaluations/performance-evaluation';
import { PerformanceEvaluationService } from '../performanceevaluations/performance-evaluation.service';
import { ApplicationUserService } from './application-user.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';

declare var swal: any;

@Component({
    templateUrl: 'app/users/application-user-manager-panel.component.html',
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

export class ApplicationUserManagerPanelComponent implements OnInit {
    constructor(
        private applicationUserService: ApplicationUserService,
        private peService: PerformanceEvaluationService,
        private route: ActivatedRoute,
        private router: Router,
        private myGlobals: MyGlobals
    ) { }

    user: ApplicationUser;
    myTeamManager: ApplicationUser[];
    peManager: PerformanceEvaluation[];
    peHistoryManager: PerformanceEvaluation[];
    filteredPes: PerformanceEvaluation[];

    ngOnInit() {
        this.applicationUserService
            .getLoggedUser()
            .then(response => this.user = response)
            .catch((error) => this.myGlobals.showException(error, 'Error with getting logged user'));

        this.initializeForManager();
    }

    initializeForManager() {
        // get manager's team
        this.applicationUserService
            .getMyTeamManager()
            .then(response => this.myTeamManager = response)
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to access:','startPage'));

        // get performance evaluations written 
        this.peService.getPerformanceEvaluationsForManager()
            .then(response => this.peManager = response)
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to access:','startPage'));

        // get performance evaluations history written by reviewers I manage
        this.peService.getPerformanceEvaluationsHistoryForManager()
            .then(response => {
                this.peHistoryManager = response;
                this.searchPerformanceEvaluationsHistory();
            })
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to access:','startPage'));
    }

    readPe(id: number) {
        this.router.navigateByUrl('pe/details/' + id);
    }

    searchPerformanceEvaluationsHistory() {
        this.filteredPes = Object.assign([], this.peHistoryManager);
    }

    filterItem(myInput: string) {
        if (!myInput) this.searchPerformanceEvaluationsHistory(); //when nothing has typed

        this.filteredPes = Object.assign([], this.peHistoryManager).filter(
            item => (item.consultantFirstName + item.consultantLastName + item.reviewerFirstName + item.reviewerLastName).toLowerCase()
                .indexOf(myInput.toLowerCase()) > -1
        )
    }

   
}