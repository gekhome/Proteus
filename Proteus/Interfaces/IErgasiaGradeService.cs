using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IErgasiaGradeService
    {
        void Create(ErgasiaGradeViewModel data, int tmimaId, string lessonText, int schoolId);
        void Destroy(ErgasiaGradeViewModel data);
        IEnumerable<ErgasiaGradeViewModel> Read(int tmimaId, string lessonText);
        void Update(ErgasiaGradeViewModel data, int tmimaId, string lessonText, int schoolId);
    }
}