using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Text;
using System.IO;

public partial class bzdr : System.Web.UI.Page
{
	private EcardService.Service ser = new EcardService.Service();
	protected void Page_Load(object sender, EventArgs e)
	{
		Security s = Session["sec"] as Security;
		if (s == null)
		{
			Response.Redirect("error.aspx");
		}
	}
	protected void btnDaoRu_Click(object sender, EventArgs e)
	{
		
		if (!FileUpload1.HasFile)
		{
			Response.Write("<script>alert('请选择EXCEL文件！');</script>");
		}
		else
		{
			Security s = Session["sec"] as Security;

			string strUpFilename = FileUpload1.FileName;
			string strSaveAsFilename = s.getUserCode() + "_" + strUpFilename + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + DateTime.Now.Millisecond.ToString() + ".xls";
			string fileFullName = strUpFilename;
			string strExprentFile = strUpFilename.Substring(strUpFilename.LastIndexOf(".") + 1);

			//获取客户端上传文件的完整路径和部分路径
			//cFullFilepath = FileUpload1.PostedFile.FileName;
			//cFilepath = Path.GetDirectoryName(cFullFilepath) + "\\";
			//string pathsf = Session["cFullFilepath"].ToString();

			//判断上传文件是否是EXCEL文件
			if (strExprentFile != "xls")
			{
				Response.Write("<script>alert('请选择EXCEL类型的文件！')</script>");
				//Label5.Text = "请选择EXCEL类型的文件！";
				//Label5.ForeColor = Color.Red;
			}
			else
			{
				//把文件上传到服务器
				//FileUpload1.SaveAs(Server.MapPath("upexcel/" + strUpFilename));
				//ViewState["file"] = Server.MapPath("upexcel/" + strUpFilename);

				FileUpload1.SaveAs(Server.MapPath("upexcel/" + strSaveAsFilename));
				ViewState["file"] = Server.MapPath("upexcel/" + strSaveAsFilename);

				string filePath = Server.MapPath("upexcel") + "\\" + strSaveAsFilename;
				string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
				OleDbConnection oleConn = new OleDbConnection();
				oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";
				try
				{
					oleConn.Open();
				}
				catch
				{

					Response.Write("<script>alert('打开文件失败！请使用标准EXCEL模板！');</script>");
					return;
				}
				string strSheetName = "";
				int nAllCount = 0;
				OleDbCommand odc = new OleDbCommand();
				odc.Connection = oleConn;
				odc.CommandText = "select count(*) from [" + fileName + "$]";

				try
				{
					nAllCount = int.Parse(odc.ExecuteScalar().ToString());
					strSheetName = "[" + fileName + "$]";
				}
				catch
				{
					odc.CommandText = "select count(*) from [Sheet1$]";
					try
					{
						nAllCount = int.Parse(odc.ExecuteScalar().ToString());
						strSheetName = "[Sheet1$]";
					}
					catch
					{

						Response.Write("<script>alert('无法打开指定工作表，工作表名必须为Sheet1和文件名一致！')</script>");
						//Label5.Text = "无法打开指定工作表，工作表名必须为Sheet1和文件名一致！";
						//Label5.ForeColor = Color.Red;
						odc.Dispose();
						oleConn.Close();
						return;
					}
				}

				odc.CommandText = "select 工号,姓名,金额,备注 from " + strSheetName + " where 工号 <> ''";

				OleDbDataAdapter ada = new OleDbDataAdapter(odc.CommandText, oleConn);
				DataTable odr = new DataTable();

				try
				{
					//odr = odc.ExecuteReader();
					ada.Fill(odr);
					odr.TableName = "importOtherRecord";
				}
				catch
				{

					Response.Write("<script>alert('文件格式不正确！请使用标准EXCEL模板！')</script>");
					//Label5.Text = "文件格式不正确！";
					//Label5.ForeColor = Color.Red;
					odc.Dispose();
					oleConn.Close();
					System.IO.File.Delete(filePath);
					return;
				}
				oleConn.Close();
				//int count = ser.importOtherRecord(odr, txtBat.Text, s.getUserCode());
				int count = ser.importOtherRecord(odr, ser.getServerTime().ToString("yyyy-MM-dd HH:mm:ss"), s.getUserCode(),pbModule.getIP());
				if ( count > 0)
				{
					lblMessage.Text = "成功导入" + count.ToString() + "条记录。";
					//txtBat.Text = "";
				}
				else
				{
					lblMessage.Text = "导入失败，请检查EXCEL文件是否有错误。";
				}
				

			
				//System.IO.File.Delete(filePath);

				////////////////////////////////
			}




		}
	}
	protected void btnDaoChuCuoWu_Click(object sender, EventArgs e)
	{
		string errFile;
		string errString;

		//创建导出错误记录的EXCEL的文件名格式
		errString = "errors_";
		Security s = Session["sec"] as Security;
		errFile = errString + s.getUserCode() +DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + DateTime.Now.Millisecond.ToString();

		//创建导出文件的路径
		string filepath2 = "upexcel/" + errFile + ".xls";
		string filepath1 = "\\" + errFile + ".xls";
		string file = Server.MapPath("upexcel") + filepath1;

		DataTable dt2 = new DataTable();
		dt2 = (DataTable)Session["dtError"];
		if (dt2 != null && dt2.Rows.Count != 0)
		{
			////调用删除以前导出的错误记录EXCEL文件的函数
			////deleteTempFile();
			//if (ExportExcel.Export(file, dt2))
			//{
			//    //清空存放错误记录的数据表
			//    DataTable dtError = new DataTable();
			//    dtError.Clear();
			//    dtError.Columns.Clear();
			//    Session["dtError"] = dtError;

			//    dt2.Clear();
			//    dt2.Columns.Clear();
			//    //string filename = ViewState["file"].ToString();
			//    //Response.Redirect(filepath2);
			//    //Response.Write("<script>location.href=('" + filepathscript2 + "')</script>");
			//    //Response.Write("<script>window.open('"+filepath2+"')</>");
			//    //Response.Write("<script>window.open('1.xls')</script>");
			//    //Response.Write("<script>alert('导出成功！')</script>");

			//    //Response.AppendHeader("Content-Disposition ", "attachment;   filename=1.xsl");
			//    //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312 ");
			//    //Response.AddHeader("Content-Length ", file.Length.ToString());
			//    //Response.ContentType = "application/octet-stream ";
			//    //Response.TransmitFile();

			//    //客户端下载所创建的EXCEL表
			//    Response.ContentType = "application/octet-stream";
			//    Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(errFile + ".xls", System.Text.Encoding.UTF8));
			//    //Response.TransmitFile("1.xls");
			//    Response.Charset = "UTF-8";
			//    Response.ContentEncoding = System.Text.Encoding.UTF8;
			//    Response.TransmitFile(file);
			//    Response.End();

			//}
			//else
			//{
			//    //MessageBox.Show("导出失败！");
			//    Response.Write("<script>alert('导出失败！');</script>");
			//    //Label5.Text = "导出失败！";
			//    //Label5.ForeColor = Color.Red;
			//}

			string fileName = errFile + ".xls";

			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			HtmlTextWriter htw = new HtmlTextWriter(sw);

			Page page = new Page();
			HtmlForm form = new HtmlForm();

			GridView gridview1 = new GridView();
			gridview1.Caption = "错误信息";
			gridview1.DataSource = dt2;
			gridview1.DataBind();
			// Deshabilitar la validación de eventos, sólo asp.net 2
			page.EnableEventValidation = false;

			// Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
			page.DesignerInitialize();

			page.Controls.Add(form);
			form.Controls.Add(gridview1);

			page.RenderControl(htw);

			Response.Clear();
			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
			Response.Charset = "UTF-8";
			Response.ContentEncoding = Encoding.UTF8;
			Response.Write(sb.ToString());
			Response.End();

		}
		else
		{
			Response.Write("<script>alert('没有错误记录可供导出，请执行上传和导入步骤！');</script>");
			//Label5.Text = "没有错误记录可供导出，请执行上传和导入步骤！";
			//Label5.ForeColor = Color.Red;
		}
	}


	private void deleteTempFile()
	{
		//获取文件所在文件夹的路径
		string filepath = Server.MapPath("") + "\\upexcel\\";
		System.IO.DirectoryInfo path = new System.IO.DirectoryInfo(filepath);

		//或取出当前文件夹的所有EXCEL文件
		System.IO.FileInfo[] files = path.GetFiles("*.xls");

		//循环判断获取的每个文件是否在当天以前产生的，如果是，则删除。
		foreach (System.IO.FileInfo fileList in files)
		{
			//把文件名保存在字符型变量中
			string filename = fileList.Name;

			//Label5.Text = filename.Substring(0, 2);

			if (filename.Substring(0, 2) == "需要")
			{
				//获取文件的创建日期--在当年中的第几天
				string getBfDay = filename.Substring(filename.IndexOf("-") + 1, filename.LastIndexOf("-") - filename.IndexOf("-") - 1);
				//Label5.Text += sttest;
				//Label5.Text += "99999"+(filename.IndexOf("-")+1).ToString()+filename.LastIndexOf("-").ToString()  ;

				//如果该文件是在当天之前产生的，则删除
				if (Convert.ToInt16(getBfDay) < DateTime.Now.DayOfYear)
				{
					string deleteFileNme = filepath + fileList;
					System.IO.File.Delete(deleteFileNme);
				}
			}
		}
	}
}
