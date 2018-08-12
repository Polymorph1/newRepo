import { Component, OnInit } from '@angular/core';
import { ApplicationUser } from '../users/application-user';
import { ApplicationUserService } from '../users/application-user.service';
import { PerformanceEvaluationService } from './performance-evaluation.service';
import { PerformanceEvaluation } from './performance-evaluation';
import { Router } from '@angular/router';

@Component({
    selector: 'my-performance-evaluations',
    templateUrl: 'app/performanceevaluations/performance-evaluation.component.html'
})

export class PerformanceEvaluationComponent implements OnInit {
    constructor(
        private userService: ApplicationUserService,
        private peService: ApplicationUserService,
        private router: Router
    ) { }

    users: ApplicationUser[];

    ngOnInit() {
        this.userService
            .getUsers()
            .then(response => this.users = response);
    }
}