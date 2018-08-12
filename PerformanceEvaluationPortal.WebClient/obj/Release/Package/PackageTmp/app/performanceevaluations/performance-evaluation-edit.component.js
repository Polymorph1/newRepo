"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var performance_evaluation_service_1 = require('./performance-evaluation.service');
var router_1 = require('@angular/router');
var forms_1 = require('@angular/forms');
var dialog_service_1 = require('../dialog.service');
var my_globals_1 = require('../my-globals');
var ng2_toastr_1 = require('ng2-toastr/ng2-toastr');
var PerformanceEvaluationEditComponent = (function () {
    //message: string = 'Discard unsaved changes?';
    function PerformanceEvaluationEditComponent(peService, route, dialogService, fb, myGlobals, toastr, vRef) {
        this.peService = peService;
        this.route = route;
        this.dialogService = dialogService;
        this.fb = fb;
        this.myGlobals = myGlobals;
        this.toastr = toastr;
        this.toastr.setRootViewContainerRef(vRef);
    }
    PerformanceEvaluationEditComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.form = this.fb.group({
            gradesandcomments: this.fb.array([
                this.initGradesAndComments()
            ])
        });
        this.route.params.forEach(function (params) {
            var id = +params['id'];
            _this.peService
                .getPerformanceEvaluationById(id)
                .then(function (response) {
                _this.performanceEvaluation = response;
                for (var i = 0; i < response.performanceEvaluationSkills.length - 1; i++) {
                    _this.addGradesAndComments();
                }
            }).catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get evaluation:', 'startPage'); });
        });
    };
    PerformanceEvaluationEditComponent.prototype.initGradesAndComments = function () {
        return this.fb.group({
            grade: ['', forms_1.Validators.required],
            comment: ['', forms_1.Validators.required]
        });
    };
    PerformanceEvaluationEditComponent.prototype.addGradesAndComments = function () {
        var control = this.form.controls['gradesandcomments'];
        control.push(this.initGradesAndComments());
    };
    PerformanceEvaluationEditComponent.prototype.savePe = function () {
        var _this = this;
        this.peService.save(this.performanceEvaluation)
            .then(function (response) {
            swal({
                title: "Success!",
                text: "Evaluation is saved.",
                type: "success",
                timer: 2000,
                showConfirmButton: false
            });
        })
            .catch(function (error) {
            _this.myGlobals.showException(error, 'Error occured while trying to save evaluation: ');
        });
    };
    PerformanceEvaluationEditComponent.prototype.submitPe = function () {
        var _this = this;
        this.dialogService.confirm('You will not be able to change this evaluation!')
            .then(function (response) {
            _this.peService.submit(_this.performanceEvaluation)
                .then(_this.goBack)
                .catch(function (error) {
                _this.myGlobals.showException(error, 'Error occured while trying to submit evaluation: ');
            });
        });
    };
    PerformanceEvaluationEditComponent.prototype.goBack = function () {
        window.history.back();
    };
    PerformanceEvaluationEditComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/performanceevaluations/performance-evaluation-edit.component.html',
            animations: [
                core_1.trigger('animateIn', [
                    core_1.state('in', core_1.style({ opacity: 1, transform: 'translateX(0)' })),
                    core_1.transition('void => *', [
                        core_1.style({
                            opacity: 0,
                            transform: 'translateY(3%)'
                        }),
                        core_1.animate('0.2s ease-in')
                    ]),
                    core_1.transition('* => void', [
                        core_1.animate('0.2s 10 ease-out', core_1.style({
                            opacity: 0,
                            transform: 'translateY(-3%)'
                        }))
                    ])
                ])
            ]
        }), 
        __metadata('design:paramtypes', [performance_evaluation_service_1.PerformanceEvaluationService, router_1.ActivatedRoute, dialog_service_1.DialogService, forms_1.FormBuilder, my_globals_1.MyGlobals, ng2_toastr_1.ToastsManager, core_1.ViewContainerRef])
    ], PerformanceEvaluationEditComponent);
    return PerformanceEvaluationEditComponent;
}());
exports.PerformanceEvaluationEditComponent = PerformanceEvaluationEditComponent;
//# sourceMappingURL=performance-evaluation-edit.component.js.map