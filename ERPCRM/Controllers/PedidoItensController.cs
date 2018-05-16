using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPCRM.Models;
using ERPCRM.Repository;

namespace ERPCRM.Controllers
{
    public class PedidoItensController : Controller
    {
        // GET: PedidoItens
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: PedidoItens/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            PedidoRepository objPedidoRep = new PedidoRepository();
            PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();
            PedidoItens objPedidoItens = new PedidoItens();
            
            objPedidoItens.pedido = objPedidoRep.Dados(id);
            objPedidoItens.listPedidoItem = objPedidoItemRep.ListaTodos(id);

            if(objPedidoItens == null)
            {
                ModelState.AddModelError("", "O  pedido não está cadastrado no sistema.");
                return RedirectToAction("Index");
            }
            else
                return View(objPedidoItens);
        }

        // GET: PedidoItens/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidoItens/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoItens/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PedidoItens/Edit/5
        [HttpPost]
        [Authorize]
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

        // GET: PedidoItens/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PedidoItens/Delete/5
        [HttpPost]
        [Authorize]
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
