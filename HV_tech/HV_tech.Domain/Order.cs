using HV_tech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static HV_tech.Model.Enums;

namespace HV_tech.Domain
{
    public class Order
    {
        #region Properties
        private string _initialOrder;
        private List<Dish> _items;
        private string _message;
        #endregion

        #region Constructor
        public Order(string value)
        {
            _initialOrder = value;
            _message = string.Empty;
        }
        #endregion

        #region Public Methods
        public string GetOrderText()
        {
            return _message;
        }

        public void CreateOrder()
        {
            try
            {
                var order = _initialOrder.Split(',');

                if (isValidInput(order))
                {
                    DishType dishType = GetDishType(order[0]);

                    var orderListed = BuildOrder(order);

                    _items = GetDishListByOrder(orderListed, dishType);

                    CheckRepeatedValues();
                }
            }
            catch (Exception ex)
            {
                _message += "error";
            }
        }
        #endregion

        #region Private Methods

        private static DishType GetDishType(string order)
        {
            return (DishType)Enum.Parse(typeof(DishType), order, true);
        }

        private List<Dish> GetDishListByOrder(List<int> lstOrder, DishType type)
        {
            var result = new List<Dish>();

            var lstDish = Dish.GetDisherByType(type);

            lstOrder.ForEach(x => result.Add(lstDish.SingleOrDefault(d => d.Id == x)));

            return result;
        }

        private string GetDishTextByList(List<int> lstOrder, DishType type)
        {
            var dishesList = Dish.GetDisherByType(type);
            var namesList = new List<string>();

            foreach (var item in lstOrder)
            {
                if (dishesList.SingleOrDefault(x => x.Id == item) != null)
                    namesList.Add(dishesList.SingleOrDefault(x => x.Id == item).Name);
                else
                    namesList.Add("error");
            }

            return String.Join(",", namesList);
        }

        private List<int> BuildOrder(string[] order)
        {
            var lstItens = new List<int>();

            for (int i = 1; i < order.Length; i++)
            {
                if (int.TryParse(order[i], out int id))
                    lstItens.Add(id);
            }
            lstItens.Sort();

            return lstItens;
        }

        private bool isValidInput(string[] order)
        {
            if (order.Length < 2)
            {
                _message = "Invalid input. Please, redo your order.";
                return false;
            }

            return Enum.IsDefined(typeof(DishType), order[0]);
        }

        private void CheckRepeatedValues()
        {
            var dishesCount = new Dictionary<Dish, int>();

            foreach (var item in _items)
            {
                if (item == null)
                {
                    dishesCount.Add(new Dish(), 0);
                    break;
                }

                if (dishesCount.ContainsKey(item))
                    dishesCount[item]++;
                else
                    dishesCount.Add(item, 1);
            }

            var aux = new List<string>();
            foreach (var item in dishesCount)
            {
                if (item.Value == 0)
                {
                    aux.Add("error");
                    break;
                }
                if (item.Value > 1)
                {
                    if (item.Key.IsAbleMultipleTimes)
                        aux.Add(item.Key.Name + "(x" + item.Value + ")");
                    else
                    {
                        aux.Add(item.Key.Name);
                        aux.Add("error");
                        break;
                    }
                }
                else
                    aux.Add(item.Key.Name);
            }

            _message = String.Join(", ", aux);
        }

        #endregion
    }
}
