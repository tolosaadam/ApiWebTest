using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWebApi.Data
{
    public class ContactoConTexto : DbContext  // Entity framework
    {
        public ContactoConTexto(DbContextOptions<ContactoConTexto> options): base(options)
        {

        }
        //crear nuestro dbset
        public DbSet<Contacto> ContactoItems { get; set; }   /* Es un mapeo de nuestro modelo de base de datos
                                                              * "Contacto".
                                                              * con el nombre "ContactoItems" yo accedo y me traigo
                                                              * todos los campos de la base de datos */
    }
}
