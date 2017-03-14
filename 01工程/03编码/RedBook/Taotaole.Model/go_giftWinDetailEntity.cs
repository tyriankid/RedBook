using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_giftWinDetailEntity {

		#region 字段名
		public static string FieldGiftWinID = "GiftWinID";
		public static string FieldUid = "Uid";
		public static string FieldAddtime = "Addtime";
		public static string FieldGiftId = "GiftId";
		public static string FieldIsWon = "IsWon";
		public static string FieldCostScore = "CostScore";
		public static string FieldOrderId = "OrderId";
		#endregion

		#region 属性
		private int  _giftWinID;
		public int  GiftWinID
		{
			get{ return _giftWinID;}
			set{ _giftWinID=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private DateTime  _addtime;
		public DateTime  Addtime
		{
			get{ return _addtime;}
			set{ _addtime=value;}
		}
		private int  _giftId;
		public int  GiftId
		{
			get{ return _giftId;}
			set{ _giftId=value;}
		}
		private int  _isWon;
		public int  IsWon
		{
			get{ return _isWon;}
			set{ _isWon=value;}
		}
		private int  _costScore;
		public int  CostScore
		{
			get{ return _costScore;}
			set{ _costScore=value;}
		}
		private string  _orderId;
		public string  OrderId
		{
			get{ return _orderId;}
			set{ _orderId=value;}
		}
		#endregion

		#region 构造函数
		public go_giftWinDetailEntity(){}

		public go_giftWinDetailEntity(DataRow dr)
		{
			if (dr[FieldGiftWinID] != DBNull.Value)
			{
			_giftWinID = (int )dr[FieldGiftWinID];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
			if (dr[FieldGiftId] != DBNull.Value)
			{
			_giftId = (int )dr[FieldGiftId];
			}
			if (dr[FieldIsWon] != DBNull.Value)
			{
			_isWon = (int )dr[FieldIsWon];
			}
			if (dr[FieldCostScore] != DBNull.Value)
			{
			_costScore = (int )dr[FieldCostScore];
			}
			if (dr[FieldOrderId] != DBNull.Value)
			{
			_orderId = (string )dr[FieldOrderId];
			}
		}
		#endregion

	}
}
