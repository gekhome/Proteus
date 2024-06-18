using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiParousiaService
    {
        void Create(PraktikiParousiaViewModel data, StudentInPraktikiViewModel source);
        void Destroy(PraktikiParousiaViewModel data);
        IEnumerable<PraktikiParousiaViewModel> Read(int ergodotisId, int studentId, int tmimaId);
        PraktikiParousiaViewModel Refresh(int entityId);
        void Update(PraktikiParousiaViewModel data, StudentInPraktikiViewModel source);
    }
}