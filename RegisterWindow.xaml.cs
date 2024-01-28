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
using System.Xml;
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        User user;
        bool passOK;
        ServiceSurfClient serviceSurf;
        public RegisterWindow()
        {
            InitializeComponent();
            user = new User();
            this.DataContext = user;
            passOK=false;   
        }
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (user.Password == null || user.FirstName == null || 
                user.LastName == null || user.Birthday == null || 
                user.Phonenum == null || user.Email == null)
            {
                MessageBox.Show("Your Not good\nFill the missing data", "NOT OK", MessageBoxButton.OK);
                return;
            }
            if (Validation.GetHasError(tbFname) || Validation.GetHasError(tbLname) || 
                Validation.GetHasError(tbBirthYear) || Validation.GetHasError(tbPhonenum) || 
                Validation.GetHasError(tbEmail) || !passOK)
            {                
                MessageBox.Show("Your Not good\nFix the data", "OK", MessageBoxButton.OK);
                return;
            }
            serviceSurf = new ServiceSurfClient();
           if (!serviceSurf.IsEmailFree(user.Email))
            {
                MessageBox.Show("Email is in my system\nTry a different email", "OK", MessageBoxButton.OK);
                return;
            }
            if(serviceSurf.InsertUser(user)==1)
            {
                MessageBox.Show("Wellcom!", "OK", MessageBoxButton.OK);
              this.Close();
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = user = new User();

        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
