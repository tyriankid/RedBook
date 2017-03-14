using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Taotaole
{
    /// <summary>
    /// 时时同步服务(间隔时间较短,使用场景：团购活动到时开奖、团购未成团到时退款)
    /// </summary>
    public class IHost
    {
        //若方法中需要访问数据库等则需要添加方法的声明
        //[Microsoft.SqlServer.Server.SqlFunction(SystemDataAccess = Microsoft.SqlServer.Server.SystemDataAccessKind.Read, DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static string SyncAlways(int type = 1, int dataid = 0, int dataid2 = 0)
        {
            string id = DateTime.Now.ToString("yyMMddHHmmss");
            switch (type)
            { 
                case 1: //开奖
                    id += "_" + new WebUtils().DoPost(API_Domain + "ilerrpush", string.Format("action=sendtuansuccess&sign={0}&id={1}", GetSignAppInner(id), id));
                    //File.AppendAllText("C:\\Lib\\Debug_Logger.txt", string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id));
                    break;
                case 2: //退款
                    id += "_" + new WebUtils().DoPost(API_Domain + "ilerrpush", string.Format("action=sendtuanfail&sign={0}&id={1}", GetSignAppInner(id), id));
                    //File.AppendAllText("C:\\Lib\\Debug_Logger.txt", string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id));
                    break;
                case 3: //微信
                    id += "_" + new WebUtils().DoPost(API_Domain + "ilerrpush", string.Format("action=sendwxmsg&sign={0}&id={1}&yid={1}", GetSignAppInner(dataid.ToString()), dataid,dataid2));
                    break;
            }
            return id;
        }

        private static readonly string connection = "server=.;uid=yihui;pwd=YiHuiSql120251986154;Trusted_Connection=no;database=1yuangou";//服务只能注册单dll，因此...
        //private static readonly string API_Domain = "http://tapi.ewaywin.com/";
        private static readonly string API_Domain = "http://10.26.90.248/";
        
        
        /// <summary>
        /// 调用存储过程_团购商品到时开奖
        /// </summary>
        public static DataTable CreateOrderTuanTimer()
        {
            DataTable dtData = new DataTable();
            SqlParameter[] para ={ };
            using (SqlCommand command = CreateDbCommand(connection, "CreateOrderTuanTimer", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                SqlDataAdapter ada = new SqlDataAdapter(command);
                ada.Fill(dtData);
                command.Connection.Close();
            }
            return dtData;
        }

        /// <summary>
        /// 调用存储过程_团购商品定时退款
        /// </summary>
        public static DataTable GetOrderTuanRefund()
        {
            DataTable dtData = new DataTable();
            SqlParameter[] para = { };
            using (SqlCommand command = CreateDbCommand(connection, "CreateOrderTuanRefund", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                SqlDataAdapter ada = new SqlDataAdapter(command);
                ada.Fill(dtData);
                command.Connection.Close();
            }
            return dtData;
        }

        #region 创建一个SqlCommand对象
        /// <summary>
        /// 创建一个SqlCommand对象
        /// </summary>
        /// <param name="connStr">数据库链接字符串</param>   
        /// <param name="sql">要执行的查询语句</param>   
        /// <param name="parameters">执行SQL查询语句所需要的参数</param>
        /// <param name="commandType">执行的SQL语句的类型</param>
        private static SqlCommand CreateDbCommand(string connStr, string sql, SqlParameter[] parameters, CommandType commandType)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = commandType;
            command.Connection = connection;
            if (!(parameters == null || parameters.Length == 0))
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        #endregion

        private static string GetSignAppInner(string s)
        {
            string str = string.Format("GetSign_{0}_AppInnerKey_jhb",s);
            byte[] result = Encoding.Default.GetBytes(str);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

    }

    /// <summary>
    /// 模拟浏览器请求(Get、POST)
    /// </summary>
    public class WebUtils
    {
        public string DoPost(string url, string value)
        {
            HttpWebRequest webRequest = this.GetWebRequest(url, "POST");
            webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
            return this.GetResponseAsString(rsp, Encoding.UTF8);
        }

        public HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest request = null;
            if (url.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(url);
            }
            request.ServicePoint.Expect100Continue = false;
            request.Method = method;
            request.KeepAlive = true;
            request.UserAgent = "Ilerr";
            return request;
        }

        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream responseStream = null;
            StreamReader reader = null;
            string str;
            try
            {
                responseStream = rsp.GetResponseStream();
                reader = new StreamReader(responseStream, encoding);
                str = reader.ReadToEnd();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return str;
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }


}
