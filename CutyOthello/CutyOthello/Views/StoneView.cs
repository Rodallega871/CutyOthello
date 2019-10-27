using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CutyOthello.Views
{
    public class StoneView : ContentView
    {
        static ImageSource Player1ImageSource = ImageSource.FromFile("shiba.png");
        static ImageSource Player2ImageSource = ImageSource.FromFile("buldogSample.png");

        public int Row { private set; get; }
        public int Col { private set; get; }
        public ContentView OneStoneView { private set; get; }

        Image Player1Image = new Image
        {
            Source = Player1ImageSource
        };

        Image Player2Image = new Image
        {
            Source = Player2ImageSource
        };

        public StoneView(int row, int col)
        {
            Row = row;
            Col = col;

            Image tmp = null;

            if (Row == 3 && Col == 4) tmp = Player1Image;
            if (Row == 4 && Col == 3) tmp = Player2Image;
            if (Row == 3 && Col == 3) tmp = Player2Image;
            if (Row == 4 && Col == 4) tmp = Player1Image;

            OneStoneView = new Frame
            {
                Content = tmp,
                BackgroundColor = Color.PaleGreen,
                BorderColor = Color.LightGray,
                Padding = new Thickness(5,5,5,5)
            };

            TapGestureRecognizer singleTap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
                
            singleTap.Tapped += ChangeStone;

            OneStoneView.GestureRecognizers.Add(singleTap);

        }

        void ChangeStone(object sender,EventArgs args)
        {
            OneStoneView.Content = OneStoneView.Content == Player1Image ? Player2Image : Player1Image;
        }
    }
}