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
using System.Windows.Shapes;
using System.Xml;
using WpfClientReef.SurfServiceReference;


namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ServiceSurfClient myServiceSurf;
        private bool passOK, emailOK;
        public LoginWindow()
        {
            InitializeComponent();
            myServiceSurf = new ServiceSurfClient();
            passOK=emailOK = false;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            UserWindow w = new UserWindow(new User { ID=1});
            this.Hide();
            w.ShowDialog();
            tbEmail.Text = pbPassword.Password = string.Empty;
            this.Show();


            if (!passOK || !emailOK)
            {
                MessageBox.Show("Something is wrong, Check your data", "No", MessageBoxButton.OK);
                return;
            }
            User user = new User { Email = tbEmail.Text, Password = pbPassword.Password };
            user = myServiceSurf.LogIn(user);
            if (user == null)
            {
                MessageBox.Show("Email or password don`t system data", "No", MessageBoxButton.OK);
                return;
            }
            UserWindow userWindow = new UserWindow(user);
            this.Hide();
            userWindow.ShowDialog();
            tbEmail.Text = pbPassword.Password = string.Empty;
            this.Show();
        }

        private void Sighnup_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerw = new RegisterWindow();
            this.Close();
            registerw.ShowDialog();
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
    }
}
