using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace DotNet.Utilities
{
    /// <summary>
    /// 验证码类（后台）
    /// </summary>
    public class VerificationCode
    {

        private string tempstring = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";


        #region 定义全局变量
        /// <summary>生成的图片
        /// </summary>
        private Bitmap _CurrentBitmap;

        /// <summary> 验证码图片
        /// </summary>
        public Bitmap CurrentBitmap
        {
            get { return _CurrentBitmap; }
            internal set { _CurrentBitmap = value; }
        }

        /// <summary>生成的验证码
        /// </summary>
        private string _Code;

        /// <summary> 验证码
        /// </summary>
        public string Code
        {
            get { return _Code; }
            internal set { _Code = value; }
        }
        #endregion


        #region 验证码生成前的准备

        /// <summary>需要生成的验证码的字符数
        /// </summary>
        private int _CountCode = 4;

        /// <summary>需要生成的验证码的字符数量
        /// </summary>
        public int CountCode
        {
            internal get { return _CountCode; }
            set { _CountCode = value; }
        }

        /// <summary>生成验证码码图片的宽度
        /// </summary>
        private int _ImageWidth = 200;
        /// <summary>生成验证码图片的宽度
        /// </summary>
        public int ImageWidth
        {
            internal get { return _ImageWidth; }
            set { _ImageWidth = value; }
        }

        /// <summary>生成验证码图片的高度
        /// </summary>
        private int _ImageHeight = 30;
        /// <summary>生成验证码图片的高度
        /// 
        /// </summary>
        public int ImageHeight
        {
            internal get { return _ImageHeight; }
            set { _ImageHeight = value; }
        }

        /// <summary>设定图片的噪点线数量
        /// </summary>
        private int _NoiseLine = 0;
        /// <summary>设定图片的噪点线数量
        /// </summary>
        public int NoiseLine
        {
            get { return _NoiseLine; }
            set { _NoiseLine = value; }
        }

        /// <summary> 前景干扰点
        /// </summary>
        private int _NoisePoint = 0;

        /// <summary>设定验证码图片的前景干扰点数量
        /// </summary>
        public int NoisePoint
        {
            get { return _NoisePoint; }
            set { _NoisePoint = value; }
        }

        /// <summary>字体大小
        /// </summary>
        private int _FontSize = 12;

        /// <summary>字体大小
        /// </summary>
        public int FontSize
        {
            internal get { return _FontSize; }
            set { _FontSize = value; }
        }



        #endregion

        public void GetCaptcha()
        {
            Code = tempcode();
            CurrentBitmap = tempImage();
        }


        #region 生成验证码

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns>验证码字符串</returns>
        private string tempcode()
        {
            StringBuilder result = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < CountCode; i++)
            {
                int index = random.Next(0, tempstring.Length);
                result.Append(tempstring[index]);
            }

            return result.ToString();

        }

        #endregion

        #region 生成图片

        /// <summary>生成验证码图片
        /// </summary>
        /// <returns></returns>
        private Bitmap tempImage()
        {
            //创建验证码图片对象
            Bitmap image = new Bitmap(ImageWidth, ImageHeight);
            //创建绘布对象
            Graphics g = Graphics.FromImage(image);
            try
            {
                Font[] fonts = {
                            new Font(new FontFamily("Impact"), RandomHelper.GetRndNext(FontSize - 2, FontSize), FontStyle.Bold),
                            new Font(new FontFamily("Kokila"), RandomHelper.GetRndNext(FontSize - 3, FontSize), FontStyle.Bold),
                            //new Font(new FontFamily("Bell MT"), RandomHelp.GetRndNext(FontSize - 3, FontSize), FontStyle.Bold),
                            new Font(new FontFamily("MV Boli"), RandomHelper.GetRndNext(FontSize - 3, FontSize), FontStyle.Bold)
                         };

                Color[] bgColors ={
                            Color.Snow,
                            Color.White,
                            Color.Linen,
                            Color.FromArgb(242,251,246),
                            Color.FromArgb(233,240,245),
                            Color.FromArgb(244,244,244),
                            Color.FromArgb(255,228,188)
                        };

                Color[] colors ={
                            Color.FromArgb(24,1,58),
                            Color.Red,
                            Color.DarkRed,
                            Color.Black,
                            Color.RoyalBlue,
                            Color.FromArgb(57,77,14),
                            Color.FromArgb(85,72,64)
                        };

                Color bgcolor = bgColors[RandomHelper.GetRndNext(0, bgColors.Length)];//背景色

                //生成随机生成器
                Random random = new Random();
                //清空图片背景色，画上背景干扰线
                g.Clear(bgcolor);
                for (int i = 0; i < NoiseLine; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.FromArgb(random.Next())), x1, y1, x2, y2);
                }

                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), colors[RandomHelper.GetRndNext(0, colors.Length)], colors[RandomHelper.GetRndNext(0, colors.Length)], 1.2f, true);

                int count = Code.Length;
                for (int i = 0; i < count; i++)
                {
                    int x = i * image.Width / count - i * RandomHelper.GetRndNext(2, 4);
                    g.DrawString(Code.Substring(i, 1), fonts[RandomHelper.GetRndNext(0, fonts.Length)], brush, RandomHelper.GetRndNext(-2, 1) + x, RandomHelper.GetRndNext(-3, -1));
                }


                //int count = Code.Length;
                //for (int i = 0; i < count; i++)
                //{
                //    //字体设定
                //    Font font = new Font("Arial", RandomHelp.GetRndNext(FontSize - 5, FontSize),
                //        (FontStyle.Bold | FontStyle.Italic));

                //    //创建画笔
                //    LinearGradientBrush brush =
                //        new LinearGradientBrush(
                //            new Rectangle( 1 + i * 10, 0, image.Width,
                //                image.Height),
                //            Color.Blue, Color.DarkRed, 2.8f, true);
                //    g.DrawString(Code.Substring(i, 1), font, brush, 5, 2);
                //}

                //画图片的前景干扰点
                for (int i = 0; i < NoisePoint; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //定义干扰线中间点位置
                int midX = RandomHelper.GetRndNext(0, image.Width /2);
                int midX2 = RandomHelper.GetRndNext(midX, image.Width);
                //定义画笔
                Pen pen = new Pen(colors[RandomHelper.GetRndNext(0, colors.Length)], 2);
                //画干扰线
                g.DrawLine(pen, 0, random.Next(image.Height), midX, random.Next(image.Height));
                g.DrawLine(pen, midX, random.Next(image.Height), midX2, random.Next(image.Height));
                g.DrawLine(pen, midX2, random.Next(image.Height), image.Width, random.Next(image.Height));

                //画多一条干扰线
                midX = RandomHelper.GetRndNext(0, image.Width / 2);
                midX2 = RandomHelper.GetRndNext(midX, image.Width);
                //定义画笔
                pen = new Pen(colors[RandomHelper.GetRndNext(0, colors.Length)], 2);
                //画干扰线
                g.DrawLine(pen, 0, random.Next(image.Height), midX, random.Next(image.Height));
                g.DrawLine(pen, midX, random.Next(image.Height), midX2, random.Next(image.Height));
                g.DrawLine(pen, midX2, random.Next(image.Height), image.Width, random.Next(image.Height));

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            }
            catch (Exception)
            {

                return image;
            }

            return image;
        }

        #endregion


        #region 图片旋转函数

        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public Image RotateImg(Image b, int angle)
        {

            angle = angle % 360;

            //弧度转换

            double radian = angle * Math.PI / 180.0;

            double cos = Math.Cos(radian);

            double sin = Math.Sin(radian);

            //原图的宽和高

            int w = b.Width;

            int h = b.Height;

            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));

            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图

            Bitmap dsImage = new Bitmap(W, H);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //计算偏移量

            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //构造图像显示区域：让图像的中心与窗口的中心点一致

            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);

            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            g.TranslateTransform(center.X, center.Y);

            g.RotateTransform(360 - angle);

            //恢复图像在水平和垂直方向的平移

            g.TranslateTransform(-center.X, -center.Y);

            g.DrawImage(b, rect);

            //重至绘图的所有变换

            g.ResetTransform();

            g.Save();

            g.Dispose();

            //保存旋转后的图片

            b.Dispose();

            //dsImage.Save("FocusPoint.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return dsImage;

        }

        public Image RotateImg(string filename, int angle)
        {

            return RotateImg(GetSourceImg(filename), angle);

        }

        public Image GetSourceImg(string filename)
        {

            Image img;

            img = Bitmap.FromFile(filename);

            return img;

        }

        #endregion 图片旋转函数




    }
}
