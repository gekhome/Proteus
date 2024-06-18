using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ITeacherAnalipsiService
    {
        void Create(TeacherAnalipsiViewModel data, int schoolId);
        void Destroy(TeacherAnalipsiViewModel data);
        List<TeacherAnalipsiViewModel> Read(int schoolId);
        TeacherAnalipsiViewModel Refresh(int entityId);
        void Update(TeacherAnalipsiViewModel data, int schoolId);
    }
}