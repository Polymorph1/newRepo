using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.BLL;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.Common.Mappers;
using System.Data.Entity;

namespace PerformanceEvaluationPortal.TestBLL
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    //var peService = new PerformanceEvaluationService(context);
                    //var userService = new ApplicationUserService(context);

                    var xx = context.Users.Include(x => x.JobTitle)
                                                    .Include(x => x.JobPosition)
                                                    .Where(x => x.ManagerId == "ab8de3ac-840d-4278-b2f5-587d28d30cee")
                                                    .Where(x => x.UsersIManage.ToList().Count > 0)
                                                    
                                                    .ToList();
                    foreach (var item in xx) {
                        Console.WriteLine(item.FirstName);
                    }
                    

                    
                    //var xx = context.Users.
                    //    Include(x => x.JobTitle)
                    //                             .Include(x => x.JobPosition)
                    //                             .Include(x => x.UsersIWriteReviewFor)
                    //                             .Include(x => x.UsersIManage)
                    //                             .Where(x =>x.Id == "ab8de3ac-840d-4278-b2f5-587d28d30cee").FirstOrDefault();

                    //foreach (var item in xx.UsersIWriteReviewFor.ToList())
                    //{
                    //    Console.WriteLine(item.FirstName);
                    //}


                    //foreach(var user in xx)
                    //{
                    //    if (user.UsersIManage.Count > 0)
                    //    {

                    //    }
                    //}
                    //var x = context.Users.Where(ax => ax.Id == "1606910d-710f-41a4-a439-de016a2296b2").FirstOrDefault();
                    //var users = x.UsersIManage.ToList();
                    //foreach (var y in users)
                    //{
                    //    Console.WriteLine(y.FirstName);
                    //}

                    //string comment = "This performance evaluation sucks!!!";

                    //var pe = peService.GetPerformanceEvaluation(1);
                    //var update = peService.AknowledgePerformanceEvaluation(pe.Id, comment);
                    //Console.WriteLine(update);

                    //var performanceEvaluationSkillsModelOne = new PerformanceEvaluationSkillModel
                    //{
                    //    Grade = "N",
                    //    Comment = "Neki komentar"
                    //};

                    //var performanceEvaluationSkillsModelTwo = new PerformanceEvaluationSkillModel
                    //{
                    //    Grade = "E",
                    //    Comment = "Neki drugi komentar"
                    //};

                    //var performanceEvaluationSkillsModelThree = new PerformanceEvaluationSkillModel
                    //{
                    //    Grade = "M",
                    //    Comment = "Neki treci komentar"
                    //};

                    //List<PerformanceEvaluationSkillModel> gradesAndComments = new List<PerformanceEvaluationSkillModel>();

                    //gradesAndComments.Add(performanceEvaluationSkillsModelOne);
                    //gradesAndComments.Add(performanceEvaluationSkillsModelTwo);
                    //gradesAndComments.Add(performanceEvaluationSkillsModelThree);

                    //var pe = peService.GetPerformanceEvaluation(1);
                    //var update = peService.SaveOrSubmitPerformanceEvaluation(pe.Id, gradesAndComments, true);

                    //var performanceEvaluationInfoModel = peService.GetPerformanceEvaluationsForReviewer("creviewer");

                    //var bla = peService.EditPerformanceEvaluation(performanceEvaluationInfoModel.First());

                    //var performanceEvaluationInfoModel = peService.GetPerformanceEvaluationsForReviewer("creviewer");

                    //var pe = peService.CreatePerformanceEvaluation(performanceEvaluationInfoModel.First());

                    //var pesForManager = peService.GetPerformanceEvaluationsForManager("cmanager");

                    //if (pesForManager.Count > 0)
                    //{
                    //    foreach (var pe in pesForManager)
                    //    {
                    //        Console.WriteLine("Consultant: {0} {1}", pe.ConsultantFirstName, pe.ConsultantLastName);
                    //        Console.WriteLine("Reviewer: {0} {1}", pe.ReviewerFirstName, pe.ReviewerLastName);
                    //        Console.WriteLine("Due date: {0}\n", pe.DueDate.ToString("MM/dd/yyyy"));
                    //    }
                    //}

                    //var pesForReviewer = peService.GetPerformanceEvaluationsForReviewer("creviewer");

                    //if (pesForReviewer.Count > 0)
                    //{
                    //    foreach (var pe in pesForReviewer)
                    //    {
                    //        if (pe.isClose || pe.isLate)
                    //        {
                    //            Console.WriteLine("Consultant: {0} {1}", pe.ConsultantFirstName, pe.ConsultantLastName);
                    //            Console.WriteLine("Job Title id, Job Position id: {0} {1}", pe.ConsultantJobTitleId, pe.ConsultantJobPositionId);
                    //            Console.WriteLine("Reviewer: {0} {1}", pe.ReviewerFirstName, pe.ReviewerLastName);
                    //            Console.WriteLine("Due date: {0}", pe.DueDate.ToString("MM/dd/yyyy"));
                    //            Console.WriteLine("Is close: {0}", pe.isClose);
                    //            Console.WriteLine("Is late: {0}\n", pe.isLate);
                    //        }
                    //    }
                    //}

                    //var myPEs = peService.GetMyPerformanceEvaluations("consultant");

                    //if (myPEs.Count > 0)
                    //{
                    //    foreach (var pe in myPEs)
                    //    {
                    //        Console.WriteLine("Consultant: {0}", pe.ConsultantName);
                    //        Console.WriteLine("Job Title, Job Position: {0} {1}", pe.ConsultantJobTitle, pe.ConsultantJobPosition);
                    //        Console.WriteLine("Reviewer: {0}", pe.ReviewerName);
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
