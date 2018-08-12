using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.TestDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.TestDAL
{
    class UserDataLogic
    {
        
      

        public static void AdminMenu()
        {
            int pick = 0;
            Console.WriteLine("Please choose a option");
            do
            {
                Console.WriteLine("1.List all users");
                Console.WriteLine("2. Change user data");
                Console.WriteLine("3. EXIT");
                try
                {
                    pick = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("wrong entry");
                }
                if (pick == 1)
                    ListAllUsers();
                if (pick == 2)
                    ChangeUserData();

            } while (pick != 3 || pick<1 || pick>3);
        }

       

        public static void ChangeUserData()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ListAllUsers();
                ApplicationUser pickedUser = null;
                
                do
                {
                    Console.WriteLine("Please insert name of user which you want to change");
                    string userName = Console.ReadLine();
                    pickedUser = GetUserByName(userName);
                    if (pickedUser == null)
                    {
                        Console.WriteLine("User with that name doesn't exist");
                    }
                    

                } while (pickedUser==null);
              
                ChangePickedUser(pickedUser);


            }
        }
        public static ApplicationUser GetUserByName(string userFirstName)

        {
            ApplicationUser User=null;
            using (ApplicationDbContext context=new ApplicationDbContext())
            {
                User = context.Users.Include(x => x.JobPosition).Include(x => x.JobTitle).Include(x => x.Reviewer).Include(x => x.Manager).Where(x => x.FirstName == userFirstName).FirstOrDefault();
            }
            
            return User;
        }

        public static void ChangePickedUser(ApplicationUser applicationUser)
        {
            int pick = 0;
            Console.WriteLine("1. Change user Last name");
            Console.WriteLine("2. Change user Job title");
            Console.WriteLine("3. Change user Job position");
            Console.WriteLine("4. Change user reviewer");
            Console.WriteLine("5. Change user manager");
            Console.WriteLine("6. Change user template");
            Console.WriteLine("7. Back");
            Console.WriteLine("8.Log out");

            Console.WriteLine("Please choose one option");

            try
            {
                pick = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("wrong entry");
            }
          
                      

            switch (pick)
            {
                case 1: AdminUserChange.UserChangeLastname(applicationUser); break;
                case 2: AdminUserChange.UserChangeTitle(applicationUser); break;
                case 3: AdminUserChange.UserChangePosition(applicationUser); break;
                case 4: AdminUserChange.UserChangeReviewer(applicationUser); break;
                case 5: AdminUserChange.UserChangeManager(applicationUser); break;
                case 6: AdminUserChange.UserChangeTemplate(applicationUser); break;
                case 7: ChangeUserData(); break;
                case 8:Program.Login(); break;
                default:
                    ChangePickedUser(applicationUser);
                    break;
            }
      
        }
        public static void ListAllUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);                
                var listOfAllUsersWithRoleAdmin = roleManager.FindByName("Admin").Users.Select(x => x.UserId).ToList();
               
                var listOfAllUsers = userManager.Users.Where(i => !listOfAllUsersWithRoleAdmin.Contains(i.Id)).Include(x => x.JobPosition).Include(x => x.JobTitle).Include(x => x.Reviewer).Include(x => x.Manager).ToList();               
            

                int ordinalNumber = 1;
                foreach (var user in listOfAllUsers)
                {
                    Console.WriteLine("Number: {0}", ordinalNumber);
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("First name: {0}", user.FirstName);
                    Console.WriteLine("Last name: {0}", user.LastName);
                    Console.WriteLine("Position: {0}", user.JobPosition == null ? "-" : user.JobPosition.PositionName);                   
                    Console.WriteLine("Title: {0}", user.JobTitle == null ? "-" : user.JobTitle.Name);
                    Console.WriteLine("Reviewer: {0}", user.Reviewer == null ? "-" : user.Reviewer.FirstName + " " + user.Reviewer.LastName);
                    Console.WriteLine("Manager: {0}", user.Manager == null ? "-" : user.Manager.FirstName + " " + user.Manager.LastName);
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();

                    ordinalNumber++;
                }

            }
        }
    }
}
