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
        [Required(ErrorMessage = "必填")]
        public string BOOK_NAME { get; set; }
        [DisplayName("圖書類別")]
        [Required(ErrorMessage = "必填")]
        public string BOOK_CLASS_ID { get; set; }
        [DisplayName("借閱人")]
        public string BOOK_KEEPER { get; set; }
        [DisplayName("借閱狀態")]
        public string BOOK_STATUS { get; set; }
        [DisplayName("作者")]
        [Required(ErrorMessage = "必填")]
        public string BOOK_AUTHOR { get; set; }
        [DisplayName("出版社")]
        [Required(ErrorMessage = "必填")]
        public string BOOK_PUBLISHER { get; set; }
        [DisplayName("內容簡介")]
        [Required(ErrorMessage = "必填")]
        public string BOOK_NOTE { get; set; }
        [DisplayName("購買日期")]
        [Required(ErrorMessage = "必填")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string BOOK_BOUGHT_DATE { get; set; }


    }
}