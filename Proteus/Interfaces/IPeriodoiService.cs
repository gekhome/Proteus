using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPeriodoiService
    {
        void Create(PeriodosViewModel data);
        void Destroy(PeriodosViewModel data);
        IEnumerable<PeriodosViewModel> Read();
        PeriodosViewModel Refresh(int entityId);
        void Update(PeriodosViewModel data);
    }
}