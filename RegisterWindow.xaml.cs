using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private User user;
        private bool passOK;
        bool birthOK;
        private TypeSurfList surftypes;
        private ServiceSurfClient serviceSurf;
        private List<CheckBox> checkboxes;
        public RegisterWindow()
        {
            InitializeComponent();
            user = new User();
            this.DataContext = user;
            passOK = false;
            birthOK = false;
            checkboxes=new List<CheckBox>();
            LoadView();
        }
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {

            if (user.Password == String.Empty || user.FirstName == String.Empty ||
                user.LastName == String.Empty || user.Birthday == null ||
                user.Phonenum == String.Empty || user.Email == null || user.Surfslst != null)
            {
                MessageBox.Show("Your Not good\nFill the missing data", "NOT OK", MessageBoxButton.OK);
                return;
            }
            if (Validation.GetHasError(tbFname) || Validation.GetHasError(tbLname) 
                 || Validation.GetHasError(tbPhonenum) ||
               Validation.GetHasError(tbEmail) || !passOK || !birthOK)
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
            GetUserTypeSurf();
            if (serviceSurf.InsertUser(user) == 1)
            {
                MessageBox.Show("Wellcom!", "OK", MessageBoxButton.OK);               

                LoginWindow loginw = new LoginWindow();
                this.Close();
                loginw.ShowDialog();
            }
        }

        private void GetUserTypeSurf()
        {
            user.Surfslst = new TypeSurfList();
           foreach (CheckBox checkbox in checkboxes)
           {
                if ((bool)checkbox.IsChecked)
                    user.Surfslst.Add(checkbox.Tag as TypeSurf);
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
            user.Password = pbPassword.Password;

        }
        private void tbBirthday_BirthdayChanged(object sender, RoutedEventArgs e)
        {
            ValidBirthYear validBirthYear = new ValidBirthYear();
            ValidationResult result = validBirthYear.Validate(user.Birthday, CultureInfo.CurrentCulture);
            if (result.IsValid)
            {
                tbBirthYear.BorderBrush = Brushes.Transparent;
                tbBirthYear.BorderThickness = new Thickness(0);
                tbBirthYear.ToolTip = null;
                birthOK = true;
            }
            else
            {
                tbBirthYear.BorderBrush = Brushes.Red;
                tbBirthYear.BorderThickness = new Thickness(2);
                tbBirthYear.ToolTip = result.ErrorContent.ToString();
                birthOK = false;
            }

        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = user = new User();

        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginw = new LoginWindow();
            this.Close();
            loginw.ShowDialog();

        }
        private void LoadView()
        {
            serviceSurf = new ServiceSurfClient();
            surftypes = serviceSurf.GetAllTypeSurf();
            Expander expander = new Expander();
            TextBlock Header = new TextBlock();
            Header.FontSize = 20;
            Header.Text = "Surf Types:";
            expander.Header = Header;
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(15, 0, 0, 0);
            expander.Content = sp;
            foreach (TypeSurf tp in surftypes)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = tp.Name;
                checkBox.Tag = tp;
                checkBox.Margin = new Thickness(5);
                sp.Children.Add(checkBox);
                checkboxes.Add(checkBox);
            }
            TypeSurfStack.Children.Add(expander);
            

        }
    
    }

}
