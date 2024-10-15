using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

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

            if (FechaRetiro < Fecha_Hoy || FechaEntrega < Fecha_Hoy || FechaEntrega < FechaRetiro)
            {
                ModelState.AddModelError("FechaRetiro", "Debe ingresar una fecha de retiro y una fecha de entrega valida.");
            }

            long totalSize = ImagenCarga.Sum(file => file.Length);

            //Validacion carga de imagenes
            if (ImagenCarga != null && ImagenCarga.Any())
            {
                if (totalSize > 10 * 1024 * 1024) // 10 MB
                {
                    ModelState.AddModelError("ImagenCarga", "El tamaño total de las imágenes no debe superar 10 MB.");
                }
            }

            //Validacion otros campos
            if (Numero_entrega <= 0 || Numero_retiro <= 0)
            {
                ModelState.AddModelError("Numero_entrega", "Los numeros de las calles, deben ser mayores o iguales a 0.");
            }

            //Validacion de registro de errores
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Chequeo del tamaño de las imagenes
            System.Diagnostics.Debug.WriteLine($"Tamaño total de las imágenes: {totalSize} bytes");

            // Envio del Mail
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("tangoapp.noreply@gmail.com", "rltx xtjg rltx rekb ");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("tangoapp.noreply@gmail.com", "Nuevo Pedido");
            mail.To.Add(new MailAddress("gaspariglesias93@gmail.com"));
            mail.Subject = "Nueva solicitud de envio!";
            mail.IsBodyHtml = true;
            mail.Body = $@"
            <h1>Nuevo Registro de Pedido</h1>
            <p><strong>Tipo de Carga:</strong> {TipoCarga}</p>
            <p><strong>Fecha de Retiro:</strong> {FechaRetiro.ToString("dd-MM-yyyy")}</p>
            <p><strong> Dirección de Retiro:</strong> {Calle_retiro}, {Numero_retiro}, {Provincia_retiro}, {Localidad_retiro},{Referencia_retiro}</p>
            <p><strong> Fecha de Entrega:</strong> {FechaEntrega.ToString("dd-MM-yyyy")}</p>
            <p><strong> Dirección de Entrega:</strong> {Calle_entrega}, {Numero_entrega}, {Provincia_entrega}, {Localidad_entrega}, {Referencia_entrega}</p>";
            
            smtp.Send(mail);
            
            TempData["SuccessMessage"] = "Pedido de envío publicado exitosamente.";
            return Page();
        }
    }
}