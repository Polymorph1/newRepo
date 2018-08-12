using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.TestDAL
{
   public class AdminUserChange
    {
        public static ApplicationDbContext context = new ApplicationDbContext();
        
            
           

        

        public static void UserChangeTemplate(ApplicationUser user)
        {
            var UserP = GetUser(user);
            Console.WriteLine(GetUserData(user));
          
            int numberOfTemplate = 0;
            var templates = context.Templates.Include(x => x.TemplateDuration).ToList();
            int numberOf = 1;
            foreach (var template in templates)
            {
                Console.Write(numberOf.ToString()+" ");
                Console.WriteLine("Template name: " + template.TemplateName + " -> "+" Template duration: " + template.TemplateDuration.Duration + " months");
                numberOf++;
            }
            Console.WriteLine("Please enter the number of template you want add to user");
            try
            {
                numberOfTemplate = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("wrong entry");
            }

          

            UserP.Template = templates[numberOfTemplate - 1];
            context.SaveChanges();
            Console.WriteLine(GetUserData(UserP));
        }

        public static void UserChangeManager(ApplicationUser user)
        {
            var UserP = GetUser(user);
            Console.WriteLine(GetUserData(user));
           
            int numberOfManager = 0;
            var managers = GetAllUsers();
            int numberOf = 1;
            foreach (var manager in managers)
            {
                Console.Write(numberOf.ToString());
                Console.WriteLine(GetUserData(manager));
                numberOf++;
            }
            Console.WriteLine("Please enter the number of manager you want add to user");
            try
            {
                numberOfManager = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("wrong entry");
            }

           

            UserP.Manager = managers[numberOfManager - 1];
            context.SaveChanges();
            Console.WriteLine(GetUserData(UserP));
        }

        public static void UserChangeReviewer(ApplicationUser user)
        {
            var UserP = GetUser(user);
            Console.WriteLine(GetUserData(user));
           
            int numberOfReviewer = 0;
            var reviewers = GetAllUsers();
            int numberOf = 1;
            foreach (var reviewer in reviewers)
            {
                Console.WriteLine(numberOf.ToString());
                Console.WriteLine(GetUserData(reviewer));
                numberOf++;
            }
            Console.WriteLine("Please enter the number of reviewer you want add to user");
            try
            {
                numberOfReviewer = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("wrong entry");
            }

         
            UserP.Reviewer = reviewers[numberOfReviewer - 1];
            context.SaveChanges();
            Console.WriteLine(GetUserData(UserP));
        }

        public static void UserChangeTitle(ApplicationUser user)
        {
            var UserP = GetUser(user);
            Console.WriteLine(GetUserData(user));
          
            int numberOfTitle = 0;
            var jobTitle = context.JobTitles.ToList();
            int numberOf = 1;
            foreach (var title in jobTitle)
            {
                Console.WriteLine(numberOf + ". Job title: " + title.Name);
                numberOf++;
            }
            Console.WriteLine("Please enter the number of job title you want add to user");
            try
            {
                numberOfTitle = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("wrong entry");
            }

          

            UserP.JobTitle = jobTitle[numberOfTitle - 1];
            context.SaveChanges();
            Console.WriteLine(GetUserData(UserP));
            
        }

        public static void UserChangePosition(ApplicationUser user) 
        {
            var UserP = GetUser(user);
            Console.WriteLine(GetUserData(user));
            
            int numberOfPosition = 0;
            int numberOf = 1;
            var jobPositions = context.JobPositions.ToList();
            foreach (var position in jobPositions)
            {
                Console.WriteLine(numberOf + ". Job position: " + position.PositionName);
                numberOf++;
            }
            Console.WriteLine("Please enter the number of job position you want add to user");
            try
            {
                 numberOfPosition = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("wrong entry");
            }       
           
           
            UserP.JobPosition = jobPositions[numberOfPosition-1];
                context.SaveChanges();
            Console.WriteLine(GetUserData(UserP));

        }

        public static void UserChangeLastname(ApplicationUser user)
        {

            var userName = GetUser(user);
            Console.WriteLine(GetUserData(user));
           
            Console.WriteLine("Please enter new lastname");
            string s = Console.ReadLine();
            userName.LastName =s;
            context.SaveChanges();
            Console.WriteLine(GetUserData(userName));
           
            
        }

        public static ApplicationUser GetUser(ApplicationUser user)
        {
            var User = context.Users.Include(x => x.JobPosition).Include(x => x.JobTitle).Include(x => x.Reviewer).Include(x => x.Manager).Where(x => x.Id == user.Id).FirstOrDefault();
            return User;
        }
        public static string GetUserData(ApplicationUser user)
        {
            string jobPosition = user.JobPosition == null ? "-" : user.JobPosition.PositionName;
            string jobTitle = user.JobTitle == null ? "-" : user.JobTitle.Name;
            string manager = user.Manager == null ? "-" : user.Manager.FirstName + " " + user.Manager.LastName;
            string reviewer = user.Reviewer == null ? "-" : user.Reviewer.FirstName + " " + user.Reviewer.LastName;
            string userData = "First name: " + user.FirstName + "\n" +
            " Last name: " + user.LastName + "\n" +
            " Job position: "+jobPosition+  "\n" +
            " Job title: " + jobTitle + "\n" +
            " Reviewer: " + reviewer + "\n" +
            " Manager: " + manager + "\n"
          + "-----------------------------------------------------------------\n";

            
            return userData;
        }
        public static List<ApplicationUser> GetAllUsers()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var listOfAllUsersWithRoleAdmin = roleManager.FindByName("Admin").Users.Select(x => x.UserId).ToList();

            var listOfAllUsers = userManager.Users.Where(i => !listOfAllUsersWithRoleAdmin.Contains(i.Id)).Include(x => x.JobPosition).Include(x => x.JobTitle).Include(x => x.Reviewer).Include(x => x.Manager).ToList();
            return listOfAllUsers;
        }

    }
}
