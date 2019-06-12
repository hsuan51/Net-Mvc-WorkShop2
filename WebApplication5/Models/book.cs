using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class book
    {
        public int BOOK_ID { get; set; }
        public string BOOK_NAME { get; set; }
        public string BOOK_CLASS_ID { get; set; }
        public string BOOK_AUTHOR { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string BOOK_BOUGHT_DATE { get; set; }
        public string BOOK_PUBLISHER { get; set; }
        public string BOOK_NOTE { get; set; }
        public string BOOK_STATUS { get; set; }
        public string BOOK_KEEPER { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
}