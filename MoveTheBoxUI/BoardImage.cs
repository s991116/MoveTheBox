using MoveTheBox;
using System.Collections.Generic;
using System.Drawing;

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
            Bitmap board = new Bitmap(100, 100);
            Graphics boardGraphics = Graphics.FromImage(board);

            var p = b.GetCurrentBoxPositions();
 
            foreach (var position in p)
            {
                var box = b.GetBoxAtPosition(position);
                DrawRectangle(boardGraphics, box.Position, BoxKindToColor[box.Kind]);
            }

            return board;
        }

        private void DrawRectangle(Graphics graphics, Position p, SolidBrush color)
        {
            graphics.FillRectangle(color, p.X * 10, p.Y * 10, 9, 9);
        }
    }
}
