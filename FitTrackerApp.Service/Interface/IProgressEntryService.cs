using FitTrackerApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Interface
{
    public interface IProgressEntryService
    {
        Task<List<ProgressEntry>> GetAll();
        Task<ProgressEntry?> GetById(Guid id);
        Task<ProgressEntry> Insert(ProgressEntry progressEntry);
        Task<ICollection<ProgressEntry>> InsertMany(ICollection<ProgressEntry> progressEntries);
        Task<ProgressEntry> Update(ProgressEntry progressEntry);
        Task<ProgressEntry> DeleteById(Guid id);

        Task<IEnumerable<ProgressEntry>> GetByUserIdAsync(string userId);
    }
}
