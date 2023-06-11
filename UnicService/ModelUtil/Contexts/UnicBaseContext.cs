using Microsoft.EntityFrameworkCore;
using ModelUtil.Entities.UnicBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Contexts
{
    public sealed class UnicBaseContext : BaseContext
    {
        #region Members
        internal DbSet<Entities.UnicBase.AcademicPeriod> AcademicPeriods { get; set; }
        internal DbSet<Entities.UnicBase.Course> Courses { get; set; }
        internal DbSet<Entities.UnicBase.Section> Sections { get; set; }
        internal DbSet<Entities.UnicBase.SectionUser> SectionUsers { get; set; }
        internal DbSet<Entities.UnicBase.User> Users { get; set; }
        #endregion

        public UnicBaseContext(DbContextOptions<UnicBaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SectionUser>().HasKey(e => new { e.SectionId, e.UserId});
        }

        #region Queryables
        internal IQueryable<AcademicPeriod> GetAcademicPeriods(int[] ids = null, string[] names = null, DateTime? startDateAfter = null, DateTime? endDateBefore = null)
        {
            var q = AcademicPeriods.AsQueryable();

            if (ids != null && ids.Any())
                q = q.Where(x => ids.Contains(x.Id));
            if (names != null && names.Length > 0)
                q = q.Where(x => names.Contains(x.Name));
            if (startDateAfter.HasValue)
                q = q.Where(x => x.StartDate >= startDateAfter.Value);
            if (endDateBefore.HasValue)
                q = q.Where(x => x.EndDate <= endDateBefore.Value);

            return q;
        }

        internal IQueryable<Course> GetCourses(int[] ids = null, string[] codes = null, string[] titles = null)
        {
            var q = Courses.AsQueryable();

            if (ids != null && ids.Any())
                q = q.Where(x => ids.Contains(x.Id));
            if (codes != null && codes.Length > 0)
                q = q.Where(x => codes.Contains(x.Code));
            if (titles != null && titles.Length > 0)
                q = q.Where(x => titles.Contains(x.Title));

            return q;
        }

        internal IQueryable<Section> GetSections(int[] ids = null, string[] sectionNumbers = null, int[] courseIds = null, int[] academicPeriodIds = null, Guid[] sectionUserIds = null, bool? includeCourse = null, bool? includeAcademicPeriod = null, bool? includeSectionUsers = null)
        {
            var q = Sections.AsQueryable();

            if (ids != null && ids.Any())
                q = q.Where(x => ids.Contains(x.Id));
            if (sectionNumbers != null && sectionNumbers.Length > 0)
                q = q.Where(x => sectionNumbers.Contains(x.SectionNumber));
            if (courseIds != null && courseIds.Any())
                q = q.Where(x => courseIds.Contains(x.CourseId));
            if (academicPeriodIds != null && academicPeriodIds.Any())
                q = q.Where(x => academicPeriodIds.Contains(x.AcademicPeriodId));
            if (sectionUserIds != null && sectionUserIds.Any())
                q = q.Where(x => x.SectionUsers.Any(su => sectionUserIds.Contains(su.UserId)));
            if (includeCourse.HasValue && includeCourse == true)
                q = q.Include(x => x.Course);
            if (includeAcademicPeriod.HasValue && includeAcademicPeriod == true)
                q = q.Include(x => x.AcademicPeriod);
            if (includeSectionUsers.HasValue && includeSectionUsers == true)
                q = q.Include(x => x.SectionUsers).ThenInclude(x => x.User);

            return q;
        }

        internal IQueryable<SectionUser> GetSectionUsers(int[] sectionIds = null, Guid[] userIds = null, UserRole? userRole = null)
        {
            var q = SectionUsers.AsQueryable();

            if (sectionIds != null && sectionIds.Any())
                q = q.Where(x => sectionIds.Contains(x.SectionId));
            if (userIds != null && userIds.Length > 0)
                q = q.Where(x => userIds.Contains(x.UserId));
            if (userRole.HasValue)
                q = q.Where(x => x.User.Role == userRole.Value);

            return q;
        }

        internal IQueryable<User> GetUsers(Guid[] ids = null, string[] firstNames = null, string[] lastNames = null, UserRole[] roles = null, string[] phone1 = null, string[] emails = null, string[] socialInsuranceNumbers = null, string[] passwordHashes = null)
        {
            var q = Users.AsQueryable();

            if (ids != null && ids.Any())
                q = q.Where(x => ids.Contains(x.Id));
            if (firstNames != null && firstNames.Length > 0)
                q = q.Where(x => firstNames.Contains(x.FirstName));
            if (lastNames != null && lastNames.Length > 0)
                q = q.Where(x => lastNames.Contains(x.LastName));
            if (roles != null && roles.Length > 0)
                q = q.Where(x => roles.Contains(x.Role));
            if (phone1 != null && phone1.Length > 0)
                q = q.Where(x => phone1.Contains(x.Phone1));
            if (emails != null && emails.Length > 0)
                q = q.Where(x => emails.Contains(x.Email));
            if (socialInsuranceNumbers != null && socialInsuranceNumbers.Length > 0)
                q = q.Where(x => socialInsuranceNumbers.Contains(x.SocialInsuranceNumber));
            if (passwordHashes != null && passwordHashes.Length > 0)
                q = q.Where(x => passwordHashes.Contains(x.PasswordHash));

            return q;
        }
        #endregion
    }
}
