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
var router_1 = require('@angular/router');
var my_globals_1 = require('../my-globals');
var TemplatesService = (function () {
    function TemplatesService(httpClient, myGlobals, router) {
        this.httpClient = httpClient;
        this.myGlobals = myGlobals;
        this.router = router;
        this.templateUrl = this.myGlobals.WebApiUrl + 'api';
    }
    TemplatesService.prototype.getActiveTemplates = function () {
        var url = this.templateUrl + '/Templates/';
        return this.httpClient.get(url)
            .toPromise().then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    TemplatesService.prototype.getTemplateById = function (id) {
        var url = this.templateUrl + '/Templates/' + id;
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    TemplatesService.prototype.getArchivedTemplates = function () {
        var url = this.templateUrl + '/GetArchivedTemplates';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    TemplatesService.prototype.archiveTemplate = function (id) {
        var url = this.templateUrl + '/ArchiveTemplate/' + id;
        return this.httpClient.put(url, null).toPromise().then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    TemplatesService.prototype.save = function (template) {
        if (template.id) {
            return this.put(template);
        }
        return this.post(template);
    };
    TemplatesService.prototype.put = function (template) {
        var url = this.templateUrl + '/Templates/' + template.id;
        return this.httpClient
            .put(url, JSON.stringify(template))
            .toPromise().then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    TemplatesService.prototype.post = function (template) {
        var url = this.templateUrl + '/Templates';
        return this.httpClient
            .post(url, JSON.stringify(template))
            .toPromise().then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    TemplatesService.prototype.handleError = function (error) {
        return Promise.reject(error.message || error);
    };
    TemplatesService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_client_1.HttpClient, my_globals_1.MyGlobals, router_1.Router])
    ], TemplatesService);
    return TemplatesService;
}());
exports.TemplatesService = TemplatesService;
//# sourceMappingURL=template.service.js.map