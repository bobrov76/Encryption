using System;
using System.Collections.Generic;
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

namespace RSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RSA_Criptographs_Class RSA = new RSA_Criptographs_Class();
        public MainWindow()
        {
            InitializeComponent();
            Coding_BTN.Click += (s, e) => { Discoding_TB.Text = RSA.Encryption(Coding_TB.Text);};
            Discoding_BTN.Click += (s,e) => { Result_TB.Text = RSA.Decryption(Discoding_TB.Text);};
            
        }

        
    }
}
