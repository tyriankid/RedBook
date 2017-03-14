using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_zhigouEntity {

		#region 字段名
		public static string FieldQuanId = "QuanId";
		public static string FieldProductid = "Productid";
		public static string FieldTitle = "Title";
		public static string FieldStock = "Stock";
		public static string FieldQuanMoney = "QuanMoney";
		public static string FieldAddTime = "AddTime";
		#endregion

		#region 属性
		private int  _quanId;
		public int  QuanId
		{
			get{ return _quanId;}
			set{ _quanId=value;}
		}
		private int  _productid;
		public int  Productid
		{
			get{ return _productid;}
			set{ _productid=value;}
		}
		private string  _title;
		public string  Title
		{
			get{ return _title;}
			set{ _title=value;}
		}
		private int  _stock;
		public int  Stock
		{
			get{ return _stock;}
			set{ _stock=value;}
		}
		private decimal  _quanMoney;
		public decimal  QuanMoney
		{
			get{ return _quanMoney;}
			set{ _quanMoney=value;}
		}
		private DateTime  _addTime;
		public DateTime  AddTime
		{
			get{ return _addTime;}
			set{ _addTime=value;}
		}
		#endregion

		#region 构造函数
		public go_zhigouEntity(){}

		public go_zhigouEntity(DataRow dr)
		{
			if (dr[FieldQuanId] != DBNull.Value)
			{
			_quanId = (int )dr[FieldQuanId];
			}
			if (dr[FieldProductid] != DBNull.Value)
			{
			_productid = (int )dr[FieldProductid];
			}
			if (dr[FieldTitle] != DBNull.Value)
			{
			_title = (string )dr[FieldTitle];
			}
			if (dr[FieldStock] != DBNull.Value)
			{
			_stock = (int )dr[FieldStock];
			}
			if (dr[FieldQuanMoney] != DBNull.Value)
			{
			_quanMoney = (decimal )dr[FieldQuanMoney];
			}
			if (dr[FieldAddTime] != DBNull.Value)
			{
			_addTime = (DateTime )dr[FieldAddTime];
			}
		}
		#endregion

	}
}
