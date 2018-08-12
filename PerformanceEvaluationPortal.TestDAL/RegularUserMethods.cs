using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.TestDAL
{
    public class RegularUserMethods
    {
        static ApplicationDbContext context = new ApplicationDbContext();

        public static void MenuOptions(ApplicationUser user)
        {
            // Every user doesn't have same options, therefore, we must ensure that user
            // who hasn't got permission for option number, for example, 4, can't choose it
            // Available options are kept in activeMenuOptions list
            List<int> activeMenuOptions = new List<int> { 0, 0, 0, 0, 0 };
            // Menu options are added or removed based on different attributes:
            // is a user a reviewer, manager or is he/she being reviewed
            // Default menu option is the Logout option
            var usersIWriteReviewFor = context.Users.Where(x => x.ReviewerId == user.Id).ToList();
            var usersIManage = context.Users.Where(x => x.ManagerId == user.Id).ToList();

            Console.WriteLine("");
            if (usersIManage.Count != 0)
            {
                Console.WriteLine("1 - Reviews Created by People I Manage");
                activeMenuOptions[0] = 1;
            }
            if (usersIWriteReviewFor.Count != 0)
            {
                Console.WriteLine("2 - Write Review");
                Console.WriteLine("3 - Reviews History");
                activeMenuOptions[1] = 1;
                activeMenuOptions[2] = 1;
            }
            if (user.ReviewerId != null)
            {
                Console.WriteLine("4 - My Reviews");
                activeMenuOptions[3] = 1;
            }
            Console.WriteLine("5 - Logout");
            activeMenuOptions[4] = 1;

            // We must ensure that the right option number is typed
            bool isOptionNumberCorrect = false;
            int selectedOption = 0;
            do
            {
                try
                {
                    Console.Write("\nChoose an option: ");
                    selectedOption = int.Parse(Console.ReadLine());

                    if (selectedOption == 1 && activeMenuOptions[0] == 1)
                        isOptionNumberCorrect = true;
                    else if (selectedOption == 2 && activeMenuOptions[1] == 1)
                        isOptionNumberCorrect = true;
                    else if (selectedOption == 3 && activeMenuOptions[2] == 1)
                        isOptionNumberCorrect = true;
                    else if (selectedOption == 4 && activeMenuOptions[3] == 1)
                        isOptionNumberCorrect = true;
                    else if (selectedOption == 5 && activeMenuOptions[4] == 1)
                        isOptionNumberCorrect = true;
                }
                catch (Exception)
                {
                    // if someone types a char that is not a number
                    Console.WriteLine("\nError! Type a number between 1 and 4.");
                }
            } while (!isOptionNumberCorrect);

            if (selectedOption == 1)
            {
                showManagerPerformanceEvaluationHistory(user, usersIManage);
            }
            else if (selectedOption == 2)
            {
                writePerformanceEvaluation(user);
            }
            else if (selectedOption == 3)
            {
                showReviewerPerformanceEvaluationHistory(user);
            }
            else if (selectedOption == 4)
            {
                showMyPerformanceEvaluations(user);
            }
            else if (selectedOption == 5)
            {
                Console.WriteLine("Logout successful!\n");
                Program.Login();
            }
        }
     
        private static void showMyPerformanceEvaluations(ApplicationUser user)
        {
            // Consultant can only see submitted performance evaluations
            var allPEs = context.PerformanceEvaluations.Where(x => x.ConsultantId == user.Id).ToList();
            var submittedPEs = allPEs.Where(x => x.IsSubmitted == true).ToList();

            if (submittedPEs.Count != 0)
            {
                showPerformanceEvaluationDetails(submittedPEs, user, true);
            }
            else
            {
                exitToMenuWithMessage(user, "\nReviews history is empty!");
            }
        }
        
        private static void showManagerPerformanceEvaluationHistory(ApplicationUser user, List<ApplicationUser> usersIManage)
        {
            List<PerformanceEvaluation> usersIManagePEs = new List<PerformanceEvaluation>();
            foreach (var userIManage in usersIManage)
            {
                var allPEs = context.PerformanceEvaluations.Where(x => x.ReviewerId == userIManage.Id).ToList();
                var submittedPEs = allPEs.Where(x => x.IsSubmitted == true).ToList();

                foreach (var pe in submittedPEs)
                {
                    usersIManagePEs.Add(pe);
                } 
            }

            if (usersIManagePEs.Count != 0)
            {
                showPerformanceEvaluationDetails(usersIManagePEs, user);
            }
            else
            {
                exitToMenuWithMessage(user, "\nReviews history is empty!");
            }

        }

        private static void exitToMenuWithMessage(ApplicationUser user, string message = "")
        {
            Console.WriteLine(message);
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
                MenuOptions(user);
        }


        // Performance Evaluation details for manager and consultant
        private static void showPerformanceEvaluationDetails(List<PerformanceEvaluation> usersPEs, ApplicationUser user, bool isConsultant = false)
        {
            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
            Console.WriteLine("  id  |   First name   |   Last name   |        Reviewer        |         Submitted date          ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            int peCount = 1;
            foreach (var pe in usersPEs)
            {
                Console.WriteLine(peCount + "     |     " + pe.ConsultantFirstName + "     |     " + pe.ConsultantLastName + "     |     " + pe.ReviewerFirstName + " " + pe.ReviewerLastName + "     |     " + pe.SubmittedDate);
                peCount++;
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            // Checking if the correct option number is typed
            int option = 0;
            bool isOptionNumberCorrect = false;
            do
            {
                try
                {
                    Console.Write("\nChoose the ordinal number of review you want to read (or type 0 to go back): ");
                    option = int.Parse(Console.ReadLine());
                    if (option >= 0 && option <= peCount)
                        isOptionNumberCorrect = true;
                }
                catch (Exception)
                {
                    // if someone types a char that is not a number
                    Console.WriteLine("\nError! Type the correct ordinal number.");
                }
            } while (!isOptionNumberCorrect);

            if (option == 0)
                MenuOptions(user);

            // Get consultant's id from pe object and put it in the chosenPEUserId variable for further usage
            string chosenPEConsultantId = usersPEs[option - 1].ConsultantId.ToString();
            var chosenConsultant = context.Users.Include(x => x.JobTitle)
                                                .Include(x => x.JobPosition)
                                                .Include(x => x.Reviewer)
                                                .Include("Template.TemplateDuration")
                                                .Include("Template.Skills")
                                                .Where(x => x.Id == chosenPEConsultantId).FirstOrDefault();


            Console.WriteLine("\n-------------------------------------------------------------");
            Console.WriteLine("Employee Name: " + chosenConsultant.FirstName + " " + chosenConsultant.LastName);
            Console.WriteLine("Job Title: " + chosenConsultant.JobTitle.Name);
            Console.WriteLine("Review period: " + chosenConsultant.Template.TemplateDuration.Duration + " months");
            Console.WriteLine("Reviewer Name: " + chosenConsultant.Reviewer.FirstName + " " + chosenConsultant.Reviewer.LastName + "\n");

            if (chosenConsultant.Template.Skills != null)
            {
                foreach (var skill in chosenConsultant.Template.Skills)
                {
                    var skillGrade = context.PerformanceEvaluationsSkills.Where(x => x.SkillId == skill.SkillId).FirstOrDefault();
                    Console.WriteLine(skill.SkillName + ": " + skillGrade.Grade);
                }
            }
            Console.WriteLine("-------------------------------------------------------------\n");

            // If it's consultant, he/she can confirm performance evaluation
            if (isConsultant)
            {
                Console.Write("\nPress 'K' to confirm.");
                bool isPressedK = false;
                do
                {
                    var keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.K)
                        isPressedK = true;
                } while (!isPressedK);

                var chosenPE = usersPEs[option - 1];
                chosenPE.SignedOnDateByConsultant = DateTime.Now;
                context.SaveChanges();

                exitToMenuWithMessage(user);
            }
            else
            {
                exitToMenuWithMessage(user);
            }
        }

        public static void writePerformanceEvaluation(ApplicationUser reviewer)
        {
            Database.SetInitializer(new NullDatabaseInitializer<ApplicationDbContext>());
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                // mustDoPEs are not-submitted performance evaluations with end date 10 days before today 
                var allOfMyPEs = context.PerformanceEvaluations.Where(x => x.ReviewerId == reviewer.Id).ToList();
                var notSubmittedPEs = allOfMyPEs.Where(x => x.IsSubmitted == false).ToList();
                var mustDoPEs = notSubmittedPEs.Where(x => x.EndDate.AddDays(-10) < DateTime.Now).ToList();

                if (mustDoPEs.Count != 0)
                {


                    Console.WriteLine("\n---------------------------------------------------------------------------------------------------");
                    Console.WriteLine("  id  |   First name   |   Last name   |         Start date          |        End date         ");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    int id = 1;
                    foreach (var pe in mustDoPEs)
                    {
                        Console.WriteLine(id + "     |     " + pe.ConsultantFirstName + "     |     " + pe.ConsultantLastName + "     |     " + pe.StartDate + "     |     " + pe.EndDate);
                        id++;
                    }
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");

                    int option = 0;
                    bool isUserOrdinalNumberCorrect = false;

                    do
                    {
                        try
                        {
                            Console.Write("\nChoose the ordinal number of user you want to review (or type 0 to go back): ");
                            option = int.Parse(Console.ReadLine());
                            if (option >= 0 && option <= id)
                                isUserOrdinalNumberCorrect = true;
                        }
                        catch (Exception)
                        {
                            // if someone types a char that is not a number
                            Console.WriteLine("\nError! Type the correct ordinal number.");
                        }
                    } while (!isUserOrdinalNumberCorrect);

                    if (option == 0)
                    {
                        MenuOptions(reviewer);
                    }

                    // Get consultant's id from pe object and put it in the chosenPEUserId variable for further usage
                    string chosenPEConsultantId = mustDoPEs[option - 1].ConsultantId.ToString();
                    var chosenConsultant = context.Users.Include(x => x.JobTitle)
                                                        .Include(x => x.JobPosition)
                                                        .Include(x => x.Reviewer)
                                                        .Include("Template.TemplateDuration")
                                                        .Include("Template.Skills")
                                                        .Where(x => x.Id == chosenPEConsultantId).FirstOrDefault();

                    Console.WriteLine("\nEmployee Name: " + chosenConsultant.FirstName + " " + chosenConsultant.LastName);
                    Console.WriteLine("Job Title: " + chosenConsultant.JobTitle.Name);
                    Console.WriteLine("Review period: " + chosenConsultant.Template.TemplateDuration.Duration + " months");
                    Console.WriteLine("Reviewer Name: " + chosenConsultant.Reviewer.FirstName + " " + chosenConsultant.Reviewer.LastName);


                    var chosenPE = mustDoPEs[option - 1];
                    // Insert new, not submitted, performance evaluation for the same user with updated
                    // start and end dates
                    PerformanceEvaluation newPerformanceEvaluation = new PerformanceEvaluation
                    {
                        ConsultantId = chosenConsultant.Id,
                        ConsultantFirstName = chosenConsultant.FirstName,
                        ConsultantLastName = chosenConsultant.LastName,
                        ReviewerId = chosenConsultant.Reviewer.Id,
                        ReviewerFirstName = chosenConsultant.Reviewer.FirstName,
                        ReviewerLastName = chosenConsultant.Reviewer.LastName,
                        JobTitle = chosenConsultant.JobTitle,
                        JobPosition = chosenConsultant.JobPosition,
                        StartDate = chosenPE.EndDate,
                        EndDate = chosenPE.EndDate.AddMonths(Convert.ToInt32(chosenConsultant.Template.TemplateDuration.Duration)),
                        IsSubmitted = false,
                    };
                    context.PerformanceEvaluations.Add(newPerformanceEvaluation);
                    context.SaveChanges();


                    chosenPE.IsSubmitted = true;
                    chosenPE.SubmittedDate = DateTime.Now;
                    context.SaveChanges();

                    if (chosenConsultant.Template.Skills != null)
                    {
                        foreach (var skill in chosenConsultant.Template.Skills)
                        {
                            bool isCorrectGrade = false;
                            string grade = "";
                            do
                            {
                                Console.WriteLine("\n" + skill.SkillName);
                                Console.Write("Type grade for the skill (E - Exceeds Expectations, M - Meets Expectations, N - Needs Improvement):");
                                grade = Console.ReadLine();
                                if (grade.ToUpper() == "E" || grade.ToUpper() == "M" || grade.ToUpper() == "N")
                                    isCorrectGrade = true;
                            } while (!isCorrectGrade);

                            PerformanceEvaluationSkill newPeSkill = new PerformanceEvaluationSkill()
                            {
                                PerformanceEvaluation = chosenPE,
                                Grade = grade,
                                Skill = skill
                            };

                            context.PerformanceEvaluationsSkills.Add(newPeSkill);
                            context.SaveChanges();
                        }
                    }

                    exitToMenuWithMessage(reviewer);
                }
                else
                {
                    exitToMenuWithMessage(reviewer, "\nNo active reviews!");
                }
            }
        }

        public static void showReviewerPerformanceEvaluationHistory(ApplicationUser reviewer)
        {
            Database.SetInitializer(new NullDatabaseInitializer<ApplicationDbContext>());

            // submittedPEs: reviewer in history sees only submitted performance evaluations 
            var allOfMyPEs = context.PerformanceEvaluations.Where(x => x.ReviewerId == reviewer.Id).ToList();
            var submittedPEs = allOfMyPEs.Where(x => x.IsSubmitted == true).ToList();

            if (submittedPEs.Count != 0)
            {
                Console.WriteLine("\n--------------------------------------------------------------------------");
                Console.WriteLine("  id  |   First name   |   Last name   |         Submitted date          ");
                Console.WriteLine("--------------------------------------------------------------------------");
                int id = 1;
                foreach (var pe in submittedPEs)
                {
                    Console.WriteLine(id + "     |     " + pe.ConsultantFirstName + "     |     " + pe.ConsultantLastName + "     |     " + pe.SubmittedDate);
                    id++;
                }
                Console.WriteLine("--------------------------------------------------------------------------");

                int option = 0;
                bool isOptionNumberCorrect = false;
                do
                {
                    try
                    {
                        Console.Write("\nChoose the ordinal number of review you want to read (or type 0 to go back): ");
                        option = int.Parse(Console.ReadLine());
                        if (Convert.ToInt32(option) >= 0 && Convert.ToInt32(option) <= id)
                            isOptionNumberCorrect = true;
                    }
                    catch (Exception)
                    {
                        // if someone types a char that is not a number
                        Console.WriteLine("\nError! Type the correct ordinal number.");
                    }
                } while (!isOptionNumberCorrect);

                if (Convert.ToInt32(option) == 0)
                {
                    MenuOptions(reviewer);
                }

                // Get consultant's id from pe object and put it in the chosenPEUserId variable for further usage
                string chosenPEConsultantId = submittedPEs[option - 1].ConsultantId.ToString();
                var chosenConsultant = context.Users.Include(x => x.JobTitle)
                                                    .Include(x => x.JobPosition)
                                                    .Include(x => x.Reviewer)
                                                    .Include("Template.TemplateDuration")
                                                    .Include("Template.Skills")
                                                    .Where(x => x.Id == chosenPEConsultantId).FirstOrDefault();

                Console.WriteLine("\n-------------------------------------------------------------");
                Console.WriteLine("\nEmployee Name: " + chosenConsultant.FirstName + " " + chosenConsultant.LastName);
                Console.WriteLine("Job Title: " + chosenConsultant.JobTitle.Name);
                Console.WriteLine("Review period: " + chosenConsultant.Template.TemplateDuration.Duration + " months");
                Console.WriteLine("Reviewer Name: " + chosenConsultant.Reviewer.FirstName + " " + chosenConsultant.Reviewer.LastName);

                if (chosenConsultant.Template.Skills != null)
                {
                    foreach (var skill in chosenConsultant.Template.Skills)
                    {
                        var skillGrade = context.PerformanceEvaluationsSkills.Where(x => x.SkillId == skill.SkillId).FirstOrDefault();
                        Console.WriteLine(skill.SkillName + ": " + skillGrade.Grade);
                    }
                }
                Console.WriteLine("-------------------------------------------------------------\n");

                exitToMenuWithMessage(reviewer);
            }
            else
            {
                exitToMenuWithMessage(reviewer, "\nReviews history is empty.");
            }
               
        }

    }
}
