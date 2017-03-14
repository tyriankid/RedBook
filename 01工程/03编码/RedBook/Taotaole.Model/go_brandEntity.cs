using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 品牌-实体类
	/// </summary>
	[Serializable]
	public  class go_brandEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldCateid = "Cateid";
		public static string FieldStatus = "Status";
		public static string FieldName = "Name";
		public static string FieldOrder = "Order";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private decimal  _cateid;
		public decimal  Cateid
		{
			get{ return _cateid;}
			set{ _cateid=value;}
		}
		private string  _status;
		public string  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		private string  _name;
		public string  Name
		{
			get{ return _name;}
			set{ _name=value;}
		}
		private int  _order;
		public int  Order
		{
			get{ return _order;}
			set{ _order=value;}
		}
		#endregion

		#region 构造函数
		public go_brandEntity(){}

		public go_brandEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldCateid] != DBNull.Value)
			{
			_cateid = (decimal )dr[FieldCateid];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (string )dr[FieldStatus];
			}
			if (dr[FieldName] != DBNull.Value)
			{
			_name = (string )dr[FieldName];
			}
			if (dr[FieldOrder] != DBNull.Value)
			{
			_order = (int )dr[FieldOrder];
			}
		}
		#endregion

	}
}
