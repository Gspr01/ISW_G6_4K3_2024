using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ImplementacionUS.Pages
{
    public class IndexModel : PageModel
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
        public string? Referencia_retiro { get; set; } //Opcional

        [BindProperty]
        public DateTime FechaRetiro { get; set; } = DateTime.Now;

        [BindProperty]
        public string Calle_entrega { get; set; }

        [BindProperty]
        public int Numero_entrega { get; set; }

        [BindProperty]
        public string Provincia_entrega { get; set; }

        [BindProperty]
        public string Localidad_entrega { get; set; }

        [BindProperty]
        public string? Referencia_entrega { get; set; } //Opcional

        [BindProperty]
        public DateTime FechaEntrega { get; set; } = DateTime.Now;

        [BindProperty]
        public IFormFileCollection ImagenCarga { get; set; }

        public List<string> ImagePaths { get; set; } = new List<string>();
        
        public IActionResult OnPost()
        {
            //Validacion Fechas
            var Fecha_Hoy = DateTime.Today;

            if (FechaRetiro < Fecha_Hoy)
            {
                ModelState.AddModelError("FechaRetiro", "La fecha de retiro debe ser hoy o una fecha futura.");
            }

            if (FechaEntrega < Fecha_Hoy)
            {
                ModelState.AddModelError("FechaEntrega", "La fecha de entrega debe ser hoy o una fecha futura.");
            }

            if (FechaEntrega < FechaRetiro)
            {
                ModelState.AddModelError("FechaEntrega", "La fecha de entrega debe ser igual o posterior a la fecha de retiro.");
            }

            long totalSize = ImagenCarga.Sum(file => file.Length);

            //Validacion carga de imagenes
            if (ImagenCarga != null && ImagenCarga.Any())
            {
                if (totalSize > 10 * 1024 * 1024) // 10 MB
                {
                    ModelState.AddModelError("ImagenCarga", "El tama�o total de las im�genes no debe superar 10 MB.");
                }
            }

            //Validacion otros campos
            if (Numero_entrega <= 0)
            {
                ModelState.AddModelError("Numero_entrega", "Ingrese un numero de calle de entrega mayor a 0.");
            }

            if (Numero_retiro <= 0)
            {
                ModelState.AddModelError("Numero_retiro", "Ingrese un numero de calle de retiro mayor a 0.");
            }

            //Validacion de registro de errores
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Chequeo del tama�o de las imagenes
            System.Diagnostics.Debug.WriteLine($"Tama�o total de las im�genes: {totalSize} bytes");

            TempData["SuccessMessage"] = "Pedido de env�o publicado exitosamente.";
            return Page();
        }
    }  
}