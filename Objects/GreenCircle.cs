using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
//using Timer = System.Threading.Timer;

namespace LAB5.Objects
{
    class GreenCircle : BaseObject
    {
        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        { }
        public Action <GreenCircle> onDeath;
        public int timer = 100;

        public override void Render(Graphics g)
        {
            timer -= 1;
            g.FillEllipse(new SolidBrush(Color.GreenYellow), -15, -15, 30, 30);
            g.DrawString(timer.ToString(), new Font("Verdana", 8), new SolidBrush(Color.Green), 10, 10);
            if(timer == 0)
            {
                timer = 100;
                onDeath(this);
            }
        }
        
        public override GraphicsPath GetGraphicsPath()// для пересечения с объектом
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
    }
}
