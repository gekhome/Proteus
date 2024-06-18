namespace Proteus.Services
{
    public interface ITableDeltioFoitisiService
    {
        string Create(int studentId, int schoolId);
        string Destroy(int studentId);
        string Update(int studentId, int schoolId);
    }
}