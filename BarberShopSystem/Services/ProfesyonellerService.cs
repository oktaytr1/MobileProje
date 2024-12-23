using BarberShopSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BarberShopSystem.Services
{
    // (Inyección de dependencias: SOLID) Este servis se encarga de obtener la lista de Profesyoneller desde la base de datos o cualquier otra fuente de datos.

    public class ProfesyonellerService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfesyonellerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Profesyonel> ObtenerListaDeProfesyoneller()
        {
            return _dbContext.Profesyoneller.ToList();
        }

        public void AgregarProfesional(Profesyonel nuevoProfesional)
        {
            _dbContext.Profesyoneller.Add(nuevoProfesional);
            _dbContext.SaveChanges();
        }
    }
}

