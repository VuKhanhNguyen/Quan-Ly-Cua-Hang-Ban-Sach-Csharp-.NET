document.addEventListener('DOMContentLoaded', function () {
    const closeButtons = document.querySelectorAll('.close-icon');

    // Lặp qua từng nút đóng và gắn sự kiện click
    closeButtons.forEach(function (icon) {
        icon.addEventListener('click', function (event) {
            // Tìm phần tử cha của biểu tượng đóng nếu cần (dựa trên yêu cầu thực tế)
            const confirmClose = confirm("Bạn có muốn thoát không?");

            if (confirmClose) {
                // Điều hướng về index.html
                window.location.href = '/Home/Index';
            } else {
                // Ngăn chặn hành động mặc định khi người dùng chọn "Hủy"
                event.preventDefault();
            }
        });
    });
});
