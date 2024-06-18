using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiPeratosiService
    {
        void Create(PraktikiPeratosiViewModel data, StudentInPraktikiViewModel source);
        void Destroy(PraktikiPeratosiViewModel data);
        IEnumerable<PraktikiPeratosiViewModel> Read(int ergodotisId, int studentId, int tmimaId);
        PraktikiPeratosiViewModel Refresh(int entityId);
        void Update(PraktikiPeratosiViewModel data, StudentInPraktikiViewModel source);
    }
}