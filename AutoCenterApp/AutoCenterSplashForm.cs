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
/// Last modified: February 11, 2017
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
    /// BONUS: A splash form to welcome users. 
    /// AutoCenterSplashForm class inheriting from Form class
    /// </summary>
    public partial class AutoCenterSplashForm : Form
    {
        /// <summary>
        /// AutoCenterSplashForm constructor, initializing splash form components
        /// </summary>
        public AutoCenterSplashForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// SharpFormTimer tick event handler, to control intervals
        /// </summary>
        /// <param name="sender">SharpFormTimer object passed as 'Object' type</param>
        /// <param name="e">Tick event arguments</param>
        private void SplashFormTimer_Tick(object sender, EventArgs e)
        {
            // instantiate SharpAutoForm
            SharpAutoForm myAutoForm = new SharpAutoForm();
            // passing the reference to the current form
            myAutoForm.parentSplashForm = this;
            // disable the timer
            this.SplashFormTimer.Enabled = false;
            myAutoForm.Show();
            this.Hide(); // hide the splash form
        }
    }
}
