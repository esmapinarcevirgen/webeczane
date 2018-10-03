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
    public class DirekSatisIlaclariController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet(DirekSatisIlaclari item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    if (item.Id == 0)
                        dbConnection.Execute("insert into public.direksatisilaclari (id, ilacid,adet ) values(@id, @ilacid,@adet)", item);
                    else
                        dbConnection.Execute("update public.direksatisilaclari set id = @id, ilacid= @ilacid, adet=@adet where Id = @Id", item);

                }

                return Json(new { basarili = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { basarisiz = true });

            }
        }

        public IActionResult Sil(DirekSatisIlaclari item)
        {
            try
            {

                using (var dbConnection = DatabaseBaglanti.Connection())
                {
                    dbConnection.Open();
                    dbConnection.Execute("delete from public.direksatisilaclari where Id = @Id ", item);
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
                    liste = dbConnection.Query("select dsi.Id, dsi.ilacid, dsi.adet from public.direksatisilaclari dsi join public.ilac i on dsi.ilacid = i.id");
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
