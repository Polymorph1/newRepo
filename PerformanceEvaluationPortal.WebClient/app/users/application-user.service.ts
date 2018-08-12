import { Injectable } from '@angular/core';
import { ApplicationUser } from './application-user';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';
import { MyGlobals } from '../my-globals';

@Injectable()

export class ApplicationUserService 
{
    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals) { }

    private usersUrl = this.myGlobals.WebApiUrl + 'api/';
    user: ApplicationUser;  

    getUsers(): Promise<ApplicationUser[]> {
        let url = this.usersUrl + 'ApplicationUsers';
        
        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getUsersByInput(searchBy: string) {
        let url = this.usersUrl + 'ApplicationUsers?='+searchBy;

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getLoggedUser(): Promise<ApplicationUser> {
        let url = this.usersUrl + 'GetLoggedUser';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getUserById(id: string): Promise<ApplicationUser> {
        let url = this.usersUrl + 'GetUserById/' + id;

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);       
    }

    getMyTeamReviewer(): Promise<ApplicationUser[]> {
        let url = this.usersUrl + 'GetMyTeamReviewer';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getMyTeamManager(): Promise<ApplicationUser[]> {
        let url = this.usersUrl + 'GetMyTeamManager';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }
    
    save(user: ApplicationUser): Promise<ApplicationUser> {       
        return this.put(user);
    }

    put(user: ApplicationUser): Promise<ApplicationUser> {
        let url = this.usersUrl + 'ApplicationUsers/' + user.id;

        return this.httpClient
            .put(url, JSON.stringify(user)).toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
}
