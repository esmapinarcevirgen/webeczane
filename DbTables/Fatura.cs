using System;

namespace webeczane.DbTables
{
    public class Fatura
    {
        public int Id { get; set; }
        public float Tutar { get; set; }
        public DateTime Tarih { get; set; }
        public int DepoId { get; set; }
      
    }
}