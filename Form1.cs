using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // создадим поле под наш прямоугольник
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2+50, pbMain.Height / 2+50, 0);
            objects.Add(marker);
            objects.Add(player);

            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);


            // меняю тут objects на objects.ToList()
            // это будет создавать копию списка
            // и позволит модифицировать оригинальный objects прямо из цикла foreach
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    // это не трогаю
                    txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;

                    // тут проверяю что достиг маркера
                    if (obj == marker)
                    {
                        // если достиг, то удаляю маркер из оригинального objects
                        objects.Remove(marker);
                        marker = null; // и обнуляю маркер
                    }
                }

                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

            // myRect.Render(g); // теперь так рисуем
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // тут добавляем проверку на marker не нулевой
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.X += dx * 2;
                player.Y += dy * 2;
            }

            // запрашиваем обновление pbMain
            // это вызовет метод pbMain_Paint по новой
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}