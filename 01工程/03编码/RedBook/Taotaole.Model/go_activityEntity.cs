using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_activityEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldTitle = "Title";
		public static string FieldContent = "Content";
		public static string FieldA_type = "A_type";
		public static string FieldAmount = "Amount";
		public static string FieldSent_type = "Sent_type";
		public static string FieldCount = "Count";
		public static string FieldSent_count = "Sent_count";
		public static string FieldStarttime = "Starttime";
		public static string FieldEndtime = "Endtime";
		public static string FieldTimespans = "Timespans";
		public static string FieldCode_ids_level = "Code_ids_level";
		public static string FieldCode_config_ids = "Code_config_ids";
		public static string FieldUse_range = "Use_range";
		public static string FieldShopnum = "Shopnum";
		public static string FieldShopnummin = "Shopnummin";
		public static string FieldShopnummax = "Shopnummax";
		public static string FieldAddtime = "Addtime";
		public static string FieldQualification = "Qualification";
		public static string FieldState = "State";
		public static string FieldRedpackday = "Redpackday";
		public static string FieldNextweeknum = "Nextweeknum";
		public static string FieldUsercodeinfo = "Usercodeinfo";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _title;
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private string  _content;
		public string  Content
		{
			get{ return _content;}
			set{ _content=value;}
		}
		private byte  _a_type;
		public byte  A_type
		{
			get{ return _a_type;}
			set{ _a_type=value;}
		}
		private int  _amount;
		public int  Amount
		{
			get{ return _amount;}
			set{ _amount=value;}
		}
		private byte  _sent_type;
		public byte  Sent_type
		{
			get{ return _sent_type;}
			set{ _sent_type=value;}
		}
		private int  _count;
		public int  Count
		{
			get{ return _count;}
			set{ _count=value;}
		}
		private int  _sent_count;
		public int  Sent_count
		{
			get{ return _sent_count;}
			set{ _sent_count=value;}
		}
		private DateTime  _starttime;
		public DateTime  Starttime
		{
			get{ return _starttime;}
			set{ _starttime=value;}
		}
		private DateTime  _endtime;
		public DateTime  Endtime
		{
			get{ return _endtime;}
			set{ _endtime=value;}
		}
		private string  _timespans;
		public string  Timespans
		{
			get{ return _timespans;}
			set{ _timespans=value;}
		}
		private string  _code_ids_level;
		public string  Code_ids_level
		{
			get{ return _code_ids_level;}
			set{ _code_ids_level=value;}
		}
		private string  _code_config_ids;
		public string  Code_config_ids
		{
			get{ return _code_config_ids;}
			set{ _code_config_ids=value;}
		}
		private string  _use_range;
		public string  Use_range
		{
			get{ return _use_range;}
			set{ _use_range=value;}
		}
		private int  _shopnum;
		public int  Shopnum
		{
			get{ return _shopnum;}
			set{ _shopnum=value;}
		}
		private int  _shopnummin;
		public int  Shopnummin
		{
			get{ return _shopnummin;}
			set{ _shopnummin=value;}
		}
		private int  _shopnummax;
		public int  Shopnummax
		{
			get{ return _shopnummax;}
			set{ _shopnummax=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private decimal  _qualification;
		public decimal  Qualification
		{
			get{ return _qualification;}
			set{ _qualification=value;}
		}
		private byte  _state;
		public byte  State
		{
			get{ return _state;}
			set{ _state=value;}
		}
		private int  _redpackday;
		public int  Redpackday
		{
			get{ return _redpackday;}
			set{ _redpackday=value;}
		}
		private int  _nextweeknum;
		public int  Nextweeknum
		{
			get{ return _nextweeknum;}
			set{ _nextweeknum=value;}
		}
		private string  _usercodeinfo;
		public string  Usercodeinfo
		{
			get{ return _usercodeinfo;}
			set{ _usercodeinfo=value;}
		}
		#endregion

		#region 构造函数
		public go_activityEntity(){}

		public go_activityEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldContent] != DBNull.Value)
			{
			_content = (string )dr[FieldContent];
			}
			if (dr[FieldA_type] != DBNull.Value)
			{
			_a_type = (byte )dr[FieldA_type];
			}
			if (dr[FieldAmount] != DBNull.Value)
			{
			_amount = (int )dr[FieldAmount];
			}
			if (dr[FieldSent_type] != DBNull.Value)
			{
			_sent_type = (byte )dr[FieldSent_type];
			}
			if (dr[FieldCount] != DBNull.Value)
			{
			_count = (int )dr[FieldCount];
			}
			if (dr[FieldSent_count] != DBNull.Value)
			{
			_sent_count = (int )dr[FieldSent_count];
			}
			if (dr[FieldStarttime] != DBNull.Value)
			{
			_starttime = (DateTime )dr[FieldStarttime];
			}
			if (dr[FieldEndtime] != DBNull.Value)
			{
			_endtime = (DateTime )dr[FieldEndtime];
			}
			if (dr[FieldTimespans] != DBNull.Value)
			{
			_timespans = (string )dr[FieldTimespans];
			}
			if (dr[FieldCode_ids_level] != DBNull.Value)
			{
			_code_ids_level = (string )dr[FieldCode_ids_level];
			}
			if (dr[FieldCode_config_ids] != DBNull.Value)
			{
			_code_config_ids = (string )dr[FieldCode_config_ids];
			}
			if (dr[FieldUse_range] != DBNull.Value)
			{
			_use_range = (string )dr[FieldUse_range];
			}
			if (dr[FieldShopnum] != DBNull.Value)
			{
			_shopnum = (int )dr[FieldShopnum];
			}
			if (dr[FieldShopnummin] != DBNull.Value)
			{
			_shopnummin = (int )dr[FieldShopnummin];
			}
			if (dr[FieldShopnummax] != DBNull.Value)
			{
			_shopnummax = (int )dr[FieldShopnummax];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldQualification] != DBNull.Value)
			{
			_qualification = (decimal )dr[FieldQualification];
			}
			if (dr[FieldState] != DBNull.Value)
			{
			_state = (byte )dr[FieldState];
			}
			if (dr[FieldRedpackday] != DBNull.Value)
			{
			_redpackday = (int )dr[FieldRedpackday];
			}
			if (dr[FieldNextweeknum] != DBNull.Value)
			{
			_nextweeknum = (int )dr[FieldNextweeknum];
			}
			if (dr[FieldUsercodeinfo] != DBNull.Value)
			{
			_usercodeinfo = (string )dr[FieldUsercodeinfo];
			}
		}
		#endregion

	}
}
