import { Skill } from '../skills/skill';
import { TemplateDuration } from '../templatedurations/template-duration';
export class Template
{
    id: number;
    name: string;
    isArchived: boolean;

    templateDurationId: number;
    templateDuration: TemplateDuration;  
    canBeArchived: boolean;
    canBeEdited: boolean;
    skillId: number;
    skills: Skill[];

}