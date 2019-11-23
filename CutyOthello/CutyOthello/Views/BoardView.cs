using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CutyOthello.Views
{
    public class BoardView : AbsoluteLayout
    {
        // Alternative sizes make the tiles a tad small.
        const int COLS = 8;         // 8
        const int ROWS = 8;         // 8

        // The array of tiles
        StoneView[,] stoneViews = new StoneView[ROWS, COLS];
         
        public BoardView()
        {
            for (int row = 0; row < ROWS; row++)
                for (int col = 0; col < COLS; col++)
                {
                    StoneView stoneView = new StoneView(row, col,"","");
                    Children.Add(stoneView.OneStoneView);
                    stoneViews[row, col] = stoneView;
                }

            SizeChanged += (sender, args) =>
            {
                double tileWidth = this.Width / COLS;
                double tileHeight = this.Height / ROWS;

                foreach (StoneView stoneView in stoneViews)
                {
                    Rectangle bounds = new Rectangle(stoneView.Col * tileWidth,
                                                     stoneView.Row * tileHeight,
                                                     tileWidth, tileHeight);
                    SetLayoutBounds(stoneView.OneStoneView, bounds);
                }
            };
        }
    }
}
