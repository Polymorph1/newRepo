import { Component, OnInit, Input, trigger, state, style, transition, animate } from '@angular/core';
import { Template } from './template';
import { TemplatesService } from './template.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Skill } from '../skills/skill';
import { TemplateDuration } from '../templatedurations/template-duration';
import { Router } from '@angular/router';
import { MyGlobals } from '../my-globals';

@Component({
    templateUrl: 'app/templates/template-details.component.html',
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

export class TemplateDetailsComponent implements OnInit {
    constructor(
        private templatesService: TemplatesService,
        private route: ActivatedRoute,
        private router: Router,
        private myGlobals: MyGlobals
        
    ) { }
    
    template: Template;
    templateToEdit: string;

    ngOnInit() {
        this.route.params.forEach((params: Params) => {

            let id = +params['id'];

            this.templatesService.getTemplateById(id).then(response => this.template = response)
                .catch((error) => {
                this.myGlobals.showException(error, "Error with getting template", 'templates');
            }).then(x => { this.templateToEdit = this.template.name });
                
              
        });
    }

    saveTemplate(template: Template) {
        let id =template.id;

        let link = ['/template/save', id];
        
        if (template.canBeEdited == false) {
            this.router.navigate(link);
        }

    }
    goBack() {
        window.history.back();
    }
}