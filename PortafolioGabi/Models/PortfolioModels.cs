namespace PortafolioGabi.Models
{
    /// <summary>
    /// Una obra de la galería. Edita estos datos en wwwroot/data/artworks.json;
    /// no es necesario tocar el código ni el HTML de la página.
    /// </summary>
    public class Artwork
    {
        public int Id { get; set; }

        /// <summary>Nombre visible de la obra.</summary>
        public string Nombre { get; set; } = "";

        /// <summary>
        /// Ruta de la imagen, p. ej. "/images/artworks/01-gatitos.jpg".
        /// Deja el archivo en wwwroot/images/artworks/. Si aún no existe,
        /// se mostrará un marcador temporal.
        /// </summary>
        public string? Imagen { get; set; }

        /// <summary>Descripción breve (opcional).</summary>
        public string? Descripcion { get; set; }

        /// <summary>Formato o características del producto (opcional).</summary>
        public string? Formato { get; set; }

        /// <summary>Precio opcional. Si es null no se muestra ningún precio inventado.</summary>
        public decimal? Precio { get; set; }

        /// <summary>Moneda para mostrar junto al precio (p. ej. "COP", "USD").</summary>
        public string? Moneda { get; set; }

        /// <summary>Categoría opcional.</summary>
        public string? Categoria { get; set; }

        /// <summary>Estado de disponibilidad.</summary>
        public bool Disponible { get; set; } = true;

        /// <summary>
        /// Marca la obra como marcador temporal mientras llega la imagen real.
        /// </summary>
        public bool EsPlaceholder { get; set; }
    }

    /// <summary>Datos de contacto configurables (sin inventar números reales).</summary>
    public class ContactoInfo
    {
        /// <summary>Número de WhatsApp en formato internacional sin "+", p. ej. "573001234567". Vacío = sin WhatsApp.</summary>
        public string? Whatsapp { get; set; }

        /// <summary>Mensaje que se precarga al abrir WhatsApp.</summary>
        public string? WhatsappMensaje { get; set; }

        /// <summary>Usuario de Instagram sin la @, p. ej. "pequenosgrandesdisenos".</summary>
        public string? Instagram { get; set; }

        /// <summary>Correo de contacto (opcional).</summary>
        public string? Email { get; set; }
    }

    /// <summary>Contenedor raíz del archivo artworks.json.</summary>
    public class PortfolioData
    {
        public ContactoInfo Contacto { get; set; } = new();
        public List<Artwork> Obras { get; set; } = new();
    }
}
