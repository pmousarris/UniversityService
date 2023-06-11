using ModelUtil.Entities.UnicBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Repositories.UnicBase
{
    public interface IUnicBaseRepository
    {
        Task<(Guid?, UserRole?, string?)> GetUserId_PasswordHashed(string email);
        Task<(IEnumerable<User>, int)> GetStudents(Guid? userId, int pageNumber, int pageSize, StudentSortColumn sortColumn, bool sortDescending);
        Task<IEnumerable<Section>> GetSectionsDetails(int? academicPeriodId = null, Guid? userId = null);
    }
}
