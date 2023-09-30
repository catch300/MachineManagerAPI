using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int MaxPageSize { get; set; }
        public int CurrentPageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public T Data { get; set; }
        public PaginatedList(int _totalCount, T _data, int _currentPageNumber, int _pageSize, int maxPageSize)
        {
            TotalCount = _totalCount;
            Data = _data;
            CurrentPageNumber = _currentPageNumber;
            PageSize = _pageSize;

            TotalPages = PageSize < 1 ? 0 : (int)Math.Ceiling((double)_totalCount / (double)PageSize);
            HasPreviousPage = CurrentPageNumber > 1;
            HasNextPage = CurrentPageNumber < TotalPages;
            MaxPageSize = maxPageSize;
        }
    }
}
