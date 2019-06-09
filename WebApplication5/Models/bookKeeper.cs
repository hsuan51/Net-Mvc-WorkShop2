using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class bookKeeper
    {
        public string USER_ID { get; set; }
        public string USER_CNAME { get; set; }
        public string USER_ENAME { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
}