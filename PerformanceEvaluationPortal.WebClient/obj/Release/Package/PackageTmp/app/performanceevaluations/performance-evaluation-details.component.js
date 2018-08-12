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
var performance_evaluation_service_1 = require('./performance-evaluation.service');
var router_1 = require('@angular/router');
var my_globals_1 = require('../my-globals');
var PerformanceEvaluationDetailsComponent = (function () {
    function PerformanceEvaluationDetailsComponent(peService, route, myGlobals) {
        this.peService = peService;
        this.route = route;
        this.myGlobals = myGlobals;
    }
    PerformanceEvaluationDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = +params['id'];
            _this.peService
                .getPerformanceEvaluationById(id)
                .then(function (response) { return _this.performanceEvaluation = response; })
                .catch(function (error) { return _this.myGlobals.showException(error, 'Error occured while trying to get evaluation. Details:', 'startPage'); });
        });
    };
    PerformanceEvaluationDetailsComponent.prototype.goBack = function () {
        window.history.back();
    };
    PerformanceEvaluationDetailsComponent.prototype.exportToPdf = function (pe) {
        var startDate_ = new Date(pe.startDate);
        var startDate_month = startDate_.getMonth() + 1;
        var startDate = startDate_.getDate() + "." + startDate_month + "." + startDate_.getFullYear() + ".";
        var endDate_ = new Date(pe.endDate);
        var endDate_month = endDate_.getMonth() + 1;
        var endDate = endDate_.getDate() + "." + endDate_month + "." + endDate_.getFullYear() + ".";
        var signedOnDate = "";
        if (pe.signedOnDate != null) {
            var signedOnDate_ = new Date(pe.signedOnDate);
            var signedOnDate_month = signedOnDate_.getMonth() + 1;
            signedOnDate = signedOnDate_.getDate() + "." + signedOnDate_month + "." + signedOnDate_.getFullYear() + ".";
        }
        var commentsAndGrades = [];
        for (var i = 0; i < pe.performanceEvaluationSkills.length; i++) {
            var grade = "";
            if (pe.performanceEvaluationSkills[i].grade == 'E')
                grade = "Exceeds Expectation";
            else if (pe.performanceEvaluationSkills[i].grade == 'M')
                grade = "Meets Expectation";
            else
                grade = "Needs Improvement";
            var comment = pe.performanceEvaluationSkills[i].comment;
            if (pe.performanceEvaluationSkills[i].comment == null)
                comment = "";
            commentsAndGrades.push({
                text: '• ' + pe.performanceEvaluationSkills[i].skill.name,
                style: 'skillName'
            }, {
                text: pe.performanceEvaluationSkills[i].skill.description,
                style: 'skillDescription'
            }, {
                style: 'tableLayout',
                table: {
                    headerRows: 1,
                    widths: [230, '*'],
                    body: [
                        [{ text: 'GRADE', style: 'tableHeader' }, { text: 'COMMENT', style: 'tableHeader' }],
                        [{ text: grade, style: 'tableDetails' }, { text: comment, style: 'tableDetails' }]
                    ]
                },
                layout: {
                    hLineWidth: function (i, node) {
                        if (i === 0 || i === node.table.body.length) {
                            return 0;
                        }
                        return (i === node.table.headerRows) ? 0.5 : 0;
                    },
                    vLineWidth: function (i) {
                        return 0;
                    },
                    hLineColor: function (i) {
                        return i === 1 ? 'grey' : 'white';
                    },
                    paddingLeft: function (i) {
                        return i === 0 ? 0 : 8;
                    },
                    paddingRight: function (i, node) {
                        return (i === node.table.widths.length - 1) ? 0 : 8;
                    }
                }
            });
        }
        var employeeAcknowledgment = [];
        if (pe.consultantComment != null) {
            employeeAcknowledgment.push({
                style: 'tableLayout',
                table: {
                    headerRows: 1,
                    widths: [230, '*'],
                    body: [
                        [{ text: 'ACKNOWLEDGMENT', style: 'tableHeader' }, { text: 'COMMENT', style: 'tableHeader' }],
                        [{
                                text: 'I ' + pe.consultantFirstName + ' ' + pe.consultantLastName +
                                    ' acknowledge my Performance Evaluation Report and I am aware that' +
                                    ' within 7 (seven) days I may submit my rebuttal to the AP management. ', style: 'tableDetails'
                            },
                            { text: pe.consultantComment, style: 'tableDetails' }]
                    ]
                },
                layout: {
                    hLineWidth: function (i, node) {
                        if (i === 0 || i === node.table.body.length) {
                            return 0;
                        }
                        return (i === node.table.headerRows) ? 0.5 : 0;
                    },
                    vLineWidth: function (i) {
                        return 0;
                    },
                    hLineColor: function (i) {
                        return i === 1 ? 'grey' : 'white';
                    },
                    paddingLeft: function (i) {
                        return i === 0 ? 0 : 8;
                    },
                    paddingRight: function (i, node) {
                        return (i === node.table.widths.length - 1) ? 0 : 8;
                    }
                }
            }, {
                text: 'Signed on date:',
                bold: true,
                fontSize: 10,
                alignment: 'right',
                margin: [0, 40, 0, 5]
            }, {
                text: signedOnDate,
                fontSize: 9,
                alignment: 'right'
            });
        }
        else {
            employeeAcknowledgment.push({
                text: "Evaluation hasn't been signed by employee yet.",
                fontSize: 9,
                margin: [0, 5, 0, 0]
            });
        }
        var docDefinition = {
            pageSize: 'A4',
            content: [
                {
                    text: 'PERFORMANCE EVALUATION REPORT',
                    style: 'header',
                    // margin: [left, top, right, bottom]
                    margin: [0, 0, 0, 30]
                },
                {
                    columns: [
                        {
                            alignment: 'right',
                            text: 'Employee Name: ',
                            margin: [0, 0, 5, 0],
                            width: 80,
                            bold: true,
                        },
                        {
                            alignment: 'left',
                            text: pe.consultantFirstName + " " + pe.consultantLastName,
                            width: 250
                        },
                        {
                            alignment: 'right',
                            text: 'Review Period: ',
                            margin: [0, 0, 5, 0],
                            width: 80,
                            bold: true,
                        },
                        {
                            alignment: 'left',
                            text: startDate + " - " + endDate
                        }
                    ],
                    style: 'details'
                },
                {
                    columns: [
                        {
                            alignment: 'right',
                            text: 'Job Title: ',
                            margin: [0, 0, 5, 0],
                            width: 80,
                            bold: true,
                        },
                        {
                            alignment: 'left',
                            text: pe.consultantJobTitle.name + " - " + pe.consultantJobPosition.name,
                            width: 250
                        },
                        {
                            alignment: 'right',
                            text: 'Reviewer Name: ',
                            margin: [0, 0, 5, 0],
                            width: 80,
                            bold: true,
                        },
                        {
                            alignment: 'left',
                            text: pe.reviewerFirstName + " " + pe.reviewerLastName
                        }
                    ],
                    style: 'details'
                },
                {
                    text: 'Performance evaluation skills',
                    margin: [0, 35, 0, 0],
                    fontSize: 11,
                },
                commentsAndGrades,
                {
                    text: 'Employee’s acknowledgment of evaluation',
                    margin: [0, 35, 0, 15],
                    fontSize: 11,
                    pageOrientation: 'portrait',
                    pageBreak: 'before'
                },
                employeeAcknowledgment
            ],
            styles: {
                header: {
                    fontSize: 12,
                    bold: true,
                    alignment: 'center',
                    color: '#808080'
                },
                details: {
                    fontSize: 9,
                    margin: [0, 0, 0, 5]
                },
                skillName: {
                    fontSize: 11,
                    bold: true,
                    margin: [0, 20, 0, 5]
                },
                skillDescription: {
                    fontSize: 9,
                    alignment: 'justify',
                    margin: [0, 0, 0, 10]
                },
                tableHeader: {
                    fontSize: 10,
                    bold: true,
                    margin: [0, 5, 0, 5]
                },
                tableLayout: {
                    margin: [0, 5, 0, 15]
                },
                tableDetails: {
                    fontSize: 9,
                    alignment: 'justify',
                    margin: [0, 5, 0, 5]
                }
            }
        };
        pdfMake.createPdf(docDefinition).open();
    };
    PerformanceEvaluationDetailsComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/performanceevaluations/performance-evaluation-details.component.html',
            animations: [
                core_1.trigger('animateIn', [
                    core_1.state('in', core_1.style({ opacity: 1, transform: 'translateX(0)' })),
                    core_1.transition('void => *', [
                        core_1.style({
                            opacity: 0,
                            transform: 'translateY(3%)'
                        }),
                        core_1.animate('0.2s ease-in')
                    ]),
                    core_1.transition('* => void', [
                        core_1.animate('0.2s 10 ease-out', core_1.style({
                            opacity: 0,
                            transform: 'translateY(-3%)'
                        }))
                    ])
                ])
            ]
        }), 
        __metadata('design:paramtypes', [performance_evaluation_service_1.PerformanceEvaluationService, router_1.ActivatedRoute, my_globals_1.MyGlobals])
    ], PerformanceEvaluationDetailsComponent);
    return PerformanceEvaluationDetailsComponent;
}());
exports.PerformanceEvaluationDetailsComponent = PerformanceEvaluationDetailsComponent;
//# sourceMappingURL=performance-evaluation-details.component.js.map