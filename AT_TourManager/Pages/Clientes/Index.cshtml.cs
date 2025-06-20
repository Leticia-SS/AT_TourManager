using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AT_TourManager.Data;
using AT_TourManager.Data.Models;

namespace AT_TourManager.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly AT_TourManager.Data.TourManagerContext _context;

        public IndexModel(AT_TourManager.Data.TourManagerContext context)
        {
            _context = context;
        }

        public IList<Cliente> Cliente { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Cliente = await _context.Clientes.ToListAsync();
        }
    }
}
