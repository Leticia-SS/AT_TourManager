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
    public class IndexModel : PageModel
    {
        private readonly AT_TourManager.Data.TourManagerContext _context;

        public IndexModel(AT_TourManager.Data.TourManagerContext context)
        {
            _context = context;
        }

        public IList<Destino> Destino { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Destino = await _context.Destinos
                .Include(d => d.PaisDestino).ToListAsync();
        }
    }
}
