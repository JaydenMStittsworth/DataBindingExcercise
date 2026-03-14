

using System.ComponentModel;

#pragma warning disable WFO1000

namespace DataBindingExcercise
{
    public partial class ContactForm : Form
    {
        // This is a more specific version of a list
        public BindingList<Contact> Contacts { get; set; }

        // This is responsible for what happens as events trigger in the Contacts property
        private BindingSource contactBindingSource;

        public ContactForm()
        {
            InitializeComponent();

            // create an empty Contacts list
            Contacts = new BindingList<Contact>();

            // create a new BindingSource
            contactBindingSource = new BindingSource();

            // connects the contactBindingSource to Contacts
            contactBindingSource.DataSource = Contacts;

            // event handler method that calls when the list is changed
            contactBindingSource.ListChanged += ContactBindingSource_ListChanged;
        }

        private void ContactBindingSource_ListChanged(object? sender, ListChangedEventArgs e)
        {
            // code for item being added to the list
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var contact = Contacts[e.NewIndex];
                AddContactControl(contact);
            }
            // code for item being removed from the list
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                // removes item from the list
                fpContacts.Controls.Clear();

                // refreshes the list
                foreach (var contact in Contacts)
                {
                    AddContactControl(contact);
                }
            }
        }

        // defines what AddContactControl is
        private void AddContactControl(Contact contact)
        {
            var item = new ContactControl(contact);
            fpContacts.Controls.Add(item);
        }

        public void AddContact(
            string first,
            string last,
            string? phone = null,
            string? email = null
            )
        {
            Contacts.Add(new Contact
            {
                FirstName = first,
                LastName = last,
                PhoneNumber = phone,
                Email = email
            });
        }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // validate the data, pull a ripcord if it fails validation
            if (txtFirstName.Text.Length <= 0) // check if first name does not contains data
            {
                // no text, fail
                MessageBox.Show("First name must contain at least one letter.");
                txtFirstName.Focus();
                return;
            }

            // validate the data, pull a ripcord if it fails validation
            if (txtLastName.Text.Length <= 0) // check if last name does not contains data
            {
                // no text, fail
                MessageBox.Show("Last name must contain at least one letter.");
                txtLastName.Focus();
                return;
            }

            // validate the data, pull a ripcord if it fails validation
            if (txtEmail.Text.Length <= 0 && txtPhone.Text.Length <= 0) // check if email does not contains data 
            {
                // no text, fail
                MessageBox.Show("You must have either a phone number or a contact for this customer.");
                txtPhone.Focus();
                return;
            }

            // create a new contact
            Contact newContact = new Contact
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhone.Text,
                Email = txtEmail.Text
            };

            // add it to the list
            Contacts.Add(newContact);

            // pull the lever to update
            ClearForm();
            txtFirstName.Focus();
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            ClearForm();
            txtFirstName.Focus();
        }
    }
}
