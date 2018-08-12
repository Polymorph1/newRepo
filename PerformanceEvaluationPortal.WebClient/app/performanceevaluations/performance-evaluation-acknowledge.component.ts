import { Component, OnInit, Input, trigger, state, style, transition, animate} from '@angular/core';
import { PerformanceEvaluation } from './performance-evaluation';
import { PerformanceEvaluationService } from './performance-evaluation.service';
import { ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { DialogService } from '../dialog.service';
import { MyGlobals } from '../my-globals';

declare var swal: any;

@Component({
    templateUrl: 'app/performanceevaluations/performance-evaluation-acknowledge.component.html',
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

export class PerformanceEvaluationAcknowledgeComponent implements OnInit {

    performanceEvaluation: PerformanceEvaluation;
    form: FormGroup;

    constructor(
        private peService: PerformanceEvaluationService,
        private route: ActivatedRoute,
        private dialogService: DialogService,
        fb: FormBuilder,
        private myGlobals: MyGlobals
    ) { 
        this.form = fb.group({
            cb: [false, Validators.pattern('true')],
            txt: ['', Validators.required],
        });  
    }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {

            let id = +params['id'];

            this.peService
                .getPerformanceEvaluationById(id)
                .then(response => this.performanceEvaluation = response)
                .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get evaluation:','startPage'));
        });
    }

    acknowledgePe() {
        this.dialogService.confirm('You will not be able to change this data!')
            .then(response => {
                this.peService.acknowledgePe(this.performanceEvaluation)
                    .then(this.goBack)
                    .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to acknowledge evaluation:'));
            });
    }      


    goBack() {
        window.history.back();
    }
}
