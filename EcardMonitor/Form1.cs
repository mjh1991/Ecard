using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace EcardMonitor


{
	public partial class Form1 : Form
	{


		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string strBorder = textBox1.Text;
			string strFilePath = textBox2.Text;
			
			if (File.Exists(strFilePath))
			{
				string[] str = File.ReadAllLines(strFilePath);
				
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i] != "")
					{
						string jpgName = str[i].Substring(str[i].Length - 11, 11);
						if (File.Exists(strBorder + "\\" + jpgName))
						{
							File.Move(strBorder + "\\" + jpgName, strBorder + "\\" + (i + 1).ToString().PadLeft(3, '0') + ".jpg");
						}
					}
				}
				MessageBox.Show("批量更新完成");
			}
			else
			{
				MessageBox.Show("文件不存在");
				return;
			}
			
	
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
			{
				textBox1.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				textBox2.Text = openFileDialog1.FileName;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			string strFilePath = textBox2.Text;
			if (File.Exists(strFilePath))
			{
				
				string[] str = File.ReadAllLines(strFilePath);
				int lines = 0;
				for (int i = 0; i < str.Length; i++)
				{
					str[i] = str[i].Replace("<br />", "");
					str[i] = str[i].Replace(" ", "");
					str[i] = str[i].Replace("</div>", "");
					str[i] = str[i].Replace("<imgsrc=\"", "");
					str[i] = str[i].Replace("\"border=\"0\"onclick=\"zoom(this)\"onload=\"attachimg(this,'load')\"alt=\"\"/>", "");
					if (str[i] != "") lines++;
					
				}
				string[] str1 = new string[lines];
				lines = 0;
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i] != "")
					{
						str1[lines++] = str[i];
					}
				}
				File.WriteAllLines(strFilePath,str1);
				MessageBox.Show("修正成功");
			}
			else
			{
				MessageBox.Show("文件不存在");
				return;
			}
		}
	}
}