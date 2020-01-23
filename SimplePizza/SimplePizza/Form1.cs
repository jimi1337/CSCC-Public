using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimplePizza
{
    public partial class Form1 : Form
    {
        Pizza pizza = new Pizza();
        PizzaShop pizzaShop= new PizzaShop("(555) 867-5309", "Peter Lemonjello");
        const string path = "../orderHistory.txt";

        FileStream historyFile;


        public Form1()
        {
            InitializeComponent();
            pizzaShop.PizzaToOrder = pizza;

            UpdateCostEstimate();

            radThick.Checked = pizza.Crust.Equals(PizzaCrust.thick);
            radThin.Checked = pizza.Crust.Equals(PizzaCrust.thin);


        }
        private void UpdateCostEstimate()
        {
            decimal costEstimate = 0;

            costEstimate += pizzaShop.CalcTotalOrder();
            txtCostEstimate.Text = costEstimate.ToString("C");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtName.Text = pizzaShop.CustomerName;
            txtPhone.Text = pizzaShop.CustomerPhoneNumber;

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void RadThin_CheckedChanged(object sender, EventArgs e)
        {
            pizza.Crust = PizzaCrust.thin;
            UpdateCostEstimate();
        }

        private void RadThick_CheckedChanged(object sender, EventArgs e)
        {
            pizza.Crust = PizzaCrust.thick;
            UpdateCostEstimate();
        }

        private void ChkPepperoni_CheckedChanged(object sender, EventArgs e)
        {
            pizza.pepperoni = chkPepperoni.Checked;
            UpdateCostEstimate();
        }

        private void ChkMushrooms_CheckedChanged(object sender, EventArgs e)
        {
            pizza.mushrooms = chkMushrooms.Checked;
            UpdateCostEstimate();
        }

        private void ChkPineapple_CheckedChanged(object sender, EventArgs e)
        {
            pizza.pineapple = chkPineapple.Checked;
            UpdateCostEstimate();
        }

        private void UpdBreadsticks_ValueChanged(object sender, EventArgs e)
        {
            int cost = (int)updBreadsticks.Value;
            pizzaShop.SetNumberOfBreadsticks(cost);
            UpdateCostEstimate();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            switch(pizzaShop.PlaceOrder())
            {
                case PizzaOrderStatus.Success:
                    txtStatus.Text = "Successful order. Cost = " + pizzaShop.GetTotalCostOfOrder().ToString("C");
                    break;

                case PizzaOrderStatus.MinimumAmountNotMet:
                    txtStatus.Text = "Order not made. Minimum amount not met";
                    break;

                case PizzaOrderStatus.DeliveryFeeAdded:
                    txtStatus.Text = "Successful order. Delivery Fee Added. Cost = " + pizzaShop.GetTotalCostOfOrder().ToString("C");
                    break;

            }

            historyFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamWriter writer = new StreamWriter(historyFile);

            foreach (decimal order in pizzaShop.GetOrderHistory())
            {
                writer.WriteLine(order);
            }

            writer.Flush();
            writer.Close();

        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            lstHistory.Items.Clear();
            foreach(decimal d in pizzaShop.GetOrderHistory())
            {
                lstHistory.Items.Add(d.ToString("C"));
            }
        }

        private void TxtCostEstimate_TextChanged(object sender, EventArgs e)
        {

        }

        private void RbtnSmall_CheckedChanged(object sender, EventArgs e)
        {
            pizza.SizeOfPizza = PizzaSize.small;
            UpdateCostEstimate();
        }

        private void RbtnMedium_CheckedChanged(object sender, EventArgs e)
        {
            pizza.SizeOfPizza = PizzaSize.medium;
            UpdateCostEstimate();
        }

        private void RbtnLarge_CheckedChanged(object sender, EventArgs e)
        {
            pizza.SizeOfPizza = PizzaSize.large;
            UpdateCostEstimate();
        }
    }
}
