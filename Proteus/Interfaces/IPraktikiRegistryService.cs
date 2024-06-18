using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.Services
{
    public interface IPraktikiRegistryService
    {
        List<regPraktikiAitisiViewModel> ReadAitiseis();
        List<regPraktikiAitisiViewModel> ReadAitiseis(int schoolId);
        List<regPraktikiApofasiViewModel> ReadApofaseis();
        List<regPraktikiApofasiViewModel> ReadApofaseis(int schoolId);
        List<regPraktikiElegxosViewModel> ReadElegxoi();
        List<regPraktikiElegxosViewModel> ReadElegxoi(int schoolId);
        List<regPraktikiParousiaViewModel> ReadParousies();
        List<regPraktikiParousiaViewModel> ReadParousies(int schoolId);
        List<regPraktikiPeratosiViewModel> ReadPeratoseis();
        List<regPraktikiPeratosiViewModel> ReadPeratoseis(int schoolId);
        List<regPraktikiStudentViewModel> ReadStudents();
        List<regPraktikiStudentViewModel> ReadStudents(int schoolId);
    }
}