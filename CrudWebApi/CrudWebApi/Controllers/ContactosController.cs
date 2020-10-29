using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly ContactoConTexto _context;

        public ContactosController(ContactoConTexto contexto)  //Constructor
        {
            _context = contexto;
        }
        //Peticion tipo GET:  api/contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactoItems()
        {
            return await _context.ContactoItems.ToListAsync();    // "await" es por el async
        }
    
        //Peticion tipo GET: Un solo registro Ej: api/contactos/4
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContactoItem(int id)
        {
            var ContactoItem = await _context.ContactoItems.FindAsync(id);  /* Nos guarda en la variable "ContactoItem"
                                                                             * lo que encuentre accediendo en el modelo 
                                                                             * "ContactoItems" y buscando ese id */
            if (ContactoItem == null)  /* Si no encuentra un registro con el id que le pasemos nos 
                                        * nos devuelve "NotFound" */
            {
                return NotFound();
            }

            return ContactoItem;  //Si lo encuentra nos devuelve el registro
        }

        //Peticion de tipo POST: api/contactos
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContactoItem(Contacto item)
        {
            _context.ContactoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContactoItem), new { id = item.Id }, item); /*LLamamos al metodo Get de arriba*/

        }
    
        
        //Peticion de tipo PUT: Ej: api/contactos/3
        [HttpPut("{id}")]   /* Con el "PUT" tengo que mandar todos los campos de vuelta, si en vez de eso  solo quiero mandar
                             * el campo que queiro modificar tengo que usar "PATCH" */
        public async Task<IActionResult> PutContactoItem(int id, Contacto item) /* Recibimos el modelo completo y el id
                                                                                 * como en el anterior metodo */
        {
            if(id != item.Id)  //Valida que el item de la peticion existe
            {
                return BadRequest();
            }
            
            _context.Entry(item).State = EntityState.Modified;  //Cuando valida que existe cambiamos el state a "modificado"
            
            await _context.SaveChangesAsync();
            
            return NoContent();  //Cuando hay una actualizacion simplemente no retornar contenido
        }


        //Peticion de tipo DELETE para borrar: Ej: api/contacts/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactoitem(int id)
        {
            var contactoItem = await _context.ContactoItems.FindAsync(id);

            if(contactoItem == null)
            {
                return NotFound();

            }
            _context.ContactoItems.Remove(contactoItem);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }

}
