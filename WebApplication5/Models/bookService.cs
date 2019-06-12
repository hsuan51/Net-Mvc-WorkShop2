using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Models
{
    public class bookService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString.ToString();
        }

        public List<Models.book> getSearchBookData(Models.bookSearchArgs args)
        {
            DataTable dt = new DataTable();
            string sql = @"Select  BOOK_ID,
                           BOOK_CLASS.BOOK_CLASS_NAME,
                           BOOK_NAME,
                           BOOK_BOUGHT_DATE,
                           MEMBER_M.USER_CNAME,
                           BOOK_CODE.CODE_NAME
                           From [dbo].[BOOK_DATA]
                           LEFT JOIN [dbo].[BOOK_CLASS] ON BOOK_DATA.BOOK_CLASS_ID=BOOK_CLASS.BOOK_CLASS_ID
                           LEFT JOIN [dbo].[MEMBER_M] ON BOOK_DATA.BOOK_KEEPER=MEMBER_M.USER_ID
                           LEFT JOIN [dbo].[BOOK_CODE] ON BOOK_DATA.BOOK_STATUS=BOOK_CODE.CODE_ID
                           Where(BOOK_DATA.BOOK_CLASS_ID = @bookClassId OR @bookClassId='') AND
                           (BOOK_DATA.BOOK_NAME  LIKE ('%'+ @bookName +'%') OR @bookName='') AND
                           (BOOK_DATA.BOOK_STATUS = @bookStatus OR @bookStatus='') AND
                           (BOOK_DATA.BOOK_KEEPER = @bookKeeper OR @bookKeeper='')  Order By BOOK_BOUGHT_DATE Desc";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookClassId", args.BOOK_CLASS_ID == null ? string.Empty : args.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@bookName", args.BOOK_NAME == null ? string.Empty : args.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@bookStatus", args.BOOK_STATUS == null ? string.Empty : args.BOOK_STATUS));
                cmd.Parameters.Add(new SqlParameter("@bookKeeper", args.BOOK_KEEPER == null ? string.Empty : args.BOOK_KEEPER));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.mapBookData(dt);
        }
        public List<Models.book> getOneBookData(int id)
        {
            DataTable dt = new DataTable();
            string sql = @"Select  BOOK_ID,
                           BOOK_CLASS.BOOK_CLASS_NAME,
                           BOOK_NAME,
                           BOOK_BOUGHT_DATE,
                           MEMBER_M.USER_CNAME,
                           BOOK_CODE.CODE_NAME
                           From [dbo].[BOOK_DATA]
                           LEFT JOIN [dbo].[BOOK_CLASS] ON BOOK_DATA.BOOK_CLASS_ID=BOOK_CLASS.BOOK_CLASS_ID
                           LEFT JOIN [dbo].[MEMBER_M] ON BOOK_DATA.BOOK_KEEPER=MEMBER_M.USER_ID
                           LEFT JOIN [dbo].[BOOK_CODE] ON BOOK_DATA.BOOK_STATUS=BOOK_CODE.CODE_ID
                           Where(BOOK_ID=@bookId)";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookId",id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.mapBookData(dt);
        }
        public List<Models.bookClass> getBookClassName()
        {
            DataTable dt = new DataTable();
            string sql = "Select BOOK_CLASS_ID,BOOK_CLASS_NAME From BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<bookClass> result = new List<bookClass>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new bookClass()
                {
                    BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                    BOOK_CLASS_NAME = row["BOOK_CLASS_NAME"].ToString()
                });
            }
            return result;
        }
        public List<Models.bookKeeper> getBookKeeperName()
        {
            DataTable dt = new DataTable();
            string sql = "Select USER_ID,USER_CNAME From MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<bookKeeper> result = new List<bookKeeper>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new bookKeeper()
                {
                    USER_ID = row["USER_ID"].ToString(),
                    USER_CNAME = row["USER_CNAME"].ToString()
                });
            }
            return result;
        }
        public List<Models.bookStatus> getBookStatusName()
        {
            DataTable dt = new DataTable();
            string sql = "Select CODE_ID,CODE_NAME From BOOK_CODE";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<bookStatus> result = new List<bookStatus>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new bookStatus()
                {
                    CODE_ID = row["CODE_ID"].ToString(),
                    CODE_NAME = row["CODE_NAME"].ToString()
                });
            }
            return result;
        }
        public int insertBook(Models.bookSearchArgs args)
        {
            string sql = @"Insert Into BOOK_DATA (BOOK_NAME,BOOK_AUTHOR,BOOK_PUBLISHER,BOOK_NOTE,
                BOOK_BOUGHT_DATE,BOOK_CLASS_ID,BOOK_STATUS,BOOK_KEEPER)
                Values (@bookName,@bookAuthor,@bookPublisher,@bookNote,
                @bookBoughtDate,@bookClassId,@bookStatus,@bookKeeper)
                Select SCOPE_IDENTITY()";
            int newId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookName", args.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@bookAuthor", args.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@bookPublisher", args.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@bookNote", args.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@bookBoughtDate", args.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@bookClassId", args.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@bookStatus", "A"));
                cmd.Parameters.Add(new SqlParameter("@bookKeeper", ""));
                newId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            
            return newId;
        }
        public int deleteBook(int id)
        {
            string sql = "Delete From BOOK_DATA Where BOOK_ID=@bookId";
            int bookId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookId", id));
                bookId = (int)(cmd.ExecuteNonQuery());
                conn.Close();
            }
            return bookId;
        }
        private List<Models.book> mapBookData(DataTable dt)
        {
            List<Models.book> result = new List<Models.book>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new book()
                {
                    BOOK_ID = (int)row["BOOK_ID"],
                    BOOK_CLASS_ID = row["BOOK_CLASS_NAME"].ToString(),
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_STATUS = row["CODE_NAME"].ToString(),
                    BOOK_KEEPER=row["USER_CNAME"].ToString()
                });
            }
            return result;
        }
    }
}