$(function () {
    console.log("ready");
    searchBookClassList();
    searchBookKeeperList();
    BookStatusList();
    searchBookResultGrid();
    searchBook();
    insertAction();
    $("#bookDatePicker").kendoDatePicker({ value: new Date(), format: "yyyy/MM/dd" });
});
function searchBookClassList() {
    $.ajax({
        type: "Post",
        url: "/Home/SearchBookClassName",
        datatype: "json",
        success: function (response) {
            $("#searchBookClass,#insertBookClass").kendoDropDownList({
                dataValueField: "BOOK_CLASS_ID",
                dataTextField: "BOOK_CLASS_NAME",
                dataSource: response,
                optionLabel: "請選擇...",
            });
        }, error: function (error) {
            console.log(error);
            alert("系統發生錯誤");
        }
    });
}
function BookStatusList() {
    $.ajax({
        type: "Post",
        url: "/Home/SearchBookStatusName",
        datatype: "json",
        success: function (response) {
            $("#searchBookStatus").kendoDropDownList({
                dataValueField: "CODE_ID",
                dataTextField: "CODE_NAME",
                dataSource: response,
                optionLabel: "請選擇..."
            });
        }, error: function (error) {
            console.log(error);
            alert("系統發生錯誤");
        }
    });
}
function searchBookKeeperList() {
    $.ajax({
        type: "Post",
        url: "/Home/SearchBookKeeperName",
        datatype: "json",
        success: function (response) {
            $("#searchBookKeeper").kendoDropDownList({
                dataValueField: "USER_ID",
                dataTextField: "USER_CNAME",
                dataSource: response,
                optionLabel: "請選擇..."
            });
        }, error: function (error) {
            console.log(error);
            alert("系統發生錯誤");
        }
    });
}
function searchBookResultGrid() {
    $.ajax({
        type: "Post",
        url: "/Home/Index",
        datatype: "json",
        success: function (response) {
            $("#searchBookResultGrid").kendoGrid({
                dataSource: response,
                pageable: { // 分頁設定
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
                                click: deleteBook
                            },
                            {
                                name: "修改",
                                click: updateBook
                            }
                        ]
                    }
                ]
            });
        }, error: function (error) {
            console.log(error);
            alert("系統發生錯誤");
        }
    });

}
function searchBook() {
    $("#searchBtn").click(function () {
        $.ajax({
            type: "Post",
            url: "/Home/Index",
            data: $("#bookSearch").serialize(),
            dataType: "json",
            success: function (response) {
                //更新Kendo Grid Datasource資料
                var grid = $('#searchBookResultGrid').data("kendoGrid");
                console.log(grid);
                console.log(response);
                grid.setDataSource(response);
            }, error: function (error) {
                alert("系統發生錯誤");
            }
        });
        return false;
    });
}
function insertBookWindow() {
    $("#bookInsert").kendoWindow({
        title: "新增書籍資料"
    });
    var dialog = $("#bookInsert").data("kendoWindow");
    dialog.center();
    dialog.open();
}
function insertBook() {
    $("#saveBtn").click(function () {
        var validator = $("#bookInsert").kendoValidator().data("kendoValidator")
        var dialog = $("#bookInsert").data("kendoWindow");
        var grid = $('#searchBookResultGrid').data("kendoGrid");
        if (validator.validate()) {
            $.ajax({
                type: "Post",
                url: "/Home/InsertBook",
                data: $("#bookInsert").serialize(),
                dataType: "json",
                success: function (response) {
                    console.log("insertresponse");
                    console.log(response);
                    dialog.close();
                    grid.dataSource.add(response[0]);
                    grid.refresh();
                }, error: function (error) {
                    alert("系統發生錯誤");
                }
            });
        }
    });
}
function insertAction() {
    $("#insertBtn").click(function () {
        insertBookWindow();
    });
    insertBook();
}
function updateBook() {

}
function deleteBook() {

}