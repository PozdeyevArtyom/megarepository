using System;
using System.Collections.Generic;
using Entities;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BLL;

namespace Task13.Models
{
    public class OrderModel
    {
        public int ID { get; set; } = 0;
        
        [Required(ErrorMessage = "Обязательное поле.")] 
        [RegularExpression(@"^[A-Z]{5}$", ErrorMessage = "Неверный ввод. Пример: WELLI.")]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(1, Logic.a, ErrorMessage = "Несуществующий идентификатор сотрудника")]
        public int EmployeeID { get; set; }

        [RegularExpression(@"^((0[13578]|1[02])-([0-2][0-9]|3[01])-[0-9]{4})|"
                          + "((0[469]|11)-([0-2][0-9]|30)-[0-9]{4})|"
                          + "(02-([01][0-9]|2[0-8])-[0-9]{4})$", ErrorMessage = "Неверный ввод.")]
        public string OrderDate { get; set; }

        [RegularExpression(@"^((0[13578]|1[02])-([0-2][0-9]|3[01])-[0-9]{4})|"
                          + "((0[469]|11)-([0-2][0-9]|30)-[0-9]{4})|"
                          + "(02-([01][0-9]|2[0-8])-[0-9]{4})$", ErrorMessage = "Неверный ввод.")]
        public string RequiredDate { get; set; }

        [RegularExpression(@"^((0[13578]|1[02])-([0-2][0-9]|3[01])-[0-9]{4})|"
                          + "((0[469]|11)-([0-2][0-9]|30)-[0-9]{4})|"
                          + "(02-([01][0-9]|2[0-8])-[0-9]{4})$", ErrorMessage = "Неверный ввод.")]
        public string ShippedDate { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Кол-во транзитных пунктов должно быть натуральным числом.")]
        public int ShipVia { get; set; } = 1;

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(0d, Double.MaxValue, ErrorMessage = "Вес должен быть положительным числом.")]
        public double Freight { get; set; } = 0.1;
        
        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage ="Наименование может быть длиной от 3 до 40 символов.")]
        public string ShipName { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Адрес может быть длиной от 5 до 60 символов.")]
        public string ShipAddress { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Город может быть длиной от 2 до 15 символов.")]
        public string ShipCity { get; set; } = "";

        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Страна может быть длиной от 3 до 15 символов.")]
        public string ShipCountry { get; set; } = "";

        [StringLength(15, MinimumLength = 0, ErrorMessage = "Регион может быть длиной до 15 символов.")]
        public string ShipRegion { get; set; } = "";

        [StringLength(10, MinimumLength = 0, ErrorMessage = "Почтовый индекс может быть длиной до 10 символов.")]
        public string ShipPostalCode { get; set; } = "";
        public List<OrderDetailsModel> Details { get; set; } = new List<OrderDetailsModel>();
        public double Price { get; set; } = 0;
        public OrderStatus Status = OrderStatus.New;

        public OrderModel() { }

        public OrderModel(Order order)
        {
            ID = order.ID;
            CustomerID = order.CustomerID;
            EmployeeID = order.EmployeeID;

            if (!order.OrderDate.Equals(DateTime.MinValue))
                OrderDate = order.OrderDate.ToShortDateString();
            else
                OrderDate = "Не указана";

            if (!order.RequiredDate.Equals(DateTime.MinValue))
                RequiredDate = order.RequiredDate.ToShortDateString();
            else
                RequiredDate = "Не указана";

            if (!order.ShippedDate.Equals(DateTime.MinValue))
                ShippedDate = order.ShippedDate.ToShortDateString();
            else
                ShippedDate = "Не указана";

            ShipVia = order.ShipVia;
            Freight = order.Freight;
            ShipName = order.ShipName;
            ShipAddress = order.ShipAddress;
            ShipCity = order.ShipCity;
            ShipRegion = order.ShipRegion;
            ShipPostalCode = order.ShipPostalCode;
            ShipCountry = order.ShipCountry;
            foreach (OrderDetails od in order.Details)
            {
                Details.Add(new OrderDetailsModel(order.ID, od));
                Price += od.Quantity * (od.UnitPrice * (1 - od.Discount));
            }            
            Status = order.Status;
        }
    }
}