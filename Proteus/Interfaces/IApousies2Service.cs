using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IApousies2Service
    {
        void Create(StudentApousies2ViewModel data, int schoolId);
        void Destroy(StudentApousies2ViewModel data);
        IEnumerable<StudentApousies2ViewModel> Read(int schoolId);
        StudentApousies2ViewModel Refresh(int entityId);
        void Update(StudentApousies2ViewModel data, int schoolId);
    }
}