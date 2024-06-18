using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IApoxorisiAitiaService
    {
        void Create(ApoxorisiAitiaViewModel data);
        void Destroy(ApoxorisiAitiaViewModel data);
        IEnumerable<ApoxorisiAitiaViewModel> Read();
        ApoxorisiAitiaViewModel Refresh(int entityId);
        void Update(ApoxorisiAitiaViewModel data);
    }
}