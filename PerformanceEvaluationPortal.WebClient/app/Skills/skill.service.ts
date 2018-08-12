import {Injectable} from '@angular/core';
import {Skill} from './skill';
import {Headers,Http} from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';

import { MyGlobals } from '../my-globals';

@Injectable()

export class SkillService {

    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals)
    { }

    private SkillUrl = this.myGlobals.WebApiUrl + 'api/Skills';

    GetAllSkills(): Promise<Skill[]> {
        return this.httpClient.get(this.SkillUrl)
            .toPromise().then(response => response.json()).catch(this.handleError);
    }

    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
}