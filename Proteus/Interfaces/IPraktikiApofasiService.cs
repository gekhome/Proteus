using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiApofasiService
    {
        void Create(PraktikiApofasiViewModel data, AitiseisPraktikisViewModel source);
        void Destroy(PraktikiApofasiViewModel data);
        IEnumerable<PraktikiApofasiViewModel> Read(int aitisiId);
        PraktikiApofasiViewModel Refresh(int entityId);
        void Update(PraktikiApofasiViewModel data, AitiseisPraktikisViewModel source);
    }
}