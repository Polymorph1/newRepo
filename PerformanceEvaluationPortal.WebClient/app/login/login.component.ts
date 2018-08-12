import { Component, Input, trigger, state, style, transition, animate } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers } from '@angular/http';
import { GlobalEventsManager } from '../common/global-events-manager';
import { AuthService } from './auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotificationService } from '../notifications/notification.service';
@Component({
    selector: 'login',
    templateUrl: '/app/login/login.component.html',
    animations: [
        trigger('loginState', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateX(-20%)'
                }),
                animate('0.2s ease-in')
            ]),
            transition('* => void', [
                animate('0.2s 10 ease-out', style({
                    opacity: 0,
                    transform: 'translateX(20%)'
                }))
            ])
        ])
    ]
})

export class LoginComponent {
    constructor(private authService: AuthService, private router: Router, private globalEventsManager: GlobalEventsManager,
        private notificationsService: NotificationService) {

    }




    login(event, username, password) {

        event.preventDefault();

        this.authService.login(username, password).then((result) => {

            if (this.authService.isAdmin) {
                this.globalEventsManager.showAdminBar.emit(true);
                this.globalEventsManager.isAdmin.emit(this.authService.isAdmin);

                this.router.navigate(['/users']);

            }
            else {
                if (this.authService.isManager) {
                    this.globalEventsManager.showManagerBar.emit(true);
                    this.globalEventsManager.isManager.emit(this.authService.isManager);
                }

                if (this.authService.isReviewer) {
                    this.globalEventsManager.showReviewerBar.emit(true);
                    this.globalEventsManager.isReviewer.emit(this.authService.isReviewer);
                }

                this.globalEventsManager.showConsultantBar.emit(true);

                this.notificationsService.getNotificationNumber().then(x => {
                    localStorage.setItem("numberOfNotifications", x.toString());
                    this.globalEventsManager.showNumberOfNotifications.emit(x);
                });

                this.router.navigate(['/startPage']);
            }

           

        }).catch(
            (result) => { document.getElementById("message").hidden = false });

    }

    logout(event) {
        event.preventDefault();
        this.authService.logout();
    }
}