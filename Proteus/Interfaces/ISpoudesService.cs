using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ISpoudesService
    {
        void Create(SpoudesViewModel data);
        void Destroy(SpoudesViewModel data);
        IEnumerable<SpoudesViewModel> Read();
        SpoudesViewModel Refresh(int entityId);
        void Update(SpoudesViewModel data);
    }
}