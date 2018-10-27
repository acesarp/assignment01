using Assignment01_ASP.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01_ASP.Data {
    public class SampleData {

        public static async Task Init(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) {

            context.Database.EnsureCreated();

            string adminID1 = "";
            string adminID2 = "";
            string adminID3 = "";
            string adminID4 = "";
            string adminID5 = "";
            string adminID6 = "";
            string role1 = "Admin";
            string desc1 = "This is the administration role";

            string role2 = "Member";
            string desc2 = "Regular user";

            string password = "P@$$w0rd";

            if(roleManager.FindByNameAsync(role1) == null) {
                await roleManager.CreateAsync(new AppRole(role1, desc1, DateTime.Now));
            }

            if(await roleManager.FindByNameAsync(role2) == null) {
                await roleManager.CreateAsync(new AppRole(role2, desc2, DateTime.Now));
            }
              
            if(await userManager.FindByNameAsync("a") == null) {
                var user = new AppUser("a", "a@a.a", 7782125454, "P@$$W0Rd", "Alonzo", "Britt", "1st", "Burnaby", "BC", "K4D8J6", "Canada") { IsAdmin = true };
                var result = await userManager.CreateAsync(user);

                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminID1 = user.Id;
            }

            if(await userManager.FindByNameAsync("m") == null) {
                var user = new AppUser("m", "m@m.m", 7782125454, "P@$$W0Rd", "Alonzo", "Britt", "1st", "Burnaby", "BC", "K4D8J6", "Canada") { IsAdmin = true };
                var result = await userManager.CreateAsync(user);

                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
                adminID2 = user.Id;
            }

            if(await userManager.FindByNameAsync("abc1212") == null) {
                var user = new AppUser("abc1212", "abc@abc.ca", 7782125454, "P@$$W0Rd", "Alonzo", "Britt", "1st", "Burnaby", "BC", "K4D8J6", "Canada"){ IsAdmin = true };
                var result = await userManager.CreateAsync(user);

                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminID3 = user.Id;
            }

            if(await userManager.FindByNameAsync("qwe2323") == null) {
                var user = new AppUser("qwe2323", "qwe@qwe.com", 6043047788, "P@$$W0Rd", "Mike", "Tyson", "45st", "Vancouver", "BC", "V7H1F5", "Canada");
                var result = await userManager.CreateAsync(user);

                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
                adminID4 = user.Id;
            }

            if(await userManager.FindByNameAsync("asd1234") == null) {
                var user = new AppUser("asd1234", "asd@asd.ca", 2501047734, "P@$$W0Rd", "Michael", "Tyler", "Main st", "Toronto", "ON", "D4H1F8", "Canada");
                var result = await userManager.CreateAsync(user);
                user.IsAdmin = true;
                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
                adminID5 = user.Id;
            }

            if(await userManager.FindByNameAsync("zxc1234") == null) {
                var user = new AppUser("zxc1234", "zxc@asd.ca", 2501047734, "P@$$W0Rd", "Nick", "Bob", "Main st", "Toronto", "ON", "D4H1F8", "UK");
                var result = await userManager.CreateAsync(user);
                user.IsAdmin = true;
                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminID6 = user.Id;
            }
        }
    }
}
