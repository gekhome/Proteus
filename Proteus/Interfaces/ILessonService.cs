using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ILessonService
    {
        void Create(LessonsIekViewModel data, int eidikotitaId);
        void Destroy(LessonsIekViewModel data);
        IEnumerable<LessonsIekViewModel> Read(int eidikotitaId);
        LessonsIekViewModel Refresh(int entityId);
        void Update(LessonsIekViewModel data, int eidikotitaId);
    }
}