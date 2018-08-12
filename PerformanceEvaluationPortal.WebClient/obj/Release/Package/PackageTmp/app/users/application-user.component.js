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
var my_globals_1 = require('../my-globals');
var ApplicationUserComponent = (function () {
    function ApplicationUserComponent(usersService, router, myGlobals) {
        this.usersService = usersService;
        this.router = router;
        this.myGlobals = myGlobals;
        this.numberOfUsers = 0;
        this.pageNumber = [];
        this.usersPerPage = 2;
        this.selectedPage = 0;
    }
    ApplicationUserComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.usersService
            .getUsers()
            .then(function (response) {
            _this.users = response;
            _this.assignCopy();
        })
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get users: '); });
    };
    ApplicationUserComponent.prototype.assignCopy = function () {
        this.filteredUsers = Object.assign([], this.users);
    };
    ApplicationUserComponent.prototype.filterItem = function (myInput) {
        if (!myInput)
            this.assignCopy(); //when nothing has typed
        this.filteredUsers = Object.assign([], this.users).filter(function (item) { return (item.firstName + item.lastName).toLowerCase().indexOf(myInput.toLowerCase()) > -1; });
    };
    ApplicationUserComponent.prototype.goToDetail = function (user) {
        var link = ['/user/details', user.id];
        this.router.navigate(link);
    };
    ApplicationUserComponent.prototype.getPerformanceEvaluationsForUser = function (user) {
        var link = ['/users/performanceevaluations', user.id];
        this.router.navigate(link);
    };
    ApplicationUserComponent.prototype.setPage = function (page) {
        var _this = this;
        this.selectedPage = page;
        this.usersService.getUsers().then(function (response) { _this.filteredUsers = response; });
    };
    ApplicationUserComponent = __decorate([
        core_1.Component({
            selector: 'my-users',
            templateUrl: 'app/users/application-user.component.html',
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
        __metadata('design:paramtypes', [application_user_service_1.ApplicationUserService, router_1.Router, my_globals_1.MyGlobals])
    ], ApplicationUserComponent);
    return ApplicationUserComponent;
}());
exports.ApplicationUserComponent = ApplicationUserComponent;
//# sourceMappingURL=application-user.component.js.map