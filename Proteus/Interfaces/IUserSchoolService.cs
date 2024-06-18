using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IUserSchoolService
    {
        void Create(UserSchoolViewModel data);
        void Destroy(UserSchoolViewModel data);
        List<UserSchoolViewModel> Read();
        void Update(UserSchoolViewModel data);
    }
}