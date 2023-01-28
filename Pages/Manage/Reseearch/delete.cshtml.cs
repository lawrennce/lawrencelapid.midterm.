using lawrencelapid.Midterm_.Infrastructure.Domain;
using lawrencelapid.Midterm_.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace lawrencelapid.Midterm_.Pages.Manage.Products
{
    public class Delete : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Delete(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(Guid? id = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (View.Id == null)
            {
                return NotFound();
            }

            var role = _context?.Products?.FirstOrDefault(a => a.Id == View.Id);

            if (role != null)
            {
                _context?.Products?.Remove(role);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/categories");
            }

            return NotFound();

        }

        public class ViewModel : ProductsLogin
        {

        }
    }
}