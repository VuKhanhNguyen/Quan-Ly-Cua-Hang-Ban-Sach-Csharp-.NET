const forms = document.querySelector(".forms"),
    pwShowHide = document.querySelectorAll(".eye-icon"),
    links = document.querySelectorAll(".link");

pwShowHide.forEach(eyeIcon => {
    eyeIcon.addEventListener("click", () => {
        let pwFields = eyeIcon.parentElement.parentElement.querySelectorAll(".password");

        pwFields.forEach(password => {
            if (password.type === "password") {
                password.type = "text";
                eyeIcon.classList.replace("bx-hide", "bx-show");
                return;
            }
            password.type = "password";
            eyeIcon.classList.replace("bx-show", "bx-hide");
        });
    });
});

links.forEach(link => {
    link.addEventListener("click", e => {
        e.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>
        forms.classList.toggle("show-signup");
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const closeButton = document.querySelectorAll('.close-icon');

    // Lặp qua từng nút đóng và gắn sự kiện click
    closeButton.forEach(function (icon) {
        icon.addEventListener('click', function (event) {
            // Tìm phần tử cha của biểu tượng đóng (.close-icon)
            const parentForm = icon.closest('.form');
            const parentForm2 = icon.closest('.form2');
            const confirmClose = confirm("Bạn có muốn thoát không?");
            const reff = document.referrer;

            if (confirmClose) {
                // Điều hướng về index.html
                window.location.href = '/Home/Index';
            } else {
                // Ngăn chặn hành động mặc định khi người dùng chọn "Hủy"
                event.preventDefault();
            }
        });
    });

    // Chuyển đổi giữa form đăng nhập và đăng ký khi nhấp vào các liên kết tương ứng
    const loginLink = document.querySelector('.login-link');
    const signupLink = document.querySelector('.signup-link');
    const loginForm = document.querySelector('.login');
    const signupForm = document.querySelector('.signup');

    if (loginLink && signupLink && loginForm && signupForm) {
        // Chuyển sang form đăng ký khi nhấp vào liên kết Đăng ký
        signupLink.addEventListener('click', function (event) {
            event.preventDefault(); // Ngừng hành vi mặc định của thẻ <a>
            loginForm.style.display = 'none';
            signupForm.style.display = 'block';
        });

        // Chuyển sang form đăng nhập khi nhấp vào liên kết Đăng nhập
        loginLink.addEventListener('click', function (event) {
            event.preventDefault(); // Ngừng hành vi mặc định của thẻ <a>
            signupForm.style.display = 'none';
            loginForm.style.display = 'block';
        });
    }
});
