import {Injectable} from '@angular/core';
import {TemplateDuration} from './template-duration';
import {Headers, Http} from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';

import { MyGlobals } from '../my-globals';


@Injectable()

export class TemplateDurationService {

    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals)
    { }

    private templateDurationUrl = this.myGlobals.WebApiUrl + 'api/TemplateDurations';


    GetAllTemplateDurations(): Promise<TemplateDuration[]> {

        return this.httpClient.get(this.templateDurationUrl)
            .toPromise().then(response => response.json()).catch(this.handleError);
    }

    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
}