import { Component, OnInit, Input, trigger, state, style, transition, animate, Output, EventEmitter } from '@angular/core';
import { ApplicationUser } from './application-user';
import { ApplicationUserService } from './application-user.service';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';
import { Ng2PaginationModule } from 'ng2-pagination';


declare var swal: any;


@Component({
    selector: 'my-users',
    templateUrl: 'app/users/application-user.component.html',
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

export class ApplicationUserComponent implements OnInit {
    @Input() id: string;
    @Input() page;
    @Input() maxSize: number;

    @Output() pageChange: EventEmitter<number> = new EventEmitter<number>();
    constructor(
        private usersService: ApplicationUserService,
        private router: Router,
        private myGlobals: MyGlobals
    ) { }

    users: ApplicationUser[];
    filteredUsers: ApplicationUser[];
    numberOfUsers: number = 0; 
    pageNumber: any[]=[];
    usersPerPage: number = 2; 
    selectedPage: number = 0;

    ngOnInit() {
         
        this.usersService
            .getUsers()
            .then(response => {
                this.users = response;                
                this.assignCopy();
            })
            .catch(error => this.myGlobals.showException(error, 'Error occured while trying to get users: '));       
    }
    onPageChange(e) {
        if (e)
            this.page = e;
    }
    assignCopy() {
        this.filteredUsers = Object.assign([], this.users);
    }

    filterItem(myInput: string) {
        if (!myInput) this.assignCopy(); //when nothing has typed
        this.filteredUsers = Object.assign([], this.users).filter(
            item => (item.firstName + item.lastName).toLowerCase().indexOf(myInput.toLowerCase()) > -1

        )
    }

    goToDetail(user: ApplicationUser) {
        let link = ['/user/details', user.id];

        this.router.navigate(link);
    }

    getPerformanceEvaluationsForUser(user: ApplicationUser) {
        let link = ['/users/performanceevaluations', user.id];

        this.router.navigate(link);
    }

    setPage(page: number) {
        this.selectedPage = page;
        this.usersService.getUsers().then(response => { this.filteredUsers = response });
    }
}