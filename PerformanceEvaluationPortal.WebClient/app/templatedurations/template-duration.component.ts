import {Component,OnInit} from '@angular/core';
import {TemplateDuration} from './template-duration';
import {TemplateDurationService} from './template-duration.service';
import {Router} from '@angular/router';

@Component({
    selector: 'my-templateduration'
})

export class TemplateDurations implements OnInit {
    constructor(private templateDurationService: TemplateDurationService)
    { }

    templateDurations: TemplateDuration[];

    ngOnInit() {
        this.templateDurationService.GetAllTemplateDurations()
            .then(response => this.templateDurations = response);
    }
}