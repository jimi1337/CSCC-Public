using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimplePizza
{
    enum PizzaOrderStatus { Success, MinimumAmountNotMet, DeliveryFeeAdded }
    class PizzaShop
    {
        private string customerPhoneNumber;
        private string customerName;
        private List<decimal> orderHistory;
        private decimal orderAmount;
        private Pizza pizzaToOrder = null;
        private decimal numberOfBreadsticks = 0;

        private const decimal BREADSTICK_ORDER_COST = 2;
        private const decimal MINIMUM_ORDER_AMT = 11;
        private const decimal DELIVERY_FEE_THRESHHOLD = 15;
        private const decimal DELIVERY_FEE_AMT = 4;

        const string path = "../orderHistory.txt";

        static FileStream historyFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        StreamReader reader = new StreamReader(historyFile);

        public PizzaShop(string customerPhoneNumber, string customerName)
        {
            this.customerPhoneNumber = customerPhoneNumber;
            this.customerName = customerName;
            orderAmount = 0;
            orderHistory = new List<decimal>();

            decimal currentItem;
            while (!reader.EndOfStream)
            {
                if(decimal.TryParse(reader.ReadLine(), out currentItem))
                {
                    orderHistory.Add(currentItem);
                }
            }
            reader.Close();
        }

        public string CustomerPhoneNumber
        {
            get
            {
                return customerPhoneNumber;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }
        }
        public Pizza PizzaToOrder
        {
            set
            {
                pizzaToOrder = value;
            }
            get
            {
                return pizzaToOrder;
            }
        }

        public void SetNumberOfBreadsticks(int breadsticksToOrder)
        {
            numberOfBreadsticks = breadsticksToOrder;
        }

        public decimal CalcTotalOrder()
        {
            decimal orderAmt = 0;

            if (pizzaToOrder != null)
            {
                orderAmt += pizzaToOrder.CalcCost();
            }

            orderAmt += numberOfBreadsticks * BREADSTICK_ORDER_COST;
            return orderAmt;
        }

        public PizzaOrderStatus PlaceOrder()
        {
            decimal totalOrder = CalcTotalOrder();
            if (totalOrder  < MINIMUM_ORDER_AMT)
            {
                return PizzaOrderStatus.MinimumAmountNotMet;
            }

            if (totalOrder < DELIVERY_FEE_THRESHHOLD)
            {
                totalOrder += DELIVERY_FEE_AMT;
                orderAmount = totalOrder;
                orderHistory.Add(orderAmount);
                return PizzaOrderStatus.DeliveryFeeAdded;
            }

            orderAmount = totalOrder;
            orderHistory.Add(orderAmount);
            return PizzaOrderStatus.Success;
        }

        public List<decimal> GetOrderHistory()
        {
            return orderHistory;
        }

        public decimal GetTotalCostOfOrder()
        {
            return orderAmount;
        }

        public decimal calcEstimate(decimal inCost)
        {
            return inCost + pizzaToOrder.CalcCost();
        }
    }

}
