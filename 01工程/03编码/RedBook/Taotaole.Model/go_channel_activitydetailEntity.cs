using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model
{
    /// <summary>
    /// -实体类
    /// </summary>
    [Serializable]
    public class go_channel_activitydetailEntity
    {

        #region 字段名
        public static string FieldAdid = "Adid";
        public static string FieldAid = "Aid";
        public static string FieldCuid = "Cuid";
        public static string FieldUid = "Uid";
        public static string FieldCreatetime = "Createtime";
        public static string FieldExchange = "Exchange";
        public static string FieldMantime = "Mantime";
        public static string FieldSettlementtime = "Settlementtime";
        public static string FieldApplytime = "Applytime";
        public static string FieldShopid = "Shopid";
        public static string FieldOrderid = "Orderid";
        #endregion

        #region 属性
        private int _adid;
        public int Adid
        {
            get { return _adid; }
            set { _adid = value; }
        }
        private int _aid;
        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }
        private int _cuid;
        public int Cuid
        {
            get { return _cuid; }
            set { _cuid = value; }
        }
        private int _uid;
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private DateTime _createtime;
        public DateTime Createtime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        private int _exchange;
        public int Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }
        private DateTime _mantime;
        public DateTime Mantime
        {
            get { return _mantime; }
            set { _mantime = value; }
        }
        private DateTime _settlementtime;
        public DateTime Settlementtime
        {
            get { return _settlementtime; }
            set { _settlementtime = value; }
        }
        private DateTime _applytime;
        public DateTime Applytime
        {
            get { return _applytime; }
            set { _applytime = value; }
        }
        private int _shopid;
        public int Shopid
        {
            get { return _shopid; }
            set { _shopid = value; }
        }
        private string _orderid;
        public string Orderid
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
        #endregion

        #region 构造函数
        public go_channel_activitydetailEntity() { }

        public go_channel_activitydetailEntity(DataRow dr)
        {
            if (dr[FieldAdid] != DBNull.Value)
            {
                _adid = (int)dr[FieldAdid];
            }
            if (dr[FieldAid] != DBNull.Value)
            {
                _aid = (int)dr[FieldAid];
            }
            if (dr[FieldCuid] != DBNull.Value)
            {
                _cuid = (int)dr[FieldCuid];
            }
            if (dr[FieldUid] != DBNull.Value)
            {
                _uid = (int)dr[FieldUid];
            }
            if (dr[FieldCreatetime] != DBNull.Value)
            {
                _createtime = (DateTime)dr[FieldCreatetime];
            }
            if (dr[FieldExchange] != DBNull.Value)
            {
                _exchange = (int)dr[FieldExchange];
            }
            if (dr[FieldMantime] != DBNull.Value)
            {
                _mantime = (DateTime)dr[FieldMantime];
            }
            if (dr[FieldSettlementtime] != DBNull.Value)
            {
                _settlementtime = (DateTime)dr[FieldSettlementtime];
            }
            if (dr[FieldApplytime] != DBNull.Value)
            {
                _applytime = (DateTime)dr[FieldApplytime];
            }
            if (dr[FieldShopid] != DBNull.Value)
            {
                _shopid = (int)dr[FieldShopid];
            }
            if (dr[FieldOrderid] != DBNull.Value)
            {
                _orderid = (string)dr[FieldOrderid];
            }
        }
        #endregion

    }
}
