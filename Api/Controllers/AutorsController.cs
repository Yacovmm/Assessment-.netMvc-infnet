using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Api.Context;
using Domain;

namespace Api.Controllers
{
    public class AutorsController : ApiController
    {
        private ContextDb db = new ContextDb();


        #region PegaAutores
        // GET: api/Autors
        public IList<Autor> GetAutores()
        {
            var retorno = db.Autores.ToList();
            List<Autor> autores = new List<Autor>();
            foreach (var item in retorno)
            {
                var autor = new Autor()
                {
                    AutorId = item.AutorId,
                    Nome = item.Nome,
                    Sobrenome = item.Sobrenome,
                    Email = item.Email,
                    DataNascimento = item.DataNascimento,
                    Livros = new List<Livro>()
                };
                foreach (var item2 in item.Livros)
                {
                    item2.Autors = new List<Autor>();
                    autor.Livros.Add(item2);
                }
                autores.Add(autor);
            }

            return autores;
        }
        #endregion

        #region PegaAutorId
        // GET: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult GetAutor(int id)
        {

            Autor busca = db.Autores.Find(id);
            if (busca == null)
            {
                return NotFound();
            }
            Autor autor = new Autor()
            {
                AutorId = busca.AutorId,
                Nome = busca.Nome,
                Sobrenome = busca.Sobrenome,
                Email = busca.Email,
                DataNascimento = busca.DataNascimento,
                Livros = new List<Livro>()
            };


            return Ok(autor);
        }
        #endregion

        #region AtualizaAutor
        // PUT: api/Autors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutor(int id, Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autor.AutorId)
            {
                return BadRequest();
            }

            db.Entry(autor).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        #region CriaAutor
        // POST: api/Autors
        [ResponseType(typeof(Autor))]
        public IHttpActionResult PostAutor(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autores.Add(autor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autor.AutorId }, autor);
        }
        #endregion

        #region DeletaAutor
        // DELETE: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult DeleteAutor(int id)
        {
            Autor autor = db.Autores.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            db.Autores.Remove(autor);
            db.SaveChanges();

            return Ok(autor);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutorExists(int id)
        {
            return db.Autores.Count(e => e.AutorId == id) > 0;
        }
    }
}