using System;

namespace Administration.Model.Common
{
    public class Result
    {
        public Int16 Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
	}
}
