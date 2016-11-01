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
    class Ball
    {
        private Color _ballColor;
        private Point _ballPoint;

        public Ball(Color color, Point point)
        {
            _ballColor = color;
            _ballPoint = point;
        }

        public Color BallColor
        {
            get
            {
                return _ballColor;
            }

            set
            {
                _ballColor = value;
            }
        }
        public Point BallPoint
        {
            get
            {
                return _ballPoint;
            }

            set
            {
                _ballPoint = value;
            }
        }

    }
}
