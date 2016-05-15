using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BCrypt.Net;
using System.Threading.Tasks;

namespace SecureLoginService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SecureLoginService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SecureLoginService.svc or SecureLoginService.svc.cs at the Solution Explorer and start debugging.
    public class SecureLoginService : ISecureLoginService
    {
        private string userid;
        private string username;
        private string hashed_pwd;
        private string dob;
        private string emailid;

        private static List<KeyValuePair<String, String>> roles = new Dictionary<String, String>
            {
                {"ROLE_USER", "1000"},
                {"ROLE_ADMIN", "1001"},
             }.ToList();

        private static Dictionary<string, int> loginAttemptsCounterRecords = new Dictionary<string, int>();

        private tbl_userinfo foundUser = new tbl_userinfo();

        private bool checkIfUserAlreadyExists(string username, string emailID) {
            bool existenceResult = true;
            
            using (SecureLoginDBEntities db = new SecureLoginDBEntities()) {
                try
                {

                    var tmpUser = db.tbl_userinfo.SingleOrDefault(x => x.Username == username);
                   if (tmpUser == null)
                   {
                       tmpUser = db.tbl_userinfo.SingleOrDefault(x => x.Email == emailID);
                       if (tmpUser == null)
                       {
                           existenceResult = false;
                       }
                   }
                   else
                       existenceResult = true;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return existenceResult;


        }

        //checks whether a username exists in database
        private tbl_userinfo findUser(string Username)
        {
            tbl_userinfo user = new tbl_userinfo();
            using (SecureLoginDBEntities db = new SecureLoginDBEntities()) {
                try
                {
                    user = db.tbl_userinfo.Where(x => x.Username == Username).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return user;
        }

        //service method for user registration .
        public bool RegisterUser(string Username, string Password, string emailId, string DateofBirth)
        {
            tbl_userinfo user = new tbl_userinfo();
            bool success = false;

            using (SecureLoginDBEntities db = new SecureLoginDBEntities())
            {
                try
                {
                    
                    this.username = Username;
                    this.dob = DateofBirth;
                    this.emailid = emailId;
                    if(checkIfUserAlreadyExists(this.username, this.emailid) == false){
                        //Generate Unique GUID for each user record.
                        userid = Guid.NewGuid().ToString();

                        //Generating Salt foor Pwd
                        string pwdSalt = BCrypt.Net.BCrypt.GenerateSalt();
                        //Hashing pwd
                        hashed_pwd = BCrypt.Net.BCrypt.HashPassword(Password, pwdSalt);

                        //Set up user record properties.
                        user.UserId = this.userid;
                        user.Username = this.username;
                        user.Password = this.hashed_pwd;
                        user.DOB = this.dob;
                        user.LockedStatus = false;
                        user.Role_id = roles[0].Value;
                        user.PwdHashSalt = pwdSalt;
                        user.Email = this.emailid;

                        //saving record into database.
                        db.tbl_userinfo.Add(user);
                        db.SaveChanges();
                        success = true;
                    }
              
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    success = false;
                }
            
            }
            return success;
        }

        //service method for login validation
        public bool LoginValidation(string Username, string Password)
        {
            int count = 0;
            bool success = false;
            string usrHashedPwd = string.Empty;

            using (SecureLoginDBEntities db = new SecureLoginDBEntities())
            {
                try
                {
                    foundUser = findUser(Username);
                    if (!foundUser.Equals(null))
                    {
                        if (foundUser.LockedStatus == true)
                        {
                            success = false;
                        }
                        else {
                            usrHashedPwd = foundUser.Password;
                            //Verify password 
                             success = BCrypt.Net.BCrypt.Verify(Password, usrHashedPwd);
                            if (!success == true)
                            {
                                if (!loginAttemptsCounterRecords.ContainsKey(Username))
                                {
                                    loginAttemptsCounterRecords.Add(Username, count + 1);
                                }
                                else
                                {
                                    if (loginAttemptsCounterRecords[Username] < 2)
                                    {
                                        loginAttemptsCounterRecords[Username]++;
                                    }
                                    else
                                    {
                                        var result = db.tbl_userinfo.SingleOrDefault(x => x.UserId == foundUser.UserId);
                                        if (result != null)
                                        {
                                            result.LockedStatus = true;
                                            db.SaveChanges();
                                        }
                                        loginAttemptsCounterRecords.Clear();
                                    }

                                }

                            }
                        }
                       

                    }
                    else
                        success = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    success = false;
                }
            }
            
            return success;
        }

        //Check if a username exists in the database
        public bool checkIfUserExists(string Username)
        {
            bool userExists = false;
            using (SecureLoginDBEntities db = new SecureLoginDBEntities()) {
                userExists = db.tbl_userinfo.Any(x => x.Username == Username);
            }
            return userExists;
        }


        public bool checkIfAccountLocked(string Username)
        {
            bool accountLocked = false;
            using(SecureLoginDBEntities db = new SecureLoginDBEntities()){
                var record = db.tbl_userinfo.SingleOrDefault(x => x.Username == Username);
                if(record != null){
                    if (record.LockedStatus == true)
                    {
                        accountLocked = true;
                    }
                }
               
            }
            return accountLocked;
            
        }
    }
}
