using Proteus.Models;
using System;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IApousiesService
    {
        void Create(StudentApousiesViewModel data, int tmimaId, DateTime? theDate, int schoolId);
        void Destroy(StudentApousiesViewModel data);
        IEnumerable<StudentApousiesViewModel> Read(int tmimaId, DateTime? theDate);
        StudentApousiesViewModel Refresh(int entityId);
        void Update(StudentApousiesViewModel data, int tmimaId, DateTime? theDate, int schoolId);
    }
}