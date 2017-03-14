using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Taotaole.IApi
{
    public class SendEsms
    {
        public static int Send(string mobile,string content)
        {
            /*
            string strContent = "企业短信通 测试c#";
            //GET 方式
            String getReturn = doGetRequest("http://api.cnsms.cn/?ac=send&uid=100226&pwd=fa246d0262c3925617b0c72bb20eeb1d&mobile=13585519197,13900008888&content=" + strContent);
            Console.WriteLine("Get response is: " + getReturn);
            StringBuilder sbTemp = new StringBuilder();

            //POST
            sbTemp.Append("ac=send&uid=70299999&pwd=fa246d0262c3925617b0c72bb20eeb1d&mobile=13339196131,15375379376&content=" + strContent);
            byte[] bTemp = Encoding.ASCII.GetBytes(sbTemp.ToString());
            String postReturn = doPostRequest("http://api.cnsms.cn/", bTemp);
            Console.WriteLine("Post response is: " + postReturn);
            */

            string url = "http://120.26.244.194:8888/sms.aspx?action=send";
            string para = string.Empty;
            para += string.Format("&userid={0}", 3041);
            para += string.Format("&account={0}", "jhblr9");
            para += string.Format("&password={0}", "yihuikeji888");
            para += string.Format("&content={0}", HttpContext.Current.Server.UrlEncode(content));
            para += string.Format("&mobile={0}", mobile);
            para += string.Format("&sendtime={0}", "");//不定时发送，值为0，定时发送，输入格式YYYYMMDDHHmmss的日期值
            string str = doPostRequest(url, Encoding.ASCII.GetBytes(para));
            DataSet ds=new DataSet();
            ds.ReadXml(str);
            //new SystemDataSet().ReadXml(xmlSR, XmlReadMode.InferTypedSchema)
            return 0;

        }

         //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }
        
        //GET方式发送得结果
        private static String doGetRequest(string url)
        {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
    }
    
}