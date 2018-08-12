import {Component,OnInit} from '@angular/core';
import {Skill} from './skill';
import {SkillService} from './skill.service';
import {Router} from '@angular/router';

@Component({
    selector: 'my-skills'
})

export class Skills implements OnInit {

    constructor(private skillService: SkillService)
    { }

    skills: Skill[];

    ngOnInit() {
        this.skillService.GetAllSkills()
            .then(response => this.skills = response);
    }
}
