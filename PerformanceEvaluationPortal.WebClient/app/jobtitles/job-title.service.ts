import { Injectable } from '@angular/core';
import { JobTitle } from './job-title';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';

import { MyGlobals } from '../my-globals';

@Injectable()
export class JobTitlesService {
    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals) { }

    private jobTitleUrl=this.myGlobals.WebApiUrl+'api/JobTitles'
    
    getJobTitles(): Promise<JobTitle[]> {
       
        return this.httpClient.get(this.jobTitleUrl)
            .toPromise().then(response => response.json()).catch(this.handleError);
    }
    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
}