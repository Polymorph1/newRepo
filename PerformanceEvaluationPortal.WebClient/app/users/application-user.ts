import { JobTitle } from '../jobtitles/job-title';
import { JobPosition } from '../jobpositions/job-position';
import { Template } from '../templates/template';
export class ApplicationUser
{
    id: string;
    firstName: string;
    lastName: string;
    username: string;

    jobTitleId: number;
    jobTitle: JobTitle;

    jobPositionId: number;
    jobPosition: JobPosition;

    managerId: string;
    manager: ApplicationUser;

    reviewerId: string;
    reviewer: ApplicationUser;

    templateId: string;
    template: Template;

    employmentDate: Date;

    usersIManage: any[];

    usersIWriteReviewFor: any[];

}