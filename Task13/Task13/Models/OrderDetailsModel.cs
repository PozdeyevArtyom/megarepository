using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace Task13.Models
{
    public class OrderDetailsModel
    {
        [Required(ErrorMessage = "Обязательное поле.")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(1, 77, ErrorMessage = "Несуществующий идентификатор.")]
        public int ProductID { get; set; }
        
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(0d, Double.MaxValue, ErrorMessage = "Цена должна быть положительным числом.")]
        public double UnitPrice { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Количество товара должно быть натуральным числом.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(0d, 1d, ErrorMessage = "Скидка может быть числом в пределах от 0 до 1.")]
        public double Discount { get; set; }

        public OrderDetailsModel() { }

        public OrderDetailsModel(int id)
        {
            OrderID = id;
        }

        public OrderDetailsModel(int id, OrderDetails od)
        {
            OrderID = id;
            ProductID = od.product.ID;
            ProductName = od.product.Name;
            UnitPrice = od.UnitPrice;
            Quantity = od.Quantity;
            Discount = od.Discount;
        }
    }
}