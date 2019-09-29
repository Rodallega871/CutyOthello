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
        public GZ101()
        {
            InitializeComponent();

            bool flag = false;
            Timer tim = new Timer(5000);

            // タイマーの処理
            tim.Elapsed += (sender, e) =>
            {
                if (flag)
                {
                    //１度ロゴを表示したら次画面に遷移
                    Application.Current.MainPage = new GZ102();
                    tim.Stop();
                }
                // 不透明度の初期値は0（透明）
                Test.Opacity = 0;
                // 1秒かけて不透明度を1にする
                Test.FadeTo(1, 3000);

                flag = true;
            };
            // タイマーを開始する
            tim.Start();
        }
    }
}