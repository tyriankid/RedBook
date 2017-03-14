using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 游戏地址-实体类
	/// </summary>
	[Serializable]
	public  class go_member_dizhi_gameEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUid = "Uid";
		public static string FieldGamename = "Gamename";
		public static string FieldGamearea = "Gamearea";
		public static string FieldGameserver = "Gameserver";
		public static string FieldGametype = "Gametype";
		public static string FieldGameusercode = "Gameusercode";
		public static string FieldShouhuoren = "Shouhuoren";
		public static string FieldMobile = "Mobile";
		public static string FieldTell = "Tell";
		public static string FieldIsdefault = "Isdefault";
		public static string FieldTime = "Time";
		public static string FieldQq = "Qq";
		public static string FieldShifoufahuo = "Shifoufahuo";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _gamename;
		public string  Gamename
		{
			get{ return _gamename;}
			set{ _gamename=value;}
		}
		private string  _gamearea;
		public string  Gamearea
		{
			get{ return _gamearea;}
			set{ _gamearea=value;}
		}
		private string  _gameserver;
		public string  Gameserver
		{
			get{ return _gameserver;}
			set{ _gameserver=value;}
		}
		private string  _gametype;
		public string  Gametype
		{
			get{ return _gametype;}
			set{ _gametype=value;}
		}
		private string  _gameusercode;
		public string  Gameusercode
		{
			get{ return _gameusercode;}
			set{ _gameusercode=value;}
		}
		private string  _shouhuoren;
		public string  Shouhuoren
		{
			get{ return _shouhuoren;}
			set{ _shouhuoren=value;}
		}
		private string  _mobile;
		public string  Mobile
		{
			get{ return _mobile;}
			set{ _mobile=value;}
		}
		private string  _tell;
		public string  Tell
		{
			get{ return _tell;}
			set{ _tell=value;}
		}
		private string  _isdefault;
		public string  Isdefault
		{
			get{ return _isdefault;}
			set{ _isdefault=value;}
		}
		private DateTime  _time;
		public DateTime  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private string  _qq;
		public string  Qq
		{
			get{ return _qq;}
			set{ _qq=value;}
		}
		private decimal  _shifoufahuo;
		public decimal  Shifoufahuo
		{
			get{ return _shifoufahuo;}
			set{ _shifoufahuo=value;}
		}
		#endregion

		#region 构造函数
		public go_member_dizhi_gameEntity(){}

		public go_member_dizhi_gameEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldGamename] != DBNull.Value)
			{
			_gamename = (string )dr[FieldGamename];
			}
			if (dr[FieldGamearea] != DBNull.Value)
			{
			_gamearea = (string )dr[FieldGamearea];
			}
			if (dr[FieldGameserver] != DBNull.Value)
			{
			_gameserver = (string )dr[FieldGameserver];
			}
			if (dr[FieldGametype] != DBNull.Value)
			{
			_gametype = (string )dr[FieldGametype];
			}
			if (dr[FieldGameusercode] != DBNull.Value)
			{
			_gameusercode = (string )dr[FieldGameusercode];
			}
			if (dr[FieldShouhuoren] != DBNull.Value)
			{
			_shouhuoren = (string )dr[FieldShouhuoren];
			}
			if (dr[FieldMobile] != DBNull.Value)
			{
			_mobile = (string )dr[FieldMobile];
			}
			if (dr[FieldTell] != DBNull.Value)
			{
			_tell = (string )dr[FieldTell];
			}
			if (dr[FieldIsdefault] != DBNull.Value)
			{
			_isdefault = (string )dr[FieldIsdefault];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
			if (dr[FieldQq] != DBNull.Value)
			{
			_qq = (string )dr[FieldQq];
			}
			if (dr[FieldShifoufahuo] != DBNull.Value)
			{
			_shifoufahuo = (decimal )dr[FieldShifoufahuo];
			}
		}
		#endregion

	}
}
