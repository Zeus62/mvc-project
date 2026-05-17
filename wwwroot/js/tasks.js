// ==========================================
// Dynamic Content Updates (JavaScript Interactivity #2)
// AJAX delete: remove tasks without page refresh
// ==========================================

function showToast(message, type) {
    // Remove existing toasts
    document.querySelectorAll('.toast').forEach(t => t.remove());

    const toast = document.createElement('div');
    toast.className = 'toast toast-' + type;
    toast.textContent = message;
    document.body.appendChild(toast);

    // Auto-remove after 3 seconds
    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transform = 'translateY(20px)';
        toast.style.transition = '0.3s ease';
        setTimeout(() => toast.remove(), 300);
    }, 3000);
}

function deleteTask(taskId) {
    // Confirm before deleting
    if (!confirm('Are you sure you want to delete this task?')) {
        return;
    }

    // Send AJAX request to delete the task
    fetch('/Tasks/Delete/' + taskId, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': getAntiForgeryToken()
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Animate the card removal
            const card = document.getElementById('task-' + taskId);
            if (card) {
                card.style.transition = 'all 0.3s ease';
                card.style.opacity = '0';
                card.style.transform = 'scale(0.95) translateY(-10px)';
                setTimeout(() => {
                    card.remove();

                    // Check if the tasks container is empty
                    const container = document.getElementById('tasksContainer');
                    if (container && container.children.length === 0) {
                        // Reload to show empty state
                        location.reload();
                    }
                }, 300);
            }
            showToast(data.message, 'success');
        } else {
            showToast(data.message || 'Failed to delete task', 'error');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showToast('An error occurred while deleting the task', 'error');
    });
}

function getAntiForgeryToken() {
    // Try to get the token from a form on the page
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
    return tokenInput ? tokenInput.value : '';
}
