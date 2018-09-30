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

namespace webeczane.Controllers
{
    public class DoktorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet(Doktor item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    if (item.Id == 0)
                        dbConnection.Execute("insert into public.doktor (adi, hastaneid,soyadi,tahsis) values(@adi, @hastaneid,@soyadi,@tahsis)", item);
                    else
                        dbConnection.Execute("update public.doktor set adi = @adi, soyadi = @soyadi, hastaneid=@hastaneid,tahsis=@tahsis where Id = @Id", item);

                }

                return Json(new { basarili = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult Sil(Doktor item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    dbConnection.Execute("delete from public.doktor where Id = @Id ", item);
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
                    liste = dbConnection.Query("select d.Id, d.Adi, d.soyadi, d.tahsis, d.hastaneid, h.adi hastaneadi from public.doktor d join public.hastane h on d.hastaneid = h.id");
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
