using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IGradesService
    {
        void Create(StudentGradesViewModel data, int tmimaId, int lessonId, int schoolId);
        void Destroy(StudentGradesViewModel data);
        IEnumerable<StudentGradesViewModel> Read(int tmimaId, int lessonId);
        void Update(StudentGradesViewModel data, int tmimaId, int lessonId, int schoolId);
    }
}