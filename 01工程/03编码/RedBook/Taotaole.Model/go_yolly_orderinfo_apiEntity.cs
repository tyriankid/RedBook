using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 永乐对账-实体类
	/// </summary>
	[Serializable]
	public  class go_yolly_orderinfo_apiEntity {

		#region 字段名
		public static string FieldSerialid = "Serialid";
		public static string FieldOrderId = "OrderId";
		public static string FieldMoney = "Money";
		public static string FieldUsenum = "Usenum";
		public static string FieldUsetime = "Usetime";
		public static string FieldUsetype = "Usetype";
		public static string FieldStatus = "Status";
		public static string FieldContext = "Context";
		#endregion

		#region 属性
		private string  _serialid;
		public string  Serialid
		{
			get{ return _serialid;}
			set{ _serialid=value;}
		}
		private string  _orderId;
		public string  OrderId
		{
			get{ return _orderId;}
			set{ _orderId=value;}
		}
		private string  _money;
		public string  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private string  _usenum;
		public string  Usenum
		{
			get{ return _usenum;}
			set{ _usenum=value;}
		}
		private DateTime  _usetime;
		public DateTime  Usetime
		{
			get{ return _usetime;}
			set{ _usetime=value;}
		}
		private string  _usetype;
		public string  Usetype
		{
			get{ return _usetype;}
			set{ _usetype=value;}
		}
		private string  _status;
		public string  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		private string  _context;
		public string  Context
		{
			get{ return _context;}
			set{ _context=value;}
		}
		#endregion

		#region 构造函数
		public go_yolly_orderinfo_apiEntity(){}

		public go_yolly_orderinfo_apiEntity(DataRow dr)
		{
			if (dr[FieldSerialid] != DBNull.Value)
			{
			_serialid = (string )dr[FieldSerialid];
			}
			if (dr[FieldOrderId] != DBNull.Value)
			{
			_orderId = (string )dr[FieldOrderId];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (string )dr[FieldMoney];
			}
			if (dr[FieldUsenum] != DBNull.Value)
			{
			_usenum = (string )dr[FieldUsenum];
			}
			if (dr[FieldUsetime] != DBNull.Value)
			{
			_usetime = (DateTime )dr[FieldUsetime];
			}
			if (dr[FieldUsetype] != DBNull.Value)
			{
			_usetype = (string )dr[FieldUsetype];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (string )dr[FieldStatus];
			}
			if (dr[FieldContext] != DBNull.Value)
			{
			_context = (string )dr[FieldContext];
			}
		}
		#endregion

	}
}
