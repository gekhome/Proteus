using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiService
    {
        void Create(ErgodotesPraktikiViewModel data, int ergodotisId, int schoolId);
        void Destroy(ErgodotesPraktikiViewModel data);
        PraktikiInfoViewModel GetInfo(int praktikiId);
        StudentInPraktikiViewModel GetStudent(int ergodotisId, int studentId, int tmimaId);
        IEnumerable<ErgodotesPraktikiViewModel> Read(int ergodotisId, int schoolId);
        IEnumerable<PraktikiInfoViewModel> ReadInfo(int ergodotisId);
        IEnumerable<StudentInPraktikiViewModel> ReadStudents(int schoolId);
        ErgodotesPraktikiViewModel Refresh(int entityId);
        void Update(ErgodotesPraktikiViewModel data, int ergodotisId, int schoolId);
    }
}