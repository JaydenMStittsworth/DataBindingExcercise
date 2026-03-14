using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBindingExcercise
{
    public partial class ContactControl : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Contact ContactDetails { get; set; }

        public ContactControl(Contact contactDetails)
        {
            InitializeComponent();
            ContactDetails = contactDetails;

            // fill in all of the details on the control using the data binding
            lblFullName.DataBindings.Add("Text", ContactDetails, "FullName");
            lblAddress.DataBindings.Add("Text", ContactDetails, "Email");
            lblPhoneNumber.DataBindings.Add("Text", ContactDetails, "PhoneNumber");
        }

        private void btnMarkContacted_Click(object sender, EventArgs e)
        {
            // delete myself from the fpContacts panel on the base form
            var frm = this.Parent?.Parent as ContactForm;

            if (frm != null)
            {
                // delete contact from contacts list
                // data binding deletes control automatically
                frm.Contacts.Remove(ContactDetails);
            }
        }
    }
}
