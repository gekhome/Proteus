using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IEidikotitaService
    {
        void Create(EidikotitesViewModel data);
        void Destroy(EidikotitesViewModel data);
        IEnumerable<EidikotitesViewModel> Read();
        EidikotitesViewModel Refresh(int entityId);
        void Update(EidikotitesViewModel data);
    }
}