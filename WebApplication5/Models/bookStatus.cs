using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class bookStatus
    {
        public string CODE_TYPE { get; set; }
        public string CODE_ID { get; set; }
        public string CODE_TYPE_DESC { get; set; }
        public string CODE_NAME { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
}