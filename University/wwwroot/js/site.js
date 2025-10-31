<script>
    // Элементы DOM для входа
    const loginModal = document.getElementById('loginModal');
    const loginBtn = document.getElementById('loginBtn');
    const closeLoginBtn = document.getElementById('closeLoginBtn');
    const loginForm = document.getElementById('loginForm');

    // Элементы DOM для регистрации
    const registerModal = document.getElementById('registerModal');
    const registerBtn = document.getElementById('registerBtn');
    const closeRegisterBtn = document.getElementById('closeRegisterBtn');
    const registerForm = document.getElementById('registerForm');

    // Открытие окна входа
    if (loginBtn) {
        loginBtn.addEventListener('click', () => {
            loginModal.classList.add('active');
        });
    }

    // Открытие окна регистрации
    if (registerBtn) {
        registerBtn.addEventListener('click', () => {
            registerModal.classList.add('active');
        });
    }

    // Закрытие окна входа
    if (closeLoginBtn) {
        closeLoginBtn.addEventListener('click', () => {
            loginModal.classList.remove('active');
        });
    }

    // Закрытие окна регистрации
    if (closeRegisterBtn) {
        closeRegisterBtn.addEventListener('click', () => {
            registerModal.classList.remove('active');
        });
    }

    // Обработка формы входа
    if (loginForm) {
        loginForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const email = document.getElementById('loginEmail').value;
            const password = document.getElementById('loginPassword').value;

            // Здесь должна быть логика авторизации
            console.log('Вход:', { email, password });
            alert('Выполняется вход в систему...');

            // Закрываем модальное окно
            loginModal.classList.remove('active');

            // Очистка формы
            loginForm.reset();

            // Можно добавить редирект или обновление интерфейса
            // window.location.href = '/dashboard';
        });
    }

    // Обработка формы регистрации
    if (registerForm) {
        registerForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const fullName = document.getElementById('regFullName').value;
            const email = document.getElementById('regEmail').value;
            const studentId = document.getElementById('regStudentId').value;
            const faculty = document.getElementById('regFaculty').value;
            const password = document.getElementById('regPassword').value;
            const confirmPassword = document.getElementById('regConfirmPassword').value;

            // Валидация
            if (password !== confirmPassword) {
                alert('Пароли не совпадают!');
                return;
            }

            if (password.length < 6) {
                alert('Пароль должен содержать минимум 6 символов!');
                return;
            }

            // Здесь должна быть логика регистрации
            console.log('Регистрация:', {
                fullName,
                email,
                studentId,
                faculty,
                password
            });

            alert('Регистрация выполнена успешно! Теперь вы можете войти в систему.');

            // Закрываем модальное окно
            registerModal.classList.remove('active');

            // Очистка формы
            registerForm.reset();
        });
    }

    // Закрытие модальных окон при клике вне их
    window.addEventListener('click', (e) => {
        if (e.target === loginModal) {
        loginModal.classList.remove('active');
        }
    if (e.target === registerModal) {
        registerModal.classList.remove('active');
        }
    });

    // Закрытие модальных окон при нажатии Escape
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            if (loginModal.classList.contains('active')) {
        loginModal.classList.remove('active');
            }
    if (registerModal.classList.contains('active')) {
        registerModal.classList.remove('active');
            }
        }
    });

    // Дополнительные функции для улучшения UX
    function showSuccessMessage(message) {
        // Можно добавить красивые уведомления
        const notification = document.createElement('div');
    notification.style.cssText = `
    position: fixed;
    top: 20px;
    right: 20px;
    background: #4CAF50;
    color: white;
    padding: 15px 20px;
    border-radius: 5px;
    z-index: 1001;
    box-shadow: 0 2px 10px rgba(0,0,0,0.1);
    `;
    notification.textContent = message;
    document.body.appendChild(notification);
        
        setTimeout(() => {
        notification.remove();
        }, 3000);
    }

    function showErrorMessage(message) {
        const notification = document.createElement('div');
    notification.style.cssText = `
    position: fixed;
    top: 20px;
    right: 20px;
    background: #f44336;
    color: white;
    padding: 15px 20px;
    border-radius: 5px;
    z-index: 1001;
    box-shadow: 0 2px 10px rgba(0,0,0,0.1);
    `;
    notification.textContent = message;
    document.body.appendChild(notification);
        
        setTimeout(() => {
        notification.remove();
        }, 3000);
    }
</script>