using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCenterApp
{
    public partial class SharpAutoForm : Form
    {

        private List<String> _additionalItemsAdded;
        private string _exteriorFinnishChosen;
        private bool _inputsAreValid;

        public SharpAutoForm()
        {   
            InitializeComponent();
            this._inputsAreValid = false; // initially set false cause no values entered yet
        }

        private void _formButtonsEventHandler(object sender, EventArgs e)
        {
            Button AutoFormButton = sender as Button;
            
            switch (AutoFormButton.Tag.ToString())
            {

                case "calculate":
                    if (_inputsAreValid)
                    {
                        // 1. calculating the sub total
                        this.SubTotalTextBox.Text = (Double.Parse(this.BasePriceTextBox.Text) + Double.Parse(this.AdditionalOptionsTextBox.Text)).ToString();

                        // 2. using a function procedure to calculate and get the sales tax on sub total
                        this.SalesTaxTextBox.Text = this._calculateSalesTax().ToString();

                        // 3. displaying the total cost (sub total + sales tax)
                        this.TotalTextBox.Text = (Double.Parse(this.SubTotalTextBox.Text) + Double.Parse(this.SalesTaxTextBox.Text)).ToString();

                        // 4. calculate and display the amount due
                        this.AmountDueTextBox.Text = (Double.Parse(this.TotalTextBox.Text) - Double.Parse(this.TradeInAllowanceTextBox.Text)).ToString();
                    }
                    else
                    {
                        this._displayError("Values entered are not valid. Please check and try again.", "Invalid Inputs");
                    }
                    break;

                case "clear":
                    // clearing all values from text boxes
                    this.BasePriceTextBox.Text = "";
                    this.AdditionalOptionsTextBox.Text = "";
                    this.SubTotalTextBox.Text = "";
                    this.SalesTaxTextBox.Text = "";
                    this.TotalTextBox.Text = "";
                    this.TradeInAllowanceTextBox.Text = "0"; // setting it to default value of 0
                    this.AmountDueTextBox.Text = "";

                    // unchecking all the check boxes of additional items
                    this.StereoSystemCheckBox.Checked = false;
                    this.LeatherInteriorCheckBox.Checked = false;
                    this.ComputerNavigationCheckBox.Checked = false;

                    // setting Standard Option as default checked radio button for exterior finnish
                    this.StandardOptionRadioButton.Checked = true;
                    this.PearlizedOptionRadioButton.Checked = false;
                    this.CustomizedDetailingOptionRadioButton.Checked = false;
                    break;

                case "exit":
                    this.Close();
                    break;

            }

        }

        private void _textBoxesEventHandler(object sender, EventArgs e)
        {

        }

        private void _radioButtonsEventHandler(object sender, EventArgs e)
        {

        }

        private void _checkBoxesEventHandler(object sender, EventArgs e)
        {
            
        }

        private double _calculateSalesTax()
        {
            double subTotal;
            // return calculated sales tax if sub total amount valid, otherwise zero
            return (Double.TryParse(this.SubTotalTextBox.Text.ToString(), out subTotal) && subTotal != 0) ? (subTotal * 0.13) : 0;
        }

        /// <summary>
        /// Method to handle displaying of the errors using MessageBox
        /// </summary>
        /// <param name="message">Despcriptive error message</param>
        /// <param name="title">Title for the MessageBox displaying error</param>
        private void _displayError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
