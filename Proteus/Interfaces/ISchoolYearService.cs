using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ISchoolYearService
    {
        void Create(SchoolYearsViewModel data);
        void Destroy(SchoolYearsViewModel data);
        List<SchoolYearsViewModel> Read();
        SchoolYearsViewModel Refresh(int entityId);
        void Update(SchoolYearsViewModel data);
    }
}