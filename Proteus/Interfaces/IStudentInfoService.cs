using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IStudentInfoService
    {
        IEnumerable<EgrafesInfoViewModel> GetEgrafes(int studentId);
        StudentInfoViewModel GetRecord(int studentId);
        IEnumerable<StudentInfoViewModel> Read();
        IEnumerable<StudentInfoViewModel> Read(int schoolId);
    }
}