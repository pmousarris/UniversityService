using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelUtil.Logger;
using ModelUtil.Repositories.UnicBase;
using UnicService.Filters;
using UnicService.ViewModels.Class;

namespace UnicService.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ClassController : BaseController
    {
        private readonly IUnicBaseRepository _unicBaseRepo;

        public ClassController(IHttpContextAccessor httpContextAccessor, IUniLogger logger, IUnicBaseRepository unicBaseRepo)
            : base(httpContextAccessor, logger)
        {
            _unicBaseRepo = unicBaseRepo;
        }

        [AllowAnonymous]
        [ExcludeFilter]
        [HttpPost]
        public async Task<ActionResult<List<ClassVM>>> GetClasses()
        {
            var userClaim = GetUserClaim();
            try
            {
                List<ClassVM> output;
                // Get sections data.
                var sections = await _unicBaseRepo.GetSectionsDetails();

                output = sections.Select(s => new ClassVM(sectionId: s.Id,
                    sectionNumber: s.SectionNumber,
                    course: new Class_CourseVM(s.Course),
                    academicPeriod: new Class_AcademicPeriodVM(s.AcademicPeriod),
                    numberOfStudents: userClaim?.UserRole == ModelUtil.UserRole.Lecturer ? s.SectionUsers.Count(x => x.User.Role == ModelUtil.UserRole.Student) : null)).ToList();

                return output;
            }
            catch (Exception ex)
            {
                sendError(exception: ex, parameters: new Dictionary<string, string> { { "UserId", userClaim?.UserId.ToString() } });
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Class_LecturersVM>>> GetClassesWithAssignedLecturers(ClassesParams parameters)
        {
            var userClaim = GetUserClaim();
            try
            {
                List<Class_LecturersVM> output;
                // Get sections data.
                var sections = await _unicBaseRepo.GetSectionsDetails(academicPeriodId: parameters.AcademicPeriodId, userId: parameters.ShowOnlyTakenClasses ? userClaim.UserId : null);

                output = sections.Select(s => new Class_LecturersVM(sectionId: s.Id,
                    sectionNumber: s.SectionNumber,
                    course: new Class_CourseVM(s.Course),
                    academicPeriod: new Class_AcademicPeriodVM(s.AcademicPeriod),
                    lecturers: s.SectionUsers.Where(x => x.User.Role == ModelUtil.UserRole.Lecturer).Select(le => new Class_SectionUserVM(le.User)).ToList())).ToList();

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
