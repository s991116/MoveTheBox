using MoveTheBox;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace MoveTheBoxUI
{
    public class BoardImage
    {

        private List<SolidBrush> BoxKindToColor = new List<SolidBrush> {
            new SolidBrush(Color.Red),
            new SolidBrush(Color.Yellow),
            new SolidBrush(Color.Blue),
            new SolidBrush(Color.Green),
            new SolidBrush(Color.Orange),
            new SolidBrush(Color.Purple)
        };

        public Bitmap CreateBitmap(Board b)
        {
            Bitmap board = new Bitmap(200, 200);
            Graphics boardGraphics = Graphics.FromImage(board);

            var p = b.GetCurrentBoxPositions();
            DrawGrid(boardGraphics);
 
            foreach (var position in p)
            {
                var box = b.GetBoxAtPosition(position);
                DrawRectangle(boardGraphics, box.Position, BoxKindToColor[box.Kind]);
            }

            return board;
        }

        private void DrawGrid(Graphics graphics)
        {
            for(var x=19; x < 199; x=x+20)
              graphics.DrawLine(Pens.Black, 0, x, 199, x);
            for (var y = 19; y < 199; y = y + 20)
                graphics.DrawLine(Pens.Black, y, 0, y, 199);

        }

        private void DrawRectangle(Graphics graphics, Position p, SolidBrush color)
        {
            graphics.FillRectangle(color, p.X * 20, 180-p.Y * 20, 19, 19);
        }
    }
}
