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
var application_user_1 = require('./application-user');
var application_user_service_1 = require('./application-user.service');
var router_1 = require('@angular/router');
var job_title_service_1 = require('../jobtitles/job-title.service');
var job_position_service_1 = require('../jobpositions/job-position.service');
var template_service_1 = require('../templates/template.service');
var router_2 = require('@angular/router');
var my_globals_1 = require('../my-globals');
var ApplicationUserSaveComponent = (function () {
    function ApplicationUserSaveComponent(applicationUserService, jobTitlesService, jobPositionsService, templatesService, route, router, myGlobals) {
        this.applicationUserService = applicationUserService;
        this.jobTitlesService = jobTitlesService;
        this.jobPositionsService = jobPositionsService;
        this.templatesService = templatesService;
        this.route = route;
        this.router = router;
        this.myGlobals = myGlobals;
        this.submitted = false;
        this.user = new application_user_1.ApplicationUser();
        this.managers = [];
        this.reviewers = [];
        this.titles = [];
        this.positions = [];
        this.templates = [];
    }
    ApplicationUserSaveComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = params['id'];
            if (id) {
                _this.applicationUserService
                    .getUserById(id)
                    .then(function (response) { return _this.user = response; }).then(function (response) { _this.userToEdit = _this.user.firstName + " " + _this.user.lastName; })
                    .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get user'); });
            }
            //one call for get users?
            _this.applicationUserService.getUsers().then(function (response) { return _this.managers = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get users'); }); /*.then(x => { this.managers = this.managers.filter(y => y.id != id) })*/
            _this.applicationUserService.getUsers().then(function (response) { return _this.reviewers = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get users'); }); /*.then(x => { this.reviewers = this.reviewers.filter(y => y.id != id) });*/
            _this.jobTitlesService.getJobTitles().then(function (response) { return _this.titles = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get titles'); });
            _this.jobPositionsService.getJobPositions().then(function (response) { return _this.positions = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get positions'); });
            _this.templatesService.getActiveTemplates().then(function (response) { return _this.templates = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get templates'); });
        });
        this.validFirst = true;
        this.validLast = true;
    };
    ApplicationUserSaveComponent.prototype.onSubmit = function () {
        this.save();
    };
    ApplicationUserSaveComponent.prototype.validName = function (name) {
        var regex = "^[a-zšđčćž A-ZŠĐČĆŽ]+$";
        var isValid = name.match(regex);
        if (isValid)
            this.validFirst = true;
        else
            this.validFirst = false;
    };
    ApplicationUserSaveComponent.prototype.validLastName = function (name) {
        var regex = "^[a-zšđčćž A-ZŠĐČĆŽ]+$";
        var isValid = name.match(regex);
        if (isValid)
            this.validLast = true;
        else
            this.validLast = false;
    };
    ApplicationUserSaveComponent.prototype.save = function () {
        var _this = this;
        this.applicationUserService.save(this.user)
            .then(this.goBack)
            .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to edit user: '); });
    };
    ApplicationUserSaveComponent.prototype.goBack = function () {
        window.history.back();
    };
    ApplicationUserSaveComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/users/application-user-save.component.html'
        }), 
        __metadata('design:paramtypes', [application_user_service_1.ApplicationUserService, job_title_service_1.JobTitlesService, job_position_service_1.JobPositionsService, template_service_1.TemplatesService, router_1.ActivatedRoute, router_2.Router, my_globals_1.MyGlobals])
    ], ApplicationUserSaveComponent);
    return ApplicationUserSaveComponent;
}());
exports.ApplicationUserSaveComponent = ApplicationUserSaveComponent;
//# sourceMappingURL=application-user-save.component.js.map