using lawrencelapid.Midterm_.Infrastructure.Domain;
using lawrencelapid.Midterm_.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using lawrencelapid.Midterm_.Infrastructure.Domain.Model;

namespace lawrencelapid.Pages.Manage.Roles
{
    public class Create : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Create(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(View.Name))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Description))
            {
                ModelState.AddModelError("", "Description name cannot be blank.");
                return Page();
            }

            Categories categories = new Categories()
            {
                Id = Guid.NewGuid(),
                Name = View.Name,
                Description = View.Description,
                Abbreviation = View.Abbreviation
            };

            _context?.Categories?.Add(categories);
            _context?.SaveChanges();

            return RedirectPermanent("~/manage/roles");
        }

        public class ViewModel : Categories
        {

        }
    }
}