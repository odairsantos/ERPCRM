using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPCRM.Models;
using ERPCRM.Repository;

namespace ERPCRM.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Contato
        public ActionResult Index()
        {
            ContatoRepository objContatoRep = new ContatoRepository();
            
            return View(objContatoRep.Listar());
        }

        // GET: Contato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contato/Create
        public ActionResult Create()
        {
            Contato objContato = new Contato();
            EstadoRepository objEstadoRepository = new EstadoRepository();

            objContato.Estados = objEstadoRepository.ListaEstados();

            return View(objContato);
        }

        // POST: Contato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contato objContato)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "ERRO - Não foi possível cadastrar o contato.");

                    return View(objContato);

                }else{
                    ContatoRepository objContatoRep = new ContatoRepository();

                    if(objContatoRep.cadastrar(objContato) != 1)
                    {
                        ModelState.AddModelError("", "ERRO - Não foi possível cadastrar o contato.");
                    }

                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "ERRO - Não foi possível cadastrar o contato.");
                return View(objContato);
            }
        }

        // GET: Contato/Edit/5
        public ActionResult Edit(int id)
        {
            ContatoRepository objContatoRep = new ContatoRepository();
            EstadoRepository objEstadoRepository = new EstadoRepository();

            Contato objContato = new Contato();

            objContato = objContatoRep.Dados(id);
            
            if (objContato == null)
            {
                
                return RedirectToAction("index");
            }
            else {
                objContato.Estados = objEstadoRepository.ListaEstados();
            }

            return View(objContato);
        }

        // POST: Contato/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contato/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contato/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
