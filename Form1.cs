using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // создадим поле под наш пр€моугольник

        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(100, 100, 45); // создать экземпл€р класса
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);


            g.Transform = myRect.GetTransform();
            
            myRect.Render(g); // теперь так рисуем
        }
    }
}