using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IAtomikoDeltioService
    {
        void Create(StudentAtomikoDeltioViewModel data, int studentId, int schoolId);
        void Destroy(StudentAtomikoDeltioViewModel data);
        List<AtomikoDeltioViewModel> GetGradesApousies(int studentId, int termId);
        List<adkStudentPraktikiViewModel> GetPraktikiData(int studentId);
        adkStudentDataViewModel GetStudentData(int studentId);
        IEnumerable<StudentAtomikoDeltioViewModel> Read(int studentId);
        StudentAtomikoDeltioViewModel Refresh(int entityId);
        void Update(StudentAtomikoDeltioViewModel data, int studentId, int schoolId);
    }
}