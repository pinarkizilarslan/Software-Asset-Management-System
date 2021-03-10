using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using YazilimVarlikYonetimSistemi.Models.DataContext;
using YazilimVarlikYonetimSistemi.Models.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using YazilimVarlikYonetimSistemi.Connection;
namespace YazilimVarlikYonetimSistemi.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        YazilimVarlikYonetimSistemiContext db = new YazilimVarlikYonetimSistemiContext();
        private DataTable dataTable = new DataTable();
        private DataTable dataTablea = new DataTable();

        public ActionResult Index()
        {
            var model = db.Department.ToList();
            return View(model);
        }

        public ActionResult TimeReport(int id)
        {
            SqlConnection database = new SqlConnection(ConnectionString.connectionString);
            database.Open();
            SqlCommand cmd = new SqlCommand("stp_TimeReport", database);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(cmd);
            dataAdaptor.Fill(dataTable);
            database.Close();
            return View(dataTable);
        }

        public ActionResult CostReport(int id)
        {
            SqlConnection database = new SqlConnection(ConnectionString.connectionString);
            database.Open();
            SqlCommand cmd = new SqlCommand("stp_CostReport", database);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(cmd);
            dataAdaptor.Fill(dataTable);
            database.Close();
            return View(dataTable);

        }

        public ActionResult Page_Load(object sender, EventArgs e, int id)
        {

            SqlConnection database = new SqlConnection(ConnectionString.connectionString);
            database.Open();
            SqlCommand cmd = new SqlCommand("stp_CostReport", database);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptor = new SqlDataAdapter(cmd);
            dataAdaptor.Fill(dataTable);

            SqlCommand cmda = new SqlCommand("stp_TimeReport", database);
            cmda.CommandType = System.Data.CommandType.StoredProcedure;
            cmda.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdaptora = new SqlDataAdapter(cmda);
            dataAdaptora.Fill(dataTablea);

            database.Close();
            SendPDFEmail(dataTable, dataTablea);
            return RedirectToAction("Index");
        }

        private bool SendPDFEmail(DataTable dt, DataTable dta)
        {
            string companyName = "ESOGU";
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Yazılım Raporu</b></td></tr>");
            sb.Append("<tr><td colspan = '2'></td></tr>");
            sb.Append("</td><td><b>Tarih: </b>");
            sb.Append(DateTime.Now);
            sb.Append(" </td></tr>");
            sb.Append("<tr><td colspan = '2'><b>Kurum Adı :</b> ");
            sb.Append(companyName);
            sb.Append("</td></tr>");
            sb.Append("</table>");
            sb.Append("<br />");
            sb.Append("<table border = '1'>");
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th style = 'background-color: #D20B0C;color:#632828'>");
                sb.Append(column.ColumnName);
                sb.Append("</th>");
            }
            sb.Append("</tr>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[column]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            ////time
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("<table border = '1'>");
            sb.Append("<tr>");
            foreach (DataColumn column in dta.Columns)
            {
                sb.Append("<th style = 'background-color: #D20B0C;color:#632828'>");
                sb.Append(column.ColumnName);
                sb.Append("</th>");
            }
            sb.Append("</tr>");
            foreach (DataRow row in dta.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dta.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[column]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");


            StringReader sr = new StringReader(sb.ToString());

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            #region Türkçe karakter sorunu için yazılması gereken kod bloğu.
            FontFactory.Register(Path.Combine("C:\\Windows\\Fonts\\Arial.ttf"), "Garamond"); // kendi türkçe karakter desteği olan fontunuzu da girebilirsiniz.
            StyleSheet css = new StyleSheet();
            css.LoadTagStyle("body", "face", "Garamond");
            css.LoadTagStyle("body", "encoding", "Identity-H");
            css.LoadTagStyle("body", "size", "12pt");
            htmlparser.SetStyleSheet(css);
            #endregion

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                MailMessage mm = new MailMessage("vtysprojeg9@gmail.com", "vtysprojeg9@gmail.com");
                mm.Subject = "Birim Yazılım Raporu";
                mm.Body = "Birim Yazılım Raporu PDF Eki";
                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "RaporPDF.pdf"));
                mm.IsBodyHtml = true;

                try
                {
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("vtysprojeg9@gmail.com", "grup9proje*");
                    smtp.Send(mm);
                    return true;
                }
                catch (Exception)
                {
                    ViewBag.Error = "Mesaj Gönderilken hata olıuştu"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                    return false;
                }

            }
        }


    }
}