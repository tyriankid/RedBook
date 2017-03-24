using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_User_DynamicEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldUserId = "UserId";
		public static string FieldDynamicType = "DynamicType";
		public static string FieldBusinessId = "BusinessId";
		public static string FieldAddTime = "AddTime";
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
		private string  _dynamicType;
		public string  DynamicType
		{
			get{ return _dynamicType;}
			set{ _dynamicType=value;}
		}
		private string  _businessId;
		public string  BusinessId
		{
			get{ return _businessId;}
			set{ _businessId=value;}
		}
		private DateTime  _addTime;
		public DateTime  AddTime
		{
			get{ return _addTime;}
			set{ _addTime=value;}
		}
		#endregion

		#region 构造函数
		public RB_User_DynamicEntity(){}

		public RB_User_DynamicEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (Guid )dr[FieldId];
			}
			if (dr[FieldUserId] != DBNull.Value)
			{
			_userId = (int )dr[FieldUserId];
			}
			if (dr[FieldDynamicType] != DBNull.Value)
			{
			_dynamicType = (string )dr[FieldDynamicType];
			}
			if (dr[FieldBusinessId] != DBNull.Value)
			{
			_businessId = (string )dr[FieldBusinessId];
			}
			if (dr[FieldAddTime] != DBNull.Value)
			{
			_addTime = (DateTime )dr[FieldAddTime];
			}
		}
		#endregion

	}
}
