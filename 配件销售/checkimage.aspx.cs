﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
//用法如下：html代码：<td >
//                    验证码&nbsp;<asp:TextBox ID="txtCheckCode" runat="server" Width="200px" 
//                        ForeColor="#C00000" ToolTip="验证码不区分大小写"></asp:TextBox>
//                    <img src="../web/checkimage.aspx" alt="" style="width: 55px; height: 20px;" title='看不清楚，双击图片换一张。' ondblclick="this.src = '../web/checkimage.aspx?flag=' + Math.random()" border="1" />
//                </td>
//cs代码：if (Session["checkcode"]==null || txtCheckCode.Text.Trim().ToUpper() != Session["checkcode"].ToString())
//        {
//            lblCheckCodeError.Text = "验证码不对！";//            
//            txtCheckCode.Text = "";
//            return;
//        }
public partial class checkimage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //
        // 创建一个包含随机内容的验证码文本 
        System.Random rand = new Random();
        int len = rand.Next(4, 6);
        char[] chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        System.Text.StringBuilder myStr = new System.Text.StringBuilder();
        for (int iCount = 0; iCount < len; iCount++)
        {
            myStr.Append(chars[rand.Next(chars.Length)]);
        }
        string text = myStr.ToString();
        // 保存验证码到 session 中以便其他模块使用 
        this.Session["checkcode"] = text;
        Size ImageSize = Size.Empty;
        Font myFont = new Font("MS Sans Serif", 20);
        // 计算验证码图片大小 
        using (Bitmap bmp = new Bitmap(10, 10))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                SizeF size = g.MeasureString(text, myFont, 10000);
                ImageSize.Width = (int)size.Width + 8;
                ImageSize.Height = (int)size.Height + 8;
            }
        }
        // 创建验证码图片 
        using (Bitmap bmp = new Bitmap(ImageSize.Width, ImageSize.Height))
        {
            // 绘制验证码文本 
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (StringFormat f = new StringFormat())
                {
                    f.Alignment = StringAlignment.Near;
                    f.LineAlignment = StringAlignment.Center;
                    f.FormatFlags = StringFormatFlags.NoWrap;
                    g.DrawString(
                    text,
                    myFont,
                    Brushes.Black,
                    new RectangleF(
                    0,
                    0,
                    ImageSize.Width,
                    ImageSize.Height),
                    f);
                }//using 
            }//using 
            // 制造噪声 杂点面积占图片面积的 30% 
            int num = ImageSize.Width * ImageSize.Height * 30 / 100;
            for (int iCount = 0; iCount < num; iCount++)
            {
                // 在随机的位置使用随机的颜色设置图片的像素 
                int x = rand.Next(ImageSize.Width);
                int y = rand.Next(ImageSize.Height);
                int r = rand.Next(255);
                int g = rand.Next(255);
                int b = rand.Next(255);
                Color c = Color.FromArgb(r, g, b);
                bmp.SetPixel(x, y, c);
            }//for 
            // 输出图片 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            this.Response.ContentType = "image/png";
            ms.WriteTo(this.Response.OutputStream);
            ms.Close();
        }//using 
        myFont.Dispose(); 

    }

    /// 检查指定的文本是否匹配验证码 
    /// 
    /// 要判断的文本 
    /// 是否匹配 
    public static bool CheckCode(string text)
    {
        string txt = System.Web.HttpContext.Current.Session["checkcode"] as string;
        return text == txt;
    } 

}
