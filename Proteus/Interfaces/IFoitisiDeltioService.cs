using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IFoitisiDeltioService
    {
        void Create(StudentFoitisiDeltioViewModel data, int studentId, int schoolId);
        void Destroy(StudentFoitisiDeltioViewModel data);
        List<FoitisiDeltioViewModel> GetGradesApousies(int studentId, int termId);
        IEnumerable<StudentFoitisiDeltioViewModel> Read(int studentId);
        StudentFoitisiDeltioViewModel Refresh(int entityId);
        void Update(StudentFoitisiDeltioViewModel data, int studentId, int schoolId);
    }
}