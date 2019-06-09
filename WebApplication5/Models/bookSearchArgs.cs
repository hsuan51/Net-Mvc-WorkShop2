using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class bookSearchArgs
    {

        [DisplayName("書籍名稱")]
        public string BOOK_NAME { get; set; }
        [DisplayName("圖書類別")]
        public string BOOK_CLASS_ID { get; set; }
        [DisplayName("借閱人")]
        public string BOOK_KEEPER { get; set; }
        [DisplayName("借閱狀態")]
        public string BOOK_STATUS { get; set; }


    }
}