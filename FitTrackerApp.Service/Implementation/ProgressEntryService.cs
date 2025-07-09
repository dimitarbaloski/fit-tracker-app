using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Repository.Interface;
using FitTrackerApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Implementation
{
    public class ProgressEntryService : IProgressEntryService
    {

        private readonly IRepository<ProgressEntry> _progressEntryRepository;

        public ProgressEntryService(IRepository<ProgressEntry> progressEntryRepository)
        {
            _progressEntryRepository = progressEntryRepository;
        }

        public async Task<ProgressEntry> DeleteById(Guid id)
        {
            var progressEntry = await _progressEntryRepository.GetAsync(selector: x => x,
                predicate: x => x.Id == id);

            if (progressEntry == null) {

                throw new Exception($"Progress entry with id {id} not found!");
            }

            return await _progressEntryRepository.DeleteAsync(progressEntry);
        }

        public async Task<List<ProgressEntry>> GetAll()
        {
            var progressEntries = await _progressEntryRepository.GetAllAsync(selector: x => x);

            return progressEntries.ToList();
        }

        public async Task<ProgressEntry?> GetById(Guid id)
        {
            var progressEntry = await _progressEntryRepository.GetAsync(selector: x => x,
                predicate: x => x.Id == id);

            if (progressEntry == null)
            {

                throw new Exception($"Progress entry with id {id} not found!");
            }

            return progressEntry;
        }

        public async Task<IEnumerable<ProgressEntry>> GetByUserIdAsync(string userId)
        {
            var progressByUser = await _progressEntryRepository.GetAllAsync(selector: x => x,
                predicate: x => x.UserId == userId);

            return progressByUser;
        }

        public async Task<ProgressEntry> Insert(ProgressEntry progressEntry)
        {
            return await _progressEntryRepository.InsertAsync(progressEntry);
        }

        public async Task<ICollection<ProgressEntry>> InsertMany(ICollection<ProgressEntry> progressEntries)
        {
            return await _progressEntryRepository.InsertManyAsync(progressEntries);
        }

        public async Task<ProgressEntry> Update(ProgressEntry progressEntry)
        {
            return await _progressEntryRepository.UpdateAsync(progressEntry);
        }
    }
}
