using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.ServicesMoria
{
    public interface IAitisiService
    {
        void Create(XmAitisiGridViewModel data, int egykliosId);
        void Create(XmAitisiGridViewModel data, int egykliosId, int schoolId);
        void Destroy(XmAitisiGridViewModel data);
        XmAitisiViewModel GetRecord(int aitisiID);
        IEnumerable<XmAitisiGridViewModel> Read(int egykliosId);
        IEnumerable<XmAitisiGridViewModel> Read(int egykliosId, int schoolId);
        XmAitisiGridViewModel Refresh(int entityId);
        void Update(XmAitisiGridViewModel data, int egykliosId);
        void Update(XmAitisiGridViewModel data, int egykliosId, int schoolId);
        void UpdateRecord(XmAitisiViewModel data, int aitisiID, int egykliosID);
    }
}