using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.TestDAL
{
    class Program
    {
       public static void Main(string[] args)
        {
            Login();
        }

        public static void Login()
        {
            Console.WriteLine("Welcome to Performance Evaluation Portal");
            Console.WriteLine("Please enter username and password");
            ApplicationUser k = null;
            do
            {
                Console.WriteLine("Enter username");
                string u = Console.ReadLine();

                Console.WriteLine("Enter password");
                string p = Console.ReadLine();

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    PasswordHasher j = new PasswordHasher();
                   k = ctx.Users.Where(x => x.UserName == u).SingleOrDefault();
                    if (k != null)
                    {

                        if (j.VerifyHashedPassword(k.PasswordHash, p) == PasswordVerificationResult.Success)
                            break;
                        else
                            k = null;
                    }


                    if (k == null)
                        Console.WriteLine("\nWrong username or password.");
                    
                }
            } while (k == null);
            Console.WriteLine("\nWelcome " + k.FirstName + " " + k.LastName + "!");

            
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ApplicationUser user = k;
                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    UserDataLogic.AdminMenu();
                }
                else
                {
                    RegularUserMethods.MenuOptions(user);
                }
            }
        }
    }
}
   

