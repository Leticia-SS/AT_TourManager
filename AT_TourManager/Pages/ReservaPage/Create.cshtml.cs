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
        ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
        ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var pacote = await _context.PacotesTuristicos.FindAsync(Reserva.PacoteTuristicoId);
            var reservasCount = await _context.Reservas
                .CountAsync(r => r.PacoteTuristicoId == Reserva.PacoteTuristicoId && !r.IsDeleted);

            pacote.CapacityReached += (pacoteAlcancado) =>
            {
                Console.WriteLine($"ALERTA: Capacidade máxima {pacoteAlcancado.CapacidadeMaxima} atingida para o pacote {pacoteAlcancado.Titulo}!");
                ModelState.AddModelError(string.Empty, $"Capacidade máxima ({pacoteAlcancado.CapacidadeMaxima}) atingida para este pacote turístico!");
            };

            if (!pacote.VerificarCapacidade(reservasCount))
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
                ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Id");
                return Page();
            }

            Reserva.ValidarReserva(_context);
            Reserva.PacoteTuristico = pacote;

            Reserva.CalcularValorTotal((diarias, preco) => diarias * preco);

            _context.Reservas.Add(Reserva);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
