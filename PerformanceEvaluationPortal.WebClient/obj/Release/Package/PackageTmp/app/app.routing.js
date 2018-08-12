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
var router_1 = require('@angular/router');
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
var login_component_1 = require('./login/login.component');
var auth_guard_1 = require('./login/auth.guard');
var login_guard_1 = require('./login/login.guard');
var can_deactivate_guard_service_1 = require('./can-deactivate-guard.service');
var routes = [
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: login_component_1.LoginComponent,
        canActivate: [login_guard_1.LoginGuard]
    },
    {
        path: 'users',
        component: application_user_component_1.ApplicationUserComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'pe/details/:id',
        component: performance_evaluation_details_component_1.PerformanceEvaluationDetailsComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'pe/edit/:id',
        component: performance_evaluation_edit_component_1.PerformanceEvaluationEditComponent,
        canActivate: [auth_guard_1.AuthGuard],
        canDeactivate: [can_deactivate_guard_service_1.CanDeactivateGuard]
    },
    {
        path: 'pe/acknowledge/:id',
        component: performance_evaluation_acknowledge_component_1.PerformanceEvaluationAcknowledgeComponent,
        canActivate: [auth_guard_1.AuthGuard],
        canDeactivate: [can_deactivate_guard_service_1.CanDeactivateGuard]
    },
    {
        path: 'templates',
        component: templates_component_1.TemplatesComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'template/save/:id',
        component: template_save_component_1.TemplateSaveComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'template/details/:id',
        component: template_details_component_1.TemplateDetailsComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'user/details/:id',
        component: application_user_details_component_1.ApplicationUsersDetailsComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'startPage',
        component: application_user_start_page_component_1.ApplicationUsersStartPageComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'managerPanel',
        component: application_user_manager_panel_component_1.ApplicationUserManagerPanelComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'reviewerPanel',
        component: application_user_reviewer_panel_component_1.ApplicationUserReviewerPanelComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'users/save/:id',
        component: application_user_save_component_1.ApplicationUserSaveComponent,
        canActivate: [auth_guard_1.AuthGuard]
    }
];
var AppRouting = (function () {
    function AppRouting() {
    }
    AppRouting = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes, { useHash: true })],
            exports: [router_1.RouterModule]
        }), 
        __metadata('design:paramtypes', [])
    ], AppRouting);
    return AppRouting;
}());
exports.AppRouting = AppRouting;
//# sourceMappingURL=app.routing.js.map