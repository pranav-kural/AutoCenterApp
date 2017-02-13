///<summary>
/// Georgian College - Computer Programmer
/// COMP 1004 - Rapid Application Development
/// Instructor: Tom Tsiliopoulos
/// 
/// Assignment 2: Sharp Auto Center
/// Description: Create a project that determines the total amount due for 
/// the purchase of a vehicle based	on accessories and options selected and 
/// a trade-in value (if any). The price of the car	will be	set by the user.
/// 
/// BONUS: Added a splash form
/// 
/// Author Name: Pranav Kural
/// Student Number: 200333253
/// 
/// Last modified: February 12, 2017
/// Trello Board: https://trello.com/b/vAgyZck7
/// 
/// Brief revision history:
/// Initial commit to add default .gitIgnore and .gitAttribute files.
/// .....
/// Added functionality for radio buttons
/// Optimised the check box event handler logic
/// Added commenting and documentaion support
/// </summary>


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
    /// <summary>
    /// SharpAutoForm Class inheriting from Form class
    /// </summary>
    public partial class SharpAutoForm : Form
    {
        /// <summary>
        /// True if all inputs are valid
        /// </summary>
        private bool _inputsAreValid;

        /// <summary>
        /// reference to the parent splash form
        /// </summary>
        public Form parentSplashForm;

        /// <summary>
        /// SharpAutoForm constructor, initializes the form components and instance variables
        /// </summary>
        public SharpAutoForm()
        {   
            InitializeComponent();
            this._inputsAreValid = false; // initially set false cause no values entered yet
        }

        /// <summary>
        /// Event handler for the form buttons - Caluclate, Clear and Exit
        /// </summary>
        /// <param name="sender">Button object associated with the event</param>
        /// <param name="e">Event arguments</param>
        private void _formButtonsEventHandler(object sender, EventArgs e)
        {
            // cast object received into Button
            Button AutoFormButton = sender as Button;
            
            // select which button has been clicked
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
                        this.AmountDueTextBox.Text = (Double.Parse(this.TotalTextBox.Text) - Double.Parse(this.TradeInAllowanceTextBox.Text)).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        this._displayError("Values entered are not valid. Please check and try again.", "Invalid Inputs");
                    }
                    break;

                case "clear":

                    // clearing all values from text boxes
                    this.BasePriceTextBox.Text = "";
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

                    // setting it after changing the checkboxes and radio buttons to avoid the change in value 
                    // cause by the events triggered on changing their Checked property
                    this.AdditionalOptionsTextBox.Text = "0";
                    break;

                case "exit":
                    // close the form

                    //  BONUS: Show's a Dialog form first to confirm if the user wants to close the form
                    DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        this.parentSplashForm.Close();
                        Application.Exit(); // terminate the application from background
                    }
                    break;

            }

        }

        /// <summary>
        /// Event handler for menu items
        /// </summary>
        /// <param name="sender">Menu item associated with the event</param>
        /// <param name="e">Event arguments</param>
        private void _menuStripEventHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            // select the menu item selected
            switch (menuItem.Tag.ToString())
            {
                case "exit":
                    this.ExitButton.PerformClick(); // uses the same exit button logic to exit the app
                    break;

                case "calculate":
                    this.CalucateButton.PerformClick(); // runs the calculate button logic
                    break;

                case "clear":
                    this.ClearButton.PerformClick(); // runs the clear button logic
                    break;

                case "font":
                    // show font dialog
                    AutoFormFontDialog.ShowDialog();
                    // setting the selected font to base price and amount due text boxes
                    this.BasePriceTextBox.Font = AutoFormFontDialog.Font;
                    this.AmountDueTextBox.Font = AutoFormFontDialog.Font;
                    break;

                case "color":
                    // show the color dialog
                    AutoFormColorDialog.ShowDialog();
                    // setting the selected color to base price and amount due text boxes
                    this.BasePriceTextBox.ForeColor = AutoFormColorDialog.Color;
                    // for read only text boxes, need to reset the back color to set the new fore color
                    this.AmountDueTextBox.BackColor = this.AmountDueTextBox.BackColor;
                    this.AmountDueTextBox.ForeColor = AutoFormColorDialog.Color;
                    break;

                case "about":
                    // BONUS: creating an About form instead of just a message box

                    // instantiating a new about form
                    AboutForm aboutForm = new AboutForm();
                    aboutForm.ShowDialog(); // show a model about form
                    break;

            }
        }

        /// <summary>
        /// Event handler for text boxes, and to validate the inputs
        /// </summary>
        /// <param name="sender">Text Box object associated with the event</param>
        /// <param name="e">Event arguments</param>
        private void _textBoxesEventHandler(object sender, EventArgs e)
        {
            TextBox AutoFormTextBox = sender as TextBox;

            // select which text box's text has been changed
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
                        // if the field is not empty
                        if (!this.TradeInAllowanceTextBox.Text.Equals(""))
                        {
                            this._displayError("Please enter valid amount for the trade-in allowance", "Invalid Inputs");
                            this.TradeInAllowanceTextBox.Text = (this.TradeInAllowanceTextBox.Text.Length != 0) ? this.TradeInAllowanceTextBox.Text.Remove(this.TradeInAllowanceTextBox.Text.Length - 1) : "0";
                            // set the caret to the end of textbox
                            TradeInAllowanceTextBox.SelectionStart = TradeInAllowanceTextBox.Text.Length;
                        }

                        this._inputsAreValid = false;
                    }
                    break;
            }
            
        }

        /// <summary>
        /// Event handler for Radio Button
        /// </summary>
        /// <param name="sender">Radio Button object associated with the event</param>
        /// <param name="e">Event arguments</param>
        private void _radioButtonsEventHandler(object sender, EventArgs e)
        {
            // casting the sender object to Radio Button to access the Tag property
            RadioButton AutoFormRadioButton = sender as RadioButton;
            
                // selecting the radio button pressed
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

        /// <summary>
        /// Event handler for Check Boxes
        /// </summary>
        /// <param name="sender">Check Box object associated with the event</param>
        /// <param name="e">Event arguments</param>
        private void _checkBoxesEventHandler(object sender, EventArgs e)
        {
            CheckBox AutoFormCheckBox = sender as CheckBox;

            // select the check box clicked on
                switch (AutoFormCheckBox.Tag.ToString())
                {
                    case "StereoSystem":
                        // if check box checked than add the value to additional options cost, else subtract it
                        this.AdditionalOptionsTextBox.Text = ((StereoSystemCheckBox.Checked) ? (Double.Parse(this.AdditionalOptionsTextBox.Text) + 425.76) : (Double.Parse(this.AdditionalOptionsTextBox.Text) - 425.76)).ToString();
                        break;

                    case "LeatherInterior":
                        this.AdditionalOptionsTextBox.Text = ((LeatherInteriorCheckBox.Checked) ? (Double.Parse(this.AdditionalOptionsTextBox.Text) + 987.41) : (Double.Parse(this.AdditionalOptionsTextBox.Text) - 987.41)).ToString();
                        break;

                    case "ComputerNavigation":
                        this.AdditionalOptionsTextBox.Text = ((ComputerNavigationCheckBox.Checked) ? (Double.Parse(this.AdditionalOptionsTextBox.Text) + 1741.23) : (Double.Parse(this.AdditionalOptionsTextBox.Text) - 1741.23)).ToString();
                        break;
                }

        }

        /// <summary>
        /// Function Procedure to calculate and return the sales tax @ 13%
        /// </summary>
        /// <returns>sales tax on sub total</returns>
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
