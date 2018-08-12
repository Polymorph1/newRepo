import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { GlobalEventsManager } from './global-events-manager';
import { Router, RouterLinkActive } from '@angular/router';
import { AuthService } from '../login/auth.service';
import { ApplicationUserService } from '../users/application-user.service';
import { NotificationService } from '../notifications/notification.service';
import { Notification } from '../notifications/notification';

@Component({
    selector: 'main-menu',
    styles: [],
    templateUrl: 'app/common/menu.component.html'
})
export class MenuComponent implements OnInit {
    public showAdminBar: boolean;
    public showManagerBar: boolean;
    public showReviewerBar: boolean;
    public showConsultantBar: boolean;
    public isAdmin: boolean;
    public isManager: boolean;
    public isReviewer: boolean;
    public loggedUserName: string;
    public numberOfNotifications: number;
    notifications: Notification[] = [];

    constructor(
        private globalEventsManager: GlobalEventsManager,
        private authService: AuthService,
        private router: Router, private userService: ApplicationUserService,
        private notificationsService: NotificationService

    ) {
      

        this.globalEventsManager.showNumberOfNotifications.subscribe((mode: number) => {
            this.numberOfNotifications = mode;
        });

        this.globalEventsManager.showAdminBar.subscribe((mode: boolean) => {

            this.showAdminBar = mode;
            this.loggedUserName = localStorage.getItem("loggedUser");
        });

        this.globalEventsManager.showManagerBar.subscribe((mode: boolean) => {
            this.showManagerBar = mode;
            this.loggedUserName = localStorage.getItem("loggedUser");
        });

        this.globalEventsManager.showReviewerBar.subscribe((mode: boolean) => {
            this.showReviewerBar = mode;
            this.loggedUserName = localStorage.getItem("loggedUser");
        });
        this.globalEventsManager.showConsultantBar.subscribe((mode: boolean) => {
            this.showConsultantBar = mode;
            this.loggedUserName = localStorage.getItem("loggedUser");
        });

        this.globalEventsManager.isAdmin.subscribe((isAdmin: boolean) => {
            this.isAdmin = isAdmin;
        });

        this.globalEventsManager.isManager.subscribe((isManager: boolean) => {
            this.isManager = isManager;
        });

        this.globalEventsManager.isReviewer.subscribe((isReviewer: boolean) => {
            this.isReviewer = isReviewer;
        });

        this.notificationsService.pushNotifications.subscribe((mode: any) => {
           
            this.notifications.push(mode);

        });
       // this.InitNotifications();
    }

    ngOnInit() {
        this.globalEventsManager.showAdminBar.emit(this.authService.isAdmin);
        this.globalEventsManager.showManagerBar.emit(this.authService.isManager);
        this.globalEventsManager.showReviewerBar.emit(this.authService.isReviewer);
        this.globalEventsManager.showConsultantBar.emit(this.authService.isLoggedIn);
        this.globalEventsManager.isManager.emit(false);
        this.globalEventsManager.isReviewer.emit(false);
        this.globalEventsManager.isAdmin.emit(false);

        var numberOfNotifications = +localStorage.getItem("numberOfNotifications");
        this.globalEventsManager.showNumberOfNotifications.emit(numberOfNotifications);


        let userName = localStorage.getItem('userName');

        if (userName != null) {
            this.notificationsService.initNotifications(userName);
        }
    }

    logout() {
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
    }

    InitNotifications() {
        if (this.showConsultantBar) {
            this.notificationsService
                .getNotificationByReceiverUserName()
                .then(response => this.notifications = response);
           
        }
    }



    onEvent(event) {
        event.stopPropagation();
        this.InitNotifications();
    }

    setNotificationToSeen(id: number, peId: number) {
        //let link = ['pe/acknowledge/'+ peId];
        if (this.showConsultantBar) {
            this.notificationsService.setSeenStatus(id).then(response => {
                this.notificationsService
                    .getNotificationByReceiverUserName()
                    .then(response => this.notifications = response)
            });
            this.router.navigateByUrl('pe/acknowledge/' + peId);
        }
    }
  
}