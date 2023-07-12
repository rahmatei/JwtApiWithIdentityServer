using API.Model;
using Microsoft.EntityFrameworkCore;
using Student.DataAccess.Data;
using Student.DataAccess.Models;

namespace API.Service
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiStudentModel>> List()
        {
            var Student = await (from Stu in dbContext.Students
                           select new ApiStudentModel()
                           {
                                Id=Stu.Id,
                                Name=Stu.Name,
                                Address=Stu.Address,
                           }).ToListAsync();
            return Student;
        }
    }
}
