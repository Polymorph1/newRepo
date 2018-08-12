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
var job_position_service_1 = require('./job-position.service');
var JobPositions = (function () {
    function JobPositions(jobPostionsService) {
        this.jobPostionsService = jobPostionsService;
    }
    JobPositions.prototype.ngOnInit = function () {
        var _this = this;
        this.jobPostionsService
            .getJobPositions()
            .then(function (response) { return _this.jobPositions = response; });
    };
    JobPositions = __decorate([
        core_1.Component({
            selector: 'my-jobpositons'
        }), 
        __metadata('design:paramtypes', [job_position_service_1.JobPositionsService])
    ], JobPositions);
    return JobPositions;
}());
exports.JobPositions = JobPositions;
//# sourceMappingURL=job-position.component.js.map