import { Component, OnInit } from '@angular/core';
import { JobTitle } from './job-title';
import { JobTitlesService } from './job-title.service';
import { Router } from '@angular/router';

@Component({
    selector: 'my-jobtitles'
})
   
export class JobTitles implements OnInit {
    constructor(
        private jobTitlesService: JobTitlesService
      
    ) { }

    jobTitles: JobTitle[];
    
    ngOnInit() {
        this.jobTitlesService
            .getJobTitles()
            .then(response => this.jobTitles = response);
   
    }
}