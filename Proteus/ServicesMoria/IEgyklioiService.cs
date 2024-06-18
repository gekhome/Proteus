using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.ServicesMoria
{
    public interface IEgyklioiService
    {
        void Create(XmEgykliosViewModel data);
        void Destroy(XmEgykliosViewModel data);
        List<XmEgykliosViewModel> Read();
        XmEgykliosViewModel Refresh(int entityId);
        void Update(XmEgykliosViewModel data);
    }
}