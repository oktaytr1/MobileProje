using BarberShopSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BarberShopSystem.Services
{
    public class ServislerService
    {
        private readonly ApplicationDbContext _dbContext;

        public ServislerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Servis> ObtenerListaDeServisler()
        {
            return _dbContext.Servisler.ToList();
        }

        public void AgregarServis(Servis nuevoServis)
        {
            _dbContext.Servisler.Add(nuevoServis);
            _dbContext.SaveChanges();
        }

        public Servis ObtenerServisPorId(int id)
        {
            return _dbContext.Servisler.FirstOrDefault(s => s.Id == id) ?? new Servis();
        }
    }
}