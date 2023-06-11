using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil
{
    public enum Priority
    {
        Informative,
        Error,
        Critical
    }

    public enum UserRole
    {
        Student = 1,
        Lecturer = 2
    }

    public enum StudentSortColumn
    {
        FirstName,
        LastName
    }
}
