using System;

namespace webeczane.DbTables
{
    public class Ilac
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public float Fiyat { get; set; }
        public int KategoriId { get; set; }
        public int FaturaId { get; set; }
        public string Firma { get; set; }
      
    }
}