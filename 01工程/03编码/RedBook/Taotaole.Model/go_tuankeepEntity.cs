using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_tuankeepEntity {

		#region 字段名
		public static string FieldId = "Id";
		public static string FieldTId = "TId";
		public static string FieldUid = "Uid";
		public static string FieldAddtime = "Addtime";
		#endregion

		#region 属性
		private int  _id;
		public int  Id
		{
			get{ return _id;}
			set{ _id=value;}
		}
		private int  _tId;
		public int  TId
		{
			get{ return _tId;}
			set{ _tId=value;}
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
		#endregion

		#region 构造函数
		public go_tuankeepEntity(){}

		public go_tuankeepEntity(DataRow dr)
		{
			if (dr[FieldId] != DBNull.Value)
			{
			_id = (int )dr[FieldId];
			}
			if (dr[FieldTId] != DBNull.Value)
			{
			_tId = (int )dr[FieldTId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldAddtime] != DBNull.Value)
			{
			_addtime = (DateTime )dr[FieldAddtime];
			}
		}
		#endregion

	}
}
