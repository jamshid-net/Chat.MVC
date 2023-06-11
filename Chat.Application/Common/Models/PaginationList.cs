using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Models;
public class PaginationList<T>
        where T : class
{
    public int PageIndex { get; set; }
    public int TotalPage { get; set; }

    public bool PrevPage => PageIndex > 1;
    public bool NextPage => PageIndex < TotalPage;

    public List<T> items { get; init; } = new List<T>();

    public PaginationList(List<T> _items, int count, int pageIndex, int pageSize)
    {

        PageIndex = pageIndex;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);

        items.AddRange(_items);

    }
    public static  async Task<PaginationList<T>>
        CreatePaginatedListAsync(List<T> _items, int pageIndex, int pageSize)
    {
        int count = _items.Count();
        var paginatedItems = _items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return await Task.FromResult( new PaginationList<T>(paginatedItems, count, pageIndex, pageSize));

    }

}