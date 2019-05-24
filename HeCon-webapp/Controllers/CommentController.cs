using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User,Doctor,Administrator")]
        [HttpGet]
        public ActionResult New(int id)
        {
            var newComment = new Comment();
            newComment.ImageId = id;
            newComment.UserId = User.Identity.GetUserId();
            return View("AddComment", newComment);
        }

        [HttpPost]
        public ActionResult New(Comment newComment)
        {

            newComment.UserId = User.Identity.GetUserId();
            db.Comment.Add(newComment);
            db.SaveChanges();

            if (User.IsInRole("User"))
            {
                return RedirectToAction("Show", "XRay");
            }
            return RedirectToAction("ShowPatients", "Users", new { id = newComment.ImageId });
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Comment comment1 = db.Comment.Find(id);

            if (comment1.UserId != User.Identity.GetUserId() && !User.IsInRole("Administrator"))
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un comentariu care nu va apartine!";
                if (User.IsInRole("User"))
                {
                    return RedirectToAction("Show", "XRay");
                }
                return RedirectToAction("ShowPatients", "Users", new { id = comment1.ImageId });
            }

            else
            {
                db.Comment.Remove(comment1);
                db.SaveChanges();
                TempData["message"] = "Comment deleted!";
                if (User.IsInRole("User"))
                {
                    return RedirectToAction("Show", "XRay");
                }
                return RedirectToAction("ShowPatients", "Users", new { id = comment1.ImageId });
            }

        }

        public ActionResult Edit(int id)
        {

            Comment comment1 = db.Comment.Find(id);
            ViewBag.Comment = comment1;

            if (comment1.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View(comment1);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui articol care nu va apartine!";
                if (User.IsInRole("User"))
                {
                    return RedirectToAction("Show", "XRay");
                }
                return RedirectToAction("ShowPatients", "Users", new { id = comment1.ImageId });
            }
        }


        [HttpPut]
        public ActionResult Edit(int id, Comment requestArticle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Comment comment1 = db.Comment.Find(id);

                    if (comment1.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(comment1))
                        {
                            comment1.Text = requestArticle.Text;
                            comment1.LastEdit = DateTime.Now;
                            db.SaveChanges();
                            TempData["message"] = "Comment modified!!";
                        }
                        if (User.IsInRole("User"))
                        {
                            return RedirectToAction("Show", "XRay");
                        }
                        return RedirectToAction("ShowPatients", "Users", new { id = comment1.ImageId });
                    }
                    else
                    {
                        TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui comentariu care nu va apartine!";
                        if (User.IsInRole("User"))
                        {
                            return RedirectToAction("Show", "XRay");
                        }
                        return RedirectToAction("ShowPatients", "Users", new { id = comment1.ImageId });
                    }


                }
                else
                {
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}