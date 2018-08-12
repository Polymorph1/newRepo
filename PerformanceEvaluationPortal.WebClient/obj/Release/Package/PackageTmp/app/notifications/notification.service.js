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
var my_globals_1 = require('../my-globals');
var http_client_1 = require('../common/http.client');
var global_events_manager_1 = require('../common/global-events-manager');
var NotificationService = (function () {
    function NotificationService(httpClient, myGlobals, globalEventsManager) {
        var _this = this;
        this.httpClient = httpClient;
        this.myGlobals = myGlobals;
        this.globalEventsManager = globalEventsManager;
        this.notificationUrl = this.myGlobals.WebApiUrl + 'api';
        this.pushNotifications = new core_1.EventEmitter();
        this.globalEventsManager.loggedUserName.subscribe(function (mode) {
            _this.loggedUserName = mode;
        });
    }
    NotificationService.prototype.getNotificationTemplates = function () {
        var url = this.notificationUrl + '/Notification';
        return this.httpClient.get(url)
            .toPromise().then(function (response) { return response.json(); });
    };
    NotificationService.prototype.getNotificationByReceiverId = function () {
        var url = this.notificationUrl + '/GetByReceiver';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    NotificationService.prototype.getNotificationByReceiverUserName = function (count) {
        if (count === void 0) { count = false; }
        var url = this.notificationUrl + '/GetByName';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    NotificationService.prototype.getNotificationNumber = function () {
        var url = this.notificationUrl + '/GetNotificationNumber';
        return this.httpClient.get(url)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    NotificationService.prototype.ngOnInit = function () {
        var _this = this;
        this.globalEventsManager.loggedUserName.subscribe(function (mode) {
            _this.loggedUserName = mode;
        });
    };
    NotificationService.prototype.setSeenStatus = function (id) {
        var _this = this;
        var url = this.notificationUrl + '/SetSeenStatus/' + id;
        console.log(url);
        return this.httpClient.put(url, null)
            .toPromise()
            .then(function (response) { return response.json(); }).then(function (x) {
            var number = +localStorage.getItem("numberOfNotifications");
            number--;
            if (number < 0)
                number = 0;
            localStorage.setItem("numberOfNotifications", number.toString());
            _this.globalEventsManager.showNumberOfNotifications.emit(number);
        });
    };
    NotificationService.prototype.post = function (notification) {
        var url = this.notificationUrl + '/Notification';
        return this.httpClient
            .post(url, JSON.stringify(notification))
            .toPromise().then(function (response) { return response.json(); });
    };
    NotificationService.prototype.initNotifications = function (userName) {
        var _this = this;
        var notification = $.connection.notificationHub;
        $.connection.hub.url = this.myGlobals.WebApiUrl + 'signalr';
        notification.client.displayNotificationConsultant = function (notification) {
            console.log('Inside displayNotification');
            _this.pushNotifications.emit(notification);
            var number = +localStorage.getItem("numberOfNotifications");
            number++;
            console.log('Number of notifications: ' + number);
            localStorage.setItem("numberOfNotifications", number.toString());
            _this.globalEventsManager.showNumberOfNotifications.emit(number);
        };
        $.connection.hub.start().done(function () {
            console.log('Inside connection start: ' + userName);
            notification.server.joinGroup(userName);
        });
    };
    ;
    NotificationService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_client_1.HttpClient, my_globals_1.MyGlobals, global_events_manager_1.GlobalEventsManager])
    ], NotificationService);
    return NotificationService;
}());
exports.NotificationService = NotificationService;
//# sourceMappingURL=notification.service.js.map