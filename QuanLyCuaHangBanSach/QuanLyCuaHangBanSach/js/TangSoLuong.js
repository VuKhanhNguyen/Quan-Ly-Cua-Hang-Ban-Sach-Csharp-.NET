//function themVaoGioHang(idSach) {
//    // Cập nhật form ẩn
//    document.getElementById("input-idSach").value = idSach;
//    document.getElementById("input-soLuong").value = 1; // Có thể thay đổi tùy vào input số lượng

//    // Gửi form
//    document.getElementById("form-them-gio-hang").submit();

//    // Cập nhật số lượng hiển thị
//    capNhatSoLuongGioHang();
//}

//function capNhatSoLuongGioHang() {
//    // Lấy số lượng hiện tại
//    var cartCount = document.getElementById("cart-count");
//    var currentCount = parseInt(cartCount.textContent);

//    // Tăng số lượng
//    cartCount.textContent = currentCount + 1;
//}
function tangSoLuong() {
    let soLuongInput = document.getElementById("soLuong");
    let soLuong = parseInt(soLuongInput.value);
    if (!isNaN(soLuong)) {
        soLuongInput.value = soLuong + 1;
    }
}