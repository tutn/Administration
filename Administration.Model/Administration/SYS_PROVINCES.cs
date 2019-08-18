namespace Administration.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class PROVINCE_Params
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(255)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string NAME_ENG { get; set; }

        public int? PARENT_ID { get; set; }

        public int? TYPE { get; set; }

        public int? ORDER_NO { get; set; }

        public int? USED_STATE { get; set; }

        [StringLength(256)]
        public string DESCRIPTION { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(64)]
        public string CREATED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(64)]
        public string MODIFIED_BY { get; set; }
    }

    public partial class SYS_PROVINCES
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string CODE { get; set; }

        [StringLength(255)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string NAME_ENG { get; set; }

        public int? PARENT_ID { get; set; }

        public int? TYPE { get; set; }

        public int? ORDER_NO { get; set; }

        public int? USED_STATE { get; set; }

        [StringLength(256)]
        public string DESCRIPTION { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(64)]
        public string CREATED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(64)]
        public string MODIFIED_BY { get; set; }
    }
}
