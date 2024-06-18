using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IAnatheseisService
    {
        void Create(TeacherAnatheseisViewModel data, int teacherId, int schoolId);
        void Destroy(TeacherAnatheseisViewModel data);
        IEnumerable<TeacherAnatheseisViewModel> Read(int teacherId);
        TeacherAnatheseisViewModel Refresh(int entityId);
        void Update(TeacherAnatheseisViewModel data, int teacherId, int schoolId);
        IEnumerable<QueryAnatheseisViewModel> View(int schoolId);
    }
}