using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IEidikotitesIekService
    {
        void Create(IekEidikotitesViewModel data);
        void Create(IekEidikotitesViewModel data, int schoolId);
        void Destroy(IekEidikotitesViewModel data);
        IEnumerable<IekEidikotitesViewModel> Read();
        IEnumerable<IekEidikotitesViewModel> Read(int schoolId);
        IekEidikotitesViewModel Refresh(int entityId);
        void Update(IekEidikotitesViewModel data);
        void Update(IekEidikotitesViewModel data, int schoolId);
    }
}