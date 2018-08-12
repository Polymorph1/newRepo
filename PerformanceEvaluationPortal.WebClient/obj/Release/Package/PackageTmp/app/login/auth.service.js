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
var http_1 = require('@angular/http');
var router_1 = require('@angular/router');
var my_globals_1 = require('../my-globals');
var application_user_service_1 = require('../users/application-user.service');
var notification_service_1 = require('../notifications/notification.service');
var global_events_manager_1 = require('../common/global-events-manager');
require('rxjs/Rx');
var AuthService = (function () {
    function AuthService(http, myGlobals, router, userService, notificationService, globalEventsManager) {
        this.http = http;
        this.myGlobals = myGlobals;
        this.router = router;
        this.userService = userService;
        this.notificationService = notificationService;
        this.globalEventsManager = globalEventsManager;
        this.isLoggedIn = false;
        this.isAdmin = false;
        this.isManager = false;
        this.isReviewer = false;
        this.isLoggedIn = !!localStorage.getItem('auth_token');
        this.isAdmin = !!localStorage.getItem('isAdmin');
        this.isManager = !!localStorage.getItem('isManager');
        this.isReviewer = !!localStorage.getItem('isReviewer');
    }
    AuthService.prototype.login = function (username, password) {
        var _this = this;
        var data = "grant_type=password&username=" + username + "&password=" + password;
        var contentHeaders = new http_1.Headers();
        contentHeaders.append('Content-Type', 'application/x-www-form-urlencoded');
        return this.http.post(this.myGlobals.WebApiUrl + 'token', data, { headers: contentHeaders })
            .toPromise()
            .then(function (response) {
            _this.handleSuccess(response);
            console.log(response);
        })
            .catch(this.handleError);
    };
    AuthService.prototype.logout = function () {
        localStorage.removeItem('auth_token');
        this.isLoggedIn = false;
        localStorage.removeItem('isAdmin');
        this.isAdmin = false;
        localStorage.removeItem('isManager');
        this.isManager = false;
        localStorage.removeItem('isReviewer');
        this.isReviewer = false;
        localStorage.removeItem('loggedUser');
        localStorage.removeItem('userName');
        localStorage.removeItem("numberOfNotifications");
    };
    AuthService.prototype.handleSuccess = function (response) {
        localStorage.setItem('auth_token', response.json().access_token);
        var roles = JSON.parse(response.json().roles);
        var isAdmin = roles.filter(function (role) { return role == 'Admin'; }).length > 0;
        if (isAdmin) {
            localStorage.setItem('isAdmin', 'true');
            this.isAdmin = true;
        }
        var IsManager = JSON.parse(response.json().isManager);
        if (IsManager) {
            localStorage.setItem('isManager', 'true');
            this.isManager = true;
        }
        var IsReviewer = JSON.parse(response.json().isReviewer);
        if (IsReviewer) {
            localStorage.setItem('isReviewer', 'true');
            this.isReviewer = true;
        }
        localStorage.setItem("loggedUser", response.json().loggedUser);
        var userName = response.json().userName;
        localStorage.setItem("userName", userName);
        this.globalEventsManager.loggedUserName.emit(userName);
        this.isLoggedIn = true;
        this.notificationService.initNotifications(userName);
    };
    AuthService.prototype.handleError = function (error) {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    };
    AuthService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http, my_globals_1.MyGlobals, router_1.Router, application_user_service_1.ApplicationUserService, notification_service_1.NotificationService, global_events_manager_1.GlobalEventsManager])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map