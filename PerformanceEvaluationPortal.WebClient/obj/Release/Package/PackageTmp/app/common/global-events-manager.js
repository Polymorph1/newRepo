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
var core_1 = require("@angular/core");
var GlobalEventsManager = (function () {
    function GlobalEventsManager() {
        this.showAdminBar = new core_1.EventEmitter();
        this.showManagerBar = new core_1.EventEmitter();
        this.showReviewerBar = new core_1.EventEmitter();
        this.showConsultantBar = new core_1.EventEmitter();
        this.isAdmin = new core_1.EventEmitter();
        this.isManager = new core_1.EventEmitter();
        this.isReviewer = new core_1.EventEmitter();
        this.showNumberOfNotifications = new core_1.EventEmitter();
        this.loggedUserName = new core_1.EventEmitter();
    }
    GlobalEventsManager = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], GlobalEventsManager);
    return GlobalEventsManager;
}());
exports.GlobalEventsManager = GlobalEventsManager;
//# sourceMappingURL=global-events-manager.js.map