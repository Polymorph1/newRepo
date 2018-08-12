import { Component, OnInit } from '@angular/core';
import { ApplicationUser } from './application-user';
import { ApplicationUserService } from './application-user.service';
import { ActivatedRoute, Params } from '@angular/router';
import { JobTitle } from '../jobtitles/job-title';
import { JobTitlesService } from '../jobtitles/job-title.service';
import { JobPosition } from '../jobpositions/job-position';
import { JobPositionsService } from '../jobpositions/job-position.service';
import { Template } from '../templates/template';
import { TemplatesService } from '../templates/template.service';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';

declare var swal: any;

@Component({
    templateUrl: 'app/users/application-user-save.component.html'
})

export class ApplicationUserSaveComponent implements OnInit {
    submitted = false;
    constructor(
        private applicationUserService: ApplicationUserService,
        private jobTitlesService: JobTitlesService,
        private jobPositionsService: JobPositionsService,
        private templatesService:TemplatesService,
        private route: ActivatedRoute,
        private router: Router,
        private myGlobals: MyGlobals
    ) { }

    user: ApplicationUser=new ApplicationUser();
    managers: ApplicationUser[] = [];
    reviewers: ApplicationUser[] = [];
    titles: JobTitle[] = [];
    positions: JobPosition[] = [];
    templates: Template[] = [];
    validFirst: boolean;
    validLast: boolean;
    userToEdit: string;
    ngOnInit() {
        this.route.params.forEach((params: Params) => {

            let id = params['id'];

            if (id) {
                this.applicationUserService
                    .getUserById(id)
                    .then(response => this.user = response).then(response => { this.userToEdit = this.user.firstName + " " + this.user.lastName })
                    .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get user'));
            }
            //one call for get users?
            this.applicationUserService.getUsers().then(response => this.managers = response)
                .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get users'));/*.then(x => { this.managers = this.managers.filter(y => y.id != id) })*/

            this.applicationUserService.getUsers().then(response => this.reviewers = response)
                .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get users'));/*.then(x => { this.reviewers = this.reviewers.filter(y => y.id != id) });*/

            this.jobTitlesService.getJobTitles().then(response => this.titles = response)
                .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get titles'));

            this.jobPositionsService.getJobPositions().then(response => this.positions = response)
                .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get positions'));

            this.templatesService.getActiveTemplates().then(response => this.templates = response)
                .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to get templates'));

        });
        this.validFirst = true;
        this.validLast = true;
       
    }
    onSubmit() {
        this.save()
    }
    validName(name:string)
    {
        var regex = "^[a-zšđčćž A-ZŠĐČĆŽ]+$";
        var isValid = name.match(regex);
        if (isValid)
            this.validFirst = true;
        else
            this.validFirst = false;
    }
    validLastName(name: string) {
        var regex = "^[a-zšđčćž A-ZŠĐČĆŽ]+$";
        var isValid = name.match(regex);
        if (isValid)
            this.validLast = true;
        else
            this.validLast = false;
    }

    save() {
        this.applicationUserService.save(this.user)
            .then(this.goBack)
            .catch((error) => this.myGlobals.showException(error, 'Error occured while trying to edit user: '));
    }

    goBack() {
        window.history.back();
    }
}

