using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Xml.Linq;
using System.Xml;
using PetSuppliesPlus.Framework;
using System.Web.Mvc;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Framework
{
    public class utilityHelper
    {

        public static double UploadProcess
        {
            get;
            set;
        }
        /// <summary>
        /// to get current date time 
        /// </summary>
        /// <returns>datetime value</returns>
        public static DateTime CurrentDateTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// to get application default language id
        /// </summary>
        public static int ApplicationDefaultLanguage
        {
            get
            {
                return Convert.ToInt32(GetAppSettings("DefaultLanguageID"));
            }
        }

        /// <summary>
        /// to get current ip address
        /// </summary>
        /// <returns>ip address string</returns>
        public static string IpAddress()
        {
            return (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ? HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }

        /// <summary>
        /// to get user browser name
        /// </summary>
        /// <returns>Browser name</returns>
        public static string UserBrowser()
        {
            return HttpContext.Current.Request.Browser.Browser;
        }

        /// <summary>
        /// to get user device name
        /// </summary>
        /// <returns>Browser name</returns>
        public static string UserDevice()
        {
            return HttpContext.Current.Request.Browser.IsMobileDevice == true ? "Mobile" : "Web";
        }

        /// <summary>
        /// to get physical path from application
        /// </summary>
        /// <returns>application path string</returns>
        public static string ApplicationPath()
        {
            return HttpContext.Current.Server.MapPath("~/");
        }

        /// <summary>
        /// to get site url from application setting file
        /// </summary>
        /// <returns>site root url in string</returns>
        public static string SiteUrl()
        {
            return GetAppSettings("SiteUrl");
        }

        /// <summary>
        /// to read application global message from Message.xml file
        /// </summary>
        /// <param name="root">root node name</param>
        /// <param name="node">child node name</param>
        /// <returns>node value</returns>
        public static string ReadGlobalMessage(string root, string node)
        {
            XDocument doc = XDocument.Load(ApplicationPath() + "DataContainer\\XML\\Message.xml");
            return (doc.Descendants(root).Elements().Where(x => x.Name == node).FirstOrDefault().Value ?? "").Replace("[NewLine]", "</br>");
        }

        /// <summary>
        /// to read application global language content from LanguageContent.xml file
        /// </summary>
        /// <param name="root">root node name</param>
        /// <param name="node">child node name</param>
        /// /// <param name="languageId">languageId</param>
        /// <returns>node value</returns>
        public static string ReadLanguageLabel(string root, string node, int languageId)
        {
            XDocument doc = XDocument.Load(ApplicationPath() + "DataContainer\\XML\\LanguageContent.xml");
            var lblNode = doc.Descendants(root).Elements().Where(x => x.Name == node);
            var label = doc.Descendants(root).Elements().Where(x => x.Name == node && x.LastAttribute.Value == languageId.ToString()).FirstOrDefault();
            return (label != null ? label.Value.Trim() : "");

        }

        /// <summary>
        /// to read html or text content from file to be saved in Mailer directory
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>file content in string</returns>
        public static string ReadFromFile(string fileName)
        {
            string path = ApplicationPath() + "DataContainer\\Mailer\\" + fileName;
            return File.ReadAllText(path);
        }

        /// <summary>
        /// used to get application settings value on besis of setting key
        /// </summary>
        /// <param name="key">application setting key</param>
        /// <returns>application setting value in string</returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        

        /// <summary>
        /// to send mail from default email
        /// </summary>
        /// <param name="toEmail">receiver email id</param>
        /// <param name="subject">email subject</param>
        /// <param name="bodyHtml">email body in html</param>
        /// <returns>bool is mail send or not</returns>
        public static void SendMail(string toEmail, string subject, string bodyHtml, string cc = null)
        {
            HttpContext currentContext = HttpContext.Current;
            System.Threading.Tasks.Task.Run(() =>
            {
                HttpContext.Current = currentContext;
                string fromEmail = GetAppSettings("EmailFrom");
                SendMail(toEmail, fromEmail, subject, bodyHtml, null, cc);
            });
        }

        /// <summary>
        /// used to send mail from any email id
        /// </summary>
        /// <param name="toEmail">receiver email id</param>
        /// <param name="fromEmail">sender email id</param>
        /// <param name="subject">email subject</param>
        /// <param name="bodyHtml">email body in html</param>
        /// <param name="AttachmentFullPath">attachment full path</param>
        /// <returns>bool is mail send or not</returns>
        public static bool SendMail(string toEmail, string fromEmail, string subject, string bodyHtml, string AttachmentFullPath, string cc)
        {
            bool status = false;
            try
            {
                if (GetAppSettings("IsMailSetup") != "1")
                {
                    return false;
                }

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient()
                {
                    DeliveryFormat = System.Net.Mail.SmtpDeliveryFormat.International,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    EnableSsl = (GetAppSettings("SmtpSslTrue") == "1"),
                    Host = GetAppSettings("SmtpIP"),
                    Credentials = new System.Net.NetworkCredential(GetAppSettings("SmtpUserName"), GetAppSettings("SmtpPassword")),
                    Port = Convert.ToInt32(GetAppSettings("SmtpPort"))
                }; 

                System.Net.Mail.MailMessage sendMail = new System.Net.Mail.MailMessage();

                sendMail.To.Add(toEmail);

                if (!string.IsNullOrEmpty(cc))
                {
                    sendMail.CC.Add(cc);
                }
                sendMail.From = new System.Net.Mail.MailAddress(fromEmail, GetAppSettings("EmailFromName"));
                sendMail.Body = bodyHtml;
                sendMail.IsBodyHtml = true;
                sendMail.Subject = subject;
                if (GetAppSettings("IsCCEnabled") == "1")
                {
                    sendMail.Bcc.Add(GetAppSettings("AdminEmail"));
                }

                if (!string.IsNullOrEmpty(AttachmentFullPath))
                {
                    sendMail.Attachments.Add(new System.Net.Mail.Attachment(AttachmentFullPath));
                }
                smtpClient.Send(sendMail);

            }
            catch (Exception ee)
            {
                status = false;
                //throw new Exception(ee.Message);
            }
            return status;
        }
        public class ApplicationSettings
        {
            //public static int CompanyDepartmentLimit
            //{
            //    get { return Convert.ToInt32(utilityHelper.GetAppSettings("CompanyDepartmentLimit")); }

            //}

            public static int SuperAdminRoleId
            {
                get { return Convert.ToInt32(utilityHelper.GetAppSettings("SuperAdminRoleId")); }
            }
        }

        /// <summary>
        /// to upload file 
        /// </summary>
        /// <param name="file">posted file base</param>
        /// <param name="fileTuye">fileType like Image, video, document</param>
        /// <returns>saved file name</returns>
        public static string UploadFile(HttpPostedFileBase file)
        {
            string fileName = "";
            try
            {
                fileName = CurrentDateTime.ToString("ddMMyyhhmmss") + "." + file.FileName.Split('.')[1];
                string path = ApplicationPath() + "DataContainer\\Documents\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                file.SaveAs(path + fileName);
            }
            catch { }
            return fileName;
        }

    }
}