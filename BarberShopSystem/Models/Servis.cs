using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopSystem.Models
{
    public class Servis
    {
        public Servis()
        {
            // Se inicializa la lista ProfesionalServisler con una lista vacía
            ProfesyonelServisler = new List<ProfesyonelServis>();
        }
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public int Sure { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fiyat { get; set; }

        // Relaciones con otras tablas
        public List<ProfesyonelServis> ProfesyonelServisler { get; set; }
    }
}
