using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AutoLink
{
    //enum unsigned int {

    //}
public partial class Link : Form
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Link()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 有参构造构造函数重载
        /// </summary>
        /// <param name="strIp"></param>
        //public Link
        //(
        //    string strIp ,
        //    string strPort,
        //    string strFreq
        //)
        //{
        //    this.strIpNumber = strIp;
        //    this.strPortNumber = strPort;
        //    this.strFreqNumber = strFreq;
        //    //InitializeComponent();
        //}

        //private string strIpNumber = "0.0.0.0";
        /// <summary>
        /// get,set 访问器，后可加默认值，设置strIp类外只读权限
        /// </summary>
        public string strIpNumber{ get; set; } = "0.0.0.0";
        public string strPortNumber{ get; set; } = "80";
        public string strFreqNumber { get; set; } = "300";
        /// <summary>
        /// 勾选框控件，是否自动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                groupBox1.Enabled = false;
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox3.Enabled = false;
            }
        }
        #region 发送前数据验证
        /// <summary>
        /// 按钮控件执行发送操作（为实现）
        /// 弹出文本框提示用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(checkBox1.CheckState == CheckState.Checked)
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("please select freq");
                }
                else
                {
                    //MessageBox.Show("get from mySQL");
                    string strSqlCmd = "INSERT INTO testing (`test`) VALUES('yesen')";
                    MySqlConnector mySqlCon = new MySqlConnector();
                    int nLinkState = mySqlCon.UpdateDatabase("world", "127.0.0.1", "yes", "YeSen@1233", strSqlCmd);
                    if (nLinkState != -1)
                    {
                        string strTemp = string.Format("连接正常,修改的最后一行为{0}", nLinkState);
                        MessageBox.Show(strTemp);
                    }
                    else
                        MessageBox.Show("连接失败！");
                }
            }
            else//手动发送
            {
                if (FocusOnBlankFromEnd()) 
                {
                    string strOutMsg = "";
                    string[] strMsg = { };
                    CheckBlankText(out strMsg);
                    for (int i = 0; i<strMsg.Length;i++ )
                    {
                        if (strMsg[i] != "")
                            strOutMsg += strMsg[i] + ",";
                    }

                    //foreach(string strTemp in strMsg)
                    //{
                    //    strOutMsg += strTemp;
                    //}
                   // string strFinalMsg = "有以下几个文本框未输入数字：{0}",strOutMsg;
                    MessageBox.Show(strOutMsg);
                }
                else
                {
                    //string strTemp = "Ip:" + strIpNumber + "\r\nPort:" + strPortNumber + "\r\nFreq:" + strFreqNumber;
                    //string strTemp = "Ip:{0}\r\nPort:{1}\r\nFreq:{2}", strIpNumber, strPortNumber, strFreqNumber;
                    //string strTemp = string.Format("Ip:{0}\r\nPort:{1}\r\nFreq:{2}", strIpNumber, strPortNumber, strFreqNumber);
                    //MessageBox.Show(strTemp);
                    string strSqlCmd = "INSERT INTO world () ";
                    MySqlConnector mySqlCon = new MySqlConnector();
                    int nLinkState = mySqlCon.UpdateDatabase("learn", "127.0.0.1", "yes", "YeSen@1233", strSqlCmd);
                    if (nLinkState!=-1)
                    {
                        string strTemp = string.Format("连接正常,修改的最后一行为{0}",nLinkState);
                        MessageBox.Show(strTemp);
                    }
                    else
                        MessageBox.Show("连接失败！");
                    ClearTextBox(textBox1);
                    ClearTextBox(textBox2);
                    ClearTextBox(textBox3);
                    ClearTextBox(textBox4);
                }
            }
        }
        /// <summary>
        /// 有空白返回true
        /// 检查未输入数字的文本框，并交由out返回又空白文本框序号数组成的字符串数组
        /// </summary>
        /// <param name="strTxtBlank"></param>
        /// <returns></returns>
        private bool CheckBlankText (out string[] strTxtBlank)
        {
            string strTemp = "";
            bool bTemp = false;
            if (textBox1.Text == "") { strTemp += "1,"; bTemp = true; }//elif
            if (textBox2.Text == "") { strTemp += "2,"; bTemp = true; }//elif
            if (textBox3.Text == "") { strTemp += "3,"; bTemp = true; }//elif
            if (textBox4.Text == "") { strTemp += "4,"; bTemp = true; }//elif
            if (textBox5.Text == "") { strTemp += "5" ; bTemp = true; }//elif
            strTxtBlank = strTemp.Split(',');
            return bTemp;
        }
        /// <summary>
        /// 有空白返回true
        /// 根据检查出的空文本框序号，依次使之获得焦点
        /// 从前往后更新焦点，最后后面的获得焦点
        /// </summary>
        /// <returns></returns>
        private bool FocusOnBlankFromHead()
        {
            string[] strBlankTxt = { };
            if (CheckBlankText(out strBlankTxt))
            {

                //从前往后更新焦点，最后后面的获得
                foreach (string strTemp in strBlankTxt)
                {
                    int nTemp = Convert.ToInt32(strTemp);
                    switch (nTemp)
                    {
                        case 1:
                            textBox1.Focus();
                            break;
                        case 2:
                            textBox2.Focus();
                            break;
                        case 3:
                            textBox3.Focus();
                            break;
                        case 4:
                            textBox4.Focus();
                            break;
                        case 5:
                            textBox5.Focus();
                            break;
                    }

                }
                return true;
            }//esif
            return false;
        }

        /// <summary>
        ///  有空白返回true
        ///  根据检查出的空文本框序号，依次使之获得焦点
        ///  从后往前更新焦点，最后前面的获得
        /// </summary>
        /// <returns></returns>
        private bool FocusOnBlankFromEnd()
        {
            string[] strBlankTxt = { };
            if (CheckBlankText(out strBlankTxt))
            {
                //从后往前更新焦点，最后前面的获得
                for (int i = strBlankTxt.Length; i > 0; i--)
                {
                    string strTemp = strBlankTxt[i-1];

                    switch (strTemp)
                    {
                        case "1":
                            textBox1.Focus();
                            break;
                        case "2":
                            textBox2.Focus();
                            break;
                        case "3":
                            textBox3.Focus();
                            break;
                        case "4":
                            textBox4.Focus();
                            break;
                        case "5":
                            textBox5.Focus();
                            break;
                    }
                }
                return true;
            }//esif
            return false;
        }
        #endregion

        #region 模仿WindowsIP输入，用于按键检查，实现自动跳转到下一文本框，删除上一文本，防止错误输入 
        /// <summary>
        /// 检查用户输入，通过handled属性跳过不正确输入，并通过便签提示
        /// 只允许数字和部分按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string strMsg = Convert.ToString((int)(e.KeyChar));
            //MessageBox.Show(strMsg);

            if (e.KeyChar == ' ' || e.KeyChar == '\r' || e.KeyChar == '\t')
            {
                e.Handled = true;
                if (textBox1.Text == "")
                    label3.Text = "输入为空";
                else
                    textBox2.Focus();
            }
            else if(e.KeyChar == '\b')
            {
                e.Handled = true;
            }

            else if (((int)e.KeyChar <= 47) || ((int)e.KeyChar >= 58))
            {
                e.Handled = true;
                label3.Text = "请输入数字";
            }
        }
        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '\r' || e.KeyChar == '\t')
            {
                e.Handled = true;
                if (textBox2.Text == "")
                    label3.Text = "输入为空";
                else
                    textBox3.Focus();
            }
            else if (e.KeyChar == '\b')
            {
                if (textBox2.Text == "")
                {
                    textBox1.Focus();
                }
            }

            else if (((int)e.KeyChar <= 47) || ((int)e.KeyChar >= 58))
            {
                e.Handled = true;
                label3.Text = "请输入数字";
            }
        }
        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '\r' || e.KeyChar == '\t')
            {
                e.Handled = true;
                if(textBox3.Text == "")
                    label3.Text = "输入为空";
                else
                    textBox4.Focus();
            }
            else if (e.KeyChar == '\b') 
            {
                if(textBox3.Text == "")
                {
                    textBox2.Focus();
                }
            }

            else if (((int)e.KeyChar <= 47) || ((int)e.KeyChar >= 58))
            {
                e.Handled = true;
                label3.Text = "请输入数字";
            }
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '\r' || e.KeyChar == '\t')
            {
                e.Handled = true;
                if (textBox4.Text == "")
                    label3.Text = "输入为空";
                else
                    textBox5.Focus();
            }
            else if (e.KeyChar == '\b')
            {
                if (textBox4.Text == "")
                {
                    textBox3.Focus();
                }
            }

            else if (((int)e.KeyChar <= 47) || ((int)e.KeyChar >= 58))
            {
                e.Handled = true;
                label3.Text = "请输入数字";
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '\r' || e.KeyChar == '\t')
            {
                e.Handled = true;
                if (textBox4.Text == "")
                    label3.Text = "输入为空";
                else
                    button1.Focus();
            }
            else if (e.KeyChar == '\b')
            {
                if (textBox5.Text == "")
                {
                    textBox4.Focus();
                }
            }

            else if (((int)e.KeyChar <= 47) || ((int)e.KeyChar >= 58))
            {
                e.Handled = true;
                label3.Text = "请输入数字";
            }
        }
        #endregion

        #region 检查文本框内容，用于自动跳转，和超出ip范围重置
        /// <summary>
        /// 当输入达3位时自动设置下一个文本框为焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string[] strTemp = strIpNumber.Split('.');
                strTemp[0] = textBox1.Text;
                if (Convert.ToInt32(textBox1.Text) >= 100 && Convert.ToInt32(textBox1.Text) <= 255)
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                    //MessageBox.Show(strIpNumber);
                    textBox2.Focus();
                }
                else if (Convert.ToInt32(textBox1.Text) > 255)
                {
                    textBox1.Text = "255";
                }
                else 
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                }
            }//elif
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                string[] strTemp = strIpNumber.Split('.');
                strTemp[1] = textBox2.Text;
                if (Convert.ToInt32(textBox2.Text) >= 100 && Convert.ToInt32(textBox2.Text) <= 255)
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                    textBox3.Focus();
                }
                else if (Convert.ToInt32(textBox2.Text) > 255)
                {
                    textBox2.Text = "255";
                }
                else
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                }
            }//elif
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                string[] strTemp = strIpNumber.Split('.');
                strTemp[2] = textBox3.Text;
                if (Convert.ToInt32(textBox3.Text) >= 100 && Convert.ToInt32(textBox3.Text) <= 255)
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                    textBox4.Focus();
                }
                else if (Convert.ToInt32(textBox3.Text) > 255)
                {
                    textBox3.Text = "255";
                }
                else 
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                }
             }//elif
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                string[] strTemp = strIpNumber.Split('.');
                strTemp[3] = textBox4.Text;
                if (Convert.ToInt32(textBox4.Text) >= 100 && Convert.ToInt32(textBox4.Text) <= 255)
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                    textBox5.Focus();
                    // MessageBox.Show(strIpNumber);
                }
                else if (Convert.ToInt32(textBox4.Text) > 255)
                {
                    textBox4.Text = "255";
                }
                else 
                {
                    strIpNumber = strTemp[0] + "." + strTemp[1] + "." + strTemp[2] + "." + strTemp[3];
                }
            }//elif
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                if (Convert.ToInt32(textBox5.Text) >= 0 && Convert.ToInt32(textBox5.Text) < 65532)
                {
                    strPortNumber = textBox5.Text;
                    //MessageBox.Show(strIpNumber);
                }
                else if (Convert.ToInt32(textBox5.Text) > 65532)
                {
                    button1.Focus();
                    strPortNumber = textBox5.Text = "65532";
                }
                else
                {
                    strPortNumber = textBox5.Text;
                }
            }//elif
        }
        #endregion
        //private void IpConvert(string strText)
        //{
        //    if (Convert.ToInt32(strText) >= 100 && Convert.ToInt32(strText) <= 255)
        //    {
        //        string[] strTemp = strIpNumber.Split('.');
        //        strTemp[0] = strText;
        //        strIpNumber = strTemp[0] + strTemp[1] + strTemp[2] + strTemp[3];
        //    }
        //}

        /// <summary>
        /// 用于抖动标签栏未实现
        /// </summary>
        private void ShakeLabel()
        {

        }
        /// <summary>
        /// 为什么会改变实参
        /// </summary>
        /// <param name="textBox"></param>
        private void ClearTextBox( TextBox textBox)
        {
            textBox.Text = "";
        }

        /// <summary>
        /// 通过选框标签获得int频率，并给strstrFreqNumber赋值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetFreqIndex(int index)
        {
            int nTemp = 0;
            switch(index)
            {
                case 0:
                    nTemp = 3000;
                    break;
                case 1:
                    nTemp = 40000;
                    break;
                case 2:
                    nTemp = 500000;
                    break;
            }
            strFreqNumber = Convert.ToString(nTemp);
            return nTemp;
                
        }
        /// <summary>
        /// 切换触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFreqIndex(comboBox1.SelectedIndex);
        }

    }
}
