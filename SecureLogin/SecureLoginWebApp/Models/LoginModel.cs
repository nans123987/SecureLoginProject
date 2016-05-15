using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SecureLoginWebApp.SecureLoginService;

namespace SecureLoginWebApp.Models
{
    public class LoginModel
    {
       
        public string login_username { get; set; }
        public string login_password { get; set; }
        public string DOB { get; set; }
        public string emailID { get; set;}
        public static string viewMessage { get; set; }

        public bool LoginUser(string username, string password) {
            bool success = false;
            using (SecureLoginServiceClient client = new SecureLoginServiceClient()) {
                success = client.LoginValidation(username, password);   

            }
            return success;
        }


        public bool findUser(string username) {
            bool found = false;
            using (SecureLoginServiceClient client = new SecureLoginServiceClient()) {
                found = client.checkIfUserExists(username);
            }
            return found;
        }

        public bool checkifAccountLocked(string username) {
            bool accountLocked = false;
            using (SecureLoginServiceClient client = new SecureLoginServiceClient()) {
                accountLocked = client.checkIfAccountLocked(username);
            }
            return accountLocked;
        }

        public bool registerNewUser(string Username, string Password, string emailId, string DateofBirth) {
            bool userRegistered = false;
            using (SecureLoginServiceClient client = new SecureLoginServiceClient()) {
                userRegistered = client.RegisterUser(Username, Password, emailId, DateofBirth);
            }

            return userRegistered;
        }
    }
}