using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopSystem.Models
{
    public class Profesyonel
    {
        public Profesyonel()
        {
            // Inicializa la lista en el constructor para evitar la advertencia CS8618
            ServislerSeleccionados = new List<int>();
        }
        public int Id { get; set; }
        public string? İsim { get; set; }
        public string? Posta { get; set; }
        public string? Telefon  { get; set; }

        // Relación con otras tablas
        public List<Rezervasyon>? Rezervasyonlar { get; set; }
        /*public List<Cupo>? Cupos { get; set; }*/
        public List<ProfesyonelServis>? ProfesyonelServisler { get; set; }

        [Display(Name = "Servisler")]
        [NotMapped] // Esta propiedad no se mapeará a la base de datos
        public List<int> ServislerSeleccionados { get; set; }
    }
}
