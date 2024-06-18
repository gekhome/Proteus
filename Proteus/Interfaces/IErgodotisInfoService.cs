using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IErgodotisInfoService
    {
        IEnumerable<PraktikiInfoViewModel> GetPraktikes(int ergodotisId);
        ErgodotesViewModel GetRecord(int ergodotisId);
        IEnumerable<ErgodotesViewModel> Read();
    }
}