import { Component, OnInit } from '@angular/core';
import { Template } from './template';
import { TemplatesService } from './template.service';
import { ActivatedRoute, Params } from '@angular/router';
import { SkillService } from '../skills/skill.service';
import { Skill } from '../skills/skill';
import { TemplateDurationService } from '../templatedurations/template-duration.service';
import { TemplateDuration } from '../templatedurations/template-duration';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MyGlobals } from '../my-globals';
declare var swal: any;

@Component({

    templateUrl: 'app/templates/template-save.component.html'

})

export class TemplateSaveComponent implements OnInit {

    constructor(
        private templatesService: TemplatesService,
        private skillService: SkillService,
        private templateDurationService: TemplateDurationService,
        private route: ActivatedRoute,
        private myGlobals: MyGlobals
    ) { }

    template: Template = new Template();
    templateDurations: TemplateDuration[] = [];
    skills: Skill[] = [];
    valid: boolean;
    templateToEdit: string;

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            let id = +params['id'];

            //Implements catch's for template,templateSkills ?,templateDurations ?
            if (id) {
                this.templatesService
                    .getTemplateById(id)
                    .then(response => {
                        this.template = response
                    }).catch((error) => this.myGlobals.showException(error,"Error with getting template",'templates')).then(response => {
                        this.skillService.GetAllSkills().then(response => this.skills = response).then(response => {

                            if (this.template.id != undefined) {
                                for (var i = 0; i < this.skills.length; i++) {
                                    for (var j = 0; j < this.template.skills.length; j++) {
                                        if (this.skills[i].id == this.template.skills[j].id) {
                                            this.skills[i].selected = true;
                                            this.valid = true;
                                        }
                                    }
                                }
                            }
                            this.templateToEdit = this.template.name
                        });
                    });
            }
            else {
                this.skillService.GetAllSkills().then(response => this.skills = response);
            }
            this.templateDurationService.GetAllTemplateDurations().then(response => this.templateDurations = response);
            this.valid = true;
        });
    }

    onSubmit() {
        this.save()
    }

    validate() {
        let isValid = true;
        let checkboxes = [];

        for (let i = 0; i < this.skills.length; i++) {
            checkboxes[i] = document.getElementById(this.skills[i].id.toString());
        }

        let selectedCheckboxes = [];

        for (let i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                selectedCheckboxes.push(checkboxes[i]);
            }
        }

        if (selectedCheckboxes.length > 0)
            isValid = true;
        else { isValid = false; }


        this.valid = isValid;
    }

    save() {
        this.template.skills = [];

        for (var i = 0; i < this.skills.length; i++) {
            if (this.skills[i].selected == true) {
                var skill = new Skill();
                skill.id = this.skills[i].id;

                this.template.skills.push(skill);
            }
        }

        this.templatesService.save(this.template).then(this.goBack)
            .catch((error) => this.myGlobals.showException(error,"Template can't be saved"));
    }

    //archive() {
    //    this.templatesService.getArchivedTemplates().catch((error) => this.myGlobals.showException(error, "Error with getting archived templates"));
    //}

    goBack() {
        window.history.back();
    }
}
