using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class RB_User_FavoriteEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldBookId = "BookId";
		public static string FieldUserId = "UserId";
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
		private Guid  _bookId;
		public Guid  BookId
		{
			get{ return _bookId;}
			set{ _bookId=value;}
		}
		private int  _userId;
		public int  UserId
		{
			get{ return _userId;}
			set{ _userId=value;}
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
		public RB_User_FavoriteEntity(){}

		public RB_User_FavoriteEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (Guid )dr[FieldId];
			}
			if (dr[FieldBookId] != DBNull.Value)
			{
			_bookId = (Guid )dr[FieldBookId];
			}
			if (dr[FieldUserId] != DBNull.Value)
			{
			_userId = (int )dr[FieldUserId];
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
