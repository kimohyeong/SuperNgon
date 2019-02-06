using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _SuperNgon
{
    public partial class Form1 : Form
    {
        PointF centerPoint;
        PointF sourcePoint;
        PointF[] npoints_player = new PointF[3];
        int n = 6;
        int time = 0;
        Timer timer = new System.Windows.Forms.Timer();

        bool right = false;
        bool left = false;
        bool anything = true;
        Graphics g;

        Color[] color = { Color.FromArgb(120, 153, 60), Color.FromArgb(160, 204, 80), Color.FromArgb(100, 127, 50) };
        Color[] color2 = { Color.Blue, Color.SkyBlue, Color.White, Color.Gray, Color.White };

        bool exitFlag = false;

        Rectangle rect;

        float j = 0;
        float m = 0;
        float r = 0;
        float l = 0;
        float angle = 0;
        bool rotate = false;
        bool rotate2 = false;
        private Random random = new Random();
        int sourcerandom = 40;

        PointF[] obstacle_point = new PointF[4];
        int distance = 600;
        int obstacle_width = 50;
        int random_num = 1;
        int random_num2;
        public Form1()
        {
            InitializeComponent();


            timer2.Interval = 150;
            random_num2 = random.Next(1, n);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if (rotate == false)
                ;
            else
                sourcerandom = random.Next(40, 55);


            rect = new Rectangle(-200, -200, ClientSize.Width + 400, ClientSize.Height + 400);  //폼을 벗어나는 엄청큰 원

            centerPoint = new PointF(ClientSize.Width / 2, ClientSize.Height / 2);  //센터점
            sourcePoint = new PointF(ClientSize.Width / 2 + sourcerandom, ClientSize.Height / 2); //도형 시작점


            PointF[] npoints = new PointF[n];

            // 배경회전
            if (n % 2 == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    g.FillPie(new SolidBrush(color[i % 2]), rect, (float)360 / n * i + j, (float)360 / n);
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (i == n - 1)
                        g.FillPie(new SolidBrush(color[2]), rect, (float)360 / n * i + j, (float)360 / n);
                    else
                        g.FillPie(new SolidBrush(color[i % 2]), rect, (float)360 / n * i + j, (float)360 / n);

                }

            }



            //장애물


            obstacle_point[0] = new PointF(centerPoint.X + distance, centerPoint.Y);
            obstacle_point[1] = new PointF(centerPoint.X + distance + obstacle_width, centerPoint.Y);


            obstacle_point[0] = Rotate(obstacle_point[0], centerPoint, (float)360 / n * random_num + j);
            obstacle_point[1] = Rotate(obstacle_point[1], centerPoint, (float)360 / n * random_num + j);



            for (int i = 0; i < random_num2; i++)
            {

                if (i % 2 == 0)
                {
                    obstacle_point[2] = Rotate(obstacle_point[1], centerPoint, (float)360 / n);
                    obstacle_point[3] = Rotate(obstacle_point[0], centerPoint, (float)360 / n);
                    g.FillPolygon(new SolidBrush(Color.Black), obstacle_point);
                }
                else
                {
                    obstacle_point[1] = Rotate(obstacle_point[2], centerPoint, (float)360 / n);
                    obstacle_point[0] = Rotate(obstacle_point[3], centerPoint, (float)360 / n);

                    g.FillPolygon(new SolidBrush(Color.Black), obstacle_point);
                }

            }

            if (rotate == false)
            {
                ;
            }
            else
            {
                distance -= 5;
                if (distance == 60)
                {

                    //장애물 충돌체크
                    if (angle > ((float)360 / n * random_num + j) || angle < ((float)360 / n * (random_num-(n-random_num2)) + j))
                    {
                        timer3.Stop();
                        timer2.Stop();
                        timer1.Stop();

                        MessageBox.Show("기록 : " + time.ToString(), "GAME OVER");

                        n = 6;
                        time = 0;

                        label3.Text = time.ToString();
                        label1.Visible = true;
                        label4.Visible = true;
                        label5.Visible = true;

                        

                        right = false;
                        left = false;
                        anything = true;

                        j = 0;
                        m = 0;
                        r = 0;
                        l = 0;
                        angle = 0;
                        rotate = false;
                        rotate2 = false;
                        distance = 500;
                        random_num = 1;
                        random_num2 = 1;
                        obstacle_width = 50;
                        sourcerandom = 40;
                        Invalidate();
                       
                        

                    }

                }
                if (distance < 40 * (-1))
                {
                    random_num = random.Next(1, n);
                    random_num2 = random.Next(1, n-1);
                    distance = 500;
                    obstacle_width = random.Next(30, 100);
                    
                }

            }



            //n각형 회전
            for (int i = 0; i < n; i++)     //n-gon포인트
            {
                npoints[i] = Rotate(sourcePoint, centerPoint, (float)360 / n * i + j);
            }

            g.FillPolygon(new SolidBrush(Color.White), npoints);    //n각형그리기


            //기본 플레이어 위치

            npoints_player[0] = new PointF(ClientSize.Width / 2 + sourcerandom + 22, ClientSize.Height / 2);
            npoints_player[1] = new PointF(ClientSize.Width / 2 + sourcerandom + 4, ClientSize.Height / 2 - 10);
            npoints_player[2] = new PointF(ClientSize.Width / 2 + sourcerandom + 4, ClientSize.Height / 2 + 10);

            if (right == true)
            {
                r += 7;

                if (r >= ((float)360 / n))
                {
                    right = false;
                    m += (float)360 / n;
                    r = 0;

                }

            }
            if (left == true)
            {
                l -= 7;
                if (l <= ((float)360 / n) * (-1))
                {
                    left = false;
                    m += (float)360 / n * (-1);
                    l = 0;
                }
            }

            angle = (float)360 / n / 2 * (-1) + m + r + l;

            if (rotate == false)
            {
                ;
            }
            else
            {
                if (rotate2 == false)
                {
                    j += 2;
                    m += 2;
                }

                else
                {
                    j -= 2;
                    m -= 2;
                }

            }
            

            npoints_player[0] = Rotate(npoints_player[0], centerPoint, angle);
            npoints_player[1] = Rotate(npoints_player[1], centerPoint, angle);
            npoints_player[2] = Rotate(npoints_player[2], centerPoint, angle);

            g.FillPolygon(new SolidBrush(Color.Black), npoints_player);


            
        }



        public PointF Rotate(PointF sourcePoint, PointF centerPoint, double rotateAngle)
        {
            PointF targetPoint = new PointF();

            double radian = rotateAngle / 180 * Math.PI;

            targetPoint.X = (float)(Math.Cos(radian) * (sourcePoint.X - centerPoint.X) - Math.Sin(radian) * (sourcePoint.Y - centerPoint.Y) + centerPoint.X);
            targetPoint.Y = (float)(Math.Sin(radian) * (sourcePoint.X - centerPoint.X) + Math.Cos(radian) * (sourcePoint.Y - centerPoint.Y) + centerPoint.Y);

            return targetPoint;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (label1.Visible == true)
            {
                if (e.KeyCode == Keys.Up)
                {
                    n++;
                    Invalidate();
                    label5.Text = n.ToString() + "-GON";
                }
                if (e.KeyCode == Keys.Down)
                {
                    n--;
                    if (n <= 3)
                        n = 3;
                    Invalidate();
                    label5.Text = n.ToString() + "-GON";
                }
            }
            else
            {
                if (e.KeyCode == Keys.Right)
                {
                    right = true;
                }
                if (e.KeyCode == Keys.Left)
                {
                    left = true;
                }
            }

            if (e.KeyCode == Keys.Space)
            {
                label1.Visible = false;

                timer1.Interval = 1000; // 1초
                timer1.Start();

                label4.Visible = false;
                label5.Visible = false;

                timer2.Start();

                rotate = true;
                timer3.Start();


            }
            if (e.KeyCode == Keys.Escape)
            {

                timer3.Stop();
                timer2.Stop();
                timer1.Stop();

              
                n = 6;
                time = 0;

                label3.Text = time.ToString();
                label1.Visible = true;
                label4.Visible = true;
                label5.Visible = true;



                right = false;
                left = false;
                anything = true;

                j = 0;
                m = 0;
                r = 0;
                l = 0;
                angle = 0;
                rotate = false;
                rotate2 = false;
                distance = 500;
                random_num = 1;
                obstacle_width = 50;
                sourcerandom = 40;
                random_num2 = 1;

                //       Invalidate();

            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)    //화면회전
        {
            Invalidate();   //다시그리기

        }

        private void timer1_Tick(object sender, EventArgs e)    //게임시간
        {
            time++;
            label3.Text = time.ToString();
        }

        private void timer3_Tick(object sender, EventArgs e)    //방향회전
        {
            timer3.Interval = (random.Next(2, 7) * 1000);
            

            if (rotate2 == false)
                rotate2 = true;
            else
                rotate2 = false;

        }
    }
}
