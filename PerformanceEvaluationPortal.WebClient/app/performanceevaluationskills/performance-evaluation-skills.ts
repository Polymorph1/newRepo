import { Template } from '../templates/template';
import { JobTitle } from '../jobtitles/job-title';
import { JobPosition } from '../jobpositions/job-position';
import { Skill } from '../skills/skill';

export class PerformanceEvaluationSkills {
    id: number;
    comment: string;
    grade: string;
    skill: Skill;
    skillId: number;
    performanceEvaluationId: number;
}