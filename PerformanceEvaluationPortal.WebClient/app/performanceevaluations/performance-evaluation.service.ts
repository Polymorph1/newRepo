import { Injectable } from '@angular/core';
import { PerformanceEvaluation } from './performance-evaluation';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '../common/http.client';
import 'rxjs/add/operator/catch';
import { MyGlobals } from '../my-globals';

declare var swal: any;

@Injectable()

export class PerformanceEvaluationService {
    constructor(private httpClient: HttpClient, private myGlobals: MyGlobals) { }
    private getUrl = this.myGlobals.WebApiUrl + 'api';


    getPerformanceEvaluationById(id: number): Promise<PerformanceEvaluation> {
        let url = this.getUrl + '/GetPerformanceEvaluationsById?performanceEvaluationId=' + id;

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getPerformanceEvaluationsForReviewer(): Promise<PerformanceEvaluation[]> {
        let url = this.getUrl + '/GetPerformanceEvaluationsForReviewer';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getPerformanceEvaluationsHistoryForReviewer(): Promise<PerformanceEvaluation[]> {
        let url = this.getUrl + '/GetPerformanceEvaluationHistoryForReviewer';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getPerformanceEvaluationsForManager(): Promise<PerformanceEvaluation[]> {
        let url = this.getUrl + '/GetPerformanceEvaluationsForManager';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getPerformanceEvaluationsHistoryForManager(): Promise<PerformanceEvaluation[]> {
        let url = this.getUrl + '/GetPerformanceEvaluationHistoryForManager';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getMyPerformanceEvaluations(): Promise<PerformanceEvaluation[]> {
        let url = this.getUrl + '/GetMyPerformanceEvaluations?signed=false';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    getMyPerformanceEvaluationsHistory(): Promise<PerformanceEvaluation[]> {
        let url = this.getUrl + '/GetMyPerformanceEvaluations?signed=true';

        return this.httpClient.get(url)
            .toPromise()
            .then(response => response.json());
    }

    createPerformanceEvaluation(userId: number): Promise<PerformanceEvaluation> {
        let url = this.getUrl + '/CreatePerformanceEvaluation/' + userId;

        return this.httpClient
            .post(url, userId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    save(pe: PerformanceEvaluation): Promise<PerformanceEvaluation> {
        let url = this.getUrl + '/SavePerformanceEvaluation';

        return this.httpClient
            .put(url, pe)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    submit(pe: PerformanceEvaluation): Promise<PerformanceEvaluation> {
        let url = this.getUrl + '/SubmitPerformanceEvaluation';

        return this.httpClient
            .put(url, pe)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    acknowledgePe(pe: PerformanceEvaluation): Promise<boolean> {
        let url = this.getUrl + '/AcknowledgePerformanceEvaluation';

        return this.httpClient
            .put(url, pe)
            .toPromise().then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any) {
        return Promise.reject(error.message || error);
    }
}