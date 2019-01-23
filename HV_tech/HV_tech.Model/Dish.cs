using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HV_tech.Model
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Enums.DishType DishType { get; set; }
        public bool IsAbleMultipleTimes { get; set; }

        public Dish()
        {
        }

        public Dish(int id, string name, Enums.DishType type, bool multTimes = false)
        {
            this.Id = id;
            this.Name = name;
            this.DishType = type;
            this.IsAbleMultipleTimes = multTimes;
        }

        public static IList<Dish> GetDishes()
        {
            var dishList = new List<Dish>
            {
                new Dish(1, "eggs", Enums.DishType.morning),
                new Dish(2, "toast", Enums.DishType.morning),
                new Dish(3, "coffee", Enums.DishType.morning, true),
                new Dish(1, "steak", Enums.DishType.night),
                new Dish(2, "potato", Enums.DishType.night, true),
                new Dish(3, "wine", Enums.DishType.night),
                new Dish(4, "cake", Enums.DishType.night)
            };

            return dishList;
        }

        public static IList<Dish> GetDisherByType(Enums.DishType type)
        {
            return GetDishes().Where(x => x.DishType.Equals(type)).ToList();
        }
    }


}
