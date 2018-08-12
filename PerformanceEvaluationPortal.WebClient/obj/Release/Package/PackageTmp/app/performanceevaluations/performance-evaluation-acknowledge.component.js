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
var PerformanceEvaluationAcknowledgeComponent = (function () {
    function PerformanceEvaluationAcknowledgeComponent(peService, route, dialogService, fb, myGlobals) {
        this.peService = peService;
        this.route = route;
        this.dialogService = dialogService;
        this.myGlobals = myGlobals;
        this.form = fb.group({
            cb: [false, forms_1.Validators.pattern('true')],
            txt: ['', forms_1.Validators.required],
        });
    }
    PerformanceEvaluationAcknowledgeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = +params['id'];
            _this.peService
                .getPerformanceEvaluationById(id)
                .then(function (response) { return _this.performanceEvaluation = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get evaluation:', 'startPage'); });
        });
    };
    PerformanceEvaluationAcknowledgeComponent.prototype.acknowledgePe = function () {
        var _this = this;
        this.dialogService.confirm('You will not be able to change this data!')
            .then(function (response) {
            _this.peService.acknowledgePe(_this.performanceEvaluation)
                .then(_this.goBack)
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to acknowledge evaluation:'); });
        });
    };
    PerformanceEvaluationAcknowledgeComponent.prototype.goBack = function () {
        window.history.back();
    };
    PerformanceEvaluationAcknowledgeComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/performanceevaluations/performance-evaluation-acknowledge.component.html',
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
        __metadata('design:paramtypes', [performance_evaluation_service_1.PerformanceEvaluationService, router_1.ActivatedRoute, dialog_service_1.DialogService, forms_1.FormBuilder, my_globals_1.MyGlobals])
    ], PerformanceEvaluationAcknowledgeComponent);
    return PerformanceEvaluationAcknowledgeComponent;
}());
exports.PerformanceEvaluationAcknowledgeComponent = PerformanceEvaluationAcknowledgeComponent;
//# sourceMappingURL=performance-evaluation-acknowledge.component.js.map