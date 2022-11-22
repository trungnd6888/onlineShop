function handleShowModalOrder(orderId, TotalMoney) {
    $.ajax({
        url: '/OrderDetail/jsonList',
        data: { 'orderId': orderId },
        type: 'GET',
        success: function (response) {
            var str = '<table class="table table-data2">';
            str += '<thead>';
            str += '<tr>';
            str += '<th>Mã chi tiết</th>';
            str += '<th>Mã sản phẩm</th>';
            str += '<th>Tên sản phẩm</th>';
            str += '<th>Đơn giá</th>';
            str += '<th>Số lượng</th>';
            str += '<th>Tổng tiền</th>';
            str += '</tr>';
            str += '</thead>';
            str += '<tbody>';
            str += '<tr class="tr-shadow">';

            response.forEach(p => {
                str += '<td>';
                str += p['Id'];
                str += '</td>';
                str += '<td>';
                str += p['ProductCode'];
                str += '</td>';
                str += '<td>';
                str += p['ProductName'];
                str += '</td>';
                str += '<td>';
                str += formatNumber(p['ProductPrice']);
                str += '</td>';
                str += '<td>';
                str += formatNumber(p['Quantity']);
                str += '</td>';
                str += '<td>';
                str += formatNumber(p['DetailTotal']);
                str += '</td>';
                str += '</tr>';
                str += '<tr class="tr-shadow">';
                str += '</tr>';
                str += '<tr class="spacer"></tr>';
            });
            str += '</tbody>';
            str += '</table >';

            $('.modal-body').html(str);
            $('#modalCenterTitle').text('Chi tiết đơn hàng ' + orderId);
            $('.modal-footer').html('<h5 class="modal-footer-title" id="modal-footer-title">Thành tiền:&ensp;' + formatNumber(TotalMoney) + '</h5>');
        },
        error: function (e) {
            console.log('Error: ', e);
        }
    });
}