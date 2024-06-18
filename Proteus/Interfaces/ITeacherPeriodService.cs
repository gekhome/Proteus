using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ITeacherPeriodService
    {
        void Create(TeacherPeriodsViewModel data, int teacherId, int schoolId);
        void Destroy(TeacherPeriodsViewModel data);
        IEnumerable<TeacherPeriodsViewModel> Read(int teacherId);
        TeacherPeriodsViewModel Refresh(int entityId);
        void Update(TeacherPeriodsViewModel data, int teacherId, int schoolId);
    }
}