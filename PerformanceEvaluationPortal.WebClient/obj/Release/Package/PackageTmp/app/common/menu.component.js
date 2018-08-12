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
var global_events_manager_1 = require('./global-events-manager');
var router_1 = require('@angular/router');
var auth_service_1 = require('../login/auth.service');
var application_user_service_1 = require('../users/application-user.service');
var notification_service_1 = require('../notifications/notification.service');
var MenuComponent = (function () {
    function MenuComponent(globalEventsManager, authService, router, userService, notificationsService) {
        var _this = this;
        this.globalEventsManager = globalEventsManager;
        this.authService = authService;
        this.router = router;
        this.userService = userService;
        this.notificationsService = notificationsService;
        this.notifications = [];
        this.globalEventsManager.showNumberOfNotifications.subscribe(function (mode) {
            _this.numberOfNotifications = mode;
        });
        this.globalEventsManager.showAdminBar.subscribe(function (mode) {
            _this.showAdminBar = mode;
            _this.loggedUserName = localStorage.getItem("loggedUser");
        });
        this.globalEventsManager.showManagerBar.subscribe(function (mode) {
            _this.showManagerBar = mode;
            _this.loggedUserName = localStorage.getItem("loggedUser");
        });
        this.globalEventsManager.showReviewerBar.subscribe(function (mode) {
            _this.showReviewerBar = mode;
            _this.loggedUserName = localStorage.getItem("loggedUser");
        });
        this.globalEventsManager.showConsultantBar.subscribe(function (mode) {
            _this.showConsultantBar = mode;
            _this.loggedUserName = localStorage.getItem("loggedUser");
        });
        this.globalEventsManager.isAdmin.subscribe(function (isAdmin) {
            _this.isAdmin = isAdmin;
        });
        this.globalEventsManager.isManager.subscribe(function (isManager) {
            _this.isManager = isManager;
        });
        this.globalEventsManager.isReviewer.subscribe(function (isReviewer) {
            _this.isReviewer = isReviewer;
        });
        this.notificationsService.pushNotifications.subscribe(function (mode) {
            _this.notifications.push(mode);
        });
        // this.InitNotifications();
    }
    MenuComponent.prototype.ngOnInit = function () {
        this.globalEventsManager.showAdminBar.emit(this.authService.isAdmin);
        this.globalEventsManager.showManagerBar.emit(this.authService.isManager);
        this.globalEventsManager.showReviewerBar.emit(this.authService.isReviewer);
        this.globalEventsManager.showConsultantBar.emit(this.authService.isLoggedIn);
        this.globalEventsManager.isManager.emit(false);
        this.globalEventsManager.isReviewer.emit(false);
        this.globalEventsManager.isAdmin.emit(false);
        var numberOfNotifications = +localStorage.getItem("numberOfNotifications");
        this.globalEventsManager.showNumberOfNotifications.emit(numberOfNotifications);
        var userName = localStorage.getItem('userName');
        if (userName != null) {
            this.notificationsService.initNotifications(userName);
        }
    };
    MenuComponent.prototype.logout = function () {
        this.globalEventsManager.showAdminBar.emit(false);
        this.globalEventsManager.showManagerBar.emit(false);
        this.globalEventsManager.showReviewerBar.emit(false);
        this.globalEventsManager.showConsultantBar.emit(false);
        this.globalEventsManager.isManager.emit(false);
        this.globalEventsManager.isReviewer.emit(false);
        this.globalEventsManager.isAdmin.emit(false);
        this.globalEventsManager.showNumberOfNotifications.emit(0);
        this.authService.logout();
        this.router.navigate(['/login']);
    };
    MenuComponent.prototype.InitNotifications = function () {
        var _this = this;
        if (this.showConsultantBar) {
            this.notificationsService
                .getNotificationByReceiverUserName()
                .then(function (response) { return _this.notifications = response; });
        }
    };
    MenuComponent.prototype.onEvent = function (event) {
        event.stopPropagation();
        this.InitNotifications();
    };
    MenuComponent.prototype.setNotificationToSeen = function (id, peId) {
        var _this = this;
        //let link = ['pe/acknowledge/'+ peId];
        if (this.showConsultantBar) {
            this.notificationsService.setSeenStatus(id).then(function (response) {
                _this.notificationsService
                    .getNotificationByReceiverUserName()
                    .then(function (response) { return _this.notifications = response; });
            });
            this.router.navigateByUrl('pe/acknowledge/' + peId);
        }
    };
    MenuComponent = __decorate([
        core_1.Component({
            selector: 'main-menu',
            styles: [],
            templateUrl: 'app/common/menu.component.html'
        }), 
        __metadata('design:paramtypes', [global_events_manager_1.GlobalEventsManager, auth_service_1.AuthService, router_1.Router, application_user_service_1.ApplicationUserService, notification_service_1.NotificationService])
    ], MenuComponent);
    return MenuComponent;
}());
exports.MenuComponent = MenuComponent;
//# sourceMappingURL=menu.component.js.map