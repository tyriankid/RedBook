using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_scoreDetailEntity {

		#region 字段名
		public static string FieldScoreId = "ScoreId";
		public static string FieldUid = "Uid";
		public static string FieldUsetime = "Usetime";
		public static string FieldMoney = "Money";
		public static string FieldUse_range = "Use_range";
		public static string FieldDetail = "Detail";
		public static string FieldType = "Type";
        public static string FieldOrderId= "orderId";
		#endregion

		#region 属性
		private int  _scoreId;
		public int  ScoreId
		{
			get{ return _scoreId;}
			set{ _scoreId=value;}
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
		private string  _use_range;
		public string  Use_range
		{
			get{ return _use_range;}
			set{ _use_range=value;}
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
        private string _orderId;
        public string OrderId
        {
            get { return _orderId;}
            set { _orderId = value;}
        }
		#endregion

		#region 构造函数
		public go_scoreDetailEntity(){}

		public go_scoreDetailEntity(DataRow dr)
		{
			if (dr[FieldScoreId] != DBNull.Value)
			{
			_scoreId = (int )dr[FieldScoreId];
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
			if (dr[FieldUse_range] != DBNull.Value)
			{
			_use_range = (string )dr[FieldUse_range];
			}
			if (dr[FieldDetail] != DBNull.Value)
			{
			_detail = (string )dr[FieldDetail];
			}
			if (dr[FieldType] != DBNull.Value)
			{
			_type = (string )dr[FieldType];
			}
            if (dr[FieldOrderId] != DBNull.Value)
            {
                _orderId = (string)dr[FieldOrderId];
            }
		}
		#endregion

	}
}
