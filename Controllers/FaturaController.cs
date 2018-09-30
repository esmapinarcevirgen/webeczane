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
    public class FaturaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet(Fatura item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    if (item.Id == 0)
                        dbConnection.Execute("insert into public.fatura (tutar, depoid,tarih ) values(@tutar, @depoid,@tarih)", item);
                    else
                        dbConnection.Execute("update public.fatura set tutar = @tutar, tarih = @tarih, depoid=@depoid where Id = @Id", item);

                }

                return Json(new { basarili = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult Sil(Fatura item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    dbConnection.Execute("delete from public.fatura where Id = @Id ", item);
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
                    liste = dbConnection.Query("select f.Id, f.tutar, f.tarih, f.depoid, d.adi depoadi from public.fatura f join public.depo d on f.depoid = d.id");
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
