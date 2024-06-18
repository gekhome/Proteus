using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ITeacherAitisiService
    {
        void Create(TeacherAitiseisViewModel data, int teacherId, int schoolId);
        void Destroy(TeacherAitiseisViewModel data);
        List<TeacherAitiseisViewModel> Read(int teacherId);
        TeacherAitiseisViewModel Refresh(int entityId);
        void Update(TeacherAitiseisViewModel data, int teacherId, int schoolId);
    }
}