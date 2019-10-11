using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using OpenCvSharp.XImgProc;
//using OpenCvSharp.XPhoto;
using OpenCvSharp;

namespace MV_EXE_1_0
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        string debug_str = "";
        static string[] cur_img_path =new string[9] {"./imgs/pic_01.jpg", "./imgs/pic_02.jpg", "./imgs/pic_03.jpg", "./imgs/pic_04.jpg",
            "./imgs/pic_05.jpg", "./imgs/pic_06.jpg", "./imgs/pic_07.jpg", "./imgs/pic_08.jpg", "./imgs/pic_09.jpg" };
        static string[] cur_pro_method = new string[6] {"Canny","Laplacian","Sobel","Roberts","Kirsch","Susan"};
        Mat cv_out_img;
        Mat cv_cur_img;
        int cur_method = 99;
        public MainWindow()
        {
            InitializeComponent();
            FormInit();
        }
        private void FormInit()
        {
            sel_img.Items.Add("Image01");
            sel_img.Items.Add("Image02");
            sel_img.Items.Add("Image03");
            sel_img.Items.Add("Image04");
            sel_img.Items.Add("Image05");
            sel_img.Items.Add("Image06");
            sel_img.Items.Add("Image07");
            sel_img.Items.Add("Image08");
            sel_img.Items.Add("Image09");
            sel_img.SelectedIndex = 1;

            //ori_img.Background = Image.FromFile(cur_img_path[sel_img.SelectedIndex]);
     
        }
        public Mat getImage(int img_index)
        {
            //cv_cur_img = new Mat(cur_img_path[sel_img.SelectedIndex],ImreadModes.AnyColor);
            cv_cur_img = Cv2.ImRead(cur_img_path[sel_img.SelectedIndex]);
            debug_win.Text = cur_img_path[sel_img.SelectedIndex];
            Cv2.ImShow(cur_img_path[sel_img.SelectedIndex], cv_cur_img);
            cv_out_img = cv_cur_img;
            return cv_cur_img;
        }
        public void showImage(int img_index)
        {
            switch (img_index)
            {
                case 0:
                    cv_out_img = cv_cur_img.Canny(Canny_min.Value,Canny_max.Value);
                    cur_method = 0;
                    break;
                case 1:
                    Cv2.Laplacian(cv_cur_img, cv_out_img, -1,3);
                    cur_method = 1;
                    break;
                case 2:
                    //Cv2.Sobel(cv_cur_img, cv_out_img, MatType.CV_16S, 1, 0, 3);
                    Sobel(cur_img_path[sel_img.SelectedIndex]);
                    cur_method = 2;
                    break;
                case 3:
                    cur_method = 3;
                    ImageOperator(cur_img_path[sel_img.SelectedIndex]);
                    break;
                case 4:
                    Cv2.Laplacian(cv_cur_img, cv_out_img, -1, 3);
                    break;
                case 5:
                    Cv2.Laplacian(cv_cur_img, cv_out_img, -1, 3);
                    break;
                case 6:
                    Cv2.Laplacian(cv_cur_img, cv_out_img, -1, 3);
                    break;
                case 7:
                    Cv2.Laplacian(cv_cur_img, cv_out_img, -1, 3);
                    break;


            }
            if (img_index!=3&&img_index!=2) {
                Cv2.ImShow(cur_pro_method[cur_method], cv_out_img);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void sel_img_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (sel_img.SelectedIndex + 1)
            {
                case 1:
                    debug_str = "您选择了图片一！\nyou have selected image 1!\n";
                    getImage(0);
                    break;
                case 2:
                    debug_str = "您选择了图片二！\nyou have selected image 2!\n";
                    getImage(1);
                    break;
                case 3:
                    debug_str = "您选择了图片三！\nyou have selected image 3!\n";
                    getImage(2);
                    break;
                case 4:
                    debug_str = "您选择了图片四！\nyou have selected image 4!\n";
                    getImage(3);
                    break;
                case 5:
                    debug_str = "您选择了图片五！\nyou have selected image 5!\n";
                    getImage(4);
                    break;
                case 6:
                    debug_str = "您选择了图片六！\nyou have selected image 6!\n";
                    getImage(5);
                    break;
                case 7:
                    debug_str = "您选择了图片七！\nyou have selected image 7!\n";
                    getImage(6);
                    break;
                case 8:
                    debug_str = "您选择了图片八！\nyou have selected image 8!\n";
                    getImage(7);
                    break;

            }
            debug_win.Text = debug_str + cur_img_path[sel_img.SelectedIndex];
        }
        private static void ImageOperator(string path)
        {
            using (Mat src = new Mat(path, ImreadModes.Color))
            using (Mat dstX = new Mat())
            {
                #region Robert算子
                Mat dstY = new Mat();
                //Robert算子 X向量
                ///*
                // *   +1       0
                // *   
                // *    0      -1
                // *   
                // *   二位矩阵
                // */
                InputArray kernelRX = InputArray.Create<int>(new int[2, 2] { { 1, 0 }, { 0, -1 } });
                Cv2.Filter2D(src, dstX, src.Depth(), kernelRX, new OpenCvSharp.Point(-1, -1), 0);

                ////Robert算子 Y向量
                ///*
                //*     0      +1
                //*     
                //*    -1       0
                //*   
                //*   二位矩阵
                //*/
                InputArray kernelRY = InputArray.Create<int>(new int[2, 2] { { 0, 1 }, { -1, 0 } });
                Cv2.Filter2D(src, dstY, src.Depth(), kernelRY, new OpenCvSharp.Point(-1, -1), 0);
                #endregion



                //return dstX;
                using (new OpenCvSharp.Window("Rober X", WindowMode.Normal, dstX)) ;
                //using (new OpenCvSharp.Window("Rober Y", WindowMode.Normal, dstY)) ;
                //using (new OpenCvSharp.Window("SRC", WindowMode.Normal, src))


            }
        }
        private static void Sobel(string path)
        {
            using (Mat src = new Mat(path, ImreadModes.AnyColor | ImreadModes.AnyDepth))
            {
                //1：高斯模糊平滑
                Mat dst = new Mat();
                Cv2.GaussianBlur(src, dst, new OpenCvSharp.Size(3, 3), 0, 0, BorderTypes.Default);
                //转为灰度
                //Mat gray = new Mat();
                //Cv2.CvtColor(dst, gray, ColorConversionCodes.BGR2GRAY);

                MatType m = src.Type();

                //求 X 和 Y 方向的梯度  Sobel  and scharr
                Mat xgrad = new Mat();
                Mat ygrad = new Mat();
                Cv2.Sobel(src, xgrad, MatType.CV_16S, 1, 0, 3);
                Cv2.Sobel(src, ygrad, MatType.CV_16S, 0, 1, 3);

                Cv2.ConvertScaleAbs(xgrad, xgrad);//缩放、计算绝对值并将结果转换为8位。不做转换的化显示不了，显示图相只能是8U类型 
                Cv2.ConvertScaleAbs(ygrad, ygrad);

                //加强边缘检测
                //Cv2.Scharr(gray, xgrad, -1, 1, 0, 3);
                //Cv2.Scharr(gray, ygrad, -1, 0, 1, 3);

                Mat output = new Mat(xgrad.Size(), xgrad.Type());
                //图像混合相加（基于权重 0.5）不精确
                //Cv2.AddWeighted(xgrad, 0.5, ygrad, 0.5, 0, output);

                //基于 算法 G=|Gx|+|Gy|    
                int width = xgrad.Cols;
                int hight = xgrad.Rows;

                //基于 G= (Gx*Gx +Gy*Gy)的开方根
                for (int x = 0; x < hight; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        int xg = xgrad.At<byte>(x, y);
                        int yg = ygrad.At<byte>(x, y);
                        //byte xy =(byte) (xg + yg);
                        double v1 = Math.Pow(xg, 2);
                        double v2 = Math.Pow(yg, 2);
                        int val = (int)Math.Sqrt(v1 + v2);
                        if (val > 255) //确保像素值在 0 -- 255 之间
                        {
                            val = 255;
                        }
                        if (val < 0)
                        {
                            val = 0;
                        }
                        byte xy = (byte)val;
                        output.Set<byte>(x, y, xy);
                    }
                }
                using (new OpenCvSharp.Window("X Image", WindowMode.Normal, xgrad)) ;
                //using (new Window("Y Image", WindowMode.Normal, ygrad))
                //using (new Window("OUTPUT Image", WindowMode.Normal, output))
                //using (new Window("SRC", WindowMode.Normal, src))
                
            }
        }

        private void Canny_Clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您选择了Canny算子！\nyou have selected Canny!\n";
            debug_win.Text = debug_str;
            cur_method = 0;
            showImage(0);
        }

        private void Laplacian_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您选择了Laplacian算子！\nyou have selected Laplacian!\n";
            debug_win.Text = debug_str;
            cur_method = 1;
            showImage(1);
        }

        private void Sobel_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您选择了Sobel算子！\nyou have selected Sobel!\n";
            debug_win.Text = debug_str;
            showImage(2);
        }

        private void Robert_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您选择了Robert算子！\nyou have selected Robert!\n";
            debug_win.Text = debug_str;
            showImage(3);
        }

        private void Kirsch_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您选择了Kirsch算子！\nyou have selected Kirsch!\n";
            debug_win.Text = debug_str;
        }

        private void Susan_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您选择了Susan算子！\nyou have selected Susan!\n";
            debug_win.Text = debug_str;
        }

        private void Save_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您保存了图片！\nyou have save the image!\n";
            debug_win.Text = debug_str;
        }

        private void Reset_clicked(object sender, RoutedEventArgs e)
        {
            debug_str = "您清除了当前图片！\nyou have reset current image!\n";
            debug_win.AppendText(debug_str);
        }

        private void thread_hold_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Canny_min_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //debug_str = Canny_min.Value.ToString();
            //Canny_min_show.Text = "test";
            
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Canny_min_show.AppendText(" ");
           // debug_str = Canny_min.Value.ToString();
           // Canny_min_show.AppendText(debug_str);
        }
    }
}
