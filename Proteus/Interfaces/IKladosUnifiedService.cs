using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IKladosUnifiedService
    {
        void Create(KladosUnifiedViewModel data);
        void Destroy(KladosUnifiedViewModel data);
        IEnumerable<sqlEidikotitesKUViewModel> GetEidikotites(int kladosunifiedId);
        IEnumerable<KladosUnifiedViewModel> Read();
        KladosUnifiedViewModel Refresh(int entityId);
        sqlEidikotitesKUViewModel RefreshEidikotita(int entityId);
        void ResetEidikotita(sqlEidikotitesKUViewModel data, int kladosunifiedId);
        void SetEidikotita(sqlEidikotitesKUViewModel data, int kladosunifiedId);
        void Update(KladosUnifiedViewModel data);
    }
}