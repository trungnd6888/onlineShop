$(document).ready(function () {
    //ADD ORDER
    $("#search-product").on("input", function () {
        ($("#search-product").val() === '')
            ? $("#search-content").hide()
            : $("#search-content").show();
    });

    $(document).click(function () {
        $("#search-content").hide();
    });

    handleSearchProduct();

    loadData();
})

function handleSearchProduct() {
    $('#search-product').on('input', function (e) {
        $.ajax({
            url: '/Product/jsonList',
            type: 'GET',
            data: { 'keyword': e.target.value },
            success: function (response) {
                console.log(response);
                var str = '';
                str += '<div class="table-responsive">';
                str += '<table class="table table-top-campaign">';
                str += '<tbody>';

                response.forEach(p => {
                    str += '<tr class="search-item" onclick="handleAddItem(' + "'" + p['Id'] + "'" + ')">';
                    str += '<td>' + p['Code'] + '</td>';
                    str += '<td>' + p['Name'] + '</td>';
                    str += '<td>' + formatNumber(p['Price']) + '</td>';
                    str += '</tr>';
                });

                str += '</tbody>';
                str += '</table>';
                str += ' </div>';

                $('#search-content').html(str);
            },
            error: function (e) {
                console.log('Error: ', e);
            }
        });
    });
}

function handleAddItem(id) {
    $.ajax({
        url: '/CartItem/addItem',
        type: 'POST',
        data: { 'product_Id': id, 'quantity': 1 },
        success: function (response) {
            loadData();
        },
        error: function (e) {
            console.log('Error: ', e);
        }
    });
}

function handleUpdateItem(event, id) {
    $.ajax({
        url: '/CartItem/updateItem',
        type: 'POST',
        data: { 'product_Id': id, 'quantity': event.target.value },
        success: function (response) {
            loadData();
        },
        error: function (e) {
            console.log('Error: ', e);
        }
    });
}

function handleRemoveItem(id) {
    $.ajax({
        url: '/CartItem/removeItem',
        type: 'POST',
        data: { 'product_Id': id},
        success: function (response) {
            loadData();
        },
        error: function (e) {
            console.log('Error: ', e);
        }
    });
}

function loadData() {
    $.ajax({
        url: '/CartItem/loadData',
        type: 'GET',
        success: function (response) {
            var str = '';
            str += '<thead>';
            str += '<tr>';
            str += '<th></th>';
            str += '<th>Mã</th>';
            str += '<th>Tên sản phẩm</th>';
            str += '<th>Ảnh</th>';
            str += '<th>Số lượng</th>';
            str += '<th>Đơn giá</th>';
            str += ' <th>Tổng tiền</th>';
            str += '</tr>';
            str += '</thead>';
            str += '<tbody>';

            var totalMoney = 0;
            var total = totalQuantity = 0;

            response.forEach(p => {
                str += '<tr class="tr-shadow product-item">';
                str += '<td>';
                str += '<div class="table-data-feature">';
                str += '<button class="item" onclick="handleRemoveItem(' + p.Product.Id + ')" data-toggle="tooltip" data-placement="top" title="Delete">';
                str += '<i class="zmdi zmdi-delete"></i>';
                str += '</button>';
                str += '</div>';
                str += '</td>';
                str += '<td>' + p.Product.Code + '</td>';
                str += '<td>' + p.Product.Name + '</td>';
                str += '<td>' + '<img src="' + p.Product.ImageUrl + '" alt="img-cart-item" />' + '</td>';
                str += '<td>' + '<input onchange="handleUpdateItem(event, ' + p.Product.Id  + ')" class="form-control form-control-sm product-item-input" value="' + p.Quantity + '" type="number" min="0" />' + '</td>';
                str += '<td>' + formatNumber(p.Product.Price) + '</td>';
                str += '<td>' + formatNumber(p.Product.Price * p.Quantity) + '</td>';
                str += '</tr>';
                str += '<tr class="spacer"></tr>';

                totalMoney += p.Product.Price * p.Quantity;
                totalQuantity += p.Quantity;
            });

            if (totalQuantity > 0) {
                str += '<tr class="tr-shadow product-item font-weight-bold">';
                str += '<td>Tổng</td>';
                str += '<td></td>';
                str += '<td></td>';
                str += '<td></td>';
                str += '<td>' + totalQuantity + '</td>';
                str += '<td></td>';
                str += '<td>' + formatNumber(totalMoney) + '</td>';
                str += '</tr>';
                str += '<tr class="spacer"></tr>';
            }

            str += '</tbody>';

            $("#cart-item-list").html(str);
        },
        error: function (e) {
            console.log('Error: ', e);
        }
    });
}


