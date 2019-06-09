$(function () {
    $("#searchBookClass").kendoDropDownList();
    $("#searchBookKeeper").kendoDropDownList();
    $("#searchBookStatus").kendoDropDownList();
    searchBookResuleGrid();
    searchBook();
});
function searchBook() {
    $("#searchBtn").click({
        $.ajax({
            type: "Post",
            url: "/Home/Index",
            data: $("#bookSearch").serialize(),
            dataType: "json",
            success: function (response) {
                //更新Kendo Grid Datasource資料
                var grid = $('#searchBookResultGrid').data("searchBookResultGrid");
                grid.setDataSource(response);
            }, error: function (error) {
                alert("系統發生錯誤");
            }
        });
        return false;
    });
}
function searchBookResultGrid() {
    $.ajax({
        type: "post",
        url: "/Home/Index",
        datatype: "json",
        success: function (response) {
            $("#searchBookResultGrid").kendoGrid({
                dataSource: response,
                pageable: { //分頁設定
                    input: true,
                    numeric: false,
                    pageSize: 5
                },
                columns: [
                    { field: "BOOK_CLASS_ID", title: "書籍分類" },
                    { field: "BOOK_NAME", title: "書名" },
                    { field: "BOOK_BOUGHT_DATE", title: "購書日期" },
                    { field: "BOOK_STATUS", title: "書籍狀態" },
                    { field: "BOOK_KEEPER", title: "保管人" },
                    {
                        command: [
                            {
                                name: "刪除",
                                click: BookDelete,
                            },
                            {
                                name: "修改",
                                click: BookUpdate,
                            }
                        ]
                    }
                ]
            });
        }, error: function (error) {
            alert("系統發生錯誤");
        }
    });
}