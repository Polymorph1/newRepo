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
var JobPositionsService = (function () {
    function JobPositionsService(httpClient, myGlobals) {
        this.httpClient = httpClient;
        this.myGlobals = myGlobals;
        this.jobPositionUrl = this.myGlobals.WebApiUrl + 'api/JobPositions';
    }
    JobPositionsService.prototype.getJobPositions = function () {
        return this.httpClient.get(this.jobPositionUrl)
            .toPromise().then(function (response) { return response.json(); }).catch(this.handleError);
    };
    JobPositionsService.prototype.handleError = function (error) {
        return Promise.reject(error.message || error);
    };
    JobPositionsService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_client_1.HttpClient, my_globals_1.MyGlobals])
    ], JobPositionsService);
    return JobPositionsService;
}());
exports.JobPositionsService = JobPositionsService;
//# sourceMappingURL=job-position.service.js.map