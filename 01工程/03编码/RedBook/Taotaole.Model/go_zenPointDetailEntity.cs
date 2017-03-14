using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_zenPointDetailEntity {

		#region 字段名
		public static string FieldZenId = "ZenId";
		public static string FieldUid = "Uid";
		public static string FieldUsetime = "Usetime";
		public static string FieldMoney = "Money";
        public static string FieldOrderId = "orderId";
		public static string FieldDetail = "Detail";
		public static string FieldType = "Type";
		#endregion

		#region 属性
		private int  _zenId;
		public int  ZenId
		{
			get{ return _zenId;}
			set{ _zenId=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private DateTime  _usetime;
		public DateTime  Usetime
		{
			get{ return _usetime;}
			set{ _usetime=value;}
		}
        private decimal _money;
        public decimal Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private string  _orderId;
		public string  OrderId
		{
            get { return _orderId; }
            set { _orderId = value; }
		}
		private string  _detail;
		public string  Detail
		{
			get{ return _detail;}
			set{ _detail=value;}
		}
		private string  _type;
		public string  Type
		{
			get{ return _type;}
			set{ _type=value;}
		}
		#endregion

		#region 构造函数
		public go_zenPointDetailEntity(){}

		public go_zenPointDetailEntity(DataRow dr)
		{
			if (dr[FieldZenId] != DBNull.Value)
			{
			_zenId = (int )dr[FieldZenId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldUsetime] != DBNull.Value)
			{
			_usetime = (DateTime )dr[FieldUsetime];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (int )dr[FieldMoney];
			}
            if (dr[FieldOrderId] != DBNull.Value)
			{
                _orderId = (string)dr[FieldOrderId];
			}
			if (dr[FieldDetail] != DBNull.Value)
			{
			_detail = (string )dr[FieldDetail];
			}
			if (dr[FieldType] != DBNull.Value)
			{
			_type = (string )dr[FieldType];
			}
		}
		#endregion

	}
}
