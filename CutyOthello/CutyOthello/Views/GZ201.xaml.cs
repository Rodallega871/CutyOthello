using CutyOthello.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CutyOthello.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GZ201 : ContentPage
    {
        GZ201ViewModel viewmodel;

        StoneView[,] stoneViews = new StoneView[8, 8];

        public GZ201()
        {
            //オセロ版ビュー生成
            board = new AbsoluteLayout();

            InitializeComponent();

            BindingContext = viewmodel = new GZ201ViewModel();
            

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    StoneView stoneView = new StoneView(row, col,viewmodel.Player1Picture,viewmodel.Player2Picture);
                    board.Children.Add(stoneView.OneStoneView);
                    stoneViews[row, col] = stoneView;
                    //stoneViews[row, col].OneStoneView.SetBinding(Frame.ContentProperty,new Binding(""));
                }

            board.SizeChanged += (sender, args) =>
            {
                double tileWidth = board.Width / 8;
                double tileHeight = board.Height / 8;

                foreach (StoneView stoneView in stoneViews)
                {
                    Rectangle bounds = new Rectangle(stoneView.Col * tileWidth,
                                                     stoneView.Row * tileHeight,
                                                     tileWidth, tileHeight);
                    AbsoluteLayout.SetLayoutBounds(stoneView.OneStoneView, bounds);
                }
            };

            MessagingCenter.Subscribe<StoneView, List<int>>(this, "Sending", (sender, args) =>
            {
                var StoneX = args[0];
                var StoneY = args[1];

                if (viewmodel.DammyModel(StoneX, StoneY))
                {
                    for (int i = 0; i < viewmodel.GetBlackStoneList()[0].Count; i++)
                    {
                        stoneViews[viewmodel.GetBlackStoneList()[0][i], viewmodel.GetBlackStoneList()[1][i]].ChangePlyerOneView();
                    }

                    for (int j = 0; j < viewmodel.GetWhiteStoneList()[0].Count; j++)
                    {
                        stoneViews[viewmodel.GetWhiteStoneList()[0][j], viewmodel.GetWhiteStoneList()[1][j]].ChangePlyerTwoView();
                    }

                    //for (int k = 0; k < viewmodel.GetNextStoneList()[0].Count; k++)
                    //{
                    //    stoneViews[viewmodel.GetNextStoneList()[0][k], viewmodel.GetNextStoneList()[1][k]].ChangeNextView();
                    //}
                }
            });

        }

        private void TapTopMenuButton(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<StoneView, List<int>>(this, "Sending");
            viewmodel.ViewModelTapTopMenuButton();
        }
    }
    }