using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";
            return View();
        }


        [HttpPost]
        public ActionResult auth_user(FormCollection form)
        {
            string email = form["email"];
            string pass = form["pass"];

            DBEntities db = new DBEntities();
            user user = db.users.FirstOrDefault(m => m.email == email && m.password == pass);
            if (user != null)
            {
                Session["fullname"] = user.fullname;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Email or password incorrect";
                return RedirectToAction("Login");
            }

            //return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult auth_register(FormCollection form)
        {
            string email = form["email"];

            DBEntities db = new DBEntities();
            user user = db.users.FirstOrDefault(m => m.email == email);
            if(user == null)
            {

                string password = form["pass"];
                string repassword = form["repass"];
                if(password == repassword)
                {
                    string fullname = form["fullname"];
                    string phonenumber = form["phonenumber"];

                    user newUser = new user();
                    newUser.email = email;
                    newUser.fullname = fullname;
                    newUser.phone_number = phonenumber;
                    newUser.password = password;

                    db.users.Add(newUser);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "password does not match";
                    return RedirectToAction("Register");
                }
            }
            else
            {
                TempData["Error"] = "Email already exists";
                return RedirectToAction("Register");
            }

        }


        public ActionResult Logout()
        {
            // Xóa session và đăng xuất người dùng
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login");
        }
    }
}