import { Component, OnInit, Input, trigger, state, style, transition, animate, Output, EventEmitter  } from '@angular/core';
import { Template } from './template';
import { TemplatesService } from './template.service';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';
import { Ng2PaginationModule } from 'ng2-pagination';
declare var swal: any;

@Component({
    selector: 'my-templates',
    templateUrl: 'app/templates/templates.component.html',
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

export class TemplatesComponent implements OnInit {
    @Input() id: string;
    @Input() page;
    @Input() maxSize: number;

    @Output() pageChange: EventEmitter<number> = new EventEmitter<number>();

    constructor(
        private templatesService: TemplatesService,
        private router: Router,
        private myGlobals: MyGlobals
    ) {
        this.page = 1;
    }

    templates: Template[];
    filteredTemplates: Template[];
    archivedTemplates: Template[];
    selectedPage: number = 0;
    templatesPerPage: number = 5;   
 
    ngOnInit() {

        this.templatesService
            .getActiveTemplates()
            .then(response => {
                this.templates = response;
                this.assignCopy();
            }).catch((error) => this.myGlobals.showException(error, " "));

        this.templatesService.getArchivedTemplates().then(x => this.archivedTemplates = x)
            .catch((error) => this.myGlobals.showException(error, " "));
    }
    onPageChange(e) {
        if (e)
            this.page = e;
    }
    setPage(page: number) {
        this.selectedPage = page;

        this.templatesService.getActiveTemplates().then(x => { this.filteredTemplates = x });
    }
    assignCopy() {
        this.filteredTemplates = Object.assign([], this.templates);
    }
    filterItem(myInput: string) {
        if (!myInput) this.assignCopy(); //when nothing has typed
        this.filteredTemplates = Object.assign([], this.templates).filter(
            item => (item.name).toLowerCase().indexOf(myInput.toLowerCase()) > -1
        )

    }


    saveTemplate(template: Template) {
        let id = template ? template.id : undefined;

        let link = ['/template/save', id];
        if (id == undefined) {
            this.router.navigate(link);
        }

        if (template.canBeEdited == false) {
            this.router.navigate(link);
        }

    }

    detailsTemplate(template: Template) {
        let id = template.id;

        let link = ['/template/details', id];

        this.router.navigate(link);
    }

    archiveTemplate(template: Template) {
        let id = template.id;
        var temp = new Template();
        temp = template;

        if (temp.canBeArchived == true) {
            swal(
                'Error!',
                "Template can't be archived",
                'error'
            );
            return;
        }

        this.templatesService.archiveTemplate(id).then(x => {
            var toArchive = this.filteredTemplates.indexOf(temp);

            if (toArchive > -1) {
                this.filteredTemplates.splice(toArchive, 1);

                this.archivedTemplates.push(temp);
            }            
        }).catch((error) => this.myGlobals.showException(error, "Template can't be archived", 'templates'));
       
    }

    restoreTemplate(template: Template) {
        let id = template.id;
        var temp = new Template();
        temp = template;

        this.templatesService.archiveTemplate(id).then(x => {
            var toRestore = this.archivedTemplates.indexOf(temp);
            if (toRestore > -1) {
                this.archivedTemplates.splice(toRestore, 1)              
            }
            this.filteredTemplates.push(temp);
        }).catch((error) => this.myGlobals.showException(error," "));

    }

}

