using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using BLL;
using System.ComponentModel.DataAnnotations;

namespace Task13.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //информация о всех заказах
        public ActionResult ShowOrders()
        {
            ViewBag.Message = "Список всех заказов. Кликните на ID заказа для получения полной информации.";
            List<Models.OrderModel> list = new List<Models.OrderModel>();
            foreach (Order o in Logic.GetAll())
                list.Add(new Models.OrderModel(o));
            return View(list);
        }

        //детальная информация
        public ActionResult FullInfo(int id)
        {
            Order o = Logic.GetByID(id);
            if (o == null)
                return new HttpStatusCodeResult(404);
            else
                return View(new Models.OrderModel(o));
        }

        //добавление заказа
        public ActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(Models.OrderModel ordermodel)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order();
                order.CustomerID = ordermodel.CustomerID;
                order.EmployeeID = ordermodel.EmployeeID;
                order.Status = OrderStatus.Shipped;

                if (!string.IsNullOrEmpty(ordermodel.ShippedDate))
                    order.ShippedDate = new DateTime(int.Parse(ordermodel.OrderDate.Substring(6, 4)),
                                                   int.Parse(ordermodel.OrderDate.Substring(0, 2)),
                                                   int.Parse(ordermodel.OrderDate.Substring(3, 2)));
                else
                    order.Status = OrderStatus.NotShipped;

                if (!string.IsNullOrEmpty(ordermodel.OrderDate))
                    order.OrderDate = new DateTime(int.Parse(ordermodel.OrderDate.Substring(6, 4)),
                                                   int.Parse(ordermodel.OrderDate.Substring(0, 2)),
                                                   int.Parse(ordermodel.OrderDate.Substring(3, 2)));

                if (!string.IsNullOrEmpty(ordermodel.RequiredDate))
                    order.RequiredDate = new DateTime(int.Parse(ordermodel.OrderDate.Substring(6, 4)),
                                                   int.Parse(ordermodel.OrderDate.Substring(0, 2)),
                                                   int.Parse(ordermodel.OrderDate.Substring(3, 2)));
                else
                    order.Status = OrderStatus.New;
                
                order.ShipVia = ordermodel.ShipVia;
                order.Freight = ordermodel.Freight;
                order.ShipName = ordermodel.ShipName;
                order.ShipAddress = ordermodel.ShipAddress;
                order.ShipCity = ordermodel.ShipCity;
                order.ShipCountry = ordermodel.ShipCountry;
                order.ShipRegion = ordermodel.ShipRegion;
                order.ShipPostalCode = ordermodel.ShipPostalCode;

                try
                {
                    return RedirectToAction("FullInfo", "Home", new { id = Logic.AddOrderNoDetails(order) });
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(e.ParamName, "Несуществующий идендификатор.");
                    return View(ordermodel);
                }
            }
            else
                return View(ordermodel);
        }

        //добавление деталей
        public ActionResult AddDetail(int orderid)
        {            
            return View(new Models.OrderDetailsModel(orderid));
        }

        [HttpPost]
        public ActionResult AddDetail(Models.OrderDetailsModel orderdetail)
        {
            if(ModelState.IsValid)
            {
                OrderDetails Detail = new OrderDetails(new Product(orderdetail.ProductID),
                                                       orderdetail.UnitPrice,
                                                       orderdetail.Quantity,
                                                       orderdetail.Discount);
                Logic.AddDetails(orderdetail.OrderID, Detail);
                return RedirectToAction("FullInfo", "Home", new { id = orderdetail.OrderID });
            }
            else
                return View(orderdetail);
        }
    }
}