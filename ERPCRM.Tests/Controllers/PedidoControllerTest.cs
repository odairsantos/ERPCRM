using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERPCRM;
using ERPCRM.Controllers;
using ERPCRM.Models;

namespace ERPCRM.Tests.Controllers
{
    public class PedidoControllerTest
    {
        [TestMethod]
        public void TestDetailsViewData()
        {
            var controller = new PedidoController();
            var result = controller.Details(1) as ViewResult;
            var pedido = (Pedido)result.ViewData.Model;
            Assert.AreEqual("Teste", pedido.nomeUsuario);
        }
    }
}
