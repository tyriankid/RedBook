using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 永乐充值-实体类
	/// </summary>
	[Serializable]
	public  class go_yolly_orderinfoEntity {

		#region 字段名
		public static string FieldSerialid = "Serialid";
		public static string FieldOrderId = "OrderId";
		public static string FieldMoney = "Money";
		public static string FieldUsenum = "Usenum";
		public static string FieldUsetime = "Usetime";
		public static string FieldUsetype = "Usetype";
		public static string FieldStatus = "Status";
		public static string FieldContext = "Context";
		public static string FieldIssynchro = "Issynchro";
		public static string FieldPaytype = "Paytype";
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
		private decimal  _money;
		public decimal  Money
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
		private int  _usetype;
		public int  Usetype
		{
			get{ return _usetype;}
			set{ _usetype=value;}
		}
		private int  _status;
		public int  Status
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
		private int  _issynchro;
		public int  Issynchro
		{
			get{ return _issynchro;}
			set{ _issynchro=value;}
		}
		private string  _paytype;
		public string  Paytype
		{
			get{ return _paytype;}
			set{ _paytype=value;}
		}
		#endregion

		#region 构造函数
		public go_yolly_orderinfoEntity(){}

		public go_yolly_orderinfoEntity(DataRow dr)
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
			_money = (decimal )dr[FieldMoney];
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
			_usetype = (int )dr[FieldUsetype];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
			if (dr[FieldContext] != DBNull.Value)
			{
			_context = (string )dr[FieldContext];
			}
			if (dr[FieldIssynchro] != DBNull.Value)
			{
			_issynchro = (int )dr[FieldIssynchro];
			}
			if (dr[FieldPaytype] != DBNull.Value)
			{
			_paytype = (string )dr[FieldPaytype];
			}
		}
		#endregion

	}
}
