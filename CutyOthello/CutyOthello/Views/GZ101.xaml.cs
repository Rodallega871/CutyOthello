using CutyOthello.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CutyOthello.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GZ101 : ContentPage
    {
        GZ101ViewModl viewmodel;

        public GZ101()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GZ101ViewModl();

            //アニメーションの記述
            // 不透明度の初期値は0（透明）
            Test.Opacity = 0;
            // 1秒かけて不透明度を1にする
            Test.FadeTo(1, 3000);

        }
    }
}