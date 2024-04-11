using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for Update_DeleteUC.xaml
    /// </summary>
    public partial class UpdateUC : UserControl
    {
        private User user;
        private ServiceSurfClient myServiceSurf;
        private bool passOK, currentpassOK, emailOK;
        public UpdateUC(User user)
        {
            InitializeComponent();
            this.user = user;
            myServiceSurf = new ServiceSurfClient();
            passOK = emailOK = currentpassOK = false;
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidEmail validEmail = new ValidEmail();
            ValidationResult result = validEmail.Validate(tbEmail.Text, CultureInfo.CurrentCulture);

            if (result.IsValid)
            {
                tbEmail.BorderBrush = Brushes.Transparent;
                tbEmail.BorderThickness = new Thickness(0);
                tbEmail.ToolTip = null;
                emailOK = true;
            }
            else
            {
                tbEmail.BorderBrush = Brushes.Red;
                tbEmail.BorderThickness = new Thickness(2);
                tbEmail.ToolTip = result.ErrorContent.ToString();
                emailOK = false;
            }
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword validPassword = new ValidPassword();
            ValidationResult result = validPassword.Validate(pbPassword.Password, CultureInfo.CurrentCulture);
            if (result.IsValid)
            {
                pbPassword.BorderBrush = Brushes.Transparent;
                pbPassword.BorderThickness = new Thickness(0);
                pbPassword.ToolTip = null;
                passOK = true;
            }
            else
            {
                pbPassword.BorderBrush = Brushes.Red;
                pbPassword.BorderThickness = new Thickness(2);
                pbPassword.ToolTip = result.ErrorContent.ToString();
                passOK = false;
            }
        }

        private void pbCurrentPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword validPassword = new ValidPassword();
            ValidationResult result = validPassword.Validate(pbPassword.Password, CultureInfo.CurrentCulture);
            if (result.IsValid)
            {
                pbCurrentPassword.BorderBrush = Brushes.Transparent;
                pbCurrentPassword.BorderThickness = new Thickness(0);
                pbCurrentPassword.ToolTip = null;
                currentpassOK = true;
            }
            else
            {
                pbCurrentPassword.BorderBrush = Brushes.Red;
                pbCurrentPassword.BorderThickness = new Thickness(2);
                pbCurrentPassword.ToolTip = result.ErrorContent.ToString();
                currentpassOK = false;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbEmail.Text = pbCurrentPassword.Password = pbPassword.Password = string.Empty;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            User userold = user;
            if (!passOK || !emailOK || !currentpassOK)
            {
                MessageBox.Show("Something is wrong, Check your data", "No", MessageBoxButton.OK);
                return;
            }
            if (!myServiceSurf.IsEmailFree(tbEmail.Text))
            {
                MessageBox.Show("Email is in use", "No", MessageBoxButton.OK);
                return;
            }
            if (!pbCurrentPassword.Password.Equals(user.Password))
            {
                MessageBox.Show("CUrrent password is incurrect!", "No", MessageBoxButton.OK);
                return;
            }
            user.Email = tbEmail.Text;
            user.Password = pbPassword.Password;
            myServiceSurf.UpdateUser(user);
            MessageBox.Show("Changes saved", "OK", MessageBoxButton.OK);
        }
    }
}
