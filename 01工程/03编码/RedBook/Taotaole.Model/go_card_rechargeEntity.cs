using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_card_rechargeEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldCode = "Code";
		public static string FieldCodepwd = "Codepwd";
		public static string FieldMoney = "Money";
		public static string FieldTime = "Time";
		public static string FieldRechargetime = "Rechargetime";
		public static string FieldOrderId = "OrderId";
		public static string FieldUid = "Uid";
		public static string FieldIsrepeat = "Isrepeat";
		public static string FieldUsetype = "Usetype";
		public static string FieldUsetime = "Usetime";
		public static string FieldUsebeizhu = "Usebeizhu";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private string  _code;
		public string  Code
		{
			get{ return _code;}
			set{ _code=value;}
		}
		private string  _codepwd;
		public string  Codepwd
		{
			get{ return _codepwd;}
			set{ _codepwd=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private DateTime  _time;
        public DateTime Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		private DateTime  _rechargetime;
		public DateTime  Rechargetime
		{
			get{ return _rechargetime;}
			set{ _rechargetime=value;}
		}
		private string  _orderId;
		public string  OrderId
		{
			get{ return _orderId;}
			set{ _orderId=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _isrepeat;
		public string  Isrepeat
		{
			get{ return _isrepeat;}
			set{ _isrepeat=value;}
		}
		private string  _usetype;
		public string  Usetype
		{
			get{ return _usetype;}
			set{ _usetype=value;}
		}
		private DateTime  _usetime;
		public DateTime  Usetime
		{
			get{ return _usetime;}
			set{ _usetime=value;}
		}
		private string  _usebeizhu;
		public string  Usebeizhu
		{
			get{ return _usebeizhu;}
			set{ _usebeizhu=value;}
		}
		#endregion

		#region 构造函数
		public go_card_rechargeEntity(){}

		public go_card_rechargeEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldCode] != DBNull.Value)
			{
			_code = (string )dr[FieldCode];
			}
			if (dr[FieldCodepwd] != DBNull.Value)
			{
			_codepwd = (string )dr[FieldCodepwd];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
                _time = (DateTime)dr[FieldTime];
			}
			if (dr[FieldRechargetime] != DBNull.Value)
			{
			_rechargetime = (DateTime )dr[FieldRechargetime];
			}
			if (dr[FieldOrderId] != DBNull.Value)
			{
			_orderId = (string )dr[FieldOrderId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldIsrepeat] != DBNull.Value)
			{
			_isrepeat = (string )dr[FieldIsrepeat];
			}
			if (dr[FieldUsetype] != DBNull.Value)
			{
			_usetype = (string )dr[FieldUsetype];
			}
			if (dr[FieldUsetime] != DBNull.Value)
			{
			_usetime = (DateTime )dr[FieldUsetime];
			}
			if (dr[FieldUsebeizhu] != DBNull.Value)
			{
			_usebeizhu = (string )dr[FieldUsebeizhu];
			}
		}
		#endregion

	}
}
