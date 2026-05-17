// ==========================================
// Form Validation (JavaScript Interactivity #1)
// Client-side validation for Register and Create Task forms
// ==========================================

document.addEventListener('DOMContentLoaded', function () {

    // --- Register Form Validation ---
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        const username = document.getElementById('regUsername');
        const password = document.getElementById('regPassword');
        const confirm = document.getElementById('regConfirmPassword');

        function validateField(input, isValid, message) {
            // Remove old custom error
            let err = input.parentElement.querySelector('.js-error');
            if (err) err.remove();

            input.classList.remove('input-error', 'input-success');

            if (input.value.trim() === '') {
                input.classList.remove('input-success');
                return false;
            }

            if (!isValid) {
                input.classList.add('input-error');
                const span = document.createElement('span');
                span.className = 'field-validation js-error';
                span.textContent = message;
                input.parentElement.appendChild(span);
                return false;
            } else {
                input.classList.add('input-success');
                return true;
            }
        }

        if (username) {
            username.addEventListener('input', function () {
                validateField(this, this.value.length >= 3 && this.value.length <= 20,
                    'Username must be 3-20 characters');
            });
        }

        if (password) {
            password.addEventListener('input', function () {
                validateField(this, this.value.length >= 6,
                    'Password must be at least 6 characters');
                // Re-validate confirm if it has a value
                if (confirm && confirm.value) {
                    validateField(confirm, confirm.value === this.value,
                        'Passwords do not match');
                }
            });
        }

        if (confirm) {
            confirm.addEventListener('input', function () {
                validateField(this, this.value === password.value,
                    'Passwords do not match');
            });
        }

        registerForm.addEventListener('submit', function (e) {
            let valid = true;
            if (username && !validateField(username, username.value.length >= 3 && username.value.length <= 20, 'Username must be 3-20 characters')) valid = false;
            if (password && !validateField(password, password.value.length >= 6, 'Password must be at least 6 characters')) valid = false;
            if (confirm && !validateField(confirm, confirm.value === password.value, 'Passwords do not match')) valid = false;
            if (!valid) e.preventDefault();
        });
    }

    // --- Create / Edit Task Form Validation ---
    const taskForm = document.getElementById('createTaskForm') || document.getElementById('editTaskForm');
    if (taskForm) {
        const title = document.getElementById('taskTitle');
        const desc = document.getElementById('taskDescription');

        function validateTaskField(input, isValid, message) {
            let err = input.parentElement.querySelector('.js-error');
            if (err) err.remove();
            input.classList.remove('input-error', 'input-success');

            if (input.value.trim() === '') {
                input.classList.remove('input-success');
                return false;
            }

            if (!isValid) {
                input.classList.add('input-error');
                const span = document.createElement('span');
                span.className = 'field-validation js-error';
                span.textContent = message;
                input.parentElement.appendChild(span);
                return false;
            } else {
                input.classList.add('input-success');
                return true;
            }
        }

        if (title) {
            title.addEventListener('input', function () {
                validateTaskField(this, this.value.trim().length >= 2, 'Title must be at least 2 characters');
            });
        }

        if (desc) {
            desc.addEventListener('input', function () {
                validateTaskField(this, this.value.trim().length >= 5, 'Description must be at least 5 characters');
            });
        }

        taskForm.addEventListener('submit', function (e) {
            let valid = true;
            if (title && !validateTaskField(title, title.value.trim().length >= 2, 'Title is required (min 2 characters)')) valid = false;
            if (desc && !validateTaskField(desc, desc.value.trim().length >= 5, 'Description is required (min 5 characters)')) valid = false;
            if (!valid) e.preventDefault();
        });
    }
});
