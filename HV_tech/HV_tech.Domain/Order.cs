using HV_tech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static HV_tech.Model.Enums;

namespace HV_tech.Domain
{
    public class Order
    {
        private string initialOrder;
        private string message;

        public Order(string value)
        {
            initialOrder = value;
        }

        public void CreateOrder()
        {
            try
            {
                var order = initialOrder.Split(',');

                if (isValidInput(order))
                {
                    var dishType = (DishType)Enum.Parse(typeof(DishType), order[0], true);
                    var lstOrder = BuildOrder(order);
                    message = GetDishTextByList(lstOrder, dishType);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        public string GetDishTextByList(List<int> lstOrder, DishType type)
        {
            var dishesList = Dish.GetDisherByType(type);
            var namesList = new List<string>();

            foreach (var item in lstOrder)
                namesList.Add(dishesList.SingleOrDefault(x=>x.Id == item).Name);

            return String.Join(",", namesList);
        }

        private List<int> BuildOrder(string[] order)
        {
            var lstItens = new List<int>();

            for (int i = 1; i < order.Length; i++)
            {
                int id = 0;
                if (int.TryParse(order[i], out id))
                    lstItens.Add(id);
                else
                {
                    message = "error";
                    //throw new Exception()
                }
                    
            }
            lstItens.Sort();

            return lstItens;
        }

        private bool isValidInput(string[] order)
        {
            if (order.Length < 2)
            {
                message = "Invalid input. Please, redo your order.";
                return false;
            }

            return Enum.IsDefined(typeof(DishType), order[0]);
        }

        public string GetOrderText()
        {
            return message;
        }
    }
}
