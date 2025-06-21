using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AT_TourManager.Pages
{
    public class ViewNotesModel : PageModel
    {
        private readonly string _filePath = Path.Combine("wwwroot", "files");

        [BindProperty]
        public string NovaNota { get; set; }

        public List<string> Notas { get; set; } = new List<string>();
        public string Mensagem { get; set; }


        public void OnGet(string? file)
        {
           LoadNotes();
            if (!string.IsNullOrEmpty(file)){
                LoadContent(file);
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(NovaNota))
            {
                Mensagem = "Nota não pode ser vazia.";
                return Page();
            }

            SaveNote(NovaNota);

            NovaNota = string.Empty;

            LoadNotes();

            Mensagem = "Nota salva com sucesso!";

            return RedirectToPage();
        }

        private void LoadNotes()
        {
            if (Directory.Exists(_filePath))
            {
                Notas = Directory.GetFiles(_filePath, "*.txt")
                    .Select(Path.GetFileName)
                    .OrderByDescending(f => f)
                    .ToList();
            }
        }

        private void LoadContent(string fileName)
        {
            var filePath = Path.Combine(_filePath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                Mensagem = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                Mensagem = "Arquivo não encontrado.";
            }
        }

        private void SaveNote(string note)
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }
            var fileName = $"Nota_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var filePath = Path.Combine(_filePath, fileName);
            System.IO.File.WriteAllText(filePath, note);
        }
    }
}
