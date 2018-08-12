import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PerformanceEvaluationComponent } from './performanceevaluations/performance-evaluation.component';
import { PerformanceEvaluationDetailsComponent } from './performanceevaluations/performance-evaluation-details.component';
import { PerformanceEvaluationAcknowledgeComponent } from './performanceevaluations/performance-evaluation-acknowledge.component';
import { PerformanceEvaluationEditComponent } from './performanceevaluations/performance-evaluation-edit.component';
import { ApplicationUserComponent } from './users/application-user.component';
import { TemplatesComponent } from './templates/templates.component';
import { TemplateSaveComponent } from './templates/template-save.component';
import { TemplateDetailsComponent } from './templates/template-details.component';
import { ApplicationUsersDetailsComponent } from './users/application-user-details.component';
import { ApplicationUserSaveComponent } from './users/application-user-save.component';
import { ApplicationUsersStartPageComponent } from './users/application-user-start-page.component';
import { ApplicationUserManagerPanelComponent } from './users/application-user-manager-panel.component';
import { ApplicationUserReviewerPanelComponent } from './users/application-user-reviewer-panel.component';
import { NotificationComponent } from './notifications/notification.component';

import { LoginComponent } from './login/login.component';
import { AuthGuard } from './login/auth.guard';
import { LoginGuard } from './login/login.guard';
import { CanDeactivateGuard } from './can-deactivate-guard.service';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent,
        canActivate: [LoginGuard]
    },
    {
        path: 'users',
        component: ApplicationUserComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'pe/details/:id',
        component: PerformanceEvaluationDetailsComponent,
        canActivate: [AuthGuard]
    },  
    {
        path: 'pe/edit/:id',
        component: PerformanceEvaluationEditComponent,
        canActivate: [AuthGuard],
        canDeactivate: [CanDeactivateGuard]
    },
    {
        path: 'pe/acknowledge/:id',
        component: PerformanceEvaluationAcknowledgeComponent,
        canActivate: [AuthGuard],
        canDeactivate: [CanDeactivateGuard]
    },
    {
        path: 'templates',
        component: TemplatesComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'template/save/:id',
        component: TemplateSaveComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'template/details/:id',
        component: TemplateDetailsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'user/details/:id',
        component: ApplicationUsersDetailsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'startPage',
        component: ApplicationUsersStartPageComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'managerPanel',
        component: ApplicationUserManagerPanelComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'reviewerPanel',
        component: ApplicationUserReviewerPanelComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'users/save/:id',
        component: ApplicationUserSaveComponent,
        canActivate: [AuthGuard]
    }

]

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRouting { }
