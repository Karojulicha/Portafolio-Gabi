using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortafolioGabi.Models;

namespace PortafolioGabi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public PortfolioData Portfolio { get; private set; } = new();

        public void OnGet()
        {
            Portfolio = LoadPortfolio();
        }

        private PortfolioData LoadPortfolio()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "artworks.json");
            try
            {
                if (System.IO.File.Exists(path))
                {
                    var json = System.IO.File.ReadAllText(path);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        AllowTrailingCommas = true
                    };
                    var data = JsonSerializer.Deserialize<PortfolioData>(json, options);
                    if (data != null)
                    {
                        return data;
                    }
                }
                else
                {
                    _logger.LogWarning("No se encontró artworks.json en {Path}", path);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo leer artworks.json; se usará contenido vacío.");
            }

            return new PortfolioData();
        }

        /// <summary>
        /// Enlace de contacto general: WhatsApp si está configurado; si no,
        /// correo; si no, Instagram; como último recurso, baja a la galería.
        /// </summary>
        public string ContactoUrl(string? mensaje = null)
        {
            var c = Portfolio.Contacto;
            var texto = string.IsNullOrWhiteSpace(mensaje) ? c.WhatsappMensaje : mensaje;

            if (!string.IsNullOrWhiteSpace(c.Whatsapp))
            {
                var numero = new string(c.Whatsapp.Where(char.IsDigit).ToArray());
                var query = string.IsNullOrWhiteSpace(texto) ? "" : "?text=" + Uri.EscapeDataString(texto);
                return $"https://wa.me/{numero}{query}";
            }
            if (!string.IsNullOrWhiteSpace(c.Email))
            {
                var asunto = string.IsNullOrWhiteSpace(texto) ? "" : "?subject=" + Uri.EscapeDataString(texto);
                return $"mailto:{c.Email}{asunto}";
            }
            if (!string.IsNullOrWhiteSpace(c.Instagram))
            {
                return $"https://instagram.com/{c.Instagram.TrimStart('@')}";
            }
            return "#galeria";
        }

        /// <summary>Enlace de consulta de compra para una obra concreta.</summary>
        public string ConsultaUrl(Artwork obra)
        {
            var mensaje = $"Hola ✨ Me gustaría encargarte este diseño: \"{obra.Nombre}\".";
            if (!string.IsNullOrWhiteSpace(obra.Imagen))
            {
                var imagenUrl = $"{Request.Scheme}://{Request.Host}{obra.Imagen}";
                mensaje += $" {imagenUrl}";
            }
            return ContactoUrl(mensaje);
        }

        public bool TieneInstagram => !string.IsNullOrWhiteSpace(Portfolio.Contacto.Instagram);
        public string InstagramUrl => $"https://instagram.com/{Portfolio.Contacto.Instagram?.TrimStart('@')}";
        public bool TieneEmail => !string.IsNullOrWhiteSpace(Portfolio.Contacto.Email);
        public string EmailUrl => $"mailto:{Portfolio.Contacto.Email}";
    }
}
