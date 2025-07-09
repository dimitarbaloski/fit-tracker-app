using FitTrackerApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Interface
{
    public interface IWorkoutService
    {
        Task<List<Workout>> GetAll();
        Task<Workout?> GetById(Guid id);
        Task<Workout> Insert(Workout workout);
        Task<ICollection<Workout>> InsertMany(ICollection<Workout> workouts);
        Task<Workout> Update(Workout workout);
        Task<Workout> DeleteById(Guid id);
        Task<IEnumerable<Workout>> GetByUserIdAsync(string userId);

        Task<IEnumerable<Workout>> GetByDateRange(DateTime from, DateTime to);
    }
}
