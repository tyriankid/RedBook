using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 用户地址-实体类
	/// </summary>
	[Serializable]
	public  class go_member_dizhiEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUid = "Uid";
		public static string FieldSheng = "Sheng";
		public static string FieldShi = "Shi";
		public static string FieldXian = "Xian";
		public static string FieldJiedao = "Jiedao";
		public static string FieldYoubian = "Youbian";
		public static string FieldShouhuoren = "Shouhuoren";
		public static string FieldMobile = "Mobile";
		public static string FieldTell = "Tell";
		public static string FieldisDefault = "isDefault";
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
		private string  _sheng;
		public string  Sheng
		{
			get{ return _sheng;}
			set{ _sheng=value;}
		}
		private string  _shi;
		public string  Shi
		{
			get{ return _shi;}
			set{ _shi=value;}
		}
		private string  _xian;
		public string  Xian
		{
			get{ return _xian;}
			set{ _xian=value;}
		}
		private string  _jiedao;
		public string  Jiedao
		{
			get{ return _jiedao;}
			set{ _jiedao=value;}
		}
		private int  _youbian;
		public int  Youbian
		{
			get{ return _youbian;}
			set{ _youbian=value;}
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
		public string  isDefault
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
		private int  _shifoufahuo;
		public int  Shifoufahuo
		{
			get{ return _shifoufahuo;}
			set{ _shifoufahuo=value;}
		}
		#endregion

		#region 构造函数
		public go_member_dizhiEntity(){}

		public go_member_dizhiEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldSheng] != DBNull.Value)
			{
			_sheng = (string )dr[FieldSheng];
			}
			if (dr[FieldShi] != DBNull.Value)
			{
			_shi = (string )dr[FieldShi];
			}
			if (dr[FieldXian] != DBNull.Value)
			{
			_xian = (string )dr[FieldXian];
			}
			if (dr[FieldJiedao] != DBNull.Value)
			{
			_jiedao = (string )dr[FieldJiedao];
			}
			if (dr[FieldYoubian] != DBNull.Value)
			{
			_youbian = (int )dr[FieldYoubian];
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
			if (dr[FieldisDefault] != DBNull.Value)
			{
                _isdefault = (string)dr[FieldisDefault];
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
			_shifoufahuo = (int )dr[FieldShifoufahuo];
			}
		}
		#endregion

	}
}
