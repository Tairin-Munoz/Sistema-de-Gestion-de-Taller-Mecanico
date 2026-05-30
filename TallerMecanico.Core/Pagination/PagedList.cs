using System.Collections.Generic;
using System.Linq;

namespace TallerMecanico.Core.Pagination
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PaginationMetadata Pagination { get; set; }

        public PagedList(IEnumerable<T> items, PaginationMetadata pagination)
        {
            Items = items;
            Pagination = pagination;
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var items = source.ToList();
            var count = items.Count;
            var pageCount = pageSize > 0 ? (int)Math.Ceiling(count / (double)pageSize) : 1;
            var pagedItems = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var metadata = new PaginationMetadata
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = pageCount
            };

            return new PagedList<T>(pagedItems, metadata);
        }
    }
}
