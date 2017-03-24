using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_User_CommentEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldBusinessId = "BusinessId";
		public static string FieldBusinessType = "BusinessType";
		public static string FieldContent = "Content";
		public static string FieldAddTime = "AddTime";
		public static string FieldFabulousCount = "FabulousCount";
		#endregion

		#region 属性
		private Guid  _id;
		public Guid  Id
		{
			get{ return _id;}
			set{ _id=value;}
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
		private string  _content;
		public string  Content
		{
			get{ return _content;}
			set{ _content=value;}
		}
		private DateTime  _addTime;
		public DateTime  AddTime
		{
			get{ return _addTime;}
			set{ _addTime=value;}
		}
		private int  _fabulousCount;
		public int  FabulousCount
		{
			get{ return _fabulousCount;}
			set{ _fabulousCount=value;}
		}
		#endregion

		#region 构造函数
		public RB_User_CommentEntity(){}

		public RB_User_CommentEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (Guid )dr[FieldId];
			}
			if (dr[FieldBusinessId] != DBNull.Value)
			{
			_businessId = (Guid )dr[FieldBusinessId];
			}
			if (dr[FieldBusinessType] != DBNull.Value)
			{
			_businessType = (string )dr[FieldBusinessType];
			}
			if (dr[FieldContent] != DBNull.Value)
			{
			_content = (string )dr[FieldContent];
			}
			if (dr[FieldAddTime] != DBNull.Value)
			{
			_addTime = (DateTime )dr[FieldAddTime];
			}
			if (dr[FieldFabulousCount] != DBNull.Value)
			{
			_fabulousCount = (int )dr[FieldFabulousCount];
			}
		}
		#endregion

	}
}
