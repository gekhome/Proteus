using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiExploreService
    {
        List<PraktikiApallagiViewModel> ReadApallages(int schoolId);
        List<PraktikiDiakopiViewModel> ReadDiakopes(int schoolId);
        List<PraktikiExploreViewModel> ReadStudents(int schoolId);
    }
}