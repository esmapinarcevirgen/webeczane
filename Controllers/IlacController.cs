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
    public class IlacController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet(Ilac item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    if (item.Id == 0)
                        dbConnection.Execute("insert into public.ilac (adi, kategoriid,faturaid,firma,fiyat ) values(@adi, @kategoriid,@faturaid,@firma,@fiyat)", item);
                    else
                        dbConnection.Execute("update public.ilac set adi = @adi, kategoriid = @kategoriid, faturaid=@faturaid,firma = @firma,fiyat=@fiyat  where Id = @Id", item);

                }

                return Json(new { basarili = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult Sil(Ilac item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    dbConnection.Execute("delete from public.ilac where Id = @Id ", item);
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
                    liste = dbConnection.Query(@"select i.Id,i.adi, i.fiyat, i.firma, i.kategoriid,i.faturaid, k.adi kategoriadi,f.tutar faturaid
                     from public.ilac i
                     join public.kategori k on i.kategoriid = k.id
                     join public.fatura f on i.faturaid = f.id");
                }

                return Json(new { basarili = true, liste });
            }
            catch (System.Exception ex)
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
