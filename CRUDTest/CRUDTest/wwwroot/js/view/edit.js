$(document).ready(function () {
    $("#btnSubmit").click(function (e) {
        Submit();
    });
})
async function Submit() {
    var par = GetAllInputValue();
    var { id, name, tel } = par;
    //可加格式驗證
    let paraAjax = {
        url: "/Member/Edit",
        type: "POST",
        data: {
            Id: id,
            Name: name,
            Tel:tel
        },
    }
    const data = await CallAjax(paraAjax)

    if (data) {
        alert("成功");
        window.location = "/Member/Index";
    }
    else {
        alert("失敗");
    }
}