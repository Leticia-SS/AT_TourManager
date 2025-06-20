using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_TourManager.Data;
using AT_TourManager.Data.Models;

namespace AT_TourManager.Pages.Destinos
{
    public class CreateModel : PageModel
    {
        private readonly AT_TourManager.Data.TourManagerContext _context;

        public CreateModel(AT_TourManager.Data.TourManagerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestinos, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Destino Destino { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Destinos.Add(Destino);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
