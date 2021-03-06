﻿namespace CourseSystem.Web.Controllers
{
    using System.Diagnostics;

    using CourseSystem.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.Redirect("/Administration/Dashboard");
            }
            else if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Courses/EnrolledCourses");
            }
            else
            {
                return this.View();
            }
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
              new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }//TODO: Show all courses to admin with delete buttons, user notifications?
    }
}
