using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.ServicesMoria
{
    public interface IExperienceService
    {
        void Create(XmExperienceViewModel data, int aitisiId, int egykliosId);
        void Destroy(XmExperienceViewModel data);
        IEnumerable<XmExperienceViewModel> Read(int aitisiId);
        XmExperienceViewModel Refresh(int entityId);
        void Update(XmExperienceViewModel data, int aitisiId, int egykliosId);
    }
}