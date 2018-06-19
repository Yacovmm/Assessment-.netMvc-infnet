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
    public class LivrosController : ApiController
    {
        private ContextDb _context = new ContextDb();


        #region PegaLivros
        // GET: api/Livros
        public IList<Livro> GetLivros()
        {
            var retorno = _context.Livros.ToList();
            List<Livro> livros = new List<Livro>();
            foreach (var item in retorno)
            {
                var livro = new Livro()
                {
                    LivroId = item.LivroId,
                    Titulo = item.Titulo,
                    Isbn = item.Isbn,
                    Ano = item.Ano,
                    Autors = new List<Autor>()
                };
                foreach (var item2 in item.Autors)
                {
                    item2.Livros = new List<Livro>();
                    livro.Autors.Add(item2);
                }
                livros.Add(livro);
            }

            return livros;

        }
        #endregion

        #region PegaLivroId
        // GET: api/Livros/5
        [ResponseType(typeof(Livro))]
        public IHttpActionResult GetLivro(int id)
        {
            Livro busca = _context.Livros.Find(id);
            if (busca == null)
            {
                return NotFound();
            }
            Livro livro = new Livro()
            {
                LivroId = busca.LivroId,
                Titulo = busca.Titulo,
                Isbn = busca.Isbn,
                Ano = busca.Ano,
                Autors = new List<Autor>()
            };
            return Ok(livro);
        }
        #endregion

        #region AtualizaLivro
        // PUT: api/Livros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLivro(int id, Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.LivroId)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
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

        #region CriaLivro
        // POST: api/Livros
        [ResponseType(typeof(Livro))]
        public IHttpActionResult PostLivro(Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = livro.LivroId }, livro);
        }
        #endregion

        #region DeletaLivro
        // DELETE: api/Livros/5
        [ResponseType(typeof(Livro))]
        public IHttpActionResult DeleteLivro(int id)
        {
            Livro livro = _context.Livros.Find(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            _context.SaveChanges();

            return Ok(livro);
        }
        #endregion

        #region CriarRelacionamento
//        [HttpGet]
//        public void CriarRelacionamento(int autorId, int livroId)
//        {
//            var autor = _context.Autores.Where(a => a.AutorId == autorId).FirstOrDefault();
//            var livro = _context.Livros.Where(b => b.LivroId == livroId).FirstOrDefault();
//
//            autor.Livros.Add(livro);
//            livro.Autores.Add(autor);
//            _context.SaveChanges();
//
//        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Count(e => e.LivroId == id) > 0;
        }
    }
}