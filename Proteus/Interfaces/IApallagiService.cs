using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IApallagiService
    {
        void Create(StudentApallagiViewModel data, int studentId, int eidikotitaId, int schoolId);
        void Destroy(StudentApallagiViewModel data);
        IEnumerable<StudentApallagiViewModel> Read(int studentId);
        StudentApallagiViewModel Refersh(int entityId);
        void Update(StudentApallagiViewModel data, int studentId, int eidikotitaId, int schoolId);
    }
}