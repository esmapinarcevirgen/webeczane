﻿using System;
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

namespace webeczane.Controllers
{
    public class DepoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet(Depo item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    if (item.Id == 0)
                        dbConnection.Execute("insert into public.depo (adi, adres, telefon) values(@adi, @adres, @telefon)", item);
                    else
                        dbConnection.Execute("update public.depo set adi = @adi, adres = @adres , telefon=@telefon where Id = @Id", item);

                }

                return Json(new { basarili = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult Sil(Depo item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    dbConnection.Execute("delete from public.depo where Id = @Id ", item);
                }

                return Json(new { basarili = true });
            }
            catch (System.Exception)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult listesi()
        {
            try
            {
                dynamic liste;
                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    liste = dbConnection.Query("select * from public.depo");
                }

                return Json(new { basarili = true, liste });
            }
            catch (System.Exception)
            {
                return Json(new { basarisiz = true });

            }

        }

         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
