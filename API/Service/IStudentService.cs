using API.Model;

namespace API.Service
{
    public interface IStudentService
    {
        Task<List<ApiStudentModel>> List();
    }
}
