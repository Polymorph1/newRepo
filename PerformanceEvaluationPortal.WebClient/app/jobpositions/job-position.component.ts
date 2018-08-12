import { Component, OnInit } from '@angular/core';
import { JobPosition } from './job-position';
import { JobPositionsService } from './job-position.service';
import { Router } from '@angular/router';

@Component({
    selector: 'my-jobpositons'
})
   

export class JobPositions implements OnInit {
    constructor(
        private jobPostionsService: JobPositionsService,
           ) { }

    jobPositions: JobPosition[];

    ngOnInit() {
        this.jobPostionsService
            .getJobPositions()
            .then(response => this.jobPositions = response);
        
    }
}