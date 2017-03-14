using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model {
	/// <summary>
	/// 用户组-实体类
	/// </summary>
	[Serializable]
	public  class go_member_groupEntity {

		#region 字段名
		public static string FieldGroupid = "Groupid";
		public static string FieldName = "Name";
		public static string FieldJingyan_start = "Jingyan_start";
		public static string FieldJingyan_end = "Jingyan_end";
		public static string FieldIcon = "Icon";
		public static string FieldType = "Type";
		#endregion

		#region 属性
		private int  _groupid;
		public int  Groupid
		{
			get{ return _groupid;}
			set{ _groupid=value;}
		}
		private string  _name;
		public string  Name
		{
			get{ return _name;}
			set{ _name=value;}
		}
		private decimal  _jingyan_start;
		public decimal  Jingyan_start
		{
			get{ return _jingyan_start;}
			set{ _jingyan_start=value;}
		}
		private int  _jingyan_end;
		public int  Jingyan_end
		{
			get{ return _jingyan_end;}
			set{ _jingyan_end=value;}
		}
		private string  _icon;
		public string  Icon
		{
			get{ return _icon;}
			set{ _icon=value;}
		}
		private string  _type;
		public string  Type
		{
			get{ return _type;}
			set{ _type=value;}
		}
		#endregion

		#region 构造函数
		public go_member_groupEntity(){}

		public go_member_groupEntity(DataRow dr)
		{
			if (dr[FieldGroupid] != DBNull.Value)
			{
			_groupid = (int )dr[FieldGroupid];
			}
			if (dr[FieldName] != DBNull.Value)
			{
			_name = (string )dr[FieldName];
			}
			if (dr[FieldJingyan_start] != DBNull.Value)
			{
			_jingyan_start = (decimal )dr[FieldJingyan_start];
			}
			if (dr[FieldJingyan_end] != DBNull.Value)
			{
			_jingyan_end = (int )dr[FieldJingyan_end];
			}
			if (dr[FieldIcon] != DBNull.Value)
			{
			_icon = (string )dr[FieldIcon];
			}
			if (dr[FieldType] != DBNull.Value)
			{
			_type = (string )dr[FieldType];
			}
		}
		#endregion

	}
}
