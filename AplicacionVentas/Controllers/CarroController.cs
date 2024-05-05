using AplicacionVentas.Infraestructura;
using AplicacionVentas.Models;
using AplicacionVentas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;


namespace AplicacionVentas.Controllers
{
    public class CarroController : Controller
    {
        private readonly ContextoDatos _context;

        public CarroController(ContextoDatos context)
        {
            _context = context;
        }






        public IActionResult Index()
        {

            List<ItemCarrito> carro = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito") ?? new List<ItemCarrito>();

            CarroViewModel carroVM = new()
            {
                ItemCarritos = carro,
                TotalGrande = carro.Sum(x => x.Cantidad * x.Precio)
            };
            return View(carroVM);
        }

        public async Task<IActionResult> Add(long id)
        {
            Producto producto =await _context.Productos.FindAsync(id);

            List<ItemCarrito> carro = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito") ?? new List<ItemCarrito>();

            ItemCarrito itemCarrito = carro.Where(c=>c.ProductoId==id).FirstOrDefault();

            if(itemCarrito == null) {
                
                carro.Add(new ItemCarrito(producto));


            }
            else
            {
                itemCarrito.Cantidad += 1;
            }

            HttpContext.Session.SetJson("Carrito", carro);
            TempData["Success"] = "El producto ha sido agregado!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(long id)
        {
            Producto producto = await _context.Productos.FindAsync(id);

            List<ItemCarrito> carro = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");

            ItemCarrito itemCarrito = carro.Where(c => c.ProductoId == id).FirstOrDefault();

            if (itemCarrito.Cantidad>1)
            {

                --itemCarrito.Cantidad;


            }
            else
            {
                carro.RemoveAll(p=> p.ProductoId == id);
            }


            if(carro.Count==0)
            {
                HttpContext.Session.Remove("Carrito");
            }
            else
            {
                HttpContext.Session.SetJson("Carrito", carro);
            }

            
            TempData["Success"] = "Se ha removido su producto!";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Remove(long id)
        {
            

            List<ItemCarrito> carro = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");

            carro.RemoveAll(p=>p.ProductoId == id);

            


            if (carro.Count == 0)
            {
                HttpContext.Session.Remove("Carrito");
            }
            else
            {
                HttpContext.Session.SetJson("Carrito", carro);
            }


            TempData["Success"] = "Se ha removido su producto!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {


            HttpContext.Session.Remove("Carrito");

            return RedirectToAction("Index");
        }   

    }
} 
