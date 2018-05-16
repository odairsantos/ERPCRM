using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPCRM.Models;
using ERPCRM.Repository;

namespace ERPCRM.Controllers
{
    public class PedidoItemController : Controller
    {
        // GET: PedidoItem
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: PedidoItem/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            PedidoItem objPedido = new PedidoItem();
            PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();

            objPedido = objPedidoItemRep.Dados(id);

            if(objPedido != null)
                return View(objPedido);
            else
                return RedirectToAction("Index","Pedido");
        }

        // GET: PedidoItem/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidoItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(PedidoItem objPedidoItemform)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int retorno = 0;
                    PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();

                    retorno = objPedidoItemRep.Inserir(objPedidoItemform);

                    if (retorno == 0)
                    {
                        ModelState.AddModelError("", "Ocorreu um erro, não foi possível cadastrar o produto no pedido, erro no processo de cadastro.");
                    }
                    else { 
                        return RedirectToAction("Details", "Pedido", new { id = objPedidoItemform.id_pedido });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ocorreu um erro, não foi possível cadastrar o produto no pedido, erro nos dados do produto.");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ocorreu um erro, não foi possível cadastrar o produto no pedido.");
                return RedirectToAction("Index");
            }
        }

        // GET: PedidoItem/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            PedidoItem objPedidoitem = new PedidoItem();
            PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();

            objPedidoitem = objPedidoItemRep.Dados(id);

            if (objPedidoitem != null)
            {
                return View(objPedidoitem);
            } else {
                ModelState.AddModelError("", "Ocorreu um erro, não foi possível encontrar o item.");
                return RedirectToAction("Index", "Pedido");
            }
        }

        // POST: PedidoItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(PedidoItem objPedidoItemForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int retorno = 0;
                    PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();

                    retorno = objPedidoItemRep.Alterar(objPedidoItemForm);

                    if (retorno == 0)
                    {
                        ModelState.AddModelError("", "Ocorreu um erro, não foi possível alterar o produto no pedido, erro no processo de alteração.");
                    }
                    else
                    {
                        return RedirectToAction("Details", "Pedido", new { id = objPedidoItemForm.id_pedido });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ocorreu um erro, não foi possível alterar o produto no pedido, erro nos dados do produto.");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ocorreu um erro, não foi possível alterar o item.");
                return RedirectToAction("Details", "Pedido", new { id = objPedidoItemForm.id_pedido});
            }
        }

        // GET: PedidoItem/Delete/5
        [Authorize]
        public ActionResult Delete()
        {
            return RedirectToAction("Index", "Pedido");
        }

        // POST: PedidoItem/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, int idPedido)
        {
            try
            {
                int retorno = 0;
                PedidoItemRepository objPedidoItemRep = new PedidoItemRepository();

                retorno = objPedidoItemRep.Apagar(id);

                if (retorno == 0)
                {
                    ModelState.AddModelError("", "Ocorreu um erro, não foi possível alterar o produto no pedido, erro no processo de alteração.");
                }
                else {
                    return RedirectToAction("Details", "Pedido", new { id = idPedido });
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ocorreu um erro, não foi possível apagar o item.");
                return RedirectToAction("Details", "Pedido", new { id = idPedido });
            }
        }
    }
}
