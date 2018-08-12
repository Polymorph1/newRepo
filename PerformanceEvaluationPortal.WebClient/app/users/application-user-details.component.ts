import { Component, OnInit, Input, trigger, state, style, transition, animate } from '@angular/core';
import { ApplicationUser } from './application-user';
import { ApplicationUserService } from './application-user.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';

@Component({
    templateUrl: 'app/users/application-user-details.component.html',
    animations: [
        trigger('animateIn', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateY(3%)'
                }),
                animate('0.2s ease-in')
            ]),
            transition('* => void', [
                animate('0.2s 10 ease-out', style({
                    opacity: 0,
                    transform: 'translateY(-3%)'
                }))
            ])
        ])
    ]
})

export class ApplicationUsersDetailsComponent implements OnInit {
    constructor(
        private applicationUserService: ApplicationUserService,
        private route: ActivatedRoute,
        private router: Router,
        private myGlobals: MyGlobals
    ) { }

    user: ApplicationUser;

    ngOnInit() {
        this.route.params.forEach((params: Params) => {

            let id = params['id'];

            this.applicationUserService
                .getUserById(id)
                .then(response => this.user = response)
                .catch((error) => this.myGlobals.showException(error, 'Error with getting user','users'));
        });
    }

    saveUser(user: ApplicationUser) {
        let id = user.id;

        let link = ['/users/save', id];

        this.router.navigate(link);
    }

    goBack() {
        window.history.back();
    }
}