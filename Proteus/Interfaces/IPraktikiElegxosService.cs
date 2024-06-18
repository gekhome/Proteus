using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiElegxosService
    {
        void Create(PraktikiElegxosViewModel data, StudentInPraktikiViewModel source);
        void Destroy(PraktikiElegxosViewModel data);
        IEnumerable<PraktikiElegxosViewModel> Read(int ergodotisId, int studentId);
        PraktikiElegxosViewModel Refresh(int entityId);
        void Update(PraktikiElegxosViewModel data, StudentInPraktikiViewModel source);
    }
}