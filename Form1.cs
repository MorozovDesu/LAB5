using LAB5.Objects;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace LAB5
{
    public partial class Form1 : Form
    {
        //MyRectangle myRect; // �������� ���� ��� ��� �������������
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle greenCircle;
        GreenCircle greenCircle2;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            // �������� ������� �� �����������
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ����� ��������� � {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnGreenCircleOverlap += (gr) =>
            {
                objects.Remove(gr);
                greenCircle = null;
                enumerator();
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            greenCircle = new GreenCircle(pbMain.Width / 10, pbMain.Height / 10, 0);
            greenCircle2 = new GreenCircle(pbMain.Width / 5+6, pbMain.Height / 5+10, 0);
            objects.Add(greenCircle);
            objects.Add(greenCircle2);
            objects.Add(marker);
            objects.Add(player);
            
            //objects.Add(new MyRectangle(50, 50, 0));
            //objects.Add(new MyRectangle(100, 100, 45));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);
            updatePlayer();
            // ����� ��� objects �� objects.ToList()
            // ��� ����� ��������� ����� ������
            // � �������� �������������� ������������ objects ����� �� ����� foreach
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            // �������� �������
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

            // myRect.Render(g); // ������ ��� ������
        }
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                // �� ���� �� ������ ���������� ������ dx, dy
                // ��� ������ ���������, ������ ���� ������ ����������
                // ������� ����������� ������ � �������
                // 0.5 ������ ����������� ������� �������� �� ����
                // � ������� ���� ������������ �������� ��������
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                // ����������� ���� �������� ������ 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // ���������� ������,
            // ����� �����, ����� ����� ��������� ������� ��������� ����������� ����������
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // �������� ������� ������ � ������� ������� ��������
            player.X += player.vX;
            player.Y += player.vY;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updatePlayer();
            pbMain.Invalidate();
            GreenCircle();
            //enumerator();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // ��� ������� �������� ������� �� ����� ���� �� ��� �� ������
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // � ������� �� ������ ��������� � objects
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
        private void GreenCircle()
        {
            Random rnd = new Random();
            int value = rnd.Next(1, 10);
            Random rnd2 = new Random();
            int value2 = rnd2.Next(1, 10);
            if (greenCircle == null)
            {
                greenCircle = new GreenCircle(pbMain.Width / value, pbMain.Height / value2, 0);
                objects.Add(greenCircle);
            }
        }
        int counter = 0; 
        private void enumerator()
        {
           
            label1.Text = string.Empty;
            if(greenCircle == null)
            {
                counter += 1;
            }
                label1.Text = string.Format("����: ")+ counter.ToString();
        }
        

    }
}
