using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using webeczane.EczaneContext;
using webeczane.Models;
using Dapper;
using webeczane.DbTables;
using Microsoft.AspNetCore.Http;

namespace webeczane.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kontrol(Eczaci item)
        {
            try
            {
                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    item = dbConnection.QueryFirst<Eczaci>("select * from public.eczaci where tc = @tc and sifre = @sifre", item);

                    HttpContext.Session.SetInt32("Id", item.Id);
                    HttpContext.Session.SetString("Adi", item.Adi);
                }


                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception ex)
            {

                HttpContext.Session.SetString("LoginError", "true");
                return RedirectToAction("Index");

            }

        }

    }
}
