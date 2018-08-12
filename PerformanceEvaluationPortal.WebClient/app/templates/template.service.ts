import { Injectable } from '@angular/core';
import { Template } from './template';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';
import { Router } from '@angular/router';

import { MyGlobals } from '../my-globals';
declare var swal: any;

@Injectable()
export class TemplatesService {
    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals, private router: Router) { }

    private templateUrl = this.myGlobals.WebApiUrl + 'api';

    getActiveTemplates(): Promise<Template[]> {
        let url = this.templateUrl + '/Templates/';
        return this.httpClient.get(url)
            .toPromise().then(response => response.json())
            .catch(this.handleError);
    }

    getTemplateById(id: number): Promise<Template> {
        let url = this.templateUrl + '/Templates/' + id;

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getArchivedTemplates(): Promise<Template[]> {
        let url = this.templateUrl + '/GetArchivedTemplates';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    archiveTemplate(id: number): Promise<Template> {
        let url = this.templateUrl + '/ArchiveTemplate/' + id;
        return this.httpClient.put(url, null).toPromise().then(response => response.json())
            .catch(this.handleError);

    }

    save(template: Template): Promise<Template> {
        if (template.id) {
            return this.put(template);
        }

        return this.post(template);
    }

    put(template: Template): Promise<Template> {
        let url = this.templateUrl + '/Templates/' + template.id;

        return this.httpClient
            .put(url, JSON.stringify(template))
            .toPromise().then(response => response.json())
            .catch(this.handleError);

    }

    post(template: Template) {

        let url = this.templateUrl + '/Templates';

        return this.httpClient
            .post(url, JSON.stringify(template))
            .toPromise().then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
    
}