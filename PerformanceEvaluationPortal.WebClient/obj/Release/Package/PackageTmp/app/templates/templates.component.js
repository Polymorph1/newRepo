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
var my_globals_1 = require('../my-globals');
var TemplatesComponent = (function () {
    function TemplatesComponent(templatesService, router, myGlobals) {
        this.templatesService = templatesService;
        this.router = router;
        this.myGlobals = myGlobals;
        this.selectedPage = 0;
        this.templatesPerPage = 5;
    }
    TemplatesComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.templatesService
            .getActiveTemplates()
            .then(function (response) {
            _this.templates = response;
            _this.assignCopy();
        }).catch(function (error) { return _this.myGlobals.showException(error, " "); });
        this.templatesService.getArchivedTemplates().then(function (x) { return _this.archivedTemplates = x; })
            .catch(function (error) { return _this.myGlobals.showException(error, " "); });
    };
    TemplatesComponent.prototype.setPage = function (page) {
        var _this = this;
        this.selectedPage = page;
        this.templatesService.getActiveTemplates().then(function (x) { _this.filteredTemplates = x; });
    };
    TemplatesComponent.prototype.assignCopy = function () {
        this.filteredTemplates = Object.assign([], this.templates);
    };
    TemplatesComponent.prototype.filterItem = function (myInput) {
        if (!myInput)
            this.assignCopy(); //when nothing has typed
        this.filteredTemplates = Object.assign([], this.templates).filter(function (item) { return (item.name).toLowerCase().indexOf(myInput.toLowerCase()) > -1; });
    };
    TemplatesComponent.prototype.saveTemplate = function (template) {
        var id = template ? template.id : undefined;
        var link = ['/template/save', id];
        if (id == undefined) {
            this.router.navigate(link);
        }
        if (template.canBeEdited == false) {
            this.router.navigate(link);
        }
    };
    TemplatesComponent.prototype.detailsTemplate = function (template) {
        var id = template.id;
        var link = ['/template/details', id];
        this.router.navigate(link);
    };
    TemplatesComponent.prototype.archiveTemplate = function (template) {
        var _this = this;
        var id = template.id;
        var temp = new template_1.Template();
        temp = template;
        if (temp.canBeArchived == true) {
            swal('Error!', "Template can't be archived", 'error');
            return;
        }
        this.templatesService.archiveTemplate(id).then(function (x) {
            var toArchive = _this.filteredTemplates.indexOf(temp);
            if (toArchive > -1) {
                _this.filteredTemplates.splice(toArchive, 1);
                _this.archivedTemplates.push(temp);
            }
        }).catch(function (error) { return _this.myGlobals.showException(error, "Template can't be archived", 'templates'); });
    };
    TemplatesComponent.prototype.restoreTemplate = function (template) {
        var _this = this;
        var id = template.id;
        var temp = new template_1.Template();
        temp = template;
        this.templatesService.archiveTemplate(id).then(function (x) {
            var toRestore = _this.archivedTemplates.indexOf(temp);
            if (toRestore > -1) {
                _this.archivedTemplates.splice(toRestore, 1);
            }
            _this.filteredTemplates.push(temp);
        }).catch(function (error) { return _this.myGlobals.showException(error, " "); });
    };
    TemplatesComponent = __decorate([
        core_1.Component({
            selector: 'my-templates',
            templateUrl: 'app/templates/templates.component.html',
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
        __metadata('design:paramtypes', [template_service_1.TemplatesService, router_1.Router, my_globals_1.MyGlobals])
    ], TemplatesComponent);
    return TemplatesComponent;
}());
exports.TemplatesComponent = TemplatesComponent;
//# sourceMappingURL=templates.component.js.map