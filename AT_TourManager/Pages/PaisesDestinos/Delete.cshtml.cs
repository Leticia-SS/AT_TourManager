using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_TourManager.Data;
using AT_TourManager.Data.Models;

namespace AT_TourManager.Pages.PaisesDestinos
{
    public class DeleteModel : PageModel
    {
        private readonly AT_TourManager.Data.TourManagerContext _context;

        public DeleteModel(AT_TourManager.Data.TourManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaisDestino PaisDestino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paisdestino = await _context.PaisesDestinos.FirstOrDefaultAsync(m => m.Id == id);

            if (paisdestino == null)
            {
                return NotFound();
            }
            else
            {
                PaisDestino = paisdestino;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paisdestino = await _context.PaisesDestinos.FindAsync(id);
            if (paisdestino != null)
            {
                PaisDestino = paisdestino;
                _context.PaisesDestinos.Remove(PaisDestino);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
