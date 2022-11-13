using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // создадим поле под наш прямоугольник
        List<BaseObject> objects = new();
        public Form1()
        {
            InitializeComponent();
            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);


            foreach(var obj in objects ){
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
            
           // myRect.Render(g); // теперь так рисуем
        }
    }
}