using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // �������� ���� ��� ��� �������������

        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(100, 100, 0); // ������� ��������� ������
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);


            var matrix = g.Transform;
            matrix.Translate(myRect.X, myRect.Y);
            g.Transform = matrix;
            
            myRect.Render(g); // ������ ��� ������
        }
    }
}