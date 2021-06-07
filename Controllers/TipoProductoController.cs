using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpticaSanfrancisco.Data;
using OpticaSanfrancisco.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace OpticaSanfrancisco.Controllers
{
    public class TipoProductoController : Controller
    {
         private readonly ILogger<TipoProductoController> _logger;
        private readonly ApplicationDbContext _context;
        private IEnumerable<Validacion> _productos;
        private List<Tipo> ListaTipos;


        public TipoProductoController(ILogger<TipoProductoController> logger,
        ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _productos = _context.Vali.ToList();
            ListaTipos = _context.Tipo.ToList();
        }

        public IActionResult Index()
        {
            var listTipo=_context.Tipo.Where(x => x.Nombre != null).ToList();
            return View(listTipo);
        }
        public async Task<IActionResult> ProductoAsync(int BuscarProducto)
        {
            dynamic modelo= new ExpandoObject();
            modelo.Tipo = ListaTipos;

            var producto = from m in _productos
            select m;

            if(BuscarProducto!=0){
            _productos = _productos.Where(s => s.IDTipo==BuscarProducto);
            }
            modelo.Vali = _productos;
            return View(await Task.FromResult(modelo));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre")] Tipo tipo)
        {
            if (id != tipo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }
        
       
       
    }
}