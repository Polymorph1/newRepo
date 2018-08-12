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
require('rxjs/add/operator/toPromise');
var http_client_1 = require('../common/http.client');
var my_globals_1 = require('../my-globals');
var ApplicationUserService = (function () {
    function ApplicationUserService(httpClient, myGlobals) {
        this.httpClient = httpClient;
        this.myGlobals = myGlobals;
        this.usersUrl = this.myGlobals.WebApiUrl + 'api/';
    }
    ApplicationUserService.prototype.getUsers = function () {
        var url = this.usersUrl + 'ApplicationUsers';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.getUsersByInput = function (searchBy) {
        var url = this.usersUrl + 'ApplicationUsers?=' + searchBy;
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.getLoggedUser = function () {
        var url = this.usersUrl + 'GetLoggedUser';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.getUserById = function (id) {
        var url = this.usersUrl + 'GetUserById/' + id;
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.getMyTeamReviewer = function () {
        var url = this.usersUrl + 'GetMyTeamReviewer';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.getMyTeamManager = function () {
        var url = this.usersUrl + 'GetMyTeamManager';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.save = function (user) {
        return this.put(user);
    };
    ApplicationUserService.prototype.put = function (user) {
        var url = this.usersUrl + 'ApplicationUsers/' + user.id;
        return this.httpClient
            .put(url, JSON.stringify(user)).toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ApplicationUserService.prototype.handleError = function (error) {
        return Promise.reject(error.message || error);
    };
    ApplicationUserService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_client_1.HttpClient, my_globals_1.MyGlobals])
    ], ApplicationUserService);
    return ApplicationUserService;
}());
exports.ApplicationUserService = ApplicationUserService;
//# sourceMappingURL=application-user.service.js.map