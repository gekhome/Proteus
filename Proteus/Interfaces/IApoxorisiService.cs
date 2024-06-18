using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IApoxorisiService
    {
        void Create(TeacherWithdrawalViewModel data, int schoolId);
        void Destroy(TeacherWithdrawalViewModel data);
        List<TeacherWithdrawalViewModel> Read(int schoolId);
        TeacherWithdrawalViewModel Refresh(int entityId);
        void Update(TeacherWithdrawalViewModel data, int schoolId);
    }
}