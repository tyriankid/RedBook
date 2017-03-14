using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 渠道推广类型-实体类
	/// </summary>
	[Serializable]
	public  class go_channel_typeEntity {

		#region 字段名
		public static string FieldTid = "Tid";
		public static string FieldTypename = "Typename";
		public static string FieldRemark = "Remark";
		#endregion

		#region 属性
		private int  _tid;
		public int  Tid
		{
			get{ return _tid;}
			set{ _tid=value;}
		}
		private string  _typename;
		public string  Typename
		{
			get{ return _typename;}
			set{ _typename=value;}
		}
		private string  _remark;
		public string  Remark
		{
			get{ return _remark;}
			set{ _remark=value;}
		}
		#endregion

		#region 构造函数
		public go_channel_typeEntity(){}

		public go_channel_typeEntity(DataRow dr)
		{
			if (dr[FieldTid] != DBNull.Value)
			{
			_tid = (int )dr[FieldTid];
			}
			if (dr[FieldTypename] != DBNull.Value)
			{
			_typename = (string )dr[FieldTypename];
			}
			if (dr[FieldRemark] != DBNull.Value)
			{
			_remark = (string )dr[FieldRemark];
			}
		}
		#endregion

	}
}
