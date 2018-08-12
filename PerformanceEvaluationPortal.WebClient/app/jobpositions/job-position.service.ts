import { Injectable } from '@angular/core';
import { JobPosition } from './job-position';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';

import { MyGlobals } from '../my-globals';

@Injectable()
export class JobPositionsService {
    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals) { }

    private jobPositionUrl = this.myGlobals.WebApiUrl + 'api/JobPositions'

    getJobPositions(): Promise<JobPosition[]> {

        return this.httpClient.get(this.jobPositionUrl)
            .toPromise().then(response => response.json()).catch(this.handleError);
    }

    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
}