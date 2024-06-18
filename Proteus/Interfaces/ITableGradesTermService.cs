namespace Proteus.Services
{
    public interface ITableGradesTermService
    {
        string Create(int tmimaId);
        string Destroy(int tmimaId);
        string Update(int tmimaId);
    }
}