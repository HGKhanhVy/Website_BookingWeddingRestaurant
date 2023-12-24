<script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
var selectedItems = [];
var selectedItemsNuoc = [];

function addToCart(TenMonAn, HinhAnh, DonGia, type) {
    console.log('Button clicked. Adding to cart...');
    var buttonId = type === 'MonAn' ? 'addCartMonAn' : 'addCartNuoc';
    var button = document.getElementById(buttonId);
    button.disabled = true;

    if (type === 'MonAn') {
        var newItem = {
            TenMonAn: TenMonAn,
            HinhAnh: HinhAnh,
            DonGia: DonGia,
        };
        selectedItems.push(newItem);
    } else {
        var newItem = {
            TenNuoc: TenNuoc,
            HinhAnh: HinhAnh,
            DonGia: DonGia,
        };
        selectedItemsNuoc.push(newItem);
    }
}

function sendToGioHangController(type) {
    console.log(type === 'MonAn' ? selectedItems : selectedItemsNuoc);

    $.ajax({
        type: 'POST',
        url: '/GioHang/GioHang',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            danhSachDaChon: {
                DanhSachMonAn: type === 'MonAn' ? selectedItems : [],
                DanhSachNuoc: type === 'Nuoc' ? selectedItemsNuoc : []
            }
        }),
        success: function (response) {
            console.log('Dữ liệu đã được gửi thành công:', response);
            window.location.href = '/GioHang/GioHang';
        },
        error: function (error) {
            console.error('Lỗi khi gửi dữ liệu:', error);
        }
    });
}