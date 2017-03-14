using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model
{
    /// <summary>
    /// -实体类
    /// </summary>
    [Serializable]
    public class go_memberEntity
    {

        #region 字段名
        public static string FieldUid = "Uid";
        public static string FieldUsername = "Username";
        public static string FieldEmail = "Email";
        public static string FieldMobile = "Mobile";
        public static string FieldPassword = "Password";
        public static string FieldUser_ip = "User_ip";
        public static string FieldImg = "Img";
        public static string FieldQianming = "Qianming";
        public static string FieldGroupid = "Groupid";
        public static string FieldAddgroup = "Addgroup";
        public static string FieldMoney = "Money";
        public static string FieldTuanmoney = "Tuanmoney";
        public static string FieldEmailcode = "Emailcode";
        public static string FieldMobilecode = "Mobilecode";
        public static string FieldOthercode = "Othercode";
        public static string FieldPasscode = "Passcode";
        public static string FieldReg_key = "Reg_key";
        public static string FieldScore = "Score";
        public static string FieldLuckyb = "Luckyb";
        public static string FieldJingyan = "Jingyan";
        public static string FieldYaoqing = "Yaoqing";
        public static string FieldBand = "Band";
        public static string FieldTime = "Time";
        public static string FieldHeadimg = "Headimg";
        public static string FieldWxid = "Wxid";
        public static string FieldTypeid = "Typeid";
        public static string FieldAuto_user = "Auto_user";
        public static string FieldServicecityid = "Servicecityid";
        public static string FieldServicechannelid = "Servicechannelid";
        public static string FieldApprouse = "Approuse";
        public static string FieldTelcode = "Telcode";
        public static string FieldCeaseuser = "Ceaseuser";
        public static string FieldUnionid = "Unionid";
        public static string FieldAppwxid = "Appwxid";
        public static string FieldZenPoint = "zenPoint";
        #endregion

        #region 属性
        private int _uid;
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _user_ip;
        public string User_ip
        {
            get { return _user_ip; }
            set { _user_ip = value; }
        }
        private string _img;
        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }
        private string _qianming;
        public string Qianming
        {
            get { return _qianming; }
            set { _qianming = value; }
        }
        private int _groupid;
        public int Groupid
        {
            get { return _groupid; }
            set { _groupid = value; }
        }
        private string _addgroup;
        public string Addgroup
        {
            get { return _addgroup; }
            set { _addgroup = value; }
        }
        private decimal _money;
        public decimal Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private decimal _tuanmoney;
        public decimal Tuanmoney
        {
            get { return _tuanmoney; }
            set { _tuanmoney = value; }
        }
        private string _emailcode;
        public string Emailcode
        {
            get { return _emailcode; }
            set { _emailcode = value; }
        }
        private string _mobilecode;
        public string Mobilecode
        {
            get { return _mobilecode; }
            set { _mobilecode = value; }
        }
        private int _othercode;
        public int Othercode
        {
            get { return _othercode; }
            set { _othercode = value; }
        }
        private string _passcode;
        public string Passcode
        {
            get { return _passcode; }
            set { _passcode = value; }
        }
        private string _reg_key;
        public string Reg_key
        {
            get { return _reg_key; }
            set { _reg_key = value; }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        private int _luckyb;
        public int Luckyb
        {
            get { return _luckyb; }
            set { _luckyb = value; }
        }
        private int _jingyan;
        public int Jingyan
        {
            get { return _jingyan; }
            set { _jingyan = value; }
        }
        private int _yaoqing;
        public int Yaoqing
        {
            get { return _yaoqing; }
            set { _yaoqing = value; }
        }
        private string _band;
        public string Band
        {
            get { return _band; }
            set { _band = value; }
        }
        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
        private string _headimg;
        public string Headimg
        {
            get { return _headimg; }
            set { _headimg = value; }
        }
        private string _wxid;
        public string Wxid
        {
            get { return _wxid; }
            set { _wxid = value; }
        }
        private int _typeid;
        public int Typeid
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        private int _auto_user;
        public int Auto_user
        {
            get { return _auto_user; }
            set { _auto_user = value; }
        }
        private int _servicecityid;
        public int Servicecityid
        {
            get { return _servicecityid; }
            set { _servicecityid = value; }
        }
        private int _servicechannelid;
        public int Servicechannelid
        {
            get { return _servicechannelid; }
            set { _servicechannelid = value; }
        }
        private int _approuse;
        public int Approuse
        {
            get { return _approuse; }
            set { _approuse = value; }
        }
        private string _telcode;
        public string Telcode
        {
            get { return _telcode; }
            set { _telcode = value; }
        }
        private int _ceaseuser;
        public int Ceaseuser
        {
            get { return _ceaseuser; }
            set { _ceaseuser = value; }
        }
        private string _unionid;
        public string Unionid
        {
            get { return _unionid; }
            set { _unionid = value; }
        }
        private string _appwxid;
        public string Appwxid
        {
            get { return _appwxid; }
            set { _appwxid = value; }
        }
        private decimal _zenPoint;
        public decimal ZenPoint
        {
            get { return _zenPoint; }
            set { _zenPoint = value; }
        }
        #endregion

        #region 构造函数
        public go_memberEntity() { }

        public go_memberEntity(DataRow dr)
        {
            if (dr[FieldUid] != DBNull.Value)
            {
                _uid = (int)dr[FieldUid];
            }
            if (dr[FieldUsername] != DBNull.Value)
            {
                _username = (string)dr[FieldUsername];
            }
            if (dr[FieldEmail] != DBNull.Value)
            {
                _email = (string)dr[FieldEmail];
            }
            if (dr[FieldMobile] != DBNull.Value)
            {
                _mobile = (string)dr[FieldMobile];
            }
            if (dr[FieldPassword] != DBNull.Value)
            {
                _password = (string)dr[FieldPassword];
            }
            if (dr[FieldUser_ip] != DBNull.Value)
            {
                _user_ip = (string)dr[FieldUser_ip];
            }
            if (dr[FieldImg] != DBNull.Value)
            {
                _img = (string)dr[FieldImg];
            }
            if (dr[FieldQianming] != DBNull.Value)
            {
                _qianming = (string)dr[FieldQianming];
            }
            if (dr[FieldGroupid] != DBNull.Value)
            {
                _groupid = (int)dr[FieldGroupid];
            }
            if (dr[FieldAddgroup] != DBNull.Value)
            {
                _addgroup = (string)dr[FieldAddgroup];
            }
            if (dr[FieldMoney] != DBNull.Value)
            {
                _money = (decimal)dr[FieldMoney];
            }
            if (dr[FieldTuanmoney] != DBNull.Value)
            {
                _tuanmoney = (decimal)dr[FieldTuanmoney];
            }
            if (dr[FieldEmailcode] != DBNull.Value)
            {
                _emailcode = (string)dr[FieldEmailcode];
            }
            if (dr[FieldMobilecode] != DBNull.Value)
            {
                _mobilecode = (string)dr[FieldMobilecode];
            }
            if (dr[FieldOthercode] != DBNull.Value)
            {
                _othercode = (int)dr[FieldOthercode];
            }
            if (dr[FieldPasscode] != DBNull.Value)
            {
                _passcode = (string)dr[FieldPasscode];
            }
            if (dr[FieldReg_key] != DBNull.Value)
            {
                _reg_key = (string)dr[FieldReg_key];
            }
            if (dr[FieldScore] != DBNull.Value)
            {
                _score = (int)dr[FieldScore];
            }
            if (dr[FieldLuckyb] != DBNull.Value)
            {
                _luckyb = (int)dr[FieldLuckyb];
            }
            if (dr[FieldJingyan] != DBNull.Value)
            {
                _jingyan = (int)dr[FieldJingyan];
            }
            if (dr[FieldYaoqing] != DBNull.Value)
            {
                _yaoqing = (int)dr[FieldYaoqing];
            }
            if (dr[FieldBand] != DBNull.Value)
            {
                _band = (string)dr[FieldBand];
            }
            if (dr[FieldTime] != DBNull.Value)
            {
                _time = (DateTime)dr[FieldTime];
            }
            if (dr[FieldHeadimg] != DBNull.Value)
            {
                _headimg = (string)dr[FieldHeadimg];
            }
            if (dr[FieldWxid] != DBNull.Value)
            {
                _wxid = (string)dr[FieldWxid];
            }
            if (dr[FieldTypeid] != DBNull.Value)
            {
                _typeid = (int)dr[FieldTypeid];
            }
            if (dr[FieldAuto_user] != DBNull.Value)
            {
                _auto_user = (int)dr[FieldAuto_user];
            }
            if (dr[FieldServicecityid] != DBNull.Value)
            {
                _servicecityid = (int)dr[FieldServicecityid];
            }
            if (dr[FieldServicechannelid] != DBNull.Value)
            {
                _servicechannelid = (int)dr[FieldServicechannelid];
            }
            if (dr[FieldApprouse] != DBNull.Value)
            {
                _approuse = (int)dr[FieldApprouse];
            }
            if (dr[FieldTelcode] != DBNull.Value)
            {
                _telcode = (string)dr[FieldTelcode];
            }
            if (dr[FieldCeaseuser] != DBNull.Value)
            {
                _ceaseuser = (int)dr[FieldCeaseuser];
            }
            if (dr[FieldUnionid] != DBNull.Value)
            {
                _unionid = (string)dr[FieldUnionid];
            }
            if (dr[FieldAppwxid] != DBNull.Value)
            {
                _appwxid = (string)dr[FieldAppwxid];
            } 
            if (dr[FieldZenPoint] != DBNull.Value)
            {
                _zenPoint = (decimal)dr[FieldZenPoint];
            }
        }
        #endregion

    }
}
