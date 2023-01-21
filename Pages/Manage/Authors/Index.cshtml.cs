using lawrencelapid.Midterm_.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lawrencelapid.Midterm_.Infrastructure.Domain.Models;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.Extensions.Options;
using lawrencelapid.Midterm_.Infrastructure.Security;
using lawrencelapid.Midterm_.Infrastructure.Domain;
using System.Data;
using lawrencelapid.Midterm_.Infrastructure.Domain.Model;
using lawrencelapid.Midterm_.Infrastructure.Domain;

namespace NeilMacabulos.Midterm_.Pages.Manage.Roles
{
    public class Index : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Index(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "")
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Name != null && a.Name.ToLower().Contains(keyword.ToLower())
                        || a.Description != null && a.Description.ToLower().Contains(keyword.ToLower())
                        || a.Abbreviation != null && a.Abbreviation.ToLower().Contains(keyword.ToLower())
                );
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Name);
                }
                else if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Name);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Description);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Description);
                }
                else if (sortBy.ToLower() == "abbreviation" && sortOrder == SortOrder.Ascending)
                {
                    query = _context.Categories.OrderBy(a => a.Abbreviation);
                }
                else if (sortBy.ToLower() == "abbreviation" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Abbreviation);
                }
            }

            var roles = query
                            .Skip(skip)
            .Take((int)pageSize)
                            .ToList();

            View.Categories = new Paged<Categories>()
            {
                Items = categories,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Keyword = keyword
            };
            return Page();
        }

        public class ViewModel
        {
            public Paged<Categories>? Categories { get; set; }
        }
    }
}
