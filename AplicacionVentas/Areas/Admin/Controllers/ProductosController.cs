
using AplicacionVentas.Infraestructura;
using AplicacionVentas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace AplicacionVentas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {

        private readonly ContextoDatos _context;
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public ProductosController(ContextoDatos context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p=1)
        {
            int pageSize = 3;
            ViewBag.PageRange = pageSize;
            ViewBag.PageNumber = p;
            ViewBag.TotalPages= (int)Math.Ceiling((decimal)_context.Productos.Count()/pageSize);

            return View(await _context.Productos.OrderByDescending(p => p.Id)
                .Include(p => p.Categoria)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre",producto.CategoriaId);

            if(ModelState.IsValid)
            {
                producto.Slug = producto.Nombre.ToLower().Replace(" ", "-");

                var slug = await _context.Productos.FirstOrDefaultAsync(p=>p.Slug == producto.Slug);

                if(slug != null)
                {
                    ModelState.AddModelError("", "El producto ya existe.");
                    return View(producto);
                }

                

                if(producto.ImageUpload!=null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/productos");
                    string nombreImagen = Guid.NewGuid().ToString() + "_" + producto.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, nombreImagen);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await producto.ImageUpload.CopyToAsync(fs);

                    fs.Close();

                    producto.Imagen = nombreImagen;
                }

                _context.Add(producto);

                await _context.SaveChangesAsync();

                TempData["Success"] = "El producto ha sido creado";

                return RedirectToAction("Index");
            }

            return View(producto);
        }


        public async Task<IActionResult> Edit(long id) 
        {
            Producto producto = await _context.Productos.FindAsync(id); 
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre", producto.CategoriaId);

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre", producto.CategoriaId);

            if (ModelState.IsValid)
            {
                producto.Slug = producto.Nombre.ToLower().Replace(" ", "-");

                var slug = await _context.Productos.FirstOrDefaultAsync(p => p.Slug == producto.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "El producto ya existe.");
                    return View(producto);
                }



                if (producto.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/productos");
                    string nombreImagen = Guid.NewGuid().ToString() + "_" + producto.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, nombreImagen);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await producto.ImageUpload.CopyToAsync(fs);

                    fs.Close();

                    producto.Imagen = nombreImagen;
                }

                _context.Update(producto);

                await _context.SaveChangesAsync();

                TempData["Success"] = "El producto ha sido editado";

                
            }

            return View(producto);
        }

        public async Task<IActionResult> Delete(long id)
        {
            Producto producto = await _context.Productos.FindAsync(id);

            if (!string.Equals(producto.Imagen, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/productos");
                string oldImagePath = Path.Combine(uploadsDir, producto.Imagen);
                if(System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }


            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            TempData["Success"] = "El producto ha sido eliminado con exito";


            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(long id)
        {
            Producto producto = await _context.Productos.Include(p=>p.Categoria).FirstOrDefaultAsync(p=>p.Id==id);

            if (producto==null)
            {
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        


    }
}
