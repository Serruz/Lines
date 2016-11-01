using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuzinLines
{
    class EmptyCell
    {
        private Point _startPoint;
        private Point _size = new Point(50, 50);
        private Graphics _g;
        private Ball _ball;
        private Color _cellColor = Color.Black;
        private bool _isEmpty = true;
        private Point _index { get; set; }

        public EmptyCell(Point startPoint, Point index, Graphics g)
        {
            _startPoint = startPoint;
            _index = index;
            _g = g;
        }

        public void DrawCircle()
        {
            RectangleF rect = new RectangleF(_ball.BallPoint.X, _ball.BallPoint.Y, 30, 30);

            _g.FillEllipse(new SolidBrush(_ball.BallColor), rect);
            _g.DrawEllipse(Pens.White, rect);
            _isEmpty = false;

        }

        public void DeleteCircle()
        {
             CellColor = Color.Black;
            _ball = null;
            _isEmpty = true;
        }

        public Point StartPoint
        {
            get
            {
                return _startPoint;
            }
            set
            {
                _startPoint = value;
            }
        }
        public Point Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        public Point Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }
        public Ball Ball
        {
            get
            {
                return _ball;
            }
            set
            {
                _ball = value;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
            }
        }
        public Color CellColor
        {
            get
            {
                return _cellColor;
            }
            set
            {
                _cellColor = value;
            }
        }

    }
}
