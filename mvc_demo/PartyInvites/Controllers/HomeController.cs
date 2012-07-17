using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyInvites.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        // 최초 생성되는 것
        //public ActionResult Index()
        //{
        //    ViewData["Message"] = "ASP.NET MVC 시작";
        //    return View();            
        //}

        // p41 스트링 출력
        //public string Index()
        //{
        //    return "Hello, World!";
        //}


        /*
         * p43 뷰 추가
         * 액션결과 (Action Results) : ActionResult, ResultRedirect(재전송), 
         *                          HttpUnauthorizedResult(방문자로그인을 하도록 유도) 등
        public ViewResult Index()
        {
            return View();
        }
         * */
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewData["Greeting"] = (hour<12 ? "Good morning" : "Good afternoon");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
