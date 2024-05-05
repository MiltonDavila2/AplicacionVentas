using AplicacionVentas.Models;
using AplicacionVentas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AplicacionVentas.Infraestructura.Componentes
{
    public class CarritoViewComponent : ViewComponent
    {
       public IViewComponentResult Invoke()
        {
            List<ItemCarrito> carro = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");
            CarritoViewModel carritoVM;

            if(carro==null || carro.Count == 0)
            {
                carritoVM = null;
            }
            else
            {
                carritoVM = new()
                {
                    NumerodeItems = carro.Sum(x=>x.Cantidad),
                    MontoTotal= carro.Sum(x=>x.Cantidad*x.Precio)
                };
            }


            return View(carritoVM);
        }
    }
}
