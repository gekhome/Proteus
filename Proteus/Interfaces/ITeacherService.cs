using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ITeacherService
    {
        void Create(TeacherGridViewModel data, int schoolId);
        void Destroy(TeacherGridViewModel data);
        TeacherViewModel GetRecord(int teacherId);
        IEnumerable<TeacherGridViewModel> Read(int schoolId);
        TeacherGridViewModel Refresh(int entityId);
        void Update(TeacherGridViewModel data, int schoolId);
        void UpdateRecord(TeacherViewModel data, int teacherId, int schoolId = 0);
    }
}