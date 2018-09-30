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
    public class ReceteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet(Recete item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    if (item.Id == 0)
                        dbConnection.Execute("insert into public.recete (turid, hastaneid,doktorid,musteriid,tutar,tarih ) values(@turid, @hastaneid,@doktorid,@musteriid,@tutar,@tarih)", item);
                    else
                        dbConnection.Execute("update public.recete set turid = @turid, hastaneid = @hastaneid, doktorid =@doktorid,musteriid = @musteriid,tutar=@tutarid,tarih=@tarihid  where Id = @Id", item);

                }

                return Json(new { basarili = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult Sil(Recete item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    dbConnection.Execute("delete from public.receteilaclari where Id = @Id ", item);
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
                    liste = dbConnection.Query(@"select r.Id,r.turid, r.hastaneid, r.doktorid, r.musteriid,r.tutar,r.tarih, rt.adi ReceteTuruadi,d.adi doktorid
                     from public.recete r
                     join public.receteturu rt on r.turid = rt.id
                     join public.hastane h on r.hastaneid = h.id
                     join public.musteri m on r.musteriid = m.tckimlikno
                     join public.doktor d on r.doktorid = d.id");
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
