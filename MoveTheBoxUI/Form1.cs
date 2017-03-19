using MoveTheBox;
using Solver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveTheBoxUI
{
    public partial class Form1 : Form
    {
        private BoardEditor boardEditor;
        private Solve solver;
        private int maxMoves;
        private BoardImage boardImage;
        private Board board;

        public Form1()
        {
            InitializeComponent();
            board = new Board(new Box[] { });
            boardEditor = new BoardEditor(board);
            solver = new Solver.Solve();
            maxMoves = 2;
            boardImage = new BoardImage();
            pictureBox1.Image = boardImage.CreateBitmap(board);
        }

        private void Solve_Click(object sender, EventArgs e)
        {
            maxMoves = Convert.ToInt32(numericUpDown2.Value);
            var solution = solver.FindSolution(boardEditor.Board, maxMoves);
            textBoxSolution.Text = PrettyPrintSolution(solution);
        }

        private string PrettyPrintSolution(Solution solution)
        {
            if (!solution.Found)
                return "No Solution found.";

            StringBuilder sb = new StringBuilder();
            foreach(var step in solution.Moves)
            {
                sb.AppendLine("Move box at x: "+step.Position.X.ToString()+" , y: "+step.Position.Y.ToString() + "  " + step.MoveDirection.ToString());
            }
            return sb.ToString();
        }

        private void Form1_Click(object sender, MouseEventArgs me)
        {
            Point coordinates = me.Location;

            var x = coordinates.X / 20;
            var y = 9-coordinates.Y / 20;

            var position = new Position(x, y);
            boardEditor.UpdateBoxField(position);
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            var image = boardImage.CreateBitmap(boardEditor.Board);
            pictureBox1.Image = image;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            boardEditor.ClearBoard();
            UpdateBoard();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Json|*.*";
            saveFileDialog1.Title = "Save Board setup";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                var sw = new System.IO.StreamWriter(saveFileDialog1.OpenFile());
                sw.Write(boardEditor.Serialize());
                sw.Close();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json|*.*";
            openFileDialog.Title = "Open Board setup";
            openFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (openFileDialog.FileName != "")
            {
                var sr = new System.IO.StreamReader(openFileDialog.OpenFile());
                boardEditor.UpdateFromState(sr.ReadToEnd());
                sr.Close();
            }

            UpdateBoard();
        }
    }
}
