// Pequeños Grandes Diseños — interacciones del portafolio
(function () {
    "use strict";

    /* ---------- Menú hamburguesa ---------- */
    var toggle = document.querySelector(".nav-toggle");
    var menu = document.getElementById("nav-menu");
    if (toggle && menu) {
        toggle.addEventListener("click", function () {
            var open = menu.classList.toggle("open");
            toggle.setAttribute("aria-expanded", open ? "true" : "false");
            toggle.setAttribute("aria-label", open ? "Cerrar menú" : "Abrir menú");
        });
        menu.querySelectorAll("a").forEach(function (link) {
            link.addEventListener("click", function () {
                menu.classList.remove("open");
                toggle.setAttribute("aria-expanded", "false");
            });
        });
    }

    /* ---------- Aparición gradual de secciones ---------- */
    var reveals = document.querySelectorAll(".reveal");
    if ("IntersectionObserver" in window && reveals.length) {
        var io = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("is-visible");
                    io.unobserve(entry.target);
                }
            });
        }, { threshold: 0.12 });
        reveals.forEach(function (el) { io.observe(el); });
    } else {
        reveals.forEach(function (el) { el.classList.add("is-visible"); });
    }

    /* ---------- Lightbox / visor de obras ---------- */
    var obras = Array.isArray(window.ARTWORKS) ? window.ARTWORKS : [];
    var box = document.getElementById("lightbox");
    if (!box || !obras.length) { return; }

    var media = document.getElementById("lb-media");
    var nameEl = document.getElementById("lb-name");
    var descEl = document.getElementById("lb-desc");
    var current = 0;
    var lastFocused = null;

    function placeholderMarkup() {
        return '<span class="art-placeholder">' +
            '<svg viewBox="0 0 24 24" width="48" height="48" aria-hidden="true"><path d="M5 19l4.5-6 3 4L16 12l3 7z" fill="#E98C85"/><circle cx="8.5" cy="8.5" r="2.2" fill="#F7D98B"/><rect x="3" y="4" width="18" height="16" rx="2.5" fill="none" stroke="#E98C85" stroke-width="1.4"/></svg>' +
            '<span>Imagen próximamente</span></span>';
    }

    function render(index) {
        current = (index + obras.length) % obras.length;
        var o = obras[current];
        if (o.placeholder || !o.imagen) {
            media.innerHTML = placeholderMarkup();
        } else {
            var img = document.createElement("img");
            img.src = o.imagen;
            img.alt = o.nombre;
            img.onerror = function () { media.innerHTML = placeholderMarkup(); };
            media.innerHTML = "";
            media.appendChild(img);
        }
        nameEl.textContent = o.nombre || "";
        descEl.textContent = o.descripcion || "";
    }

    function open(id) {
        var idx = obras.findIndex(function (o) { return String(o.id) === String(id); });
        if (idx < 0) idx = 0;
        lastFocused = document.activeElement;
        render(idx);
        box.hidden = false;
        document.body.style.overflow = "hidden";
        box.querySelector(".lightbox-close").focus();
    }

    function close() {
        box.hidden = true;
        document.body.style.overflow = "";
        if (lastFocused && lastFocused.focus) lastFocused.focus();
    }

    document.querySelectorAll("[data-open-id]").forEach(function (btn) {
        btn.addEventListener("click", function () { open(btn.getAttribute("data-open-id")); });
    });

    box.querySelectorAll("[data-close]").forEach(function (el) {
        el.addEventListener("click", close);
    });
    box.querySelector(".lightbox-prev").addEventListener("click", function () { render(current - 1); });
    box.querySelector(".lightbox-next").addEventListener("click", function () { render(current + 1); });

    document.addEventListener("keydown", function (e) {
        if (box.hidden) return;
        if (e.key === "Escape") close();
        else if (e.key === "ArrowLeft") render(current - 1);
        else if (e.key === "ArrowRight") render(current + 1);
    });
})();
