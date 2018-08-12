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
var template_duration_service_1 = require('./template-duration.service');
var TemplateDurations = (function () {
    function TemplateDurations(templateDurationService) {
        this.templateDurationService = templateDurationService;
    }
    TemplateDurations.prototype.ngOnInit = function () {
        var _this = this;
        this.templateDurationService.GetAllTemplateDurations()
            .then(function (response) { return _this.templateDurations = response; });
    };
    TemplateDurations = __decorate([
        core_1.Component({
            selector: 'my-templateduration'
        }), 
        __metadata('design:paramtypes', [template_duration_service_1.TemplateDurationService])
    ], TemplateDurations);
    return TemplateDurations;
}());
exports.TemplateDurations = TemplateDurations;
//# sourceMappingURL=template-duration.component.js.map