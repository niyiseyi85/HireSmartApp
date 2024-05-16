using HireSmartApp.Core.Data;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.Domain.Authorization;
using HireSmartApp.Core.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HireSmartApp.API.Configurations
{
    public class DefaultData
    {
        /// <summary>
        /// <c>Initialize</c> Offers extension for default data initialization
        /// </summary>
        /// <param name="app"></param>
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();

                // Populate Claims table if it is null or empty
                if (context.AuthClaims == null || !context.AuthClaims.AsEnumerable().Any())
                {
                    var claimModel = LoadDefaultClaims().ToArray();

                    var set = context.Set<AuthClaim>().AsNoTracking().ToList();
                    context.AuthClaims.AddRange(set);
                    context.AuthClaims.AddRange(claimModel);
                    context.SaveChanges();
                }

                // Populate Roles table if it is null or empty
                if (context.Roles == null || !context.Roles.AsEnumerable().Any())
                {
                    var roleModel = LoadDefaultRoles().ToArray();

                    var set = context.Set<Role>().AsNoTracking().ToList();
                    context.Roles.AddRange(set);

                    context.Roles.AddRange(roleModel);
                    context.SaveChanges();
                }


                if (context.RoleAuthClaims == null || !context.RoleAuthClaims.AsEnumerable().Any())
                {
                    //Assign claims to the default roles created

                    // Admin
                    int[] ClaimsLevelI = new int[] { 1, 2, 3, 4 };

                    // Claim Processor
                    int[] ClaimsLevelII = new int[] { 1, 2, 3 };

                    //Policy Holder
                    int[] ClaimsLevelIII = new int[] { 4 };

                    SaveRoleWithClaims(ClaimsLevelI, 1, app);
                    SaveRoleWithClaims(ClaimsLevelII, 2, app);
                    SaveRoleWithClaims(ClaimsLevelIII, 3, app);
                }
            }



            //Creates the default User and Party for admin and also add the Admin permission to it
            LoadDefaultAdminUser(app);

            //Continue application load
            return;
        }

        /// <summary>
        /// Update roleClaim table with matching claim for default roles
        /// </summary>
        /// <param name="arrayClaimId"></param>
        /// <param name="roleId"></param>
        /// <param name="app"></param>
        public static void SaveRoleWithClaims(int[] arrayClaimId, int roleId, IApplicationBuilder app)
        {
            foreach (int claimId in arrayClaimId)
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DataContext>();
                    context.Database.EnsureCreated();

                    Role roleModel = new Role { RoleId = roleId };
                    context.Roles.Add(roleModel);
                    context.Roles.Attach(roleModel);

                    AuthClaim claimModel = new AuthClaim { ClaimId = claimId };
                    context.AuthClaims.Add(claimModel);
                    context.AuthClaims.Attach(claimModel);

                    RoleAuthClaim roleClaimModel = new RoleAuthClaim
                    {
                        AuthClaimId = claimId,
                        RoleId = roleId
                    };

                    roleModel.RoleClaims.Add(roleClaimModel);

                    // call SaveChanges
                    context.SaveChanges();
                }
            }
        }


        /// <summary>
        /// Populates the Role table with default data
        /// </summary>
        /// <returns></returns>
        public static List<Role> LoadDefaultRoles()
        {
            List<Role> role = new List<Role>() {
                new Role
                {
                    Name = RoleType.Administrator.ToString(),
                    Description = "The administrator of the system",
                    RoleType = "System Created",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Role
                {
                    Name = RoleType.Employer.ToString(),
                    Description = "The user that want to employer",
                    RoleType = "System Created",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Role
                {
                    Name = RoleType.Candidate.ToString(),
                    Description = "A user applying in the system",
                    RoleType = "System created",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
            };

            return role;
        }

        /// <summary>
        /// Populates the Claim table with default data
        /// </summary>
        /// <returns></returns>
        public static List<AuthClaim> LoadDefaultClaims()
        {
            List<AuthClaim> authClaims = new List<AuthClaim>() {
                new AuthClaim
                {
                    Name = Claims.CanViewAllUsers,
                    Description = "Allows a user to view all Users",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new AuthClaim
                {
                    Name = Claims.CanViewAllClaims,
                    Description = "Allows a user to view all Claims",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new AuthClaim
                {
                    Name = Claims.CanReview,
                    Description = "Allows a user to review Claims",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },

                new AuthClaim
                {
                    Name = Claims.CanEditClaims,
                    Description = "Allows a user to edit a claim",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                }
            };

            return authClaims;
        }

        /// <summary>
        /// Populates the user and party table with default data
        /// </summary>
        /// <param name="app"></param>
        public static void LoadDefaultAdminUser(IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DataContext>();
                    context.Database.EnsureCreated();
                    if (context.Users != null && context.Users.Any())
                    {
                        return;
                    }

                    //Populates default user

                    var users = new List<User>
                    {
                        new User
                        {
                            FirstName = "Admin",
                            LastName = "Admin",
                            DateOfBirth = DateTime.Now,
                            Email = "Admin@gmail.com",
                            RoleId = 1,
                            PhoneNumber = "070123456789",
                            Nationality = "Nigeria",
                            CurrentResidence = "3, jones aveune",
                            Gender = "Male",
                            IDNumber = "112233",
                            IsActive = true,
                            Salt = "f178314aa377b0051d1565a9cc5e51c1",
                            HashPassword = "RO8NBn+uwzq4mUuNXgVPZqvwzyt0BSpq3Ih4QRVzsAg="
                        },
                        new User
                        {
                            FirstName = "Job",
                            LastName = "Doe",
                            DateOfBirth = DateTime.Now,
                            Email = "Job@gmail.com",
                            RoleId = 2,
                            PhoneNumber = "09098765432",
                            Nationality = "Nigeria",
                            CurrentResidence = "3, kiki close",
                            Gender = "Male",
                            IDNumber = "98767",
                            IsActive = true,
                            Salt = "fb8f1d30cf96154c4953c531cc45d942",
                            HashPassword = "50tkzURyXXLr6kUeU/hnfuGVCRCVzqDLvfE5Eeru+WI="
                        }
                    };

                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
