using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_Book_ContentEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldBusinessId = "BusinessId";
		public static string FieldBusinessType = "BusinessType";
		public static string FieldContent = "Content";
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
		#endregion

		#region 构造函数
		public RB_Book_ContentEntity(){}

		public RB_Book_ContentEntity(DataRow dr)
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
		}
		#endregion

	}
}
