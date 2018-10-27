using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01_ASP.Data {
    public class AppRole : IdentityRole {

        public AppRole() { }
        public AppRole(string roleName) : base() { }

        public AppRole(string roleName, string description, DateTime creationDate) : base(roleName) {
            base.Name = roleName;
            Description = description;
            CreationDate = creationDate;
        }

        public override string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
