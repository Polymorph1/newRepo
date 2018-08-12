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
var performance_evaluation_service_1 = require('../performanceevaluations/performance-evaluation.service');
var application_user_service_1 = require('./application-user.service');
var router_1 = require('@angular/router');
var router_2 = require('@angular/router');
var my_globals_1 = require('../my-globals');
var ApplicationUserReviewerPanelComponent = (function () {
    function ApplicationUserReviewerPanelComponent(applicationUserService, peService, route, router, myGlobals) {
        this.applicationUserService = applicationUserService;
        this.peService = peService;
        this.route = route;
        this.router = router;
        this.myGlobals = myGlobals;
    }
    ApplicationUserReviewerPanelComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.applicationUserService
            .getLoggedUser()
            .then(function (response) { return _this.user = response; })
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error with getting logged user'); });
        this.initializeForReviewer();
    };
    ApplicationUserReviewerPanelComponent.prototype.initializeForReviewer = function () {
        var _this = this;
        // get reviewer's team
        this.applicationUserService
            .getMyTeamReviewer()
            .then(function (response) { return _this.myTeamReviewer = response; })
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to access:', 'startPage'); });
        // get performance evaluations for reviewer
        this.peService.getPerformanceEvaluationsForReviewer()
            .then(function (response) { return _this.peReviewer = response; })
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to access:', 'startPage'); });
        // get performance written by reviewer
        this.peService.getPerformanceEvaluationsHistoryForReviewer()
            .then(function (response) {
            _this.peHistoryReviewer = response;
            _this.searchPerformanceEvaluationsHistory();
        })
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to access:', 'startPage'); });
    };
    ApplicationUserReviewerPanelComponent.prototype.editPe = function (id) {
        this.router.navigateByUrl('pe/edit/' + id);
    };
    ApplicationUserReviewerPanelComponent.prototype.readPe = function (id) {
        this.router.navigateByUrl('pe/details/' + id);
    };
    ApplicationUserReviewerPanelComponent.prototype.createPe = function (userId) {
        var _this = this;
        var newPe = this.peService.createPerformanceEvaluation(userId)
            .then(function (response) { _this.router.navigateByUrl('pe/edit/' + response.id); });
    };
    ApplicationUserReviewerPanelComponent.prototype.searchPerformanceEvaluationsHistory = function () {
        this.filteredPes = Object.assign([], this.peHistoryReviewer);
    };
    ApplicationUserReviewerPanelComponent.prototype.filterItem = function (myInput) {
        if (!myInput)
            this.searchPerformanceEvaluationsHistory(); //when nothing has typed
        this.filteredPes = Object.assign([], this.peHistoryReviewer).filter(function (item) { return (item.consultantFirstName + item.consultantLastName).toLowerCase()
            .indexOf(myInput.toLowerCase()) > -1; });
    };
    ApplicationUserReviewerPanelComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/users/application-user-reviewer-panel.component.html',
            providers: [application_user_service_1.ApplicationUserService, performance_evaluation_service_1.PerformanceEvaluationService],
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
        __metadata('design:paramtypes', [application_user_service_1.ApplicationUserService, performance_evaluation_service_1.PerformanceEvaluationService, router_1.ActivatedRoute, router_2.Router, my_globals_1.MyGlobals])
    ], ApplicationUserReviewerPanelComponent);
    return ApplicationUserReviewerPanelComponent;
}());
exports.ApplicationUserReviewerPanelComponent = ApplicationUserReviewerPanelComponent;
//# sourceMappingURL=application-user-reviewer-panel.component.js.map