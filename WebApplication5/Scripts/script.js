$(function () {
    console.log("ready");
    searchBookClassList();
    searchBookKeeperList();
    BookStatusList();
    searchBookResultGrid();
    searchBook();
    insertAction();
    $("#windowBookBoughtDate").kendoDatePicker({ value: new Date(), format: "yyyy/MM/dd" });
});
function searchBookClassList() {
    $.ajax({
        type: "Post",
        url: "/Home/SearchBookClassName",
        datatype: "json",
        success: function (response) {
            $("#searchBookClass,#windowBookClass").kendoDropDownList({
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
            $("#searchBookStatus,#windowBookStatus").kendoDropDownList({
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
            $("#searchBookKeeper,#windowBookKeeper").kendoDropDownList({
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
                    { hidden: true, field: "BOOK_ID" },
                    { field: "BOOK_CLASS_ID", title: "書籍分類" },
                    { field: "BOOK_NAME", title: "書名" },
                    { field: "BOOK_BOUGHT_DATE", title: "購書日期", format: "{0:yyyy-MM-dd}" },
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
    $("#saveInsertBtn").show();
    $("#bookWindow").kendoWindow({
        title: "新增書籍資料"
    });
    var dialog = $("#bookWindow").data("kendoWindow");
    dialog.center();
    dialog.open();
}
function insertBook() {
    $("#saveInsertBtn").click(function () {
        var validator = $("#bookWindow").kendoValidator().data("kendoValidator")
        var dialog = $("#bookWindow").data("kendoWindow");
        var grid = $('#searchBookResultGrid').data("kendoGrid");
        if (validator.validate()) {
            $.ajax({
                type: "Post",
                url: "/Home/InsertBook",
                data: $("#bookWindow").serialize(),
                dataType: "json",
                success: function (response) {
                    console.log("insertresponse");
                    console.log(response);
                    dialog.close();
                    grid.dataSource.add(response[0]);
                    grid.refresh();
                    $("#saveInsertBtn").hide();
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
function updateBookWindow(item) {
    $("#updateBookStatusArea,#updateBookKeeperArea,#saveUpdateBtn").show();
    $("#bookWindow").kendoWindow({
        title: "修改書籍資料"
    });
    $("#windowBookId").val(item.BOOK_ID);
    $("#windowBookName").val(item.BOOK_NAME);
    $("#windowBookAuthor").val(item.BOOK_AUTHOR);
    $("#windowBookPublisher").val(item.BOOK_PUBLISHER);
    $("#windowBookNote").val(item.BOOK_NOTE);
    $("#windowBookBoughtDate").val(item.BOOK_BOUGHT_DATE);
    var windowBookStatus = $("#windowBookStatus").data("kendoDropDownList");
    windowBookStatus.select(function (dataItem) {
        return dataItem.CODE_NAME === item.BOOK_STATUS;
    });
    var windowBookKeeper = $("#windowBookKeeper").data("kendoDropDownList");
    windowBookKeeper.select(function (dataItem) {
        return dataItem.USER_CNAME === item.BOOK_KEEPER;
    });
    var windowBookClass = $("#windowBookClass").data("kendoDropDownList");
    windowBookClass.select(function (dataItem) {
        return dataItem.BOOK_CLASS_NAME === item.BOOK_CLASS_ID;
    });
    var dialog = $("#bookWindow").data("kendoWindow");
    dialog.center();
    dialog.open();
}
function updateBook(e) {
    var grid = $('#searchBookResultGrid').data("kendoGrid");
    var item = grid.dataItem($(e.target).closest("tr"));
    console.log(item);
    updateBookWindow(item);
    console.log(item.BOOK_ID);
    console.log($("#bookWindow").serialize());
    var dialog = $("#bookWindow").data("kendoWindow");
    var validator = $("#bookWindow").kendoValidator().data("kendoValidator");
    $("#saveUpdateBtn").click(function () {
        if (validator.validate()) {
            $.ajax({
                type: "Post",
                url: "/Home/UpdateBook",
                data: $("#bookWindow").serialize(),
                dataType: "json",
                success: function (response) {
                    $("#saveUpdateBtn").hide();
                    item.set("BOOK_CLASS_ID", response[0]["BOOK_CLASS_ID"]);
                    item.set("BOOK_NAME", response[0]["BOOK_NAME"]);
                    item.set("BOOK_BOUGHT_DATE", response[0]["BOOK_BOUGHT_DATE"]);
                    item.set("BOOK_STATUS", response[0]["BOOK_STATUS"]);
                    item.set("BOOK_KEEPER", response[0]["BOOK_KEEPER"]);
                    item.set("BOOK_AUTHOR", response[0]["BOOK_AUTHOR"]);
                    item.set("BOOK_PUBLISHER", response[0]["BOOK_PUBLISHER"]);
                    item.set("BOOK_NOTE", response[0]["BOOK_NOTE"]);
                    console.log(response);
                    dialog.close();
                    grid.refresh();
                }, error: function (error) {
                    alert("系統發生錯誤");
                    console.log(error);
                }
            });
        }
    });
}
function deleteBook(e) {
    if (confirm("確定要刪除?")) {
        var grid = $('#searchBookResultGrid').data("kendoGrid");
        var item = grid.dataItem($(e.target).closest("tr"));
        console.log(item.BOOK_ID);
        $.ajax({
            type: "Post",
            url: "/Home/DeleteBook",
            data: { "BOOK_ID": item.BOOK_ID },
            dataType: "json",
            success: function (response) {
                console.log(response);
                grid.dataSource.remove(item);
                grid.refresh();
            }, error: function (error) {
                alert("系統發生錯誤");
                console.log(error);
            }
        });
    }
}