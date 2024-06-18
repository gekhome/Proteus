using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IEidikotitesKatartisiService
    {
        void Create(SYS_EIDIKOTITES_IEKViewModel data);
        void Destroy(SYS_EIDIKOTITES_IEKViewModel data);
        IEnumerable<SYS_EIDIKOTITES_IEKViewModel> Read();
        SYS_EIDIKOTITES_IEKViewModel Refresh(int entityId);
        void Update(SYS_EIDIKOTITES_IEKViewModel data);
    }
}