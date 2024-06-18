using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IBekService
    {
        void Create(StudentBekViewModel data, int studentId, int schoolId);
        void Destroy(StudentBekViewModel data);
        IEnumerable<StudentBekViewModel> Read(int studentId);
        IEnumerable<StudentBekViewModel> ReadAll(int schoolId);
        StudentBekViewModel Refresh(int entityId);
        void Update(StudentBekViewModel data, int studentId, int schoolId);
        void UpdateRecord(StudentBekViewModel data, int bekId, int schoolId);
    }
}