using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using hMailServer;
using Aspose;



namespace diplomv3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Timer"] = DateTime.Now.AddMinutes(5).ToString();
            }
           
        }
        private hMailServer.Application Authenticate(string userName, string password)
        {
            hMailServer.Application hMailApp = new hMailServer.Application();
            if (hMailApp != null)
                hMailApp.Authenticate(userName, password);
            return hMailApp;
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            hMailServer.Application hMailApp = Authenticate("Administrator", "12345");
            hMailServer.Domain myDomain = hMailApp.Domains.ItemByName["diplom.com"];
            if (myDomain != null)
            {
                hMailServer.Account account = myDomain.Accounts.Add();
                account.Address = Результат.Text;
                account.Password = "123";
                account.Active = true;
                account.MaxSize = 10000;
                account.Save();
            }
        }
        protected void Submit_Click2(object sender, EventArgs e)
        {
            string sql5 = "SELECT hm_messages.messageaccountid, hm_messages.messagefilename, hm_messages.messagefrom, hm_messages.messagecreatetime from hm_messages where messageaccountid=(select (accountid) from hm_accounts where accountaddress = '" + Результат.Text + "')";
            DataTable subjects = dbconnection.Request(sql5);
            gvMailBox.DataSource = subjects;
            gvMailBox.DataBind();
        }
        protected void gvMailBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msgfn = Convert.ToString((sender as Button).CommandArgument);
            string login1 = Результат.Text;//Результат.Text
            string login = login1.Substring(0, login1.Length - 11);
            string sql4 = "SELECT (messagefilename) from hm_messages where messageaccountid=(select (accountid) from hm_accounts where accountaddress='"+ Результат.Text + "')";
            DataTable subjects2 = dbconnection.Request(sql4);
            string pathfnfd2 = msgfn.Substring(0, msgfn.Length - 39);
            string pathfnfd = pathfnfd2.Substring(1);
            string pathmain = "C:/Program Files (x86)/hMailServer/Data/diplom.com/";
            string path = pathmain + login + "/" + pathfnfd + "/" + msgfn;
            string output = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".html";

            // Выбор пути к EML файлу 
            using (var message = Aspose.Email.MailMessage.Load(path))
            {
                // конвертация EML в HTML
                message.Save(output, Aspose.Email.SaveOptions.DefaultHtml);
            }
            // Запуск конвертированного html в браузере
            System.Diagnostics.Process.Start(output);
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(Session["Timer"].ToString())) < 0)
            {
                litMsg.Text = "Времени осталось : " + ((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString()
                               + "Минут" + (((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalSeconds) % 60).ToString() + "Секунд";

            }
            else
            {
                litMsg.Text = "Ящик удален...";
                string login1 = Результат.Text;
                string sql5 = "DELETE FROM hm_accounts where accountaddress='" + Результат.Text + "'";
                DataTable subjects3 = dbconnection.Request(sql5); 
                string login = login1.Substring(0, login1.Length - 11);
                string path = "C:/Program Files (x86)/hMailServer/Data/diplom.com/"+login;
                Directory.Delete(path, true);
                Response.Redirect(Request.RawUrl);
            }

        }
        protected void Submit_Click3(object sender, EventArgs e) 
        {     
                Session["Timer"] = DateTime.Now.AddMinutes(5).ToString();
        }
        protected void Submit_Click4(object sender, EventArgs e)
        {
            litMsg.Text = "Ящик удален...";
            string login1 = Результат.Text;
            string sql6 = "DELETE FROM hm_accounts where accountaddress='" + Результат.Text + "'";
            DataTable subjects3 = dbconnection.Request(sql6);
            string login = login1.Substring(0, login1.Length - 11);
            string path = "C:/Program Files (x86)/hMailServer/Data/diplom.com/" + login;
            Directory.Delete(path, true);
            Response.Redirect(Request.RawUrl);
        }
    }
}






