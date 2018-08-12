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
var template_1 = require('./template');
var template_service_1 = require('./template.service');
var router_1 = require('@angular/router');
var skill_service_1 = require('../skills/skill.service');
var skill_1 = require('../skills/skill');
var template_duration_service_1 = require('../templatedurations/template-duration.service');
var my_globals_1 = require('../my-globals');
var TemplateSaveComponent = (function () {
    function TemplateSaveComponent(templatesService, skillService, templateDurationService, route, myGlobals) {
        this.templatesService = templatesService;
        this.skillService = skillService;
        this.templateDurationService = templateDurationService;
        this.route = route;
        this.myGlobals = myGlobals;
        this.template = new template_1.Template();
        this.templateDurations = [];
        this.skills = [];
    }
    TemplateSaveComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = +params['id'];
            //Implements catch's for template,templateSkills ?,templateDurations ?
            if (id) {
                _this.templatesService
                    .getTemplateById(id)
                    .then(function (response) {
                    _this.template = response;
                }).catch(function (error) { return _this.myGlobals.showException(error, "Error with getting template", 'templates'); }).then(function (response) {
                    _this.skillService.GetAllSkills().then(function (response) { return _this.skills = response; }).then(function (response) {
                        if (_this.template.id != undefined) {
                            for (var i = 0; i < _this.skills.length; i++) {
                                for (var j = 0; j < _this.template.skills.length; j++) {
                                    if (_this.skills[i].id == _this.template.skills[j].id) {
                                        _this.skills[i].selected = true;
                                        _this.valid = true;
                                    }
                                }
                            }
                        }
                        _this.templateToEdit = _this.template.name;
                    });
                });
            }
            else {
                _this.skillService.GetAllSkills().then(function (response) { return _this.skills = response; });
            }
            _this.templateDurationService.GetAllTemplateDurations().then(function (response) { return _this.templateDurations = response; });
            _this.valid = true;
        });
    };
    TemplateSaveComponent.prototype.onSubmit = function () {
        this.save();
    };
    TemplateSaveComponent.prototype.validate = function () {
        var isValid = true;
        var checkboxes = [];
        for (var i = 0; i < this.skills.length; i++) {
            checkboxes[i] = document.getElementById(this.skills[i].id.toString());
        }
        var selectedCheckboxes = [];
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                selectedCheckboxes.push(checkboxes[i]);
            }
        }
        if (selectedCheckboxes.length > 0)
            isValid = true;
        else {
            isValid = false;
        }
        this.valid = isValid;
    };
    TemplateSaveComponent.prototype.save = function () {
        var _this = this;
        this.template.skills = [];
        for (var i = 0; i < this.skills.length; i++) {
            if (this.skills[i].selected == true) {
                var skill = new skill_1.Skill();
                skill.id = this.skills[i].id;
                this.template.skills.push(skill);
            }
        }
        this.templatesService.save(this.template).then(this.goBack)
            .catch(function (error) { return _this.myGlobals.showException(error, "Template can't be saved"); });
    };
    //archive() {
    //    this.templatesService.getArchivedTemplates().catch((error) => this.myGlobals.showException(error, "Error with getting archived templates"));
    //}
    TemplateSaveComponent.prototype.goBack = function () {
        window.history.back();
    };
    TemplateSaveComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/templates/template-save.component.html'
        }), 
        __metadata('design:paramtypes', [template_service_1.TemplatesService, skill_service_1.SkillService, template_duration_service_1.TemplateDurationService, router_1.ActivatedRoute, my_globals_1.MyGlobals])
    ], TemplateSaveComponent);
    return TemplateSaveComponent;
}());
exports.TemplateSaveComponent = TemplateSaveComponent;
//# sourceMappingURL=template-save.component.js.map