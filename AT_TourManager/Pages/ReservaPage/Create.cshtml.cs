using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AT_TourManager.Data;
using AT_TourManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AT_TourManager.Pages.ReservaPage
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
        ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
        ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Titulo");
            return Page();
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Logger.MultipleLoggers(logToConsole: true, logToFile: true, logToMemory: false);

            var pacote = await _context.PacotesTuristicos.FindAsync(Reserva.PacoteTuristicoId);
            if (pacote != null)
            {
                Reserva.CalcularValorTotal((diarias, precoDiaria) => diarias * precoDiaria);
                var reservasCount = await _context.Reservas
                    .CountAsync(r => r.PacoteTuristicoId == Reserva.PacoteTuristicoId && !r.IsDeleted);

                pacote.CapacityReached += (p) =>
                {
                    Logger.Mensagem?.Invoke($"ALERTA: Capacidade máxima atingida para o pacote {p.Titulo} (ID: {p.Id})");
                };

                pacote.VerificarCapacidade(reservasCount + 1);
            }

            _context.Reservas.Add(Reserva);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
