$(document).ready(function () {
    loadData();
})

async function loadData() {
    let paraAjax = {
        url: "/Member/List",
        type: "GET",
        data: null
    }

    const data = await CallAjax(paraAjax);
    var columns = [
        { "data": "Id", "width": "15%", title: "ID" },
        { "data": "Name", "width": "15%", title: "姓名" },
        { "data": "Tel", "width": "15%", title: "電話" },
        {
            "data": "CreateTime", "width": "15%", title: "新增時間", "render": function (data) {
                return moment(data).format(`YYYY-MM-DD HH:mm:ss`)
            },
        },
        {
            "data": "EditTime", "width": "15%", title: "編輯時間", "render": function (data, type, row) {
                let edittime = "";
                if (data == null)
                    edittime = "無";
                else
                    edittime = moment(data).format("YYYY-MM-DD HH:mm:ss");
                return `${edittime}`;
            }
        },
        {
            "data": "Id", "width": "15%", title: "功能", "render": function (data, type, row) {
                return `
                       <div class="row">
                            <div class="col-auto">
                            <button type="button" onclick="Edit('${row.Id}')" class="btn btn-warning btn-sm">編輯</button>
                            </div>
                            <div class="col-auto">
                            <button type="button" onclick="Delete('${row.Id}')" class="btn btn-danger btn-sm">刪除</button>
                            </div>
                        </div>     
                       `
            },
        },
    ];

    const paraTable = {
        tableId: "dataTable",
        data,
        columns
    }
    FillDataTable(paraTable);

    //DataTableSetting(paraTable);
}

//function DataTableSetting(parameters) {
//    const { tableId, data, columns } = parameters;
//    FillDataTable(parameters);
//}

function Edit(id) {
    location.href = `/Member/Edit?id=${id}`;
}

async function Delete(id) {
    let paraAjax = {
        url: "/Member/Delete",
        type: "POST",
        data: {
            "id":id
        },
    }
    const data = await CallAjax(paraAjax)
    if (data) {
        alert("刪除成功");
        location.reload();
    }
    else {
        alert("失敗");
    }
}

///**
// * Call Ajax
// * @param {any} parameters
// */
function CallAjax(parameters) {
    var { url, type, data } = parameters;

    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            type: type,
            dataType: 'json',
            data: data,
            success: function (Response) {
                return resolve(Response);
            }
        });
    });
}



