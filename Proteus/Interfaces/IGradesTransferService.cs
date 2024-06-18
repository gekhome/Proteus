using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IGradesTransferService
    {
        void Create(StudentGradesViewModel data, int tmimaId, int studentId, int schoolId);
        void Destroy(StudentGradesViewModel data);
        IEnumerable<StudentGradesViewModel> Read(int tmimaId, int studentId);
        void Update(StudentGradesViewModel data, int tmimaId, int studentId, int schoolId);
    }
}