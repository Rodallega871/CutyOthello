using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CutyOthello.Views
{
    public class StoneView : ContentView
    {
        static ImageSource flagImageSource = ImageSource.FromFile("buldogSample.png");
        static ImageSource bugImageSource = ImageSource.FromFile("buldogSample.png");

        public int Row { private set; get; }
        public int Col { private set; get; }
        public ContentView OneStoneView { private set; get; }

        Image flagImage = new Image
        {
            Source = flagImageSource
        };

        Image bugImage = new Image
        {
            Source = bugImageSource
        };

        public StoneView(int row, int col)
        {
            Row = row;
            Col = col;

            OneStoneView = new Frame
            {
                Content = bugImage,
                BackgroundColor = Color.PaleGreen,
                BorderColor = Color.LightGray,
                Padding = new Thickness(5,5,5,5)
            };

            TapGestureRecognizer singleTap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };

            //singleTap.Tapped += ChangeStone();

            this.GestureRecognizers.Add(singleTap);

        }

        void ChangeStone(object sender,object args)
        {
            
        }
    }
}