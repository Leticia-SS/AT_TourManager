using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AT_TourManager.Data;
using AT_TourManager.Data.Models;

namespace AT_TourManager.Pages.Destinos
{
    public class EditModel : PageModel
    {
        private readonly AT_TourManager.Data.TourManagerContext _context;

        public EditModel(AT_TourManager.Data.TourManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Destino Destino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino =  await _context.Destinos.FirstOrDefaultAsync(m => m.Id == id);
            if (destino == null)
            {
                return NotFound();
            }
            Destino = destino;
           ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestinos, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Destino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinoExists(Destino.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DestinoExists(int id)
        {
            return _context.Destinos.Any(e => e.Id == id);
        }
    }
}
