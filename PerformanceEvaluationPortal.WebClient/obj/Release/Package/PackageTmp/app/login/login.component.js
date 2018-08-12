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
var global_events_manager_1 = require('../common/global-events-manager');
var auth_service_1 = require('./auth.service');
var notification_service_1 = require('../notifications/notification.service');
var LoginComponent = (function () {
    function LoginComponent(authService, router, globalEventsManager, notificationsService) {
        this.authService = authService;
        this.router = router;
        this.globalEventsManager = globalEventsManager;
        this.notificationsService = notificationsService;
    }
    LoginComponent.prototype.login = function (event, username, password) {
        var _this = this;
        event.preventDefault();
        this.authService.login(username, password).then(function (result) {
            if (_this.authService.isAdmin) {
                _this.globalEventsManager.showAdminBar.emit(true);
                _this.globalEventsManager.isAdmin.emit(_this.authService.isAdmin);
                _this.router.navigate(['/users']);
            }
            else {
                if (_this.authService.isManager) {
                    _this.globalEventsManager.showManagerBar.emit(true);
                    _this.globalEventsManager.isManager.emit(_this.authService.isManager);
                }
                if (_this.authService.isReviewer) {
                    _this.globalEventsManager.showReviewerBar.emit(true);
                    _this.globalEventsManager.isReviewer.emit(_this.authService.isReviewer);
                }
                _this.globalEventsManager.showConsultantBar.emit(true);
                _this.notificationsService.getNotificationNumber().then(function (x) {
                    localStorage.setItem("numberOfNotifications", x.toString());
                    _this.globalEventsManager.showNumberOfNotifications.emit(x);
                });
                _this.router.navigate(['/startPage']);
            }
        }).catch(function (result) { document.getElementById("message").hidden = false; });
    };
    LoginComponent.prototype.logout = function (event) {
        event.preventDefault();
        this.authService.logout();
    };
    LoginComponent = __decorate([
        core_1.Component({
            selector: 'login',
            templateUrl: '/app/login/login.component.html',
            animations: [
                core_1.trigger('loginState', [
                    core_1.state('in', core_1.style({ opacity: 1, transform: 'translateX(0)' })),
                    core_1.transition('void => *', [
                        core_1.style({
                            opacity: 0,
                            transform: 'translateX(-20%)'
                        }),
                        core_1.animate('0.2s ease-in')
                    ]),
                    core_1.transition('* => void', [
                        core_1.animate('0.2s 10 ease-out', core_1.style({
                            opacity: 0,
                            transform: 'translateX(20%)'
                        }))
                    ])
                ])
            ]
        }), 
        __metadata('design:paramtypes', [auth_service_1.AuthService, router_1.Router, global_events_manager_1.GlobalEventsManager, notification_service_1.NotificationService])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map