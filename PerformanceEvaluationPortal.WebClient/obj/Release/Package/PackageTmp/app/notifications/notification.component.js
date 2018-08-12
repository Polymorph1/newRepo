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
var router_1 = require('@angular/router');
var notification_service_1 = require('./notification.service');
var global_events_manager_1 = require('../common/global-events-manager');
var NotificationComponent = (function () {
    function NotificationComponent(notificationService, router, globalEventsManager) {
        var _this = this;
        this.notificationService = notificationService;
        this.router = router;
        this.globalEventsManager = globalEventsManager;
        this.notifications = [];
        this.notificationService.pushNotifications.subscribe(function (mode) {
            console.log('Inside push notifications');
            _this.notifications.push(mode);
        });
    }
    NotificationComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.notificationService.getNotificationByReceiverUserName().then(function (response) { _this.notifications = response; });
    };
    NotificationComponent.prototype.setSeenStatus = function (notification) {
        var id = notification.id;
        this.notificationService.setSeenStatus(id);
    };
    NotificationComponent = __decorate([
        core_1.Component({
            selector: 'my-notifications',
            templateUrl: 'app/notifications/notification.component.html',
            styleUrls: []
        }), 
        __metadata('design:paramtypes', [notification_service_1.NotificationService, router_1.Router, global_events_manager_1.GlobalEventsManager])
    ], NotificationComponent);
    return NotificationComponent;
}());
exports.NotificationComponent = NotificationComponent;
//# sourceMappingURL=notification.component.js.map