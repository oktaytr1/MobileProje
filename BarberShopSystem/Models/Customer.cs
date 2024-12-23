namespace BarberShopSystem.Models
{
    public class Customer
    {
        public Customer()
        {
            // Se inicializa la lista Rezervasyonlar con una lista vacía
            Rezervasyonlar = new List<Rezervasyon>();
        }
        public int Id { get; set; }
        public string? İsim { get; set; }
        public string? Posta { get; set; }
        public string? Telefon  { get; set; }

        // Relación con otras tablas
        public List<Rezervasyon> Rezervasyonlar { get; set; }
    }
}
