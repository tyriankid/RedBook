using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model
{
    /// <summary>
    /// -实体类
    /// </summary>
    [Serializable]
    public class go_adminEntity
    {

        #region 字段名
        public static string FieldUid = "Uid";
        public static string FieldMid = "Mid";
        public static string FieldUsername = "Username";
        public static string FieldUserpass = "Userpass";
        public static string FieldUseremail = "Useremail";
        public static string FieldAddtime = "Addtime";
        public static string FieldLogintime = "Logintime";
        public static string FieldLoginip = "Loginip";
        public static string FieldMenujson = "Menujson";
        public static string FieldState = "State";
        #endregion

        #region 属性
        private int _uid;
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private int _mid;
        public int Mid
        {
            get { return _mid; }
            set { _mid = value; }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _userpass;
        public string Userpass
        {
            get { return _userpass; }
            set { _userpass = value; }
        }
        private string _useremail;
        public string Useremail
        {
            get { return _useremail; }
            set { _useremail = value; }
        }
        private DateTime _addtime;
        public DateTime Addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private DateTime _logintime;
        public DateTime Logintime
        {
            get { return _logintime; }
            set { _logintime = value; }
        }
        private string _loginip;
        public string Loginip
        {
            get { return _loginip; }
            set { _loginip = value; }
        }
        private string _menujson;
        public string Menujson
        {
            get { return _menujson; }
            set { _menujson = value; }
        }
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        #endregion

        #region 构造函数
        public go_adminEntity() { }

        public go_adminEntity(DataRow dr)
        {
            if (dr[FieldUid] != DBNull.Value)
            {
                _uid = (int)dr[FieldUid];
            }
            if (dr[FieldMid] != DBNull.Value)
            {
                _mid = (int)dr[FieldMid];
            }
            if (dr[FieldUsername] != DBNull.Value)
            {
                _username = (string)dr[FieldUsername];
            }
            if (dr[FieldUserpass] != DBNull.Value)
            {
                _userpass = (string)dr[FieldUserpass];
            }
            if (dr[FieldUseremail] != DBNull.Value)
            {
                _useremail = (string)dr[FieldUseremail];
            }
            if (dr[FieldAddtime] != DBNull.Value)
            {
                _addtime = (DateTime)dr[FieldAddtime];
            }
            if (dr[FieldLogintime] != DBNull.Value)
            {
                _logintime = (DateTime)dr[FieldLogintime];
            }
            if (dr[FieldLoginip] != DBNull.Value)
            {
                _loginip = (string)dr[FieldLoginip];
            }
            if (dr[FieldMenujson] != DBNull.Value)
            {
                _menujson = (string)dr[FieldMenujson];
            }
            if (dr[FieldState] != DBNull.Value)
            {
                _state = (int)dr[FieldState];
            }
        }
        #endregion

    }
}
