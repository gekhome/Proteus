using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface ITeacherInfoService
    {
        List<TeacherAnatheseisInfoViewModel> GetAnatheseis(int teacherId);
        List<TeacherPeriodsInfoViewModel> GetPeriods(int teacherId);
        TeacherInfoViewModel GetRecord(int teacherId);
        IEnumerable<TeacherInfoViewModel> Read();
        IEnumerable<TeacherInfoViewModel> Read(int schoolId);
    }
}