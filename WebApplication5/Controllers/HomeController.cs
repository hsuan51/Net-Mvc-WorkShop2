using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public ActionResult Index(Models.bookSearchArgs args)
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getSearchBookData(args);
            return Json(result);
        }
        [HttpPost()]
        public ActionResult SearchBookClassName()
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getBookClassName();
            return Json(result);
        }
        [HttpPost()]
        public ActionResult SearchBookKeeperName()
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getBookKeeperName();
            return Json(result);
        }
        [HttpPost()]
        public ActionResult SearchBookStatusName()
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getBookStatusName();
            return Json(result);
        }
        [HttpPost()]
        public ActionResult DeleteBook()
        {
            return View();
        }
    }
}