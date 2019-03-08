using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Text.RegularExpressions;

namespace PetSuppliesPlus.Framework
{
    public static class Extenctions
    {

        /// <summary>
        /// to check object is parsable to Date or not
        /// </summary>
        /// <param name="value">object date value</param>
        /// <returns>returns bool</returns>
        public static bool isDate(this object value)
        {
            if (value == null) { return false; }
            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// to check string is parsable to decimal or not
        /// </summary>
        /// <param name="value">string numeric value</param>
        /// <returns>returns bool</returns>
        public static bool IsDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        /// <summary>
        /// to check string is parsable to integer or not
        /// </summary>
        /// <param name="value">string number value</param>
        /// <returns>returns bool</returns>
        public static bool IsNumber(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }
            try
            {
                Convert.ToInt64(value);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        /// <summary>
        /// to validate Loopcard number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidLoopCardNo(this string value)
        {
            return value.IsNumber() ? (value.Trim().Length == 16) : false;
            //return value.IsNumber();
        }

        /// <summary>
        /// to validate enctypted value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidEncryptedID(this string value)
        {
            return value.IsNumber();
        }

        /// <summary>
        /// to encrypt string into MD5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnctyptedPassword(this string value)
        {
            Security objSecurity = new Security();
            return objSecurity.MD5Hash(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnctypt(this string value)
        {
            //Security objSecutiry = new Security();
            //return objSecutiry.Encrypt(value);
            return value;
        }

        /// <summary>
        /// to decrypt enctypted value
        /// </summary>
        /// <param name="value">enctypted</param>
        /// <returns></returns>
        public static string ToDecrypt(this string value)
        {
            // Security objSecutiry = new Security();
            //return objSecutiry.Decrypt(value);
            return value;
        }

        /// <summary>
        /// calculate number of records to be skip 
        /// </summary>
        /// <param name="value">current page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns>number of records to be skip</returns>
        public static int TableSkipRecord(this int value, int pageSize)
        {
            int skip = (value - 1) * pageSize;
            return skip;
        }

        /// <summary>
        /// to convert date to (dd hh mm) string format, ex. 03d 12h 22m
        /// </summary>
        /// <param name="value">expiration date</param>
        /// <returns>string date, ex. 03d 12h 22m </returns>
        public static string ToExpireTime(this DateTime value)
        {
            string days = (value - DateTime.Now).TotalDays.ToString("00d");
            string hours = (value - DateTime.Now).Hours.ToString("00h");
            string minutes = (value - DateTime.Now).Minutes.ToString("00m");
            string expireTime = value.ToString("dd") + "d " +
                value.ToString("hh") + "h " +
                value.ToString("mm") + "m ";
            expireTime = days + " " + hours.Replace("-", "") + " " + minutes.Replace("-", "");
            return expireTime;
        }

        /// <summary>
        /// to convert video name to video thumbnail name
        /// </summary>
        /// <param name="value">video file name</param>
        /// <returns></returns>
        public static string ToImageName(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "" : value;
            return value.Replace("mp4", "jpg");
        }

        /// <summary>
        /// to encode url
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value, Encoding.ASCII);
        }

        /// <summary>
        /// to decode encoded url
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlDecode(this string value)
        {
            return HttpUtility.UrlDecode(value, Encoding.ASCII);
        }

        /// <summary>
        /// to remove special charactar from string 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlFilter(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "" : value;
            value = value.Replace(" ", "-");
            value = value.Replace("&", "and");
            value = Regex.Replace(value, "[^0-9a-zA-Z,-]+", "");
            return value.ToLower();
        }

        /// <summary>
        /// to replace special charactar to string 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FromUrlFilter(this string value)
        {
            value = string.IsNullOrEmpty(value) ? "" : value;
            value = value.Replace("-", " ");
            value = value.Replace("&", "and");
            return value.ToLower();
        }

        /// <summary>
        /// to make video link
        /// </summary>
        /// <param name="title">video title</param>
        /// <param name="EncryptedId">video encrypted id</param>
        /// <returns>video url</returns>
        public static string ToMakeVideoLink(this string title, string EncryptedId)
        {
            return "watch/" + EncryptedId + "/" + title.ToUrlFilter();
        }

        /// <summary>
        /// convert seconds to hours minutes and seconds
        /// </summary>
        /// <param name="duration">total duration in seconds</param>
        /// <returns>duration in minutes</returns>
        public static decimal ToDuration(this double totalSeconds)
        {
            int minutes = 0;
            int seconds = 0;
            decimal duration = 0;

            seconds = Convert.ToInt32(totalSeconds % 60);


            string time = "";
            if (totalSeconds >= 60)
            {
                minutes = Convert.ToInt32(totalSeconds / 60);
                //time = Convert.ToInt32(totalSeconds / 60).ToString("00.");
                //time += Convert.ToInt32(totalSeconds % 60).ToString();
            }
            else
            {
                //time = Convert.ToInt32(totalSeconds % 60).ToString("00.00");
            }

            duration = Convert.ToDecimal(minutes + (seconds * 0.01));

            return duration;
        }

        /// <summary>
        /// To check data file extension like csv,xls
        /// </summary>
        /// <param name="fileExtension">file extension</param>
        /// <returns></returns>
        public static bool IsDataFileExtension(this String fileExtension)
        {
            fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "").ToLower() : fileExtension.ToLower();
            return (fileExtension == "csv" || fileExtension == "xls" || fileExtension == "xlsx");
        }

        /// <summary>
        /// To check csv file extension 
        /// </summary>
        /// <param name="fileExtension">file extension</param>
        /// <returns></returns>
        public static bool IsCsvFile(this String fileExtension)
        {
            fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "").ToLower() : fileExtension.ToLower();
            return (fileExtension == "csv");
        }

        /// <summary>
        /// To check excel file extension xls,xlsx
        /// </summary>
        /// <param name="fileExtension">file extension</param>
        /// <returns></returns>
        public static bool IsXlsFile(this String fileExtension)
        {
            fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "").ToLower() : fileExtension.ToLower();
            return (fileExtension == "xls" || fileExtension == "xlsx");
        }

        /// <summary>
        /// To get limited text
        /// </summary>
        /// <param name="value">text</param>
        /// <returns>string</returns>
        public static string GetLimitedText(this String value, int length)
        {
            if (string.IsNullOrEmpty(value)) { return ""; }

            if (value.Length > length)
            {
                string tmp = value.Substring(0, length);
                return tmp.Contains(' ') ? tmp.Substring(0, tmp.LastIndexOf(' ')) : tmp;
            }
            else
            {
                return value;
            }

        }

        public static TimeSpan ToTimeSpan(this DateTime value)
        {
            return new TimeSpan(value.Hour, value.Minute, value.Second);
        }
    }
}
