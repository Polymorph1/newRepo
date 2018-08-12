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
var application_user_service_1 = require('./application-user.service');
var router_1 = require('@angular/router');
var router_2 = require('@angular/router');
var my_globals_1 = require('../my-globals');
var ApplicationUsersDetailsComponent = (function () {
    function ApplicationUsersDetailsComponent(applicationUserService, route, router, myGlobals) {
        this.applicationUserService = applicationUserService;
        this.route = route;
        this.router = router;
        this.myGlobals = myGlobals;
    }
    ApplicationUsersDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = params['id'];
            _this.applicationUserService
                .getUserById(id)
                .then(function (response) { return _this.user = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error with getting user', 'users'); });
        });
    };
    ApplicationUsersDetailsComponent.prototype.saveUser = function (user) {
        var id = user.id;
        var link = ['/users/save', id];
        this.router.navigate(link);
    };
    ApplicationUsersDetailsComponent.prototype.goBack = function () {
        window.history.back();
    };
    ApplicationUsersDetailsComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/users/application-user-details.component.html',
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
        __metadata('design:paramtypes', [application_user_service_1.ApplicationUserService, router_1.ActivatedRoute, router_2.Router, my_globals_1.MyGlobals])
    ], ApplicationUsersDetailsComponent);
    return ApplicationUsersDetailsComponent;
}());
exports.ApplicationUsersDetailsComponent = ApplicationUsersDetailsComponent;
//# sourceMappingURL=application-user-details.component.js.map