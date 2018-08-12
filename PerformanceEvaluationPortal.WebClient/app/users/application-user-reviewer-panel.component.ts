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
    templateUrl: 'app/users/application-user-reviewer-panel.component.html',
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

export class ApplicationUserReviewerPanelComponent implements OnInit {
    constructor(
        private applicationUserService: ApplicationUserService,
        private peService: PerformanceEvaluationService,
        private route: ActivatedRoute,
        private router: Router,
        private myGlobals: MyGlobals
    ) { }

    user: ApplicationUser;
    myTeamReviewer: ApplicationUser[];
    peReviewer: PerformanceEvaluation[];
    peHistoryReviewer: PerformanceEvaluation[];
    filteredPes: PerformanceEvaluation[];
    itemPerPage: number = 3;

    ngOnInit() {
        this.applicationUserService
            .getLoggedUser()
            .then(response => this.user = response)
            .catch((error) => this.myGlobals.showException(error, 'Error with getting logged user'));

        this.initializeForReviewer();
    }
    showMore()
    {
        if(this.filteredPes.length>this.itemPerPage)
        this.itemPerPage += 3;
    }

    initializeForReviewer() {
        // get reviewer's team
        this.applicationUserService
            .getMyTeamReviewer()
            .then(response => this.myTeamReviewer = response)
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to access:','startPage'));

        // get performance evaluations for reviewer
        this.peService.getPerformanceEvaluationsForReviewer()
            .then(response => this.peReviewer = response)
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to access:','startPage'));

        // get performance written by reviewer
        this.peService.getPerformanceEvaluationsHistoryForReviewer()
            .then(response => {
                this.peHistoryReviewer = response;
                this.searchPerformanceEvaluationsHistory();
            })
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to access:','startPage'));
    }

    editPe(id: number) {
        this.router.navigateByUrl('pe/edit/' + id)
    }

    readPe(id: number) {
        this.router.navigateByUrl('pe/details/' + id)
    }

    createPe(userId: number) {
        let newPe = this.peService.createPerformanceEvaluation(userId)
            .then(response => { this.router.navigateByUrl('pe/edit/' + response.id) });
    }

    searchPerformanceEvaluationsHistory() {
        this.filteredPes = Object.assign([], this.peHistoryReviewer);
    }

    filterItem(myInput: string) {
        if (!myInput) this.searchPerformanceEvaluationsHistory(); //when nothing has typed

        this.filteredPes = Object.assign([], this.peHistoryReviewer).filter(
            item => (item.consultantFirstName + item.consultantLastName).toLowerCase()
                .indexOf(myInput.toLowerCase()) > -1
        )
    }
  
}