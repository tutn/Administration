namespace Administration.Model
{
    using Administration.Model.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class USER_Params : PagingOption
    {
        public string USER_NAME { get; set; }

        public string FULL_NAME { get; set; }

        public string EMAIL { get; set; }

        public int? ORDER_NO { get; set; }

        public int? USED_STATE { get; set; }

    }

    public partial class SYS_USERS
    {
        public int USER_ID { get; set; }

        [StringLength(64)]
        public string USER_NAME { get; set; }

        [StringLength(256)]
        public string PASSWORD { get; set; }

        [StringLength(256)]
        public string FULL_NAME { get; set; }

        [StringLength(256)]
        public string EMAIL { get; set; }

        [StringLength(256)]
        public string AVATAR { get; set; }

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

        public string USEDSTATE_NAME { get; set; }
        
    }
}
