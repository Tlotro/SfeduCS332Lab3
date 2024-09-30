using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab3.Polygon;

namespace Lab3
{ 

    public partial class Form1 : Form
    {
        public List<Polygon> polygons = new List<Polygon>();
        Polygon currentPoligon;
        PointWrapper currentPoint;
        int flag;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                //Код для постройки полигона
                case 0:
                    if (e.Button == MouseButtons.Right && currentPoligon != null)
                    {
                        polygons.Add(currentPoligon);
                        UpdateDraw();
                        currentPoligon = null;
                        Form1.ActiveForm.MouseMove -= DrawPolygon;
                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        if (currentPoligon != null)
                        {
                            currentPoligon.points.Add(new PointWrapper(PointToClient(Cursor.Position)));
                        }
                        if (currentPoligon == null)
                        {
                            int W,D;
                            if (!int.TryParse(textBox2.Text, out W))
                                W = 1;
                            if (!int.TryParse(textBox3.Text, out D))
                                D = 5;
                            currentPoligon = new Polygon(ColorDialog.Color,W,D);
                            Form1.ActiveForm.MouseMove += DrawPolygon;
                            currentPoligon.points.Add(new PointWrapper(PointToClient(Cursor.Position)));
                        }
                    } 
                break;
                //Код для перемещения точек
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (currentPoint != null)
                        {
                            currentPoint = null;
                            Form1.ActiveForm.MouseMove -= PointMove;
                        }
                    }
                    break;
                //Код для удаления точек
                case 2:
                    if (e.Button == MouseButtons.Left)
                    {
                        bool t = false;
                        for (int polyi = 0; polyi < polygons.Count(); polyi++)
                        {
                            for (int pointi=0; pointi < polygons[polyi].points.Count(); pointi++)
                            {
                                Point cursorpoint = PointToClient(Cursor.Position);
                                if (Math.Sqrt(Math.Pow(cursorpoint.X - polygons[polyi].points[pointi].X, 2) + Math.Pow(cursorpoint.Y - polygons[polyi].points[pointi].Y, 2)) < Math.Max(polygons[polyi].pointDiam, 8) / 2)
                                {
                                    t = true;
                                    polygons[polyi].points.RemoveAt(pointi);
                                    if (polygons[polyi].points.Count == 0)
                                    { polygons.RemoveAt(polyi); }
                                    UpdateDraw();
                                    break;
                                }
                            }
                            if (t)
                            break;
                        }
                    }
                    break;
                //Код для перемещения полигона
                case 3:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (flag == 1)
                        {
                            foreach (Polygon poly in polygons)
                            {
                                foreach (PointWrapper point in poly.points)
                                {
                                    Point cursorpoint = PointToClient(Cursor.Position);
                                    if (Math.Sqrt(Math.Pow(cursorpoint.X - point.X, 2) + Math.Pow(cursorpoint.Y - point.Y, 2)) < Math.Max(poly.pointDiam, 8) / 2)
                                    {
                                        currentPoligon = poly;
                                        break;
                                    }
                                }
                                if (currentPoligon != null)
                                    break;
                            }
                            if (currentPoligon != null)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligon.points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligon.points.Count();
                                Y /= currentPoligon.points.Count();
                                Label4.Text = X + " " + Y;
                                Button2.Enabled = true;
                                flag = 0;
                            }
                        }
                    }
                    break;
                //Код для вращения полигона
                case 4:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (flag == 1)
                        {
                            foreach (Polygon poly in polygons)
                            {
                                foreach (PointWrapper point in poly.points)
                                {
                                    Point cursorpoint = PointToClient(Cursor.Position);
                                    if (Math.Sqrt(Math.Pow(cursorpoint.X - point.X, 2) + Math.Pow(cursorpoint.Y - point.Y, 2)) < Math.Max(poly.pointDiam, 8) / 2)
                                    {
                                        currentPoligon = poly;
                                        break;
                                    }
                                }
                                if (currentPoligon != null)
                                    break;
                            }
                            if (currentPoligon != null)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligon.points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligon.points.Count();
                                Y /= currentPoligon.points.Count();
                                Label3.Text = X + " " + Y;
                                if (currentPoint != null)
                                    Button1.Enabled = true;
                                flag = 0;
                            }
                        } else if (flag == 2)
                        {
                            currentPoint = new PointWrapper(PointToClient(Cursor.Position));
                            Label4.Text = currentPoint.X + " " + currentPoint.Y;
                            if (currentPoligon != null)
                                Button1.Enabled = true;
                            flag = 0;
                            Form1.ActiveForm.MouseMove -= DrawExtraPoint;
                        }
                    }
                    break;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                //Код для перемещения точек
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (currentPoint == null)
                        {
                            foreach (Polygon poly in polygons)
                            {
                                foreach (PointWrapper point in poly.points)
                                {
                                    Point cursorpoint = PointToClient(Cursor.Position);
                                    if (Math.Sqrt(Math.Pow(cursorpoint.X - point.X, 2) + Math.Pow(cursorpoint.Y - point.Y, 2)) < Math.Max(poly.pointDiam,8) / 2)
                                    {
                                        currentPoint = point;
                                        Form1.ActiveForm.MouseMove += PointMove;
                                        break;
                                    }
                                }
                                if (currentPoint != null)
                                    break;
                            }
                        }
                    }
                break;
            }
        }
        private void PointMove(object sender, MouseEventArgs e)
        {
            Point cursorpoint = PointToClient(Cursor.Position);
            currentPoint.X = cursorpoint.X;
            currentPoint.Y = cursorpoint.Y;
            UpdateDraw();
        }

        private void DrawPolygon(object sender, MouseEventArgs e)
        {
            UpdateDraw();
            currentPoligon.DrawIncomplete(g, PointToClient(Cursor.Position));
        }

        private void DrawExtraPoint(object sender, MouseEventArgs e)
        {
            UpdateDraw();
            Point current = PointToClient(Cursor.Position);
            g.FillEllipse(new Pen(Color.Green).Brush, current.X - 6 / 2, current.Y - 6 / 2, 6, 6);
        }
        /// <summary>
        /// Обновить изображение
        /// </summary>
        private void UpdateDraw()
        {
            g.Clear(Form1.ActiveForm.BackColor);
            foreach (Polygon polygon in polygons)
                polygon.DrawNew(g);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            polygons.Clear();
            g.Clear(Form1.ActiveForm.BackColor);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.ActiveForm.MouseMove -= PointMove;
            Form1.ActiveForm.MouseMove -= DrawPolygon;
            Form1.ActiveForm.MouseMove -= DrawExtraPoint;
            UpdateDraw();
            currentPoligon = null;
            currentPoint = null;
            ColorButton.Visible = false;
            ColorButton.Enabled = false;
            textBox1.Visible = false;
            textBox1.Enabled = false;
            textBox2.Visible = false;
            textBox2.Enabled = false;
            textBox3.Visible = false;
            textBox3.Enabled = false;
            Label1.Visible = false;
            Label1.Enabled = false;
            Label2.Visible = false;
            Label2.Enabled = false;
            Label3.Visible = false;
            Label3.Enabled = false;
            Label4.Visible = false;
            Label4.Enabled = false;
            Button1.Visible = false;
            Button1.Enabled = false;
            Button2.Visible = false;
            Button2.Enabled = false;
            Button3.Visible = false;
            Button3.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            switch (ComboBox.SelectedIndex)
            {
                case 0:
                    ColorButton.Visible = true;
                    ColorButton.Enabled = true;
                    textBox2.Visible = true;
                    textBox2.Enabled = true;
                    textBox3.Visible = true;
                    textBox3.Enabled = true;
                    Label1.Visible = true;
                    Label1.Enabled = true;
                    Label2.Visible = true;
                    Label2.Enabled = true;
                    Label3.Visible = true;
                    Label3.Enabled = true;
                    Label1.Text = "Color:";
                    Label2.Text = "Line Width:";
                    Label3.Text = "Point Size:";
                    break;

                case 3:
                    textBox1.Visible = true;
                    textBox1.Enabled = true;
                    textBox2.Visible = true;
                    textBox2.Enabled = true;
                    Label1.Visible = true;
                    Label1.Enabled = true;
                    Label2.Visible = true;
                    Label2.Enabled = true;
                    Label4.Visible = true;
                    Label4.Enabled = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button3.Enabled = true;
                    Label1.Text = "X:";
                    Label2.Text = "Y:";
                    Label4.Text = "None";
                    Button2.Text = "Move";
                    Button3.Text = "Set Polygon";
                    break;
                case 4:
                    textBox1.Visible = true;
                    textBox1.Enabled = true;
                    Label1.Visible = true;
                    Label1.Enabled = true;
                    Label3.Visible = true;
                    Label3.Enabled = true;
                    Label4.Visible = true;
                    Label4.Enabled = true;
                    Button1.Visible = true;
                    Button2.Enabled = true;
                    Button2.Visible = true;
                    Button3.Enabled = true;
                    Button3.Visible = true;
                    Label1.Text = "Degree:";
                    Label3.Text = "None";
                    Label4.Text = "None";
                    Button1.Text = "Rotate";
                    Button2.Text = "Set Polygon";
                    Button3.Text = "Set Point";
                    flag = 0;
                    break;
                case 5:
                    textBox1.Visible = true;
                    textBox1.Enabled = true;
                    Label1.Visible = true;
                    Label1.Enabled = true;
                    Label3.Visible = true;
                    Label3.Enabled = true;
                    Button1.Visible = true;
                    Button2.Enabled = true;
                    Button2.Visible = true;
                    Label1.Text = "Degree:";
                    Label3.Text = "None";
                    Button1.Text = "Rotate";
                    Button2.Text = "Set Polygon";
                    flag = 1;
                    break;
                case 6:
                    textBox1.Visible = true;
                    textBox1.Enabled = true;
                    textBox2.Visible = true;
                    textBox2.Enabled = true;
                    Label1.Visible = true;
                    Label1.Enabled = true;
                    Label2.Visible = true;
                    Label2.Enabled = true;
                    Label4.Visible = true;
                    Label4.Enabled = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button3.Enabled = true;
                    Label1.Text = "X:";
                    Label2.Text = "Y:";
                    Label4.Text = "None";
                    Button2.Text = "Scale";
                    Button3.Text = "Set Polygon";
                    break;
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            ColorButton.BackColor = ColorDialog.Color;
            ColorButton.FlatAppearance.BorderColor = ColorDialog.Color;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                case 4:
                    float deg;
                    if (currentPoligon != null && float.TryParse(textBox1.Text, out deg))
                    {
                        foreach (PointWrapper point in currentPoligon.points)
                        {
                            float Xt = point.X-currentPoint.X, Yt = point.Y-currentPoint.Y;
                            point.X = (float)(Xt * Math.Cos(deg*Math.PI/180)- Yt * Math.Sin(deg * Math.PI / 180)) + currentPoint.X; 
                            point.Y = (float)(Xt * Math.Sin(deg * Math.PI / 180) + Yt * Math.Cos(deg * Math.PI / 180)) + currentPoint.Y;
                        }
                        float X = 0, Y = 0;
                        foreach (PointWrapper point in currentPoligon.points)
                        {
                            X += point.X;
                            Y += point.Y;
                        }
                        X /= currentPoligon.points.Count();
                        Y /= currentPoligon.points.Count();
                        Label3.Text = X + " " + Y;
                        UpdateDraw();
                        g.FillEllipse(new Pen(Color.Green).Brush, currentPoint.X - 6 / 2, currentPoint.Y - 6 / 2, 6, 6);
                    }
                    break;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                //Код для перемещения полигона
                case 3:
                    float X; float Y;
                    if (currentPoligon != null && float.TryParse(textBox1.Text,out X) && float.TryParse(textBox2.Text, out Y))
                    {
                        foreach (PointWrapper point in currentPoligon.points)
                        {
                            point.X += X; point.Y += Y;
                        }
                        foreach (PointWrapper point in currentPoligon.points)
                        {
                            X += point.X;
                            Y += point.Y;
                        }
                        X /= currentPoligon.points.Count();
                        Y /= currentPoligon.points.Count();
                        Label4.Text = X + " " + Y;
                        UpdateDraw();
                    }
                break;
                case 4:
                    flag = 1;
                    Button1.Enabled = false;
                    currentPoligon = null;
                    break;
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                case 3:
                    flag = 1;
                    Button2.Enabled = false;
                    currentPoligon = null;
                    break;
                case 4:
                    flag = 2;
                    Button1.Enabled = false;
                    currentPoint = null;
                    Form1.ActiveForm.MouseMove += DrawExtraPoint;
                    break;
            }
        }
    }
    public class Polygon
    {
        public class PointWrapper
        {
            PointF _point;
            public PointF point { get { return _point; } set { _point = value; } }
            public float X { get { return _point.X; } set { _point.X = value; } }
            public float Y { get { return _point.Y; } set { _point.Y = value; } }
            public static PointWrapper operator + (PointWrapper left, Point right) 
            { 
                left.X += right.X;
                left.Y += right.Y;
                return left;
            }

            public PointWrapper(Point point)
            {
                this.point = point;
            }

            public PointWrapper(int X, int Y)
            {
                point = new Point(X, Y);
            }
        }
        public List<PointWrapper> points;
        public Color color;
        public float lineWidth;
        public float pointDiam;

        public Polygon(Color color, float lineWidth, float pointDiam)
        {
            points = new List<PointWrapper>();
            this.color = color;
            this.lineWidth = lineWidth;
            this.pointDiam = pointDiam;
        }

        public Polygon()
        {
            points = new List<PointWrapper>();
            color = Color.FromArgb(255,0,0,0);
            lineWidth = 1.0f;
            pointDiam = 6.0f;
        }

        public void DrawNew(Graphics graphics)
        {
            Pen pen = new Pen(color, lineWidth);
            for (int i = 0; i < points.Count()-1; i++) 
            {
                graphics.DrawLine(pen, points[i].point, points[i+1].point);
            }
            graphics.DrawLine(pen, points.Last().point, points.First().point);
            foreach (PointWrapper point in points) 
            {
                graphics.FillEllipse(pen.Brush, point.X-pointDiam/2, point.Y-pointDiam/2, pointDiam, pointDiam);
            }
        }

        public void DrawIncomplete(Graphics graphics, Point extraPoint) 
        {
            Pen pen = new Pen(color, lineWidth);
            for (int i = 0; i < points.Count() - 1; i++)
            {
                graphics.DrawLine(pen, points[i].point, points[i + 1].point);
            }
            graphics.DrawLine(pen, points.Last().point, extraPoint);
            foreach (PointWrapper point in points)
            {
                graphics.FillEllipse(pen.Brush, point.X - pointDiam / 2, point.Y - pointDiam / 2, pointDiam, pointDiam);
            }
            graphics.FillEllipse(pen.Brush, extraPoint.X - pointDiam / 2, extraPoint.Y - pointDiam / 2, pointDiam, pointDiam);
        }
    }
}
