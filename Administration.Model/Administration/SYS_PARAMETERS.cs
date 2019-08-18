namespace Administration.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class SYS_PARAMETERS
    {
        public int ID { get; set; }

        public int? VALUE { get; set; }

        [StringLength(256)]
        public string NAME { get; set; }

        [StringLength(64)]
        public string TYPE { get; set; }

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
