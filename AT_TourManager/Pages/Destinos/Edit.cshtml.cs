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

        [BindProperty]
        public List<int> SelectedPacotesIds { get; set; } = new List<int>();

        public MultiSelectList PacotesOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await _context.Destinos
                .Include(d => d.PacotesTuristicos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (destino == null)
            {
                return NotFound();
            }
            Destino = destino;

            var allPacotes = await _context.PacotesTuristicos.ToListAsync();

            var pacotesAssociadosIds = Destino.PacotesTuristicos.Select(p => p.Id).ToList();

            PacotesOptions = new MultiSelectList(
                allPacotes,
                nameof(PacoteTuristico.Id),
                nameof(PacoteTuristico.Titulo),
                Destino.PacotesTuristicos.Select(p => p.Id).ToList());

            SelectedPacotesIds = pacotesAssociadosIds;
            ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestinos, "Id", "Nome", Destino.PaisDestinoId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var destinoToUpdate = await _context.Destinos
                .Include(d => d.PacotesTuristicos)
                .FirstOrDefaultAsync(d => d.Id == Destino.Id);

            if (destinoToUpdate == null)
            {
                return NotFound();
            }

            _context.Entry(destinoToUpdate).CurrentValues.SetValues(Destino);

            await UpdatePacotesAssociados(destinoToUpdate);

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
                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool DestinoExists(int id)
        {
            return _context.Destinos.Any(e => e.Id == id);
        }

        private async Task UpdatePacotesAssociados(Destino destino)
        {
            destino.PacotesTuristicos.Clear();

            if (SelectedPacotesIds != null && SelectedPacotesIds.Any())
            {
                var pacotesParaAdicionar = await _context.PacotesTuristicos
                    .Where(p => SelectedPacotesIds.Contains(p.Id))
                    .ToListAsync();

                foreach (var pacote in pacotesParaAdicionar)
                {
                    destino.PacotesTuristicos.Add(pacote);
                }
            }
        }
    }
}
