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

        private bool _onClear;
        private string _exteriorFinnishChosen;
        private bool _inputsAreValid;

        public SharpAutoForm()
        {   
            InitializeComponent();
            this._inputsAreValid = false; // initially set false cause no values entered yet
            this._onClear = false;
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

                    // clear button is pressed
                    /// if not set, selecting and unselecting checkboxes and radio 
                    /// buttons may add or diduct values from the additional options cost
                    this._onClear = true;

                    // clearing all values from text boxes
                    this.BasePriceTextBox.Text = "";
                    this.AdditionalOptionsTextBox.Text = "0";
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
                    
                    this._onClear = false;
                    break;

                case "exit":
                    this.Close();
                    break;

            }

        }

        private void _textBoxesEventHandler(object sender, EventArgs e)
        {
            TextBox AutoFormTextBox = sender as TextBox;

            switch (AutoFormTextBox.Tag.ToString())
            {
                case "BasePrice":
                    double basePrice = 0;
                    // check if value of base price is parsable to a double
                    if (Double.TryParse(this.BasePriceTextBox.Text, out basePrice))
                    {
                        this._inputsAreValid = true; // if value is valid
                    }
                    else
                    {
                        // if the field is not empty
                        if (!this.BasePriceTextBox.Text.Equals(""))
                        {
                            this._displayError("Please enter valid amount for the base price", "Invalid Inputs");
                            // remove the last entered invalid character
                            this.BasePriceTextBox.Text = (this.BasePriceTextBox.Text.Length != 0) ? this.BasePriceTextBox.Text.Remove(this.BasePriceTextBox.Text.Length - 1) : "";
                            // set the caret to the end of textbox
                            BasePriceTextBox.SelectionStart = BasePriceTextBox.Text.Length;
                        }

                        this._inputsAreValid = false; // if value is invalid

                    }
                    break;

                case "TradeInAllowance":
                    double tradeInAllowance = 0;
                    // check if value entered in Trad-In Allowance field is a valid number
                    if (Double.TryParse(this.TradeInAllowanceTextBox.Text, out tradeInAllowance))
                    {
                        this._inputsAreValid = true; // input value is valid
                    }
                    else
                    {
                        this._inputsAreValid = false;
                        this._displayError("Please enter valid amount for the trade-in allowance", "Invalid Inputs");
                        this.TradeInAllowanceTextBox.Text = (this.TradeInAllowanceTextBox.Text.Length != 0) ? this.TradeInAllowanceTextBox.Text.Remove(this.TradeInAllowanceTextBox.Text.Length - 1) : "0";
                        // set the caret to the end of textbox
                        TradeInAllowanceTextBox.SelectionStart = TradeInAllowanceTextBox.Text.Length;
                    }
                    break;
            }
            
        }

        private void _radioButtonsEventHandler(object sender, EventArgs e)
        {

            RadioButton AutoFormRadioButton = sender as RadioButton;

            if (!this._onClear)
            {

                switch (AutoFormRadioButton.Tag.ToString())
                {
                    case "Pearlized":
                        // if Perlized is selected the add the value to additional otions, else subtract it
                        this.AdditionalOptionsTextBox.Text = ((PearlizedOptionRadioButton.Checked) ? (Double.Parse(this.AdditionalOptionsTextBox.Text) + 345.72) : (Double.Parse(this.AdditionalOptionsTextBox.Text) - 345.72)).ToString();
                        break;
                    case "CustomizedDetailing":
                        // if Cutomized Detailing is selected the add the value to additional otions, else subtract it
                        this.AdditionalOptionsTextBox.Text = ((CustomizedDetailingOptionRadioButton.Checked) ? (Double.Parse(this.AdditionalOptionsTextBox.Text) + 599.99) : (Double.Parse(this.AdditionalOptionsTextBox.Text) - 599.99)).ToString();
                        break;
                }

            }

        }

        private void _checkBoxesEventHandler(object sender, EventArgs e)
        {
            CheckBox AutoFormCheckBox = sender as CheckBox;

            if (!this._onClear)
            {

                switch (AutoFormCheckBox.Tag.ToString())
                {
                    case "StereoSystem":

                        if (StereoSystemCheckBox.Checked)
                        {
                            this.AdditionalOptionsTextBox.Text = (Double.Parse(this.AdditionalOptionsTextBox.Text) + 425.76).ToString();
                        }
                        else
                        {
                            this.AdditionalOptionsTextBox.Text = (Double.Parse(this.AdditionalOptionsTextBox.Text) - 425.76).ToString();
                        }

                        break;
                    case "LeatherInterior":

                        if (LeatherInteriorCheckBox.Checked)
                        {
                            this.AdditionalOptionsTextBox.Text = (Double.Parse(this.AdditionalOptionsTextBox.Text) + 987.41).ToString();
                        }
                        else
                        {
                            this.AdditionalOptionsTextBox.Text = (Double.Parse(this.AdditionalOptionsTextBox.Text) - 987.41).ToString();
                        }

                        break;
                    case "ComputerNavigation":

                        if (ComputerNavigationCheckBox.Checked)
                        {
                            this.AdditionalOptionsTextBox.Text = (Double.Parse(this.AdditionalOptionsTextBox.Text) + 1741.23).ToString();
                        }
                        else
                        {
                            this.AdditionalOptionsTextBox.Text = (Double.Parse(this.AdditionalOptionsTextBox.Text) - 1741.23).ToString();
                        }

                        break;
                }

            }

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
