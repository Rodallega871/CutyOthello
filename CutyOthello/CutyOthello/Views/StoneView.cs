using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CutyOthello.Views
{
    public class StoneView : ContentView
    {
        public int Row { private set; get; }
        public int Col { private set; get; }
        public string status;
        public ContentView OneStoneView { private set; get; }
        Image Player1Image;
        Image Player2Image;
        Image NextCanPutImage;

        TapGestureRecognizer singleTap;

        public StoneView(int row, int col,string BlackStone,string WhiteStone)
        {
            Player1Image = new Image
            {
                Source = ImageSource.FromFile(BlackStone)
            };

            Player2Image = new Image
            {
                Source = ImageSource.FromFile(WhiteStone)
            };

            NextCanPutImage = new Image
            {
                Source = ImageSource.FromFile("Yazirusi.png")
            };
            Row = row;
            Col = col;

            Image tmp = null;

            if (Row == 3 && Col == 4) tmp = Player1Image;
            if (Row == 4 && Col == 3) tmp = Player1Image;
            if (Row == 3 && Col == 3) tmp = Player2Image;
            if (Row == 4 && Col == 4) tmp = Player2Image;

            OneStoneView = new Frame
            {
                Content = tmp,
                BackgroundColor = Color.PaleGreen,
                BorderColor = Color.LightGray,
                Padding = new Thickness(5,5,5,5)
            };

            singleTap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };

            singleTap.Tapped += ChangeStone;

            OneStoneView.GestureRecognizers.Add(singleTap);

        }

        public void ChangeStone(object sender,EventArgs args)
        {
            MessagingCenter.Send<StoneView, List<int>>(this, "Sending", new List<int> {Col, Row });
        }

        public void ChangePlyerOneView()
        {
            OneStoneView.Content = Player1Image;
        }

        public void ChangePlyerTwoView()
        {
            OneStoneView.Content = Player2Image;
        }

        public void ChangeNextView()
        {
            OneStoneView.Content = NextCanPutImage;
        }

    }
}