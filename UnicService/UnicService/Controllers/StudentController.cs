using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelUtil;
using ModelUtil.Entities.UnicBase;
using ModelUtil.Logger;
using ModelUtil.Repositories.UnicBase;
using UnicService.ViewModels.Student;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using System.Reflection;
using System.Collections;
using System.Linq.Dynamic.Core;

namespace UnicService.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IUnicBaseRepository _unicBaseRepo;

        public StudentController(IHttpContextAccessor httpContextAccessor, IUniLogger logger, IUnicBaseRepository unicBaseRepo)
            : base(httpContextAccessor, logger)
        {
            _unicBaseRepo = unicBaseRepo;
        }

        [HttpPost]
        public async Task<ActionResult<StudentsVM>> GetStudents(StudentParams parameters)
        {
            var userClaim = GetUserClaim();
            try
            {
                StudentsVM output = new StudentsVM();
                // Get students data.
                var students = await _unicBaseRepo.GetStudents(userId: userClaim.UserId, pageNumber: parameters.PageNumber, pageSize: parameters.PageSize, sortColumn: parameters.SortColumn, sortDescending: parameters.SortDescending);
                
                if (userClaim.UserRole == UserRole.Lecturer)
                {
                    output.Students.AddRange(students.Item1.Select(s => new StudentVM(userId: s.Id, firstName: s.FirstName, lastName: s.LastName, phone1: s.Phone1, email: s.Email)).ToList());
                }
                else if (userClaim.UserRole == UserRole.Student)
                {
                    output.Students.AddRange(students.Item1.Select(s => new StudentVM(userId: s.Id, firstName: s.FirstName, lastName: s.LastName)).ToList());
                }

                output.TotalRecords = students.Item2;

                return output;
            }
            catch (Exception ex)
            {
                sendError(exception: ex, parameters: new Dictionary<string, string> { { "UserId", userClaim?.UserId.ToString() } });
                return StatusCode(500);
            }
        }
    }
}
