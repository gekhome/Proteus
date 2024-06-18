using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IErgodotesService
    {
        void Create(ErgodotesViewModel data, int schoolId);
        void Destroy(ErgodotesViewModel data);
        ErgodotesViewModel GetRecord(int ergodotisId);
        IEnumerable<ErgodotesViewModel> Read(int schoolId);
        ErgodotesViewModel Refresh(int entityId);
        void Update(ErgodotesViewModel data, int schoolId);
        void UpdateRecord(ErgodotesViewModel data, int ergodotisId, int schoolId = 0);
    }
}