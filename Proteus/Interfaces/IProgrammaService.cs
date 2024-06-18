using Proteus.Models;
using System;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IProgrammaService
    {
        void Create(ProgrammaDayViewModel data, int tmimaId, DateTime? theDate, int week, int schoolId);
        void Create(ProgrammaDayViewModel data, int tmimaId, int schoolId);
        void Destroy(ProgrammaDayViewModel data);
        IEnumerable<ProgrammaDayViewModel> Read(int tmimaId, DateTime? theDate);
        IEnumerable<ProgrammaDayViewModel> Read(int tmimaId, DateTime? theDate1, DateTime? theDate2);
        void Update(ProgrammaDayViewModel data, int tmimaId, DateTime? theDate, int week, int schoolId);
        void Update(ProgrammaDayViewModel data, int tmimaId, int schoolId);
    }
}