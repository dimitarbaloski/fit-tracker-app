using FitTrackerApp.Data;
using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Repository.Implementation
{
    public class MealRepository : IMealRepository

    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Meal> _entities;

        public MealRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<Meal>();
        }

        public Meal? GetById(Guid? id)
        {
            return _entities.FirstOrDefault(x => x.Id == id.Value);
        }
    }
}
