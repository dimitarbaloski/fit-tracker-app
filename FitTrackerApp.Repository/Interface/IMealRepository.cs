using FitTrackerApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Repository.Interface
{
    public interface IMealRepository
    {
        public Meal? GetById(Guid? id);
    }
}
