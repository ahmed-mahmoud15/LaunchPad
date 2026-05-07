using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.DTOs.User
{
    public class ProfileQueryRequest
    {
        private int pageNumber = 1;
        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = value < 1 ? 1 : value;
        }

        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set {
                if(value > 50)
                {
                    pageSize = 50;
                }else if(value < 1)
                {
                    pageSize = 1;
                }
                else
                {
                    pageSize = value;
                }
            }
        }

        public string? SortBy { get; set; }
        public bool Descending { get; set; } = false;

        public PagedRequest ToPagedRequest()
        {
            return new PagedRequest { PageNumber = PageNumber, PageSize = PageSize };
        }
       
    }
}
