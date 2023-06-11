using Microsoft.EntityFrameworkCore;
using ModelUtil.Contexts;
using ModelUtil.Entities.UnicBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Repositories.UnicBase
{
    public class UnicBaseRepository : IUnicBaseRepository
    {
        readonly UnicBaseContext _dbContext;

        public UnicBaseRepository(UnicBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves user details, including ID, user role, and hashed password, for a specific email.
        /// </summary>
        /// <param name="email">The email address for which to retrieve user details.</param>
        /// <returns>A tuple containing the user's ID, role, and hashed password. If no user with the specified email exists, each item in the tuple will be null.</returns>
        public async Task<(Guid?, UserRole?, string?)> GetUserId_PasswordHashed(string email)
        {
            var userData = await _dbContext.GetUsers(emails: new string[] { email })
                .Where(x => x.Email == email)
                .Select(s => new { s.Id, s.Role, s.PasswordHash })
                .FirstOrDefaultAsync();

            return (userData?.Id, userData?.Role, userData?.PasswordHash);
        }

        /// <summary>
        /// Retrieves a list of students, with pagination and sorting options. 
        /// If a user ID is provided, the method will return only students who share at least one class with the user.
        /// </summary>
        /// <param name="userId">Optional. The ID of a user to match classes with. If provided, only students who share a class with this user will be returned.</param>
        /// <param name="pageNumber">The page number to retrieve in the pagination. This is 1-based; so, passing 1 will retrieve the first page.</param>
        /// <param name="pageSize">The number of records to retrieve per page for pagination purposes.</param>
        /// <param name="sortColumn">The column to sort the results by.</param>
        /// <param name="sortDescending">A boolean value indicating whether to sort the results in descending order. If false, results will be sorted in ascending order.</param>
        /// <returns>A tuple containing an enumerable collection of User objects and the total number of records.</returns>
        public async Task<(IEnumerable<User>, int)> GetStudents(Guid? userId, int pageNumber, int pageSize, StudentSortColumn sortColumn, bool sortDescending)
        {
            var q = _dbContext.GetSectionUsers(userRole: UserRole.Student);
            
            // Get students taken class together.
            if (userId.HasValue && userId != Guid.Empty)
                q = q.Where(su => su.Section.SectionUsers.Any(su2 => su2.UserId == userId));
            // Remove duplicates.
            IQueryable<User> users = q.Select(s => s.User).Distinct();
            int totalRecords = await users.CountAsync();
            // Sort.
            users = sortColumn switch
            {
                StudentSortColumn.FirstName when sortDescending => users.OrderByDescending(x => x.FirstName),
                StudentSortColumn.FirstName => users.OrderBy(x => x.FirstName),
                StudentSortColumn.LastName when sortDescending => users.OrderByDescending(x => x.LastName),
                StudentSortColumn.LastName => users.OrderBy(x => x.LastName),
                _ => users
            };
            // Paging.
            users = users.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await Task.FromResult((users.AsEnumerable(), totalRecords));
        }

        /// <summary>
        /// Retrieves detailed information about academic sections. 
        /// If an academic period ID and/or a user ID is provided, the method will return only the sections associated with them.
        /// </summary>
        /// <param name="academicPeriodId">Optional. The ID of an academic period. If provided, only sections belonging to this academic period will be returned.</param>
        /// <param name="userId">Optional. The ID of a user. If provided, only sections associated with this user will be returned.</param>
        /// <returns>An enumerable collection of Section objects, each with detailed information.</returns>
        public async Task<IEnumerable<Section>> GetSectionsDetails(int? academicPeriodId = null, Guid? userId = null)
        {
            var q = _dbContext.GetSections(academicPeriodIds: (academicPeriodId.HasValue ? new int[] { academicPeriodId.Value } : null), 
                sectionUserIds: (userId.HasValue ? new Guid[] { userId.Value } : null),
                includeCourse: true,
                includeAcademicPeriod: true,
                includeSectionUsers: true);

            return await Task.FromResult(q.AsEnumerable());
        }
    }
}
