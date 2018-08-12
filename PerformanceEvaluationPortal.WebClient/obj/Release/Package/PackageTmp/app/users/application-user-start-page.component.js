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
var notification_service_1 = require('../notifications/notification.service');
var ApplicationUsersStartPageComponent = (function () {
    function ApplicationUsersStartPageComponent(applicationUserService, peService, route, router, myGlobals, notificationService) {
        this.applicationUserService = applicationUserService;
        this.peService = peService;
        this.route = route;
        this.router = router;
        this.myGlobals = myGlobals;
        this.notificationService = notificationService;
    }
    ApplicationUsersStartPageComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.applicationUserService
            .getLoggedUser()
            .then(function (response) { return _this.user = response; })
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error with getting logged user'); });
        this.initializeForConsultant();
    };
    ApplicationUsersStartPageComponent.prototype.initializeForConsultant = function () {
        var _this = this;
        // get performance evaluations written for consultant
        this.peService.getMyPerformanceEvaluations()
            .then(function (response) { return _this.peConsultant = response; });
        // get performance evaluations written for consultant and signed by consultant
        this.peService.getMyPerformanceEvaluationsHistory()
            .then(function (response) {
            _this.peHistoryConsultant = response;
            _this.searchPerformanceEvaluationsHistory();
        });
    };
    ApplicationUsersStartPageComponent.prototype.acknowledgePe = function (id) {
        this.router.navigateByUrl('pe/acknowledge/' + id);
    };
    ApplicationUsersStartPageComponent.prototype.readPe = function (id) {
        this.router.navigateByUrl('pe/details/' + id);
    };
    ApplicationUsersStartPageComponent.prototype.createPe = function (userId) {
        var _this = this;
        var newPe = this.peService.createPerformanceEvaluation(userId)
            .then(function (response) { return _this.router.navigateByUrl('pe/details/' + response.id); });
    };
    ApplicationUsersStartPageComponent.prototype.searchPerformanceEvaluationsHistory = function () {
        this.filteredPes = Object.assign([], this.peHistoryConsultant);
    };
    ApplicationUsersStartPageComponent.prototype.filterItem = function (myInput) {
        if (!myInput)
            this.searchPerformanceEvaluationsHistory(); //when nothing has typed
        this.filteredPes = Object.assign([], this.peHistoryConsultant).filter(function (item) { return (item.reviewerFirstName + item.reviewerLast).toLowerCase()
            .indexOf(myInput.toLowerCase()) > -1; });
    };
    ApplicationUsersStartPageComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/Users/application-user-start-page.component.html',
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
        __metadata('design:paramtypes', [application_user_service_1.ApplicationUserService, performance_evaluation_service_1.PerformanceEvaluationService, router_1.ActivatedRoute, router_2.Router, my_globals_1.MyGlobals, notification_service_1.NotificationService])
    ], ApplicationUsersStartPageComponent);
    return ApplicationUsersStartPageComponent;
}());
exports.ApplicationUsersStartPageComponent = ApplicationUsersStartPageComponent;
//# sourceMappingURL=application-user-start-page.component.js.map