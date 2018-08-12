import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

declare var swal: any;

@Injectable()
export class MyGlobals {
    WebApiUrl: string = 'http://localhost:51813/';
    //WebApiUrl: string = 'https://aplab12performanceevaluationportalapi.azurewebsites.net/';
    constructor(private router: Router) { }

    showException(error: any, message: string, redirectRoute: string = null) {
        swal({
                title: 'Error!',
                text: message + '\n' + JSON.parse(error._body).message,
                type: 'error'
        }).then(result => {
            
                if (redirectRoute)
                    this.router.navigateByUrl(redirectRoute);   
        })
    }
}