using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.ServicesMoria
{
    public interface IEidikotitesService
    {
        void Create(XmEidikotitesViewModel data);
        void Destroy(XmEidikotitesViewModel data);
        List<XmEidikotitesViewModel> Read();
        XmEidikotitesViewModel Refresh(int entityId);
        void Update(XmEidikotitesViewModel data);
    }
}