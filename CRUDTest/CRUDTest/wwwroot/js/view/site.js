/**
 * 獲取同樣tag的 回傳物件
 * @param {any} type
 */
function GetValueByTagName(type = 'input') {
    var ids = document.getElementsByTagName(type);
    let data = {};
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type !== 'radio') {
            data[ids[i].id] = ids[i].value;
        }
    }
    return data;
}

function GetAllInputValue() {
    var ids = document.getElementsByTagName('input');
    let data = {};
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type == 'radio') {
            if (ids[i].checked) {
                data[ids[i].name] = ids[i].value;
            }
        } else {
            if (!ids[i].disabled) {
                data[ids[i].id] = ids[i].value;
            }
        }
    }
    return data;
}

/* 判別日期 */
function checkDate(obj) {
    let isValid = true;

    if (Date.parse(obj.enddate) <= Date.parse(obj.startdate)) {
        isValid = false;

        Swal.fire({
            icon: 'error',
            title: '起始時間不能相同或晚於截止時間!'
        })
    }

    return isValid;
}

/**
 * clear same classname element value
 * @param {any} tag
 */
function clearInput(tag) {
    var elements = document.getElementsByClassName(tag)
    for (var ii = 0; ii < elements.length; ii++) {
        elements[ii].value = "";
    }
}

/**
 * string builder
 * */
var StringBuilder = function () {
    this.buffer = [];
    this.append = function (val) {
        this.buffer.push(val);
        return this;
    };
    this.toString = function () {
        return this.buffer.join('');
    };
};


/**
 * Call Ajax
 * @param {any} parameters
 */
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

//判斷是否為純中文
function ValidatePureChinese(value) {
    var reg = /^[\u4E00-\u9FA5]+$/
    if (reg.test(value)) {
        return true
    } else {
        return false
    }
}

//判斷是否為手機格式
function ValidatePhone(value) {
    return String(value)
        .match(
            /^09[0-9]{8}$/
        );
    //另外的表達式 /^09\d{2}(\d{6}|-\d{3}-\d{3})$/   
}