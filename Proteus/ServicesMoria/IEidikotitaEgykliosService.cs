using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.ServicesMoria
{
    public interface IEidikotitaEgykliosService
    {
        void Create(XmEgykliosEidikotitesViewModel data, int egykliosId, int schoolId);
        void Destroy(XmEgykliosEidikotitesViewModel data);
        IEnumerable<XmEgykliosEidikotitesViewModel> Read(int egykliosId, int schoolId);
        void Update(XmEgykliosEidikotitesViewModel data, int egykliosId, int schoolId);
    }
}