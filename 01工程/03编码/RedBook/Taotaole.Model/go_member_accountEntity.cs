using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_member_accountEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUid = "Uid";
		public static string FieldType = "Type";
		public static string FieldPay = "Pay";
		public static string FieldContent = "Content";
		public static string FieldMoney = "Money";
		public static string FieldTime = "Time";
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
		private string  _type;
		public string  Type
		{
			get{ return _type;}
			set{ _type=value;}
		}
		private string  _pay;
		public string  Pay
		{
			get{ return _pay;}
			set{ _pay=value;}
		}
		private string  _content;
		public string  Content
		{
			get{ return _content;}
			set{ _content=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private DateTime  _time;
		public DateTime  Time
		{
			get{ return _time;}
			set{ _time=value;}
		}
		#endregion

		#region 构造函数
		public go_member_accountEntity(){}

		public go_member_accountEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldType] != DBNull.Value)
			{
			_type = (string )dr[FieldType];
			}
			if (dr[FieldPay] != DBNull.Value)
			{
			_pay = (string )dr[FieldPay];
			}
			if (dr[FieldContent] != DBNull.Value)
			{
			_content = (string )dr[FieldContent];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldTime] != DBNull.Value)
			{
			_time = (DateTime )dr[FieldTime];
			}
		}
		#endregion

	}
}
