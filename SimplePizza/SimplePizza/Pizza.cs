using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePizza
{
    enum PizzaCrust  { thin, thick };
    enum PizzaSize { small, medium, large };
    class Pizza
    {
        public bool pepperoni;
        public bool mushrooms;
        public bool pineapple;
        private PizzaCrust crust;
        private PizzaSize pizzaSize;


        private decimal cost;

        private const int COST_SMALL = 8;
        private const int COST_MEDIUM = 10;
        private const int COST_LARGE = 12;

        private const int COST_PER_TOPPING = 2;
        public Pizza()
        {
            pizzaSize = PizzaSize.medium;
            pepperoni = false;
            mushrooms = false;
            pineapple = false;

            Crust = PizzaCrust.thick;
        }

        public PizzaSize SizeOfPizza
        {
            get
            {
                return pizzaSize;
            }
            set
            {
                pizzaSize = value;
            }
        }

        public PizzaCrust Crust
        {
            get
            {
                return crust;
            }
            set
            {
                crust = value;
            }
        }
        public decimal CalcCost()
        {
            cost = BaseCost();
            cost += ToppingCost();

            return cost;           
        }

        private decimal ToppingCost()
        {
            decimal toppings = 0;
            toppings += mushrooms ? COST_PER_TOPPING : 0;
            toppings += pepperoni ? COST_PER_TOPPING : 0;
            toppings += pineapple ? COST_PER_TOPPING : 0;

            return toppings;
        }
        private decimal BaseCost()
        {
            decimal baseCost=-1;
            switch (pizzaSize)
            {
                case PizzaSize.small:
                    baseCost = COST_SMALL;
                    break;
                case PizzaSize.medium:
                    baseCost = COST_MEDIUM;
                    break;
                case PizzaSize.large:
                    baseCost = COST_LARGE;
                    break;
            }
            baseCost += crust.Equals(PizzaCrust.thick) ? 0.5m : 0m;
            return baseCost;
        }

        
    }

    
}
