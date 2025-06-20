using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_TourManager.Data;
using AT_TourManager.Data.Models;

namespace AT_TourManager.Pages.Destinos
{
    public class DetailsModel : PageModel
    {
        private readonly AT_TourManager.Data.TourManagerContext _context;

        public DetailsModel(AT_TourManager.Data.TourManagerContext context)
        {
            _context = context;
        }

        public Destino Destino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await _context.Destinos
                .Include(d => d.PaisDestino)
                .Include(d => d.PacotesTuristicos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destino == null)
            {
                return NotFound();
            }
            else
            {
                Destino = destino;
            }
            return Page();
        }
    }
}
