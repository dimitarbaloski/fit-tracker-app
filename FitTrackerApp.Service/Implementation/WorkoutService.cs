using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Repository.Implementation;
using FitTrackerApp.Repository.Interface;
using FitTrackerApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Implementation
{
    public class WorkoutService : IWorkoutService
    {

        private readonly IRepository<Workout> _workoutRepository;

        public WorkoutService(IRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<Workout> DeleteById(Guid id)
        {
            var workout = await _workoutRepository.GetAsync(selector: x => x,
                predicate: x => x.Id == id);

            if (workout == null)
            {
                throw new Exception($"Workout with id {id} not found!");
            }

            return await _workoutRepository.DeleteAsync(workout);
        }

        public async Task<List<Workout>> GetAll()
        {
            var workouts = await _workoutRepository.GetAllAsync(selector: x => x);

            return workouts.ToList();
        }

        public async Task<IEnumerable<Workout>> GetByDateRange(DateTime from, DateTime to)
        {
            var workoutByDate = await _workoutRepository.GetAllAsync(selector: x => x,
                predicate: x => x.Date >=  from && x.Date <= to);

            return workoutByDate;
        }

        public async Task<Workout?> GetById(Guid id)
        {
            var workout = await _workoutRepository.GetAsync(selector: x => x,
                predicate: x => x.Id == id);

            if (workout == null)
            {
                throw new Exception($"Workout with id {id} not found!");
            }

            return workout;
        }

        public async Task<IEnumerable<Workout>> GetByUserIdAsync(string userId)
        {
            var workouts = await _workoutRepository.GetAllAsync(
                selector: x => x,
                predicate: m => m.UserId == userId
            );

            return workouts.ToList();
        }

        public async Task<Workout> Insert(Workout workout)
        {
            return await _workoutRepository.InsertAsync(workout);
        }

        public async Task<ICollection<Workout>> InsertMany(ICollection<Workout> workouts)
        {
            return await _workoutRepository.InsertManyAsync(workouts);
        }


        public async Task<Workout> Update(Workout workout)
        {
            return await _workoutRepository.UpdateAsync(workout);
        }

        
    }
}
