using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        private int clickCount = 0;
        private PointF[] selectedPoints = new PointF[2];
        private PointF[] drawnLine = new PointF[2];
        public List<Polygon> polygons = new List<Polygon>();
        List<Polygon> currentPoligons = new List<Polygon>();
        List<PointWrapper> currentPoints = new List<PointWrapper>();
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
                    if (e.Button == MouseButtons.Right && currentPoligons.Count() != 0)
                    {
                        polygons.Add(currentPoligons[0]);
                        UpdateDraw();
                        currentPoligons.RemoveAt(0);
                        Form1.ActiveForm.MouseMove -= DrawPolygon;
                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        if (currentPoligons.Count() != 0)
                        {
                            currentPoligons[0].points.Add(new PointWrapper(PointToClient(Cursor.Position)));
                        }
                        if (currentPoligons.Count() == 0)
                        {
                            int W,D;
                            if (!int.TryParse(textBox2.Text, out W))
                                W = 1;
                            if (!int.TryParse(textBox3.Text, out D))
                                D = 5;
                            currentPoligons.Add(new Polygon(ColorDialog.Color,W,D));
                            Form1.ActiveForm.MouseMove += DrawPolygon;
                            currentPoligons[0].points.Add(new PointWrapper(PointToClient(Cursor.Position)));
                        }
                    } 
                break;
                //Код для перемещения точек
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (currentPoints.Count() != 0)
                        {
                            currentPoints.RemoveAt(0);
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
                            currentPoligons.Clear();
                            SelectPolygon();
                            if (currentPoligons.Count() != 0)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligons[0].points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligons[0].points.Count();
                                Y /= currentPoligons[0].points.Count();
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
                            currentPoligons.Clear();
                            SelectPolygon();
                            if (currentPoligons.Count() != 0)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligons[0].points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligons[0].points.Count();
                                Y /= currentPoligons[0].points.Count();
                                Label3.Text = X + " " + Y;
                                if (currentPoints.Count() != 0)
                                    Button1.Enabled = true;
                                flag = 0;
                            }
                        } else if (flag == 2)
                        {
                            currentPoints.Clear();
                            currentPoints.Add(new PointWrapper(PointToClient(Cursor.Position)));
                            Label4.Text = currentPoints[0].X + " " + currentPoints[0].Y;
                            if (currentPoligons.Count() != 0)
                                Button1.Enabled = true;
                            flag = 0;
                            Form1.ActiveForm.MouseMove -= DrawExtraPoint;
                        }
                    }
                    break;
                case 5:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (flag == 1)
                        {
                            currentPoligons.Clear();
                            SelectPolygon();
                            if (currentPoligons.Count() != 0)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligons[0].points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligons[0].points.Count();
                                Y /= currentPoligons[0].points.Count();
                                Label3.Text = X + " " + Y;
                                Button1.Enabled = true;
                                flag = 0;
                            }
                        }
                    }
                    break;
                case 6:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (flag == 1)
                        {
                            currentPoligons.Clear();
                            SelectPolygon();
                            if (currentPoligons.Count() != 0)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligons[0].points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligons[0].points.Count();
                                Y /= currentPoligons[0].points.Count();
                                Label4.Text = X + " " + Y;
                                if (currentPoints.Count() != 0)
                                    Button2.Enabled = true;
                                flag = 0;
                            }
                        }
                        else if (flag == 2)
                        {
                            currentPoints.Clear();
                            currentPoints.Add(new PointWrapper(PointToClient(Cursor.Position)));
                            Label5.Text = currentPoints[0].X + " " + currentPoints[0].Y;
                            if (currentPoligons.Count() != 0)
                                Button2.Enabled = true;
                            flag = 0;
                            Form1.ActiveForm.MouseMove -= DrawExtraPoint;
                        }
                    }
                    break;
                case 7:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (flag == 1)
                        {
                            currentPoligons.Clear();
                            SelectPolygon();
                            if (currentPoligons.Count() != 0)
                            {
                                float X = 0, Y = 0;
                                foreach (PointWrapper point in currentPoligons[0].points)
                                {
                                    X += point.X;
                                    Y += point.Y;
                                }
                                X /= currentPoligons[0].points.Count();
                                Y /= currentPoligons[0].points.Count();
                                Label4.Text = X + " " + Y;
                                Button2.Enabled = true;
                                flag = 0;
                            }
                        }
                    }
                    break;
                case 8:
                    if (clickCount < 2)
                    {
                        // Первые два нажатия: выбираем две точки в полигоне
                        SelectPointsInPolygon(e.Location);
                    }
                    else if (clickCount < 4)
                    {
                        // Следующие два нажатия: рисуем новую линию
                        DrawNewLine(e.Location);
                    }

                    clickCount++;

                    if (clickCount == 4)
                    {
                        // Проверяем пересечение и выводим результат
                        CheckIntersectionAndClear();
                        clickCount = 0; // Сбрасываем счетчик кликов
                    }
                    break;
                case 9:
                    Point clickPoint = PointToClient(Cursor.Position);
                    bool isInside = false;

                    foreach (Polygon polygon in polygons)
                    {
                        if (IsPointInPolygon(clickPoint, polygon))
                        {
                            isInside = true;
                            break;
                        }
                    }

                    MessageBox.Show(isInside ? "Точка находится внутри полигона" : "Точка находится вне полигона");
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
                        if (currentPoints.Count() == 0)
                        {
                            foreach (Polygon poly in polygons)
                            {
                                foreach (PointWrapper point in poly.points)
                                {
                                    Point cursorpoint = PointToClient(Cursor.Position);
                                    if (Math.Sqrt(Math.Pow(cursorpoint.X - point.X, 2) + Math.Pow(cursorpoint.Y - point.Y, 2)) < Math.Max(poly.pointDiam,8) / 2)
                                    {
                                        currentPoints.Add(point);
                                        Form1.ActiveForm.MouseMove += PointMove;
                                        break;
                                    }
                                }
                                if (currentPoints.Count() != 0)
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
            currentPoints[0].X = cursorpoint.X;
            currentPoints[0].Y = cursorpoint.Y;
            UpdateDraw();
        }

        private void DrawPolygon(object sender, MouseEventArgs e)
        {
            UpdateDraw();
            currentPoligons[0].DrawIncomplete(g, PointToClient(Cursor.Position));
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
            currentPoligons.Clear();
            currentPoints.Clear();
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
                    Label5.Visible = true;
                    Label5.Enabled = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button3.Enabled = true;
                    Button4.Visible = true;
                    Button4.Enabled = true;
                    Label1.Text = "X:";
                    Label2.Text = "Y:";
                    Label4.Text = "None";
                    Label5.Text = "None";
                    Button2.Text = "Scale";
                    Button3.Text = "Set Polygon";
                    Button4.Text = "Set Point";
                    break;
                case 7:
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
                case 10:
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
            float n;
            switch (ComboBox.SelectedIndex)
            {
                case 4:
                    if (currentPoligons.Count() != 0 && float.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out n))
                    {
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            float Xt = point.X - currentPoints[0].X, Yt = point.Y - currentPoints[0].Y;
                            point.X = (float)(Xt * Math.Cos(n*Math.PI/180)- Yt * Math.Sin(n * Math.PI / 180)) + currentPoints[0].X; 
                            point.Y = (float)(Xt * Math.Sin(n * Math.PI / 180) + Yt * Math.Cos(n * Math.PI / 180)) + currentPoints[0].Y;
                        }
                        float X = 0, Y = 0;
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            X += point.X;
                            Y += point.Y;
                        }
                        X /= currentPoligons[0].points.Count();
                        Y /= currentPoligons[0].points.Count();
                        Label3.Text = X + " " + Y;
                        UpdateDraw();
                        g.FillEllipse(new Pen(Color.Green).Brush, currentPoints[0].X - 6 / 2, currentPoints[0].Y - 6 / 2, 6, 6);
                    }
                    break;
                case 5:
                    if (currentPoligons.Count() != 0 && float.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out n))
                    {
                        float X = 0, Y = 0;
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            X += point.X;
                            Y += point.Y;
                        }
                        X /= currentPoligons[0].points.Count();
                        Y /= currentPoligons[0].points.Count();
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            float Xt = point.X - X, Yt = point.Y - Y;
                            point.X = (float)(Xt * Math.Cos(n * Math.PI / 180) - Yt * Math.Sin(n * Math.PI / 180)) + X;
                            point.Y = (float)(Xt * Math.Sin(n * Math.PI / 180) + Yt * Math.Cos(n * Math.PI / 180)) + Y;
                        }
                        UpdateDraw();
                    }
                    break;
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            float X; float Y;
            switch (ComboBox.SelectedIndex)
            {
                //Код для перемещения полигона
                case 3:
                    if (currentPoligons.Count() != 0 && float.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out X) && float.TryParse(textBox2.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out Y))
                    {
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            point.X += X; point.Y += Y;
                        }
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            X += point.X;
                            Y += point.Y;
                        }
                        X /= currentPoligons[0].points.Count();
                        Y /= currentPoligons[0].points.Count();
                        Label4.Text = X + " " + Y;
                        UpdateDraw();
                    }
                break;
                case 4:
                    flag = 1;
                    Button1.Enabled = false;
                    currentPoligons.Clear();
                    break;
                case 5:
                    flag = 1;
                    Button1.Enabled = false;
                    currentPoligons.Clear();
                    break;
                case 6:
                    if (currentPoligons.Count() != 0 && float.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out X) && float.TryParse(textBox2.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out Y))
                    {
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            float Xt = point.X - currentPoints[0].X, Yt = point.Y - currentPoints[0].Y;
                            point.X = (float)(Xt * X) + currentPoints[0].X;
                            point.Y = (float)(Yt * Y)+ currentPoints[0].Y;
                        }
                        X = 0; Y = 0;
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            X += point.X;
                            Y += point.Y;
                        }
                        X /= currentPoligons[0].points.Count();
                        Y /= currentPoligons[0].points.Count();
                        Label3.Text = X + " " + Y;
                        UpdateDraw();
                        g.FillEllipse(new Pen(Color.Green).Brush, currentPoints[0].X - 6 / 2, currentPoints[0].Y - 6 / 2, 6, 6);
                    }
                    break;
                case 7:
                    if (currentPoligons.Count() != 0 && float.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out X) && float.TryParse(textBox2.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out Y))
                    {
                        float Xc = 0, Yc = 0;
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            Xc += point.X;
                            Yc += point.Y;
                        }
                        Xc /= currentPoligons[0].points.Count();
                        Yc /= currentPoligons[0].points.Count();
                        foreach (PointWrapper point in currentPoligons[0].points)
                        {
                            float Xt = point.X - Xc, Yt = point.Y - Yc;
                            point.X = (float)(Xt * X) + Xc;
                            point.Y = (float)(Yt * Y) + Yc;
                        }
                        Label3.Text = X + " " + Y;
                        UpdateDraw();
                    }
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
                    currentPoligons.Clear();
                    break;
                case 4:
                    flag = 2;
                    Button1.Enabled = false;
                    currentPoints.Clear();
                    Form1.ActiveForm.MouseMove += DrawExtraPoint;
                    break;
                case 6:
                    flag = 1;
                    Button2.Enabled = false;
                    currentPoligons.Clear();
                    break;
                case 7:
                    flag = 1;
                    Button2.Enabled = false;
                    currentPoligons.Clear();
                    break;
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                case 6:
                    flag = 2;
                    Button2.Enabled = false;
                    currentPoints.Clear();
                    Form1.ActiveForm.MouseMove += DrawExtraPoint;
                    break;
            }
        }
        private void SelectPolygon()
        {
            foreach (Polygon poly in polygons)
            {
                foreach (PointWrapper point in poly.points)
                {
                    Point cursorpoint = PointToClient(Cursor.Position);
                    if (Math.Sqrt(Math.Pow(cursorpoint.X - point.X, 2) + Math.Pow(cursorpoint.Y - point.Y, 2)) < Math.Max(poly.pointDiam, 8) / 2)
                    {
                        currentPoligons.Add(poly);
                        break;
                    }
                }
                if (currentPoligons.Count() != 0)
                    break;
            }
        }
        // Определение положения точки относительно ребер
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ComboBox.SelectedIndex == 10) // Проверяем, выбран ли пункт "Position Relative to Segment"
            {
                positionTextBox.Visible = true;
                positionTextBox.Enabled = true;
                positionTextBox.AutoSize = true;
                positionTextBox.Multiline = true;
                positionTextBox.ScrollBars = ScrollBars.Vertical;

                Point doubleClickPoint = PointToClient(Cursor.Position);
                string positionInfo = "";

                foreach (Polygon polygon in polygons)
                {
                    for (int i = 0; i < polygon.points.Count; i++)
                    {
                        PointF edgeStart, edgeEnd;

                        if (i == polygon.points.Count - 1)
                        {
                            // Соединение последней точки с первой
                            edgeStart = polygon.points[i].point;
                            edgeEnd = polygon.points[0].point;
                        }
                        else
                        {
                            edgeStart = polygon.points[i].point;
                            edgeEnd = polygon.points[i + 1].point;
                        }

                        int position = ClassifyPointToEdge(edgeStart, edgeEnd, doubleClickPoint);
                        string positionText = position == 1 ? "слева" : (position == -1 ? "справа" : "на ребре");
                        positionInfo += $"Точка находится {positionText} от {i + 1} ребра {polygons.IndexOf(polygon) + 1} полигона\r\n";
                    }
                }

                positionTextBox.Text = positionInfo;

                // Изменение размера текстового поля в зависимости от размера текста
                using (Graphics g = CreateGraphics())
                {
                    SizeF textSize = g.MeasureString(positionInfo, positionTextBox.Font, positionTextBox.Width);
                    positionTextBox.Height = (int)textSize.Height;
                }

                //UpdateDraw();
                //g.FillEllipse(new Pen(Color.Green).Brush, doubleClickPoint.X - 6 / 2, doubleClickPoint.Y - 6 / 2, 6, 6);
            }
        }

        public static int ClassifyPointToEdge(PointF edgeStart, PointF edgeEnd, PointF point)
        {
            float xa = edgeStart.X - edgeEnd.X;
            float ya = edgeStart.Y - edgeEnd.Y;
            float xb = point.X - edgeStart.X;
            float yb = point.Y - edgeStart.Y;

            float crossProduct = yb * xa - xb * ya;

            if (crossProduct > 0)
                return 1; // Точка слева от ребра
            else if (crossProduct < 0)
                return -1; // Точка справа от ребра
            else
                return 0; // Точка на ребре
        }
        private void SelectPointsInPolygon(PointF clickPoint)
        {
            foreach (Polygon polygon in polygons)
            {
                for (int i = 0; i < polygon.points.Count; i++)
                {
                    PointF point = polygon.points[i].point;

                    if (IsPointCloseToClick(clickPoint, point))
                    {
                        if (clickCount == 0)
                        {
                            selectedPoints[0] = point;
                        }
                        else if (clickCount == 1)
                        {
                            selectedPoints[1] = point;
                            if (!ArePointsConnected(polygon, selectedPoints[0], selectedPoints[1]))
                            {
                                MessageBox.Show("Выбранные точки не соединены линией в полигоне.");
                                clickCount = 0; // Сбрасываем счетчик кликов
                            }
                        }
                        return;
                    }
                }
            }
        }

        private bool ArePointsConnected(Polygon polygon, PointF p1, PointF p2)
        {
            for (int i = 0; i < polygon.points.Count - 1; i++)
            {
                if ((polygon.points[i].point == p1 && polygon.points[i + 1].point == p2) ||
                    (polygon.points[i].point == p2 && polygon.points[i + 1].point == p1))
                {
                    return true;
                }
            }

            // Проверяем последнюю линию, соединяющую последнюю и первую точки
            if ((polygon.points.Last().point == p1 && polygon.points.First().point == p2) ||
                (polygon.points.Last().point == p2 && polygon.points.First().point == p1))
            {
                return true;
            }

            return false;
        }

        private void DrawNewLine(PointF clickPoint)
        {
            if (clickCount == 2)
            {
                drawnLine[0] = clickPoint;
            }
            else if (clickCount == 3)
            {
                drawnLine[1] = clickPoint;
            }
        }

        private void CheckIntersectionAndClear()
        {
            PointF? intersectionPoint = FindIntersection(selectedPoints[0], selectedPoints[1], drawnLine[0], drawnLine[1]);
            if (intersectionPoint.HasValue)
            {
                MessageBox.Show($"Линии пересекаются в точке: ({intersectionPoint.Value.X}, {intersectionPoint.Value.Y})");
            }
            else
            {
                MessageBox.Show("Линии не пересекаются");
            }

            // Очищаем нарисованную линию
            drawnLine[0] = new PointF();
            drawnLine[1] = new PointF();
        }

        private bool IsPointCloseToClick(PointF clickPoint, PointF point)
        {
            float epsilon = 5.0f; // Допустимая погрешность
            return Math.Abs(clickPoint.X - point.X) < epsilon && Math.Abs(clickPoint.Y - point.Y) < epsilon;
        }

        private PointF? FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            float denominator = (p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y);

            if (denominator == 0)
            {
                return null; // Линии параллельны
            }

            float ua = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X)) / denominator;
            float ub = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X)) / denominator;

            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                // Линии пересекаются
                float x = p1.X + ua * (p2.X - p1.X);
                float y = p1.Y + ua * (p2.Y - p1.Y);
                return new PointF(x, y);
            }

            return null; // Линии не пересекаются
        }

        private bool IsPointInPolygon(Point point, Polygon polygon)
        {
            int i, j;
            bool c = false;
            int nvert = polygon.points.Count;

            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon.points[i].Y > point.Y) != (polygon.points[j].Y > point.Y)) &&
                    (point.X < (polygon.points[j].X - polygon.points[i].X) * (point.Y - polygon.points[i].Y) / (polygon.points[j].Y - polygon.points[i].Y) + polygon.points[i].X))
                {
                    c = !c;
                }
            }

            return c;
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
