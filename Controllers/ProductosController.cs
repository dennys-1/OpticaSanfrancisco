using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpticaSanfrancisco.Models;
using OpticaSanfrancisco.Data;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System.Dynamic;

namespace OpticaSanfrancisco.Controllers
{
     public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
         private IEnumerable<Validacion> _validacion;
        private List<Tipo> ListaTipos;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Codigo,Familia,Linea,Categoria,Diseño,Descripcion_Producto,Marca,Material,Talla,Kalibre,Color,Descrip_Color,Stock,Precio,Imagen,Status")] Productos producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Codigo,Familia,Linea,Categoria,Diseño,Descripcion_Producto,Marca,Material,Talla,Kalibre,Color,Descrip_Color,Stock,Precio,Imagen,Status")] Productos producto)
        {
            if (id != producto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ID == id);
        }


         public IActionResult Formulario()
        {
            var ListaTipo = _context.Tipo.ToList();
            var validacion = new Validacion();
            dynamic model = new ExpandoObject();
            model.validacion = validacion;
            model.Tipo = ListaTipo;
            return View(model);
        }


        //VALIDACION

        [HttpPost]
        public IActionResult Formulario(Validacion validacion)
        {
            validacion.IDTipo = int.Parse(Request.Form["IDTipo"]);
            if (ModelState.IsValid)
            {
                _context.Add(validacion);
                _context.SaveChanges();
                validacion.Respuesta="validacion Creado";
                return RedirectToAction("Index");
            }
            else{
                validacion.Respuesta="No se pudo añadir";
                return View(validacion);
            }         
        }        
        
        [HttpPost]
        public IActionResult Cargar(Validacion objProducto){
            if (ModelState.IsValid)
            {
                _context.Add(objProducto);
                _context.SaveChanges();
                objProducto.Respuesta = "";
                return View(objProducto);
            }else
            {
                objProducto.Respuesta = "";
                return View(objProducto);
            }
            
        }

        //PDF
           public async Task<IActionResult> Documento()
        {
           // return View(await _context.Documento.ToListAsync());
             return new ViewAsPdf("Documento", await _context.Productos.ToListAsync())
            {
                 // ...
            };
        }

  
    }
}