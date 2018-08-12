import { Component, OnInit, Input, trigger, state, style, transition, animate, ViewContainerRef } from '@angular/core';
import { PerformanceEvaluation } from './performance-evaluation';
import { PerformanceEvaluationService } from './performance-evaluation.service';
import { ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { DialogService } from '../dialog.service';
import { MyGlobals } from '../my-globals';
import { ToastsManager, Toast } from 'ng2-toastr/ng2-toastr';

declare var swal: any;

@Component({
    templateUrl: 'app/performanceevaluations/performance-evaluation-edit.component.html',
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

export class PerformanceEvaluationEditComponent implements OnInit {

    performanceEvaluation: PerformanceEvaluation;
    form: FormGroup;
    //message: string = 'Discard unsaved changes?';

    constructor(
        private peService: PerformanceEvaluationService,
        private route: ActivatedRoute,
        private dialogService: DialogService,
        private fb: FormBuilder,
        private myGlobals: MyGlobals,
        private toastr: ToastsManager,
        vRef: ViewContainerRef
    ) {
        this.toastr.setRootViewContainerRef(vRef);
    }

    ngOnInit() {
        this.form = this.fb.group({
            gradesandcomments: this.fb.array([
                this.initGradesAndComments()
            ])
        });

        this.route.params.forEach((params: Params) => {
            let id = +params['id'];

            this.peService
                .getPerformanceEvaluationById(id)
                .then(response => {
                    this.performanceEvaluation = response;

                    for (var i = 0; i < response.performanceEvaluationSkills.length - 1; i++) {
                        this.addGradesAndComments();
                    }
                }).catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get evaluation:', 'startPage'));
        });
    }

    initGradesAndComments() {
        return this.fb.group({
            grade: ['', Validators.required],
            comment: ['', Validators.required]
        });
    }

    addGradesAndComments() {
        const control = <FormArray>this.form.controls['gradesandcomments'];
        control.push(this.initGradesAndComments());
    }

    savePe() {
        this.peService.save(this.performanceEvaluation)
            .then(response => {
                swal({
                    title: "Success!",
                    text: "Evaluation is saved.",
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
            })
            .catch((error) => {
                this.myGlobals.showException(error, 'Error occured while trying to save evaluation: ')
            })
    }

    submitPe() {
        this.dialogService.confirm('You will not be able to change this evaluation!')
            .then(response => {
                this.peService.submit(this.performanceEvaluation)
                    .then(this.goBack)
                    .catch((error) => {
                        this.myGlobals.showException(error, 'Error occured while trying to submit evaluation: ')
                    })
            });
    }

    goBack() {
        window.history.back();
    }

    
}




