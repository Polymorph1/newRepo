import { EventEmitter, Injectable } from "@angular/core";

@Injectable()
export class GlobalEventsManager {
    public showAdminBar: EventEmitter<boolean> = new EventEmitter<boolean>();
    public showManagerBar: EventEmitter<boolean> = new EventEmitter<boolean>();
    public showReviewerBar: EventEmitter<boolean> = new EventEmitter<boolean>();
    public showConsultantBar: EventEmitter<boolean> = new EventEmitter<boolean>();
    public isAdmin: EventEmitter<boolean> = new EventEmitter<boolean>();
    public isManager: EventEmitter<boolean> = new EventEmitter<boolean>();
    public isReviewer: EventEmitter<boolean> = new EventEmitter<boolean>();
    public showNumberOfNotifications: EventEmitter<number> = new EventEmitter<number>();
    public loggedUserName: EventEmitter<string> = new EventEmitter<string>();
    
    constructor() {

    }
}