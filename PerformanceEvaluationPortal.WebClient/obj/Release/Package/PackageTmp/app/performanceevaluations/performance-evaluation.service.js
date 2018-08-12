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
require('rxjs/add/operator/catch');
var my_globals_1 = require('../my-globals');
var PerformanceEvaluationService = (function () {
    function PerformanceEvaluationService(httpClient, myGlobals) {
        this.httpClient = httpClient;
        this.myGlobals = myGlobals;
        this.getUrl = this.myGlobals.WebApiUrl + 'api';
    }
    PerformanceEvaluationService.prototype.getPerformanceEvaluationById = function (id) {
        var url = this.getUrl + '/GetPerformanceEvaluationsById?performanceEvaluationId=' + id;
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    PerformanceEvaluationService.prototype.getPerformanceEvaluationsForReviewer = function () {
        var url = this.getUrl + '/GetPerformanceEvaluationsForReviewer';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    PerformanceEvaluationService.prototype.getPerformanceEvaluationsHistoryForReviewer = function () {
        var url = this.getUrl + '/GetPerformanceEvaluationHistoryForReviewer';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    PerformanceEvaluationService.prototype.getPerformanceEvaluationsForManager = function () {
        var url = this.getUrl + '/GetPerformanceEvaluationsForManager';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    PerformanceEvaluationService.prototype.getPerformanceEvaluationsHistoryForManager = function () {
        var url = this.getUrl + '/GetPerformanceEvaluationHistoryForManager';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    PerformanceEvaluationService.prototype.getMyPerformanceEvaluations = function () {
        var url = this.getUrl + '/GetMyPerformanceEvaluations?signed=false';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    PerformanceEvaluationService.prototype.getMyPerformanceEvaluationsHistory = function () {
        var url = this.getUrl + '/GetMyPerformanceEvaluations?signed=true';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    PerformanceEvaluationService.prototype.createPerformanceEvaluation = function (userId) {
        var url = this.getUrl + '/CreatePerformanceEvaluation/' + userId;
        return this.httpClient
            .post(url, userId)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    PerformanceEvaluationService.prototype.save = function (pe) {
        var url = this.getUrl + '/SavePerformanceEvaluation';
        return this.httpClient
            .put(url, pe)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    PerformanceEvaluationService.prototype.submit = function (pe) {
        var url = this.getUrl + '/SubmitPerformanceEvaluation';
        return this.httpClient
            .put(url, pe)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    PerformanceEvaluationService.prototype.acknowledgePe = function (pe) {
        var url = this.getUrl + '/AcknowledgePerformanceEvaluation';
        return this.httpClient
            .put(url, pe)
            .toPromise().then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    PerformanceEvaluationService.prototype.handleError = function (error) {
        return Promise.reject(error.message || error);
    };
    PerformanceEvaluationService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_client_1.HttpClient, my_globals_1.MyGlobals])
    ], PerformanceEvaluationService);
    return PerformanceEvaluationService;
}());
exports.PerformanceEvaluationService = PerformanceEvaluationService;
//# sourceMappingURL=performance-evaluation.service.js.map