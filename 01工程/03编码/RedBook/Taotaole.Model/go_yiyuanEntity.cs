using System;
using System.Data;
using System.Collections;

namespace Taotaole.Model
{
    /// <summary>
    /// 一元购-实体类
    /// </summary>
    [Serializable]
    public class go_yiyuanEntity
    {

        #region 字段名
        public static string FieldYId = "YId";
        public static string FieldProductid = "Productid";
        public static string FieldTitle = "Title";
        public static string FieldYunjiage = "Yunjiage";
        public static string FieldZongjiage = "Zongjiage";
        public static string FieldZongrenshu = "Zongrenshu";
        public static string FieldCanyurenshu = "Canyurenshu";
        public static string FieldShengyurenshu = "Shengyurenshu";
        public static string FieldQishu = "Qishu";
        public static string FieldMaxqishu = "Maxqishu";
        public static string FieldTime = "Time";
        public static string FieldQ_uid = "Q_uid";
        public static string FieldQ_code = "Q_code";
        public static string FieldQ_counttime = "Q_counttime";
        public static string FieldQ_end_time = "Q_end_time";
        public static string FieldQ_showtime = "Q_showtime";
        public static string FieldZhiding = "Zhiding";
        public static string FieldHadConfirm = "HadConfirm";
        public static string FieldPricerange = "Pricerange";
        public static string FieldCodetable = "Codetable";
        public static string Fieldorder = "orders";
        public static string Fieldrecover = "recover";
        public static string Fieldoriginalid = "originalid";
        #endregion

        #region 属性
        private int _yId;
        public int YId
        {
            get { return _yId; }
            set { _yId = value; }
        }
        private int _productid;
        public int Productid
        {
            get { return _productid; }
            set { _productid = value; }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private decimal _yunjiage;
        public decimal Yunjiage
        {
            get { return _yunjiage; }
            set { _yunjiage = value; }
        }
        private decimal _zongjiage;
        public decimal Zongjiage
        {
            get { return _zongjiage; }
            set { _zongjiage = value; }
        }
        private int _zongrenshu;
        public int Zongrenshu
        {
            get { return _zongrenshu; }
            set { _zongrenshu = value; }
        }
        private int _canyurenshu;
        public int Canyurenshu
        {
            get { return _canyurenshu; }
            set { _canyurenshu = value; }
        }
        private int _shengyurenshu;
        public int Shengyurenshu
        {
            get { return _shengyurenshu; }
            set { _shengyurenshu = value; }
        }
        private int _qishu;
        public int Qishu
        {
            get { return _qishu; }
            set { _qishu = value; }
        }
        private int _maxqishu;
        public int Maxqishu
        {
            get { return _maxqishu; }
            set { _maxqishu = value; }
        }
        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
        private int _q_uid;
        public int Q_uid
        {
            get { return _q_uid; }
            set { _q_uid = value; }
        }
        private string _q_code;
        public string Q_code
        {
            get { return _q_code; }
            set { _q_code = value; }
        }
        private string _q_counttime;
        public string Q_counttime
        {
            get { return _q_counttime; }
            set { _q_counttime = value; }
        }
        private DateTime _q_end_time;
        public DateTime Q_end_time
        {
            get { return _q_end_time; }
            set { _q_end_time = value; }
        }
        private int _q_showtime;
        public int Q_showtime
        {
            get { return _q_showtime; }
            set { _q_showtime = value; }
        }
        private int _zhiding;
        public int Zhiding
        {
            get { return _zhiding; }
            set { _zhiding = value; }
        }
        private int _hadConfirm;
        public int HadConfirm
        {
            get { return _hadConfirm; }
            set { _hadConfirm = value; }
        }
        private string _pricerange;
        public string Pricerange
        {
            get { return _pricerange; }
            set { _pricerange = value; }
        }
        private string _codetable;
        public string Codetable
        {
            get { return _codetable; }
            set { _codetable = value; }
        }
        private int _orders;
        public int  Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
        private int _recover;
        public int recover
        {
            get { return _recover; }
            set { _recover = value; }
        }
        /// <summary>
        /// 1进行中，2正在揭晓，3已揭晓
        /// </summary>
        public int Q_showstate
        {
            get
            {
                if (this._shengyurenshu > 0) return 1;
                if (this._q_showtime == 1) return 2;
                return 3;

            }
        }
        private int _originalid;
        public int originalid
        {
            get { return _originalid; }
            set { _originalid = value; }
        }
        
        #endregion

        #region 构造函数
        public go_yiyuanEntity() { }

        public go_yiyuanEntity(DataRow dr)
        {
            if (dr[FieldYId] != DBNull.Value)
            {
                _yId = (int)dr[FieldYId];
            }
            if (dr[FieldProductid] != DBNull.Value)
            {
                _productid = (int)dr[FieldProductid];
            }
            if (dr[FieldTitle] != DBNull.Value)
            {
                _title = (string)dr[FieldTitle];
            }
            if (dr[FieldYunjiage] != DBNull.Value)
            {
                _yunjiage = (decimal)dr[FieldYunjiage];
            }
            if (dr[FieldZongjiage] != DBNull.Value)
            {
                _zongjiage = (decimal)dr[FieldZongjiage];
            }
            if (dr[FieldZongrenshu] != DBNull.Value)
            {
                _zongrenshu = (int)dr[FieldZongrenshu];
            }
            if (dr[FieldCanyurenshu] != DBNull.Value)
            {
                _canyurenshu = (int)dr[FieldCanyurenshu];
            }
            if (dr[FieldShengyurenshu] != DBNull.Value)
            {
                _shengyurenshu = (int)dr[FieldShengyurenshu];
            }
            if (dr[FieldQishu] != DBNull.Value)
            {
                _qishu = (int)dr[FieldQishu];
            }
            if (dr[FieldMaxqishu] != DBNull.Value)
            {
                _maxqishu = (int)dr[FieldMaxqishu];
            }
            if (dr[FieldTime] != DBNull.Value)
            {
                _time = (DateTime)dr[FieldTime];
            }
            if (dr[FieldQ_uid] != DBNull.Value)
            {
                _q_uid = (int)dr[FieldQ_uid];
            }
            if (dr[FieldQ_code] != DBNull.Value)
            {
                _q_code = (string)dr[FieldQ_code];
            }
            if (dr[FieldQ_counttime] != DBNull.Value)
            {
                _q_counttime = (string)dr[FieldQ_counttime];
            }
            if (dr[FieldQ_end_time] != DBNull.Value)
            {
                _q_end_time = (DateTime)dr[FieldQ_end_time];
            }
            if (dr[FieldQ_showtime] != DBNull.Value)
            {
                _q_showtime = (int)dr[FieldQ_showtime];
            }
            if (dr[FieldZhiding] != DBNull.Value)
            {
                _zhiding = (int)dr[FieldZhiding];
            }
            if (dr[FieldHadConfirm] != DBNull.Value)
            {
                _hadConfirm = (int)dr[FieldHadConfirm];
            }
            if (dr[FieldPricerange] != DBNull.Value)
            {
                _pricerange = (string)dr[FieldPricerange];
            }
            if (dr[FieldCodetable] != DBNull.Value)
            {
                _codetable = (string)dr[FieldCodetable];
            }
            if (dr[Fieldorder] != DBNull.Value)
            {
                _orders = (int)dr[Fieldorder];
            }
            if (dr[Fieldrecover] != DBNull.Value)
            {
                _recover = (int)dr[Fieldrecover];
            }
            if (dr[Fieldoriginalid] != DBNull.Value)
            {
                _originalid = (int)dr[Fieldoriginalid];
            }
        }
        #endregion

    }
}
