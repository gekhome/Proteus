using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IEgrafesService
    {
        void Create(StudentEgrafesViewModel data, int studentId, int schoolId);
        void Destroy(StudentEgrafesViewModel data);
        IEnumerable<StudentEgrafesViewModel> Read(int studentId);
        StudentEgrafesViewModel Refresh(int entityId);
        void Update(StudentEgrafesViewModel data, int studentId, int schoolId);
    }
}