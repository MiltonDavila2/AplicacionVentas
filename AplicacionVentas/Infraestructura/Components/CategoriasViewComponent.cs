using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionVentas.Infraestructura.Componentes
{
    public class CategoriasViewComponent : ViewComponent
    {
        private readonly ContextoDatos _context;

        public CategoriasViewComponent(ContextoDatos context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categorias.ToListAsync());
    }
}
