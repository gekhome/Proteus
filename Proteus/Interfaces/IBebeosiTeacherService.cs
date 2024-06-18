using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IBebeosiTeacherService
    {
        void Create(TeacherBebeoseisViewModel data, int teacherId, int schoolId);
        void Destroy(TeacherBebeoseisViewModel data);
        IEnumerable<TeacherBebeoseisViewModel> Read(int teacherId);
        List<TeacherBebeoseisViewModel> ReadArchive(int teacherId);
        TeacherBebeoseisViewModel Refresh(int entityId);
        void Update(TeacherBebeoseisViewModel data, int teacherId, int schoolId);
    }
}