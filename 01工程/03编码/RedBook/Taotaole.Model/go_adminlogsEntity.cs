using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_adminlogsEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUsername = "Username";
		public static string FieldLoginip = "Loginip";
		public static string FieldActiontime = "Actiontime";
		public static string FieldActionform = "Actionform";
        public static string FieldAction = operationAction.Add.ToString();
		public static string FieldInfobefore = "Infobefore";
		public static string FieldInfoafter = "Infoafter";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _username;
		public string  Username
		{
			get{ return _username;}
			set{ _username=value;}
		}
		private string  _loginip;
		public string  Loginip
		{
			get{ return _loginip;}
			set{ _loginip=value;}
		}
		private DateTime  _actiontime;
		public DateTime  Actiontime
		{
			get{ return _actiontime;}
			set{ _actiontime=value;}
		}
		private string  _actionform;
		public string  Actionform
		{
			get{ return _actionform;}
			set{ _actionform=value;}
		}
        private operationAction _action;
        public operationAction Action
		{
			get{ return _action;}
			set{ _action=value;}
		}
		private string  _infobefore;
		public string  Infobefore
		{
			get{ return _infobefore;}
			set{ _infobefore=value;}
		}
		private string  _infoafter;
		public string  Infoafter
		{
			get{ return _infoafter;}
			set{ _infoafter=value;}
		}
		#endregion

		#region 构造函数
		public go_adminlogsEntity(){}

		public go_adminlogsEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldUsername] != DBNull.Value)
			{
			_username = (string )dr[FieldUsername];
			}
			if (dr[FieldLoginip] != DBNull.Value)
			{
			_loginip = (string )dr[FieldLoginip];
			}
			if (dr[FieldActiontime] != DBNull.Value)
			{
			_actiontime = (DateTime )dr[FieldActiontime];
			}
			if (dr[FieldActionform] != DBNull.Value)
			{
			_actionform = (string )dr[FieldActionform];
			}
            if (dr[FieldAction] != DBNull.Value)
			{
			_action = (operationAction )dr[FieldAction];
			}
			if (dr[FieldInfobefore] != DBNull.Value)
			{
			_infobefore = (string )dr[FieldInfobefore];
			}
			if (dr[FieldInfoafter] != DBNull.Value)
			{
			_infoafter = (string )dr[FieldInfoafter];
			}
		}
		#endregion

	}

    public enum operationAction
    { 
         Add,
        Delete,
        Update,
        Review,
        Login
    }
}
