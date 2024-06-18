using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IBebeosiService
    {
        void Create(StudentBebeoseisViewModel data, int studentId, int schoolId);
        void Destroy(StudentBebeoseisViewModel data);
        StudentBebeoseisViewModel GetRecord(int bebeosiId);
        IEnumerable<StudentBebeoseisViewModel> Read(int studentId);
        StudentBebeoseisViewModel Refresh(int entityId);
        void Update(StudentBebeoseisViewModel data, int studentId, int schoolId);
    }
}