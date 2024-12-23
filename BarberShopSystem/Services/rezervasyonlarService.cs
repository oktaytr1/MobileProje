using BarberShopSystem.Models;

namespace BarberShopSystem.Services
{
    // (Inyección de dependencias: SOLID) Este servis se encarga de obtener la lista de rezervasyonlar desde la base de datos o cualquier otra fuente de datos.
    public class rezervasyonlarService
    {
        // Método para obtener la lista de rezervasyonlar
        public List<Rezervasyon> ObtenerListaDeRezervasyonlar()
        {
            // Simulación de obtención de datos (actualmente no se extrae info de la BD)
            List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>
            {
                new Rezervasyon { Id = 1, Tarih = DateTime.Now, ProfesyonelId = 1 },
                new Rezervasyon { Id = 2, Tarih = DateTime.Now.AddDays(1), ProfesyonelId = 2 },
            };

            return rezervasyonlar;
        }
    }
}
