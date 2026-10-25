# Guía de despliegue — Pequeños Grandes Diseños

Este proyecto es una app **ASP.NET Core 8 (Razor Pages)**. Ya está lista para
desplegarse con **Docker** en cualquier hosting que lo soporte. Abajo está el
paso a paso para **Render** (tiene plan gratuito y se conecta a GitHub), y al
final una alternativa con **Azure**.

El repositorio en GitHub es `Karojulicha/Nuevo-repositorio` y el proyecto vive
en la subcarpeta:

```
OneDrive/Escritorio/Projects_06/PortafolioGabi/PortafolioGabi
```

> Importante: ese subdirectorio es la **raíz** que el hosting debe usar (ahí
> están el `Dockerfile` y el `.csproj`).

---

## Opción A — Render (recomendada, gratis)

1. Entra a <https://render.com> y crea una cuenta (puedes usar tu cuenta de GitHub).
2. Autoriza a Render para acceder al repositorio `Nuevo-repositorio`.
3. En el panel: **New +** → **Web Service**.
4. Selecciona el repositorio `Nuevo-repositorio`.
5. Configura así:
   - **Branch**: `review-1` (o la rama donde esté el proyecto).
   - **Root Directory**: `OneDrive/Escritorio/Projects_06/PortafolioGabi/PortafolioGabi`
   - **Runtime / Language**: **Docker** (Render detecta el `Dockerfile`).
   - **Instance Type**: **Free**.
6. Haz clic en **Create Web Service**.

Render construirá la imagen y, al terminar, te dará una URL pública tipo:

```
https://pequenos-grandes-disenos.onrender.com
```

No hace falta configurar el puerto: la app lee la variable `PORT` que Render
inyecta automáticamente.

> Nota del plan gratuito: el servicio “se duerme” tras un rato sin visitas y
> tarda unos segundos en despertar en la primera visita. Es normal.

### Cada vez que cambies algo
Solo haz `commit` y `push` a la rama conectada; Render vuelve a desplegar solo.

---

## Opción B — Azure App Service (desde Visual Studio)

1. En Visual Studio, clic derecho sobre el proyecto **PortafolioGabi** → **Publicar**.
2. Destino: **Azure** → **Azure App Service (Linux)**.
3. Inicia sesión con tu cuenta de Azure y crea un App Service nuevo
   (puedes elegir el plan gratuito **F1**).
4. Finaliza y pulsa **Publicar**. Al terminar se abrirá la URL pública
   `https://<tu-nombre>.azurewebsites.net`.

---

## Probar el contenedor en tu PC (opcional)

Con Docker Desktop abierto, desde la carpeta del proyecto:

```bash
docker build -t portafoliogabi .
docker run -p 8080:8080 portafoliogabi
```

Luego abre <http://localhost:8080>.

---

## Cómo actualizar los datos del sitio

Todo el contenido editable está en:

```
wwwroot/data/artworks.json
```

- **Obras**: nombre, imagen, descripción, formato, precio, categoría, disponibilidad.
- **Contacto**: WhatsApp, Instagram, correo.

Las imágenes de las obras van en `wwwroot/images/artworks/`.
Después de editar, haz `commit` + `push` y el hosting se actualiza solo.
