﻿namespace DTOShared.Pagging
{
    public class PagedList<T> : List<T>
    {

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPreviousPage => (CurrentPage > 1);
        public bool HasNextPage => (CurrentPage < TotalPages);

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {

            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {

            int count = source.Count();

            int totalPage = (int)Math.Ceiling((decimal)count / pageSize);

            var ListPerPage = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(ListPerPage, count, pageNumber, pageSize);

        }
    }
}
