import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';
import { ApplicationUserService } from '../users/application-user.service';
import { NotificationService } from '../notifications/notification.service';
import { GlobalEventsManager } from '../common/global-events-manager';

import 'rxjs/Rx';

@Injectable()
export class AuthService {

    isLoggedIn: boolean = false;
    isAdmin: boolean = false;
    isManager: boolean = false;
    isReviewer: boolean = false;
    public loggedUserName: string;
    constructor(public http: Http, public myGlobals: MyGlobals, public router: Router,
        public userService: ApplicationUserService, public notificationService: NotificationService, private globalEventsManager: GlobalEventsManager) {
        this.isLoggedIn = !!localStorage.getItem('auth_token');
        this.isAdmin = !!localStorage.getItem('isAdmin');
        this.isManager = !!localStorage.getItem('isManager');
        this.isReviewer = !!localStorage.getItem('isReviewer');
    }

    login(username, password) {

        let data = "grant_type=password&username=" + username + "&password=" + password;



        let contentHeaders = new Headers();
        contentHeaders.append('Content-Type', 'application/x-www-form-urlencoded');
        return this.http.post(this.myGlobals.WebApiUrl + 'token', data, { headers: contentHeaders })
            .toPromise()
            .then(response => {
                this.handleSuccess(response);
                console.log(response);
            })
            .catch(this.handleError);
    }

    logout() {
        localStorage.removeItem('auth_token');
        this.isLoggedIn = false;

        localStorage.removeItem('isAdmin');
        this.isAdmin = false;

        localStorage.removeItem('isManager');
        this.isManager = false;

        localStorage.removeItem('isReviewer');
        this.isReviewer = false;

        localStorage.removeItem('loggedUser');

        localStorage.removeItem('userName');

        localStorage.removeItem("numberOfNotifications");
    }

    private handleSuccess(response: any) {

        localStorage.setItem('auth_token', response.json().access_token);

        let roles = JSON.parse(response.json().roles);

        let isAdmin = roles.filter(role => role == 'Admin').length > 0;



        if (isAdmin) {
            localStorage.setItem('isAdmin', 'true');
            this.isAdmin = true;
        }

        let IsManager = JSON.parse(response.json().isManager);
        if (IsManager) {
            localStorage.setItem('isManager', 'true');
            this.isManager = true;
        }

        let IsReviewer = JSON.parse(response.json().isReviewer);
        if (IsReviewer) {
            localStorage.setItem('isReviewer', 'true');
            this.isReviewer = true;
        }

        localStorage.setItem("loggedUser", response.json().loggedUser);

        let userName = response.json().userName;

        localStorage.setItem("userName", userName);

        this.globalEventsManager.loggedUserName.emit(userName);

        this.isLoggedIn = true;

        this.notificationService.initNotifications(userName);

    }

    private handleError(error: any) {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}
