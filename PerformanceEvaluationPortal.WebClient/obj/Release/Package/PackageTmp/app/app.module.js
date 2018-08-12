"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var platform_browser_1 = require('@angular/platform-browser');
var http_1 = require('@angular/http');
var forms_1 = require('@angular/forms');
var app_routing_1 = require('./app.routing');
var app_component_1 = require('./app.component');
var router_1 = require('@angular/router');
var performance_evaluation_component_1 = require('./performanceevaluations/performance-evaluation.component');
var performance_evaluation_details_component_1 = require('./performanceevaluations/performance-evaluation-details.component');
var performance_evaluation_acknowledge_component_1 = require('./performanceevaluations/performance-evaluation-acknowledge.component');
var performance_evaluation_edit_component_1 = require('./performanceevaluations/performance-evaluation-edit.component');
var application_user_component_1 = require('./users/application-user.component');
var templates_component_1 = require('./templates/templates.component');
var template_save_component_1 = require('./templates/template-save.component');
var template_details_component_1 = require('./templates/template-details.component');
var application_user_details_component_1 = require('./users/application-user-details.component');
var application_user_save_component_1 = require('./users/application-user-save.component');
var application_user_start_page_component_1 = require('./users/application-user-start-page.component');
var application_user_manager_panel_component_1 = require('./users/application-user-manager-panel.component');
var application_user_reviewer_panel_component_1 = require('./users/application-user-reviewer-panel.component');
var application_user_service_1 = require('./users/application-user.service');
var performance_evaluation_service_1 = require('./performanceevaluations/performance-evaluation.service');
var template_service_1 = require('./templates/template.service');
var job_title_service_1 = require('./jobtitles/job-title.service');
var job_position_service_1 = require('./jobpositions/job-position.service');
var skill_service_1 = require('./skills/skill.service');
var template_duration_service_1 = require('./templatedurations/template-duration.service');
var dialog_service_1 = require('./dialog.service');
var my_globals_1 = require('./my-globals');
var login_guard_1 = require('./login/login.guard');
var auth_guard_1 = require('./login/auth.guard');
var auth_service_1 = require('./login/auth.service');
var login_component_1 = require('./login/login.component');
var http_client_1 = require('./common/http.client');
var menu_component_1 = require('./common/menu.component');
var global_events_manager_1 = require('./common/global-events-manager');
var can_deactivate_guard_service_1 = require('./can-deactivate-guard.service');
var ng2_tooltip_1 = require('ng2-tooltip');
var ng2_toastr_1 = require('ng2-toastr/ng2-toastr');
var notification_service_1 = require('./notifications/notification.service');
var notification_component_1 = require('./notifications/notification.component');
var ng2_pagination_1 = require('ng2-pagination');
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpModule,
                forms_1.FormsModule,
                router_1.RouterModule,
                app_routing_1.AppRouting,
                ng2_tooltip_1.TooltipModule,
                forms_1.ReactiveFormsModule,
                ng2_toastr_1.ToastModule,
                ng2_pagination_1.Ng2PaginationModule
            ],
            declarations: [
                app_component_1.AppComponent,
                performance_evaluation_component_1.PerformanceEvaluationComponent,
                performance_evaluation_details_component_1.PerformanceEvaluationDetailsComponent,
                performance_evaluation_acknowledge_component_1.PerformanceEvaluationAcknowledgeComponent,
                performance_evaluation_edit_component_1.PerformanceEvaluationEditComponent,
                application_user_component_1.ApplicationUserComponent,
                templates_component_1.TemplatesComponent,
                template_save_component_1.TemplateSaveComponent,
                template_details_component_1.TemplateDetailsComponent,
                application_user_details_component_1.ApplicationUsersDetailsComponent,
                application_user_save_component_1.ApplicationUserSaveComponent,
                login_component_1.LoginComponent,
                application_user_start_page_component_1.ApplicationUsersStartPageComponent,
                application_user_manager_panel_component_1.ApplicationUserManagerPanelComponent,
                application_user_reviewer_panel_component_1.ApplicationUserReviewerPanelComponent,
                menu_component_1.MenuComponent,
                notification_component_1.NotificationComponent
            ],
            bootstrap: [
                app_component_1.AppComponent
            ],
            providers: [
                application_user_service_1.ApplicationUserService,
                performance_evaluation_service_1.PerformanceEvaluationService,
                template_service_1.TemplatesService,
                job_title_service_1.JobTitlesService,
                job_position_service_1.JobPositionsService,
                skill_service_1.SkillService,
                template_duration_service_1.TemplateDurationService,
                dialog_service_1.DialogService,
                my_globals_1.MyGlobals,
                auth_service_1.AuthService,
                http_client_1.HttpClient,
                global_events_manager_1.GlobalEventsManager,
                auth_guard_1.AuthGuard,
                login_guard_1.LoginGuard,
                can_deactivate_guard_service_1.CanDeactivateGuard,
                notification_service_1.NotificationService
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map