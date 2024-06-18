using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiAitisiService
    {
        void Create(PraktikiAitisiViewModel data, int ergodotisId, int studentId, int tmimaId, int schoolId);
        void Destroy(PraktikiAitisiViewModel data);
        AitiseisPraktikisViewModel GetInfo(int aitisiId);
        IEnumerable<PraktikiAitisiViewModel> Read(int ergodotisId, int studentId, int tmimaId);
        IEnumerable<AitiseisPraktikisViewModel> ReadInfo(int schoolId);
        PraktikiAitisiViewModel Refresh(int entityId);
        void Update(PraktikiAitisiViewModel data, int ergodotisId, int studentId, int tmimaId, int schoolId);
    }
}