using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Taotaole.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class go_wxmenuEntity {

		#region 字段名
		public static string FieldMenuid = "Menuid";
        public static string FieldParentid = "Parentid";
		public static string FieldName = "Name";
		public static string FieldType = "Type";
		public static string FieldUrl = "Url";
        public static string FieldSort= "sort";
		#endregion

		#region 属性
		private int  _menuid;
		public int  Menuid
		{
			get{ return _menuid;}
			set{ _menuid=value;}
		}
        private int _parentid;
        public int Parentid
		{
            get { return _parentid; }
            set { _parentid = value; }
		}
		private string  _name;
		public string  Name
		{
			get{ return _name;}
			set{ _name=value;}
		}
		private string  _type;
		public string  Type
		{
			get{ return _type;}
			set{ _type=value;}
		}
        private string _sort;
		public string  Sort
		{
			get{ return _sort;}
			set{ _sort=value;}
		}
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public IList<go_wxmenuEntity> Chilren { get; set; }
		#endregion

		#region 构造函数
		public go_wxmenuEntity(){}

		public go_wxmenuEntity(DataRow dr)
		{
			if (dr[FieldMenuid] != DBNull.Value)
			{
			_menuid = (int )dr[FieldMenuid];
			}
            if (dr[FieldParentid] != DBNull.Value)
			{
                _parentid = (int)dr[FieldParentid];
			}
			if (dr[FieldName] != DBNull.Value)
			{
			_name = (string )dr[FieldName];
			}
			if (dr[FieldType] != DBNull.Value)
			{
			_type = (string )dr[FieldType];
			}
			if (dr[FieldUrl] != DBNull.Value)
			{
			_url = (string )dr[FieldUrl];
			}
            if (dr[FieldSort] != DBNull.Value)
            {
                _sort = (string)dr[FieldSort];
            }
		}
		#endregion

	}
}
