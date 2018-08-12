import { EventEmitter, Injectable, OnInit } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Notification } from './notification';
import 'rxjs/add/operator/toPromise';
import { Observable } from 'rxjs/Observable';
import { MyGlobals } from '../my-globals';
import { HttpClient } from '../common/http.client';
import { AuthService } from '../login/auth.service';
import { GlobalEventsManager } from '../common/global-events-manager';

@Injectable()
export class NotificationService implements OnInit {
    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals, private globalEventsManager: GlobalEventsManager) {
        this.globalEventsManager.loggedUserName.subscribe((mode: string) => {
            this.loggedUserName = mode;
        });
    }

    public loggedUserName: string;

    private notificationUrl = this.myGlobals.WebApiUrl + 'api';

    public pushNotifications: EventEmitter<Notification> = new EventEmitter<Notification>();

    getNotifications(): Promise<Notification[]> {
        let url = this.notificationUrl + '/Notification';
        return this.httpClient.get(url)
            .toPromise().then(response => response.json());
    }

    getNotificationByReceiverId(): Promise<Notification[]> {
        let url = this.notificationUrl + '/GetByReceiver';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getNotificationByReceiverUserName(): Promise<Notification[]> {
        let url = this.notificationUrl + '/GetByName';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getNotificationNumber(): Promise<number>
    {
        let url = this.notificationUrl + '/GetNotificationNumber';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    ngOnInit() {
        this.globalEventsManager.loggedUserName.subscribe((mode: string) => {
            this.loggedUserName = mode;
        });
    }

    setSeenStatus(id: number) {

        let url = this.notificationUrl + '/SetSeenStatus/' + id;
        console.log(url);
        return this.httpClient.put(url, null)
            .toPromise()
            .then(response => response.json()).then(x=>{
                var number = +localStorage.getItem("numberOfNotifications");
                number--;
                if (number < 0)
                    number = 0;
                localStorage.setItem("numberOfNotifications", number.toString());
                this.globalEventsManager.showNumberOfNotifications.emit(number);});
    }

    getNotificationIdByPeId(PeId: number):Promise<number> {

        let url = this.notificationUrl + '/GetNotificationByPeId/' + PeId;
        
        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    post(notification: Notification) {

        let url = this.notificationUrl + '/Notification';

        return this.httpClient
            .post(url, JSON.stringify(notification))
            .toPromise().then(response => response.json());
    }

    initNotifications(userName: string): void {
        
       var notification = $.connection.notificationHub;
        $.connection.hub.url = this.myGlobals.WebApiUrl + 'signalr';

        notification.client.displayNotificationConsultant = (notification) => {
            console.log('Inside displayNotification');
            this.pushNotifications.emit(notification);

            var number = +localStorage.getItem("numberOfNotifications");
            number++;
            console.log('Number of notifications: ' + number);
            localStorage.setItem("numberOfNotifications", number.toString());
            this.globalEventsManager.showNumberOfNotifications.emit(number);
        };

        $.connection.hub.start().done(() => {

            console.log('Inside connection start: ' + userName);
          notification.server.joinGroup(userName);

        });     
    };
}