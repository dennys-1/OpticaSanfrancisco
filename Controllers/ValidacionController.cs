using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpticaSanfrancisco.Models;
using OpticaSanfrancisco.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using Rotativa.AspNetCore;

namespace OpticaSanfrancisco.Controllers
{
    public class ValidacionController : Controller
    {
             
         private readonly ILogger<ValidacionController> _logger;
        private readonly ApplicationDbContext _context; 
        private IEnumerable<Validacion> _validacion;
        private List<Tipo> ListaTipos;

        
        public ValidacionController(ILogger<ValidacionController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
           
            ListaTipos = _context.Tipo.ToList();
        }            
        
   

        public async Task<IActionResult> Index(int BuscarProducto)
        {
            _validacion = _context.Vali.ToList();
            dynamic model= new ExpandoObject();
            model.TipoProducto = ListaTipos;

            var producto = from m in _validacion
            select m;

            if(BuscarProducto!=0){
            _validacion = _validacion.Where(s => s.IDTipo==BuscarProducto);
            }
            model.Producto = _validacion;
            return View(await Task.FromResult(model));
        }

        public IActionResult Detalle(int? ID)
        {
            _validacion = _context.Vali.ToList();
            if (ID == null)
            {
                return NotFound();
            }
            Validacion validacion = new Validacion();
            foreach(var i in _validacion)
            {
                if (i.ID==ID)
                validacion = i;
            }
            return View(validacion);
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
                objProducto.Respuesta = "Producto cargado a la tienda";
                return View(objProducto);
            }else
            {
                objProducto.Respuesta = "Error, no se pudo cargar a la tienda";
                return View(objProducto);
            }
            
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _validacion = await _context.Vali.FindAsync(id);
            if (_validacion == null)
            {
                return NotFound();
            }

            ViewBag.Tipo = ListaTipos;

            return View(_validacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EMAIL,IDTipo,N_DOCUMNETO,NOMBRE,APELLIDO,TELEFONO")] Validacion _validacion)
        {
            if (id != _validacion.ID)
            {
                 return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _validacion.IDTipo = int.Parse(Request.Form["IDTipo"]);
                    _context.Update(_validacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            dynamic model = new ExpandoObject();
            model.Tipo=ListaTipos;
            model.validacion=_validacion;
            return View(model);
        }
        
        public IActionResult Delete(int? id)
        {
            var validacion = _context.Vali.Find(id);
            _context.Vali.Remove(validacion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        /*

         public IActionResult Habilitar(int? id)
        {
            var validacion = _context.Vali.Find(id);
            //algo hice mal
            // planta.Deshabilitado = true ? false : true;

            if(validacion.Deshabilitado==true){
                validacion.Deshabilitado=false;
            }
            else{
            validacion.Deshabilitado=true;}

            _context.Vali.Update(validacion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        */
    

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