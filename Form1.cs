using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // создадим поле под наш пр€моугольник

        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(0, 0, 0); // создать экземпл€р класса
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            /* ”Ѕ»–ј≈ћ
            g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 200, 100, 50, 30);
            */
            myRect.Render(g); // теперь так рисуем
        }
    }
}