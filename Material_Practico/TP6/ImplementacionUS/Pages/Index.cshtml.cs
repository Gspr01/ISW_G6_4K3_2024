using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ImplementacionUS.Pages
{
    public class PedidoEnvio : PageModel
    {
        [BindProperty]
        public string TipoCarga { get; set; }

        [BindProperty]
        public string Calle_retiro { get; set; }

        [BindProperty]
        public int Numero_retiro { get; set; }

        [BindProperty]
        public string Provincia_retiro { get; set; }

        [BindProperty]
        public string Localidad_retiro { get; set; }

        [BindProperty]
        public string Referencia_retiro { get; set; }

        [BindProperty]
        public DateTime FechaRetiro { get; set; }

        [BindProperty]
        public string Calle_entrega{ get; set; }
        [BindProperty]
        public int Numero_entrega { get; set; }

        [BindProperty]
        public string Provincia_entrega { get; set; }

        [BindProperty]
        public string Localidad_entrega { get; set; }

        [BindProperty]
        public string Referencia_entrega { get; set; }

        [BindProperty]
        public DateTime FechaEntrega { get; set; }

        public IActionResult OnPost()
        {
            TempData["SuccessMessage"] = "Pedido publicado exitosamente!";
            return Page();
        }
    }
}