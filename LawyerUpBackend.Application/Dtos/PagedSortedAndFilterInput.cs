using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Dtos
{
    public class PagedSortedAndFilterInput
    {
        private int _currentPage = 1;
        private int _maxResultCount = 10;

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = (value==0)?1:value;
            }
        }

        public int MaxResultCount
        {
            get
            {
                return _maxResultCount;
            }
            set
            {
                _maxResultCount = (value == 0) ? 10 : value;
            }
        }
        public string? Sort{ get; set; }
        public string? FilterText { get; set; }
    }
}
