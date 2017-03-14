using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_activityMemberEntity {

		#region 字段名
		public static string FieldActivityId = "ActivityId";
		public static string FieldUid = "Uid";
		public static string FieldIsReceive = "IsReceive";
		#endregion

		#region 属性
		private int  _activityId;
		public int  ActivityId
		{
			get{ return _activityId;}
			set{ _activityId=value;}
		}
		private int  _uid;
		public int  Uid
		{
			get{ return _uid;}
			set{ _uid=value;}
		}
		private string  _isReceive;
		public string  IsReceive
		{
			get{ return _isReceive;}
			set{ _isReceive=value;}
		}
		#endregion

		#region 构造函数
		public go_activityMemberEntity(){}

		public go_activityMemberEntity(DataRow dr)
		{
			if (dr[FieldActivityId] != DBNull.Value)
			{
			_activityId = (int )dr[FieldActivityId];
			}
			if (dr[FieldUid] != DBNull.Value)
			{
			_uid = (int )dr[FieldUid];
			}
			if (dr[FieldIsReceive] != DBNull.Value)
			{
			_isReceive = (string )dr[FieldIsReceive];
			}
		}
		#endregion

	}
}
