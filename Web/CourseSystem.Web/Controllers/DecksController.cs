namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class DecksController : Controller
    {
        public IActionResult MyDecks()
        {
            return this.View("MyDecks");
        }
    }
}
