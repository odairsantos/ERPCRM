using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPCRM.Models;
using ERPCRM.Repository;

namespace ERPCRM.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        [Authorize]
        public ActionResult Index()
        {
            PedidoRepository objPedidoRep = new PedidoRepository();

            return View(objPedidoRep.ListaTodos());
        }

        // GET: Pedido/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            PedidoRepository objPedidoRep = new PedidoRepository();
            PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();
            PedidoItens objPedidoItens = new PedidoItens();

            objPedidoItens.pedido = objPedidoRep.Dados(id);
            objPedidoItens.listPedidoItem = objPedidoItemRep.ListaTodos(id);

            if (objPedidoItens == null)
            {
                ModelState.AddModelError("", "O pedido não está cadastrado no sistema.");
                return RedirectToAction("Index");
            }
            else
                return View(objPedidoItens);
        }

        // GET: Pedido/Create
        [Authorize]
        public ActionResult Create()
        {

            int retornoCadastro = 0;
            PedidoRepository objPedidoRep = new PedidoRepository();

            retornoCadastro = objPedidoRep.Inserir(1);
            if(retornoCadastro > 0)
                return RedirectToAction("Details", "Pedido", new { id = retornoCadastro });
            else
            {
                ModelState.AddModelError("", "Infelizmente não foi possível criar um novo pedido.");
                return RedirectToAction("Index", "Pedido");
            }
            
        }

        // POST: Pedido/Create
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

        // GET: Pedido/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedido/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // Verificar se o pedido está finalizado

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedido/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedido/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Verificar se o pedido está finalizado

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
