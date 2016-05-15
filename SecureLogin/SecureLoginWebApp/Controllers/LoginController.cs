using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecureLoginWebApp.Models;
using System.Web.Security;
using log4net;

namespace SecureLoginWebApp.Controllers
{
    public class LoginController : Controller
    {
        #region Log4net Logging
        private static ILog Log { get; set;  }

        ILog log = LogManager.GetLogger(typeof(LoginController));

        #endregion


        #region Login/Logout functionality methods
        //Login Model Object
        LoginModel client = new LoginModel();
        private static int loginAttemptsCounter = 0;

        public ActionResult Login()
        {
            Session["LogedInUser"] = null;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser(LoginModel user)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                log.Debug("User trying to login to Service");
                success = client.LoginUser(user.login_username, user.login_password);
                if (success == true)
                {
                    log.Debug("User Logged In Succesfully to the Login Service");
                    Session["LogedInUser"] = user.login_username;
                    return RedirectToAction("Welcome");
                }
                else
                {

                    bool check = client.findUser(user.login_username);
                    bool accountLockedStatus = client.checkifAccountLocked(user.login_username);
                   
                    if (check == true)
                    {
                        if (accountLockedStatus == true)
                        {
                            log.Fatal("User account locked due to unsuccessfull login attempts for three times");
                            LoginModel.viewMessage = "Your Account has been Locked!";

                        }
                        else if (loginAttemptsCounter < 2)
                        {
                            log.Error("Usernames or Password records not found in the User Repository");
                            LoginModel.viewMessage = "Username or Password Doesnt match our records";
                            loginAttemptsCounter++;
                        }
                        else
                        {
                            log.Fatal("User account locked due to unsuccessfull login attempts for three times");
                            LoginModel.viewMessage = "Your Account has been Locked!";
                            loginAttemptsCounter = 0;
                        }
                        
                        

                    }
                    else {
                        log.Error("Username Provided was not disocvered in the database for password Checks");
                        LoginModel.viewMessage = "Username or Password Doesnt match our records";
                    }
                   


                    return RedirectToAction("Login");
                }
            }

            return View();
        }


        [OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Welcome()
        {

            if (Session["LogedInUser"] != null)
            {
                log.Debug("User logged In! Redirecting to Homepage/Dashboard");
                return View();
            }
            else
            {
                log.Error("Session Expired due to inactivity of the User");
                LoginModel.viewMessage = "Your Session Has been Expired";
                return RedirectToAction("Login");
            }

        }


        public ActionResult Logout()
        {
            log.Debug("User Logout Activity initiated");
            FormsAuthentication.SignOut();
            LoginModel.viewMessage = "Logged out Successfully";
            return RedirectToAction("Login");
        }


        #endregion


        #region User registration functionality

        public ActionResult Register(LoginModel model)
        {

            return PartialView("_registerForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewUser(LoginModel newUser) {
            bool userRegistered;
            log.Debug("User account registration activity intitiated");
            userRegistered = client.registerNewUser(newUser.login_username, newUser.login_password, newUser.emailID, newUser.DOB);
            if (userRegistered) {
                log.Debug("User Account Registration completed");
                LoginModel.viewMessage = "Account Created Successfully";
                return RedirectToAction("Login");
            }

            return View();
        }

        
        #endregion

    }
}
