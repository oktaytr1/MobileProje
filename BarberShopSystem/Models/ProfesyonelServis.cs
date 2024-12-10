namespace BarberShopSystem.Models
{
    public class ProfesyonelServis
    {
        // Clave primaria compuesta
        public int ProfesyonelId { get; set; }
        public int ServisId { get; set; }

        // Relaciones con otras tablas
        public Profesyonel? Profesyonel { get; set; }
        public Servis? Servis { get; set; }
    }
}
