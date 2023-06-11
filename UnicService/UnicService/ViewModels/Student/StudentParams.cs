using ModelUtil;

namespace UnicService.ViewModels.Student
{   
    public class StudentParams
    {
        private int _pageNumber = 1;
        public int PageNumber { get { return _pageNumber; } set { _pageNumber = value; } }

        private int _pageSize = 10;
        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        private StudentSortColumn _sortColumn = StudentSortColumn.FirstName;
        public StudentSortColumn SortColumn { get { return _sortColumn; } set { _sortColumn = value; } }

        private bool _sortDescending = false;
        public bool SortDescending { get { return _sortDescending; } set { _sortDescending = value; } }
    }    
}
