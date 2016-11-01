using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace RuzinLines
{
    class Board
    {

        public Graphics gr;

        public EmptyCell[,] Cells = new EmptyCell[9, 9];
        public List<Ball> Balls = new List<Ball>();
        Ball choosenBall;
        EmptyCell choosenCell;
        bool theBallIsChoosen;
        public List<EmptyCell> EmptyCells = new List<EmptyCell>();// Пустые ячейки
        Random rnd = new Random();
        public PictureBox pb;

        public Color RandomColor()
        {
            int randomNum = rnd.Next(4);
            switch (randomNum)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Yellow;
                default:
                    return Color.Red;
            }
        }


        public void DrawFirst(Graphics gr)// Рисует поле
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    EmptyCell cell = new EmptyCell(new Point(i * 50, j * 50), new Point(i, j), gr);
                    Cells[i, j] = cell;  
                    EmptyCells.Add(cell);
                    gr.FillRectangle(new SolidBrush(Color.Black), i * 50, j * 50, 50, 50);
                    gr.DrawRectangle(new Pen(Color.White), i * 50, j * 50, 50, 50);
                }

            }
            CreateNewBalls();

            for (int i = 0; i < Balls.Count; i++)
            {
                Ball ball = Balls[i];
                RectangleF rect = new RectangleF(ball.BallPoint.X, ball.BallPoint.Y, 30, 30);

                gr.FillEllipse(new SolidBrush(Balls[i].BallColor), rect);
                gr.DrawEllipse(Pens.White, rect);
            }
        }

        private void CreateNewBalls()// Рисует 3 шара при запуске игры
        {
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                int newNumber = rnd.Next(0,EmptyCells.Count - 1);
                Ball newBall = new Ball(RandomColor(), new Point(EmptyCells[newNumber].StartPoint.X + 10, EmptyCells[newNumber].StartPoint.Y + 10));
                EmptyCells[newNumber].Ball = newBall;
                EmptyCells[newNumber].IsEmpty = false;
                Balls.Add(newBall);
                EmptyCells.RemoveAt(newNumber);
            }
        }

        public void Draw(Graphics gr)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    gr.FillRectangle(new SolidBrush(Cells[i, j].CellColor), i * 50, j * 50, 50, 50);
                    gr.DrawRectangle(new Pen(Color.White), i * 50, j * 50, 50, 50);
                }
            }

            for (int i = 0; i < Balls.Count; i++)
            {
                Ball ball = Balls[i];
                RectangleF rect = new RectangleF(ball.BallPoint.X, ball.BallPoint.Y, 30, 30);

                gr.FillEllipse(new SolidBrush(Balls[i].BallColor), rect);
                gr.DrawEllipse(Pens.White, rect);
            }
        }

        private void CreateNewBalls(EmptyCell prevCell)
        {
            EmptyCells.Remove(prevCell);
            if (EmptyCells.Count >= 3) // Если более трех ячеек свободны, то пилим три шара, иначе просто забиваем место
            {
                for (int i = 0; i < 3; i++)
                {
                    int newNumber = rnd.Next(EmptyCells.Count - 1);
                    Ball newBall = new Ball(RandomColor(), new Point(EmptyCells[newNumber].StartPoint.X + 10, EmptyCells[newNumber].StartPoint.Y + 10));
                    EmptyCells[newNumber].Ball = newBall;
                    EmptyCells[newNumber].IsEmpty = false;
                    Balls.Add(newBall);
                    EmptyCells.RemoveAt(newNumber);
                }
            }
            else
            {
                for (int i = 0; i < EmptyCells.Count; i++)
                {
                    int newNumber = rnd.Next(EmptyCells.Count - 1);
                    Ball newBall = new Ball(RandomColor(), new Point(EmptyCells[newNumber].StartPoint.X + 10, EmptyCells[newNumber].StartPoint.Y + 10));
                    EmptyCells[newNumber].Ball = newBall;
                    EmptyCells[newNumber].IsEmpty = false;
                    Balls.Add(newBall);
                    EmptyCells.RemoveAt(newNumber);
                }
            }
        }

        private bool RuleChecking(EmptyCell finalCell)// Проверка возможности перемещения
        {
            if (Math.Abs(choosenCell.Index.X - finalCell.Index.X) == Math.Abs(choosenCell.Index.Y - finalCell.Index.Y) ||//если по диагонали 
            choosenCell.Index.X == finalCell.Index.X ||//или вертикально 
            choosenCell.Index.Y == finalCell.Index.Y)//или горизонтально 
            {
                int dX = Math.Sign(finalCell.Index.X - choosenCell.Index.X);
                int dY = Math.Sign(finalCell.Index.Y - choosenCell.Index.Y);
                int i = choosenCell.Index.X + dX;
                int j = choosenCell.Index.Y + dY;
                while (i != finalCell.Index.X || j != finalCell.Index.Y)
                {
                    if (!Cells[i, j].IsEmpty)
                    {
                        return false;
                    }
                    i += dX;
                    j += dY;
                }
                return true;
            }
            else return false;
        }
        /*{

            if (choosenCell.Index.X == finalCell.Index.X) // Движение по оси Y
            {
                if (choosenCell.Index.Y < finalCell.Index.Y)// Движение вертикально вверх
                {
                    for (int j = choosenCell.Index.Y + 1; j <= finalCell.Index.Y; j++)
                    {
                        if (!Cells[choosenCell.Index.X, j].IsEmpty)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    for (int j = choosenCell.Index.Y - 1; j >= finalCell.Index.Y; j--)// Вертикально вниз
                    {
                        if (!Cells[choosenCell.Index.X, j].IsEmpty)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            else if (choosenCell.Index.Y == finalCell.Index.Y) // Движение по оси Х
            {
                if (choosenCell.Index.X < finalCell.Index.X)
                {
                    for (int i = choosenCell.Index.X + 1; i <= finalCell.Index.X; i++)// Горизонтально вправо
                    {
                        if (!Cells[i, finalCell.Index.Y].IsEmpty)
                        {
                            return false;
                        }
                    }
                    return true;

                }
                else
                {
                    for (int i = choosenCell.Index.X - 1; i >= finalCell.Index.X; i--)// Горизонтально влево
                    {

                        if (!Cells[i, finalCell.Index.Y].IsEmpty)
                        {
                            return false;
                        }

                    }
                    return true;
                }

            }
           
            else return false;
        }*/

        public void CheckFive()
        {
            List<EmptyCell> cellsToClean = new List<EmptyCell>();
            List<EmptyCell> cellsToClean2 = new List<EmptyCell>();
            int temp1 = 0;
            int temp2 = 0;
            Color currentColor1 = Color.Black;
            Color currentColor2 = Color.Black;

            for (int i = 0; i < 9; i++)
            {
                if (temp1 >= 5)
                {
                    temp1 = 0;
                    CleanCells(cellsToClean);
                }

                temp1 = 0;
                cellsToClean.Clear();

                for (int j = 0; j < 9; j++)
                {
                    if (Cells[i, j].Ball != null)
                    {
                        if (temp1 == 0)
                        {
                            currentColor1 = Cells[i, j].Ball.BallColor;
                        }

                        if (Cells[i, j].Ball.BallColor == currentColor1)
                        {
                            temp1++;
                            cellsToClean.Add(Cells[i, j]);
                        }
                        else
                        {
                            if (temp1 >= 5)
                            {
                                CleanCells(cellsToClean);
                                temp1 = 1;
                                cellsToClean.Clear();
                                cellsToClean.Add(Cells[i, j]);
                                currentColor1 = Cells[i, j].Ball.BallColor;
                            }
                            else
                            {
                                temp1 = 1;
                                cellsToClean.Clear();
                                cellsToClean.Add(Cells[i, j]);
                                currentColor1 = Cells[i, j].Ball.BallColor;
                            }
                        }
                    }
                    else
                    {
                        if (temp1 >= 5)
                        {
                            temp1 = 0;
                            CleanCells(cellsToClean);
                        }
                        else
                        {
                            temp1 = 0;
                            currentColor1 = Color.Black;
                            cellsToClean.Clear();
                        }
                    }
                }
            }

            temp2 = 0;
            currentColor2 = Color.Black;

            for (int i = 0; i < 9; i++)
            {
                if (temp2 >= 5)
                {
                    temp2 = 0;
                    CleanCells(cellsToClean2);
                }

                temp2 = 0;
                cellsToClean2.Clear();

                for (int j = 0; j < 9; j++)
                {
                    if (Cells[j, i].Ball != null)
                    {
                        if (temp2 == 0)
                        {
                            currentColor2 = Cells[j, i].Ball.BallColor;
                        }

                        if (Cells[j, i].Ball.BallColor == currentColor2)
                        {
                            temp2++;
                            cellsToClean2.Add(Cells[j, i]);
                        }
                        else
                        {
                            if (temp2 >= 5)
                            {
                                CleanCells(cellsToClean2);
                                temp2 = 1;
                                cellsToClean2.Clear();
                                cellsToClean2.Add(Cells[j, i]);
                                currentColor2 = Cells[j, i].Ball.BallColor;
                            }
                            else
                            {

                                temp2 = 1;
                                cellsToClean2.Clear();
                                cellsToClean2.Add(Cells[j, i]);
                                currentColor2 = Cells[j, i].Ball.BallColor;
                            }
                        }
                    }
                    else
                    {
                        if (temp2 >= 5)
                        {
                            temp2 = 0;
                            CleanCells(cellsToClean2);
                        }
                        else
                        {
                            temp2 = 0;
                            currentColor2 = Color.Black;
                            cellsToClean2.Clear();
                        }
                    }
                }
            }
        }

        public void CleanCells(List<EmptyCell> cellsToClean)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int z = 0; z < cellsToClean.Count; z++)
                    {
                        if (cellsToClean[z] == Cells[i, j])
                        {
                            Cells[i, j].CellColor = Color.Red;
                            Draw(gr);
                            pb.Refresh();
                            Thread.Sleep(300);
                            Balls.Remove(cellsToClean[z].Ball);
                            cellsToClean.RemoveAt(z);
                            EmptyCells.Add(Cells[i, j]);
                            Cells[i, j].DeleteCircle();
                        }
                    }
                }
            }
        }

        public void checkClick(Point mousePos)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (MouseInBounds(mousePos, Cells[i, j].StartPoint))// Проверка того что курсор в определенной ячейке
                    {
                        if (Cells[i, j].IsEmpty)
                        {
                            if (theBallIsChoosen)
                            {
                                if (RuleChecking(Cells[i, j]))
                                {
                                    EmptyCells.Add(choosenCell);
                                    choosenBall.BallPoint = new Point(Cells[i, j].StartPoint.X + 10, Cells[i, j].StartPoint.Y + 10);
                                    Cells[i, j].Ball = choosenBall;
                                    choosenCell.DeleteCircle();
                                    Cells[i, j].DrawCircle();
                                    choosenCell = null;
                                    choosenBall = null;
                                    CreateNewBalls(Cells[i, j]);
                                    theBallIsChoosen = false;
                                    pb.Refresh();
                                    CheckFive();
                                }
                                else// Убираем выделение
                                {
                                    choosenCell.CellColor = Color.Black;
                                    choosenBall = null;
                                    choosenCell = null;
                                    theBallIsChoosen = false;
                                }
                            }
                            break;
                        }
                        else
                        {
                            if (!theBallIsChoosen)
                            {
                                Ball clickedBall = Cells[i, j].Ball;
                                for (int z = 0; z < Balls.Count; z++)
                                {
                                    if ((Balls[z].BallPoint.X == clickedBall.BallPoint.X) && (Balls[z].BallPoint.Y == clickedBall.BallPoint.Y))
                                    {
                                        choosenBall = Balls[z];
                                        choosenCell = Cells[i, j];
                                        theBallIsChoosen = true;
                                        Cells[i, j].CellColor = Color.DarkGray;
                                    }
                                }
                            }
                            else
                            {
                                choosenCell.CellColor = Color.Black;
                                Ball clickedBall = Cells[i, j].Ball;
                                for (int z = 0; z < Balls.Count; z++)
                                {
                                    if ((Balls[z].BallPoint.X == clickedBall.BallPoint.X) && (Balls[z].BallPoint.Y == clickedBall.BallPoint.Y))
                                    {
                                        choosenBall = Balls[z];
                                        choosenCell = Cells[i, j];
                                        theBallIsChoosen = true;
                                        Cells[i, j].CellColor = Color.DarkGray;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        private bool MouseInBounds(Point mousePos, Point cellPos)
        {
            return (mousePos.X >= cellPos.X) && (mousePos.Y >= cellPos.Y) && (mousePos.X <= cellPos.X + 50) && (mousePos.Y <= cellPos.Y + 50);
        }
    }
}
