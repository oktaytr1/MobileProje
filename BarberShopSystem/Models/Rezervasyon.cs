using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopSystem.Models
{
    public class Rezervasyon
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }

        // Relaciones con otras tablas
        [Display(Name = "Profesyonel")]
        public int ProfesyonelId { get; set; }
        public Profesyonel? Profesyonel { get; set; }
        /*[Display(Name = "Cupo")]
        public int CupoId { get; set; }
        public Cupo? Cupo { get; set; }*/
        [Display(Name = "Servis")]
        public int ServisId { get; set; }
        public Servis? Servis { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Otros campos
        public bool RezervasyonDurumu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime TarihGuncelleme { get; set; }
        public string? Yenilik { get; set; }
    }
}
