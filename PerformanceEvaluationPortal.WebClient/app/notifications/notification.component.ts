import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Notification } from './notification';
import { NotificationService } from './notification.service';
import { Observable } from 'rxjs/Observable';
import { GlobalEventsManager } from '../common/global-events-manager';

@Component({
    selector: 'my-notifications',
    templateUrl: 'app/notifications/notification.component.html',
    styleUrls: []
})
export class NotificationComponent implements OnInit {

    constructor(
        private notificationService: NotificationService,
        private router: Router,
        private globalEventsManager: GlobalEventsManager
    ) {
        this.notificationService.pushNotifications.subscribe((mode: any) => {
            console.log('Inside push notifications');
            this.notifications.push(mode);

        });
}

notifications: Notification[] = [];

ngOnInit() {

    this.notificationService.getNotificationByReceiverUserName().then(response => { this.notifications = response });


}

setSeenStatus(notification: Notification) {
    let id = notification.id;
    this.notificationService.setSeenStatus(id);
}

}