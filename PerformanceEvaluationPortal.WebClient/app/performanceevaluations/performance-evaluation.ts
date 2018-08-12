import { Template } from '../templates/template';
import { JobTitle } from '../jobtitles/job-title';
import { JobPosition } from '../jobpositions/job-position';
import { PerformanceEvaluationSkills } from '../performanceevaluationskills/performance-evaluation-skills'; 

export class PerformanceEvaluation
{
    id: number;
    consultantId: string;
    consultantFirstName: string;
    consultantLastName: string;
    reviewerId: string;
    reviewerFirstName: string;
    reviewerLastName: string;
    jobTitleId: number;
    consultantJobTitle: JobTitle;
    consultantJobPosition: JobPosition;
    jobPositionId: number;
    startDate: Date;
    dueDate: Date;
    endDate: Date;
    performanceEvaluationSkills: PerformanceEvaluationSkills[];
    isSubmited: boolean;
    submitedDate: Date;
    consultantComment: string;
    signedOnDate: Date;
    templateId: number;
    template: Template;
}