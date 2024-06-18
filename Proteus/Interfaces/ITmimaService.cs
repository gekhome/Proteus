using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ITmimaService
    {
        void Create(TmimaViewModel data);
        void Create(TmimaViewModel data, int schoolId);
        void Destroy(TmimaViewModel data);
        IEnumerable<TmimaViewModel> Read();
        IEnumerable<TmimaViewModel> Read(int schoolId);
        TmimaViewModel Refresh(int entityId);
        void Update(TmimaViewModel data);
        void Update(TmimaViewModel data, int schoolId);
    }
}