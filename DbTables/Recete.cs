using System;

namespace webeczane.DbTables
{
    public class Recete
    {
        public int Id { get; set; }
        public int TurId{ get; set; }
        public int HastaneId { get; set; }
        public int DoktorId { get; set; }
        public int MusteriId { get; set; }
        public float Tutar { get; set; }
        public DateTime Tarih { get; set; }
      
    }
}