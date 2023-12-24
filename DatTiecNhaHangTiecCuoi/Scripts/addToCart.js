<script>
    function addToCart(ten, hinhAnh, donGia) {
        event.target.disabled = true;

        var danhSachDaChonString = localStorage.getItem('danhSachDaChon');
        var danhSachDaChon = danhSachDaChonString ? JSON.parse(danhSachDaChonString) : [];

        var monAnNuoc = {
            ten: ten,
            hinhAnh: hinhAnh,
            donGia: donGia,
        };

        danhSachDaChon.push(monAnNuoc);

        localStorage.setItem('danhSachDaChon', JSON.stringify(danhSachDaChon));
    }
</script>
