using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts;
using VendorManagement.Contracts.Authentication;
using VendorManagement.Contracts.Common;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.Migrations;

namespace VendorManagement.DBclient.Models
{
    [Index(nameof(email), IsUnique = true)]
    public class User : BaseEntity
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public List<Partner> Partners { get; set; }

        public List<Role> Roles { get; set; }


        public static ErrorOr<LoginRequest> From(LoginRequest loginRequest)
        {
            List<Error> errors = ValidateLogin(loginRequest.Username, loginRequest.Password);

            if (errors.Count > 0)
                return errors;
            return loginRequest;

        }
        public static ErrorOr<User> From(Guid Id, UserRequest userRequest)
        {
            return Update(userRequest);
        }
        private static ErrorOr<User> Update(UserRequest userRequest)
        {
            List<Error> errors = Validate(userRequest);

            if (errors.Count > 0)
                return errors;

            User user = new User();
            user.firstName = userRequest.firstName;
            user.lastName = userRequest.lastName;
            user.email = userRequest.email;
            user.MobileNumber = userRequest.MobileNumber;
            user.Address = userRequest.Address;
            user.LastModifiedDate = DateTime.UtcNow;
            user.LastModifiedBy = "System";
            return user;
        }
        public static ErrorOr<User> From(RegisterRequest registerRequest)
        {
            List<Error> errors = ValidateRegisterUser(registerRequest);

            if (errors.Count > 0)
                return errors;

            User user = new User();
            user.firstName = registerRequest.firstName; 
            user.lastName = registerRequest.lastName; 
            user.email = registerRequest.email;
            user.password = HashPassword.GetPasswordHash(registerRequest.password);
            user.MobileNumber= registerRequest.mobileNumber;
            user.Address = registerRequest.address;
            user.CreatedBy = "System";
            user.CreatedDate = DateTime.Now;
            return user;

        }
        private static List<Error> ValidateLogin(string username, string password)
        {
            List<Error> errors = new();
            if (string.IsNullOrEmpty(username))
            {
                errors.Add(Errors.Login.EmptyUserName);
            }
            if (string.IsNullOrEmpty(password))
            {
                errors.Add(Errors.Login.EmptyPassword);
            }
            return errors;
        }
        private static List<Error> ValidateRegisterUser(RegisterRequest registerRequest)
        {
            List<Error> errors = new();
            if (string.IsNullOrEmpty(registerRequest.firstName))
            {
                errors.Add(Errors.Register.EmptyFirstName);
            }
            if (string.IsNullOrEmpty(registerRequest.lastName))
            {
                errors.Add(Errors.Register.InvalidLastName);
            }
            if (string.IsNullOrEmpty(registerRequest.email))
            {
                errors.Add(Errors.Register.InvalidEmail);
            }
            if (string.IsNullOrEmpty(registerRequest.mobileNumber))
            {
                errors.Add(Errors.Register.InvalidMobileNumber);
            }
            if (string.IsNullOrEmpty(registerRequest.roleid))
            {
                errors.Add(Errors.Register.InvalidRole);
            }
            return errors;
        }

        private static List<Error> Validate(UserRequest userRequest)
        {
            List<Error> errors = new();
            if (string.IsNullOrEmpty(userRequest.firstName))
            {
                errors.Add(Errors.User.InvalidfirstName);
            }
            if (string.IsNullOrEmpty(userRequest.lastName))
            {
                errors.Add(Errors.User.InvalidlastName);
            }
            if (string.IsNullOrEmpty(userRequest.email))
            {
                errors.Add(Errors.User.InvalidEmail);
            }
            
            return errors;
        }
    }
}
