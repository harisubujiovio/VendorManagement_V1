using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;
namespace VendorManagement.DBclient.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role : BaseEntity
    {
        public const int MinDescriptionLength = 4;
        public const int MaxDescriptionLength = 150;

        public const int MinNameLength = 4;
        public const int MaxNameLength = 50;

        public string Name { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; }

        private Role()
        {

        }
        public static ErrorOr<Role> From(RoleRequest roleRequest)
        {
            return Create(roleRequest);
        }
        public static ErrorOr<Role> From(Guid Id, RoleRequest roleRequest)
        {
            return Update(Id, roleRequest);
        }
        private static ErrorOr<Role> Create(RoleRequest roleRequest)
        {
            List<Error> errors = Validate(roleRequest);

            if (errors.Count > 0)
                return errors;

            Role role = new Role();
            role.Name = roleRequest.Name;
            role.Description = roleRequest.Description;
            role.CreatedDate = DateTime.UtcNow;
            role.CreatedBy = "System";
            return role;
        }
        private static ErrorOr<Role> Update(Guid Id, RoleRequest roleRequest)
        {
            List<Error> errors = Validate(roleRequest);

            if (errors.Count > 0)
                return errors;

            Role role = new Role();
            role.Name = roleRequest.Name;
            role.Description = roleRequest.Description;
            role.LastModifiedDate = DateTime.UtcNow;
            role.LastModifiedBy = "System";
            return role;
        }

        private static List<Error> Validate(RoleRequest roleRequest)
        {
            List<Error> errors = new();
            if (roleRequest.Name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(Errors.Role.InvalidName);
            }
            if (roleRequest.Description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.Role.InvalidDescription);
            }
            return errors;
        }
    }
}
