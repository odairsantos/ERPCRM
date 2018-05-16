using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPCRM.Models;
using ERPCRM.Repository;

namespace ERPCRM.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        [Authorize]
        public ActionResult Index()
        {
            ProdutoRepository objProduto = new ProdutoRepository();
            return View(objProduto.ListaTodos());
        }

        // GET: Produto/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            ProdutoRepository objProduto = new ProdutoRepository();

            return View(objProduto.Dados(id));
        }

        // GET: Produto/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Produto objProdutoIns)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    ProdutoRepository objProduto = new ProdutoRepository();

                    if (objProduto.Inserir(objProdutoIns) == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else {
                        ModelState.AddModelError("", "Ocorreu um erro ao cadastrar o produto.");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Não foi possível cadastrar o produto.");
            }

            return View(objProdutoIns);
        }

        // GET: Produto/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            ProdutoRepository objProduto = new ProdutoRepository();
            return View(objProduto.Dados(id));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Produto objProdutoEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProdutoRepository objProduto = new ProdutoRepository();

                    if (objProduto.Alterar(objProdutoEdit) == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else {
                        ModelState.AddModelError("", "Ocorreu um erro ao alterar o produto.");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Não foi possível alterar o produto.");
            }

            return View(objProdutoEdit);
        }

        // GET: Produto/Delete/5
        [Authorize]
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Produto/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                ProdutoRepository objProduto = new ProdutoRepository();
                if(objProduto.Apagar(id) == 1)
                {
                    return RedirectToAction("Index");
                }
                else {
                    ModelState.AddModelError("", "Ocorreu um erro ao apagar o produto.");
                }

            }
            catch
            {
                ModelState.AddModelError("", "Não foi possível apagar o produto.");
            }

            return RedirectToAction("Index");
        }
    }
}
