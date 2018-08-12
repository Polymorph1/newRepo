import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule,FormControl, FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AppRouting } from './app.routing';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';

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

import { ApplicationUserService } from './users/application-user.service';
import { PerformanceEvaluationService } from './performanceevaluations/performance-evaluation.service';
import { TemplatesService } from './templates/template.service';
import { JobTitlesService } from './jobtitles/job-title.service';
import { JobPositionsService } from './jobpositions/job-position.service';
import { SkillService } from './skills/skill.service';
import { TemplateDurationService } from './templatedurations/template-duration.service';
import { DialogService } from './dialog.service';

import { MyGlobals } from './my-globals';
import { LoginGuard } from './login/login.guard';
import { AuthGuard } from './login/auth.guard';

import { AuthService } from './login/auth.service';
import { LoginComponent } from './login/login.component';
import { HttpClient } from './common/http.client';
import { MenuComponent } from './common/menu.component';
import { GlobalEventsManager } from './common/global-events-manager';
import { CanDeactivateGuard } from './can-deactivate-guard.service';

import { TooltipModule } from 'ng2-tooltip';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { NotificationService } from './notifications/notification.service';
import { NotificationComponent } from './notifications/notification.component';
import { Ng2PaginationModule } from 'ng2-pagination';


@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
        TooltipModule,
        ReactiveFormsModule,
        ToastModule,
        Ng2PaginationModule
    ],
    declarations: [
        AppComponent,
        PerformanceEvaluationComponent,
        PerformanceEvaluationDetailsComponent,
        PerformanceEvaluationAcknowledgeComponent,
        PerformanceEvaluationEditComponent,
        ApplicationUserComponent,
        TemplatesComponent,
        TemplateSaveComponent,
        TemplateDetailsComponent,
        ApplicationUsersDetailsComponent,
        ApplicationUserSaveComponent,
        LoginComponent,
        ApplicationUsersStartPageComponent,
        ApplicationUserManagerPanelComponent,
        ApplicationUserReviewerPanelComponent,
        MenuComponent,
        NotificationComponent
    ],
    bootstrap: [
        AppComponent
    ],
    providers: [
        ApplicationUserService,
        PerformanceEvaluationService,
        TemplatesService,
        JobTitlesService,
        JobPositionsService,
        SkillService,
        TemplateDurationService,
        DialogService,
        MyGlobals,
        AuthService,
        HttpClient,
        GlobalEventsManager,
        AuthGuard,
        LoginGuard,
        CanDeactivateGuard,
        NotificationService
    ]
})
export class AppModule { }
