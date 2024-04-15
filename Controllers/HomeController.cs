using WebApplication0628.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebApplication0628.Controllers
{
    public class HomeController : Controller
    {
        MyDataBase db = new MyDataBase();
        public ActionResult Index()
        {
            List<City> cityList = db.GetCityList();
            List<Village> villageList = new List<Village>();

            ViewBag.CityList = cityList;
            ViewBag.VillageList = villageList;
            return View(new UserData());
        }

        [HttpPost]
        public ActionResult Index(UserData data)
        {
            if (string.IsNullOrWhiteSpace(data.password1) || data.password1 != data.password2)
            {
                List<City> cityList = db.GetCityList();
                List<Village> villageList = new List<Village>();
                if (!string.IsNullOrWhiteSpace(data.city))
                    villageList = db.GetVillageList(data.city);
                ViewBag.CityList = cityList;
                ViewBag.VillageList = villageList;
                ViewBag.Msg = "密碼輸入錯誤";
                return View(data);
            }
            else
            {
                if (db.AddUserData(data))
                {
                    Response.Redirect("~/Home/Login");
                    return new EmptyResult();
                }
                else
                {
                    ViewBag.Msg = "註冊失敗...";
                    return View(data);
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection post)
        {
            string account = post["account"];
            string password = post["password"];

            //驗證帳號密碼
            if (db.CheckUserData(account, password))
            {
                Session["account"] = account;

                Response.Redirect("~/Home/Home");
                return new EmptyResult();
            }
            else
            {
                ViewBag.Msg = "登入失敗...";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Village(string id = "")
        {
            List<Village> list = db.GetVillageList(id);
            string result = "";
            if (list == null)
            {
                //讀取資料庫錯誤
                return Json(result);
            }
            else
            {
                result = JsonConvert.SerializeObject(list);
                return Json(result);
            }
        }

        public ActionResult Home()
        {
            return View();
        }

    }
}