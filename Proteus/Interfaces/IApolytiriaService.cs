using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IApolytiriaService
    {
        void Create(ApolytiriaViewModel data);
        void Destroy(ApolytiriaViewModel data);
        IEnumerable<ApolytiriaViewModel> Read();
        ApolytiriaViewModel Refresh(int entityId);
        void Update(ApolytiriaViewModel data);
    }
}