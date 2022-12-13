using ErrorOr;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Authentication;
using VendorManagement.Contracts.Common;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.Migrations;

namespace VendorManagement.DBclient.Models
{
    public class User : BaseEntity
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public static ErrorOr<LoginRequest> From(LoginRequest loginRequest)
        {
            List<Error> errors = ValidateLogin(loginRequest.Username, loginRequest.Password);

            if (errors.Count > 0)
                return errors;
            return loginRequest;

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
                errors.Add(Errors.Register.EmptyLastName);
            }
            if (string.IsNullOrEmpty(registerRequest.email))
            {
                errors.Add(Errors.Register.EmptyEmail);
            }
            if (string.IsNullOrEmpty(registerRequest.mobileNumber))
            {
                errors.Add(Errors.Register.EmptyMobileNumber);
            }
            return errors;
        }
    }
}
