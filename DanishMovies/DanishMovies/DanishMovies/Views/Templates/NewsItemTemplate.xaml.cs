using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsItemTemplate : ContentView
    {
        public NewsItemTemplate()
        {
            InitializeComponent();
        }
    }
}