using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IIekInfoService
    {
        IEnumerable<qryIekEidikotitesViewModel> GetEidikotites(int schoolId);
        SYS_SCHOOLSViewModel GetRecord(int schoolId);
        IEnumerable<SYS_SCHOOLSViewModel> Read();
    }
}