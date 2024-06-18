using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IAitiseisService
    {
        void Create(StudentAitisiViewModel data, int studentId, int schoolId);
        void Destroy(StudentAitisiViewModel data);
        StudentAitisiViewModel GetRecord(int aitisiId);
        IEnumerable<StudentAitisiViewModel> Read(int studentId);
        StudentAitisiViewModel Refresh(int entityId);
        void Update(StudentAitisiViewModel data, int studentId, int schoolId);
    }
}