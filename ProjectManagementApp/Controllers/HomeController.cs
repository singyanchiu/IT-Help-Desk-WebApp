using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
//using ProjectManagementApp.Models;
using DataLibraryNew2.BusinessLogic;
using DataLibraryNew2.Models;
using ProjectManagementApp.ViewModels;


namespace ProjectManagementApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Redirect to admin dashboard if it's admin
                if (UserProcessor.GetRoleIdByUserId(User.Identity.GetUserId()) == "2")
                {
                    return RedirectToAction("AdminDashboard", "Home");
                }

                var requests = RequestProcessor.LoadRequests(User.Identity.GetUserId());
                var requestView = new RequestViewModel
                {
                    Requests = requests
                };
                return View(requestView);
            }
                else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult AdminDashboard()
        {
            //Check admin status, to prevent other users get to this method by typing url
            if (UserProcessor.GetRoleIdByUserId(User.Identity.GetUserId()) == "2")
            {
                var requests = RequestProcessor.LoadRequests(null);
                var requestView = new RequestViewModel
                {
                    Requests = requests
                };
                return View(requestView);
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult ViewRequest(int id)
        {
            Request request = RequestProcessor.LoadRequest(id);
            request.Statuses = RequestProcessor.GetStatusCollection();
            return View(request);
        }


        [Authorize]
        public ActionResult EditRequest(int id)
        {
            if (UserProcessor.GetRoleIdByUserId(User.Identity.GetUserId()) == "2")
            {
                Request request = RequestProcessor.LoadRequest(id);
                request.Statuses = RequestProcessor.GetStatusCollection();
                return View(request);
            } else
            {
                return RedirectToAction("ViewRequest", "Home", new { id=id });
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                int statusId = request.SelectedStatus;
                int requestId = request.Id;
                int records = RequestProcessor.EditRequest(request.Subject, request.Description, userId, statusId, requestId);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult CreateRequest()
        {
            ViewBag.Message = "Create Request Page";

            Request request = new Request
            {
                Statuses = RequestProcessor.GetStatusCollection()
            };
            return View(request);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                int statusId = request.SelectedStatus;
                int records = RequestProcessor.CreateRequest(request.Subject, request.Description, userId, statusId);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}