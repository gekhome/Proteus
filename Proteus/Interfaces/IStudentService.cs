using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IStudentService
    {
        void Create(StudentGridViewModel data, int schoolId);
        void Destroy(StudentGridViewModel data);
        StudentViewModel GetRecord(int studentId);
        IEnumerable<StudentGridViewModel> Read(int schoolId);
        StudentGridViewModel Refresh(int entityId);
        void Update(StudentGridViewModel data, int schoolId);
        void UpdateRecord(StudentViewModel data, int studentId, int schoolId = 0);
    }
}