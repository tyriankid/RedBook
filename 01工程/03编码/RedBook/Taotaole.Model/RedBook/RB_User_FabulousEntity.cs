using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_User_FabulousEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUserId = "UserId";
		public static string FieldBusinessId = "BusinessId";
		public static string FieldBusinessType = "BusinessType";
		public static string FieldAddTime = "AddTime";
		public static string FieldStatus = "Status";
		#endregion

		#region 属性
		private Guid  _id;
		public Guid  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private int  _userId;
		public int  UserId
		{
			get{ return _userId;}
			set{ _userId=value;}
		}
		private Guid  _businessId;
		public Guid  BusinessId
		{
			get{ return _businessId;}
			set{ _businessId=value;}
		}
		private string  _businessType;
		public string  BusinessType
		{
			get{ return _businessType;}
			set{ _businessType=value;}
		}
		private DateTime  _addTime;
		public DateTime  AddTime
		{
			get{ return _addTime;}
			set{ _addTime=value;}
		}
		private int  _status;
		public int  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		#endregion

		#region 构造函数
		public RB_User_FabulousEntity(){}

		public RB_User_FabulousEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (Guid )dr[FieldId];
			}
			if (dr[FieldUserId] != DBNull.Value)
			{
			_userId = (int )dr[FieldUserId];
			}
			if (dr[FieldBusinessId] != DBNull.Value)
			{
			_businessId = (Guid )dr[FieldBusinessId];
			}
			if (dr[FieldBusinessType] != DBNull.Value)
			{
			_businessType = (string )dr[FieldBusinessType];
			}
			if (dr[FieldAddTime] != DBNull.Value)
			{
			_addTime = (DateTime )dr[FieldAddTime];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
		}
		#endregion

	}
}
