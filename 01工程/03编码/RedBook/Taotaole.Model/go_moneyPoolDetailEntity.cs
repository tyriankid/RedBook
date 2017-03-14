using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_moneyPoolDetailEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldMoney = "Money";
		public static string FieldMoneyType = "MoneyType";
		public static string FieldAddtime = "Addtime";
		public static string FieldUid = "Uid";
        public static string FieldOrderId = "orderid";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private decimal  _money;
		public decimal  Money
		{
			get{ return _money;}
			set{ _money=value;}
		}
		private string  _moneyType;
		public string  MoneyType
		{
			get{ return _moneyType;}
			set{ _moneyType=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
        private string  _orderid;
        public string  OrderId
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
		#endregion

		#region 构造函数
		public go_moneyPoolDetailEntity(){}

		public go_moneyPoolDetailEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldMoney] != DBNull.Value)
			{
			_money = (decimal )dr[FieldMoney];
			}
			if (dr[FieldMoneyType] != DBNull.Value)
			{
			_moneyType = (string )dr[FieldMoneyType];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
            if (dr[FieldOrderId] != DBNull.Value)
            {
                _orderid = (string)dr[FieldOrderId];
            }
		}
		#endregion

	}
}
