// Simple Dark Theme Toggle
// Save theme preference to browser storage and apply it

// Load theme preference on page load
function loadTheme() {
    var savedTheme = localStorage.getItem('theme');
    
    if (savedTheme === 'dark') {
        document.body.classList.add('dark-mode');
        updateThemeButton('dark');
    } else {
        document.body.classList.remove('dark-mode');
        updateThemeButton('light');
    }
}

// Toggle between light and dark theme
function toggleTheme() {
    var isDarkMode = document.body.classList.contains('dark-mode');
    
    if (isDarkMode) {
        // Switch to light theme
        document.body.classList.remove('dark-mode');
        localStorage.setItem('theme', 'light');
        updateThemeButton('light');
    } else {
        // Switch to dark theme
        document.body.classList.add('dark-mode');
        localStorage.setItem('theme', 'dark');
        updateThemeButton('dark');
    }
}

// Update button icon and text
function updateThemeButton(theme) {
    var button = document.getElementById('themeToggleBtn');
    
    if (theme === 'dark') {
        button.innerHTML = '☀️'; // Sun icon for light mode
        button.title = 'Switch to Light Mode';
    } else {
        button.innerHTML = '🌙'; // Moon icon for dark mode
        button.title = 'Switch to Dark Mode';
    }
}

// Run when page loads
document.addEventListener('DOMContentLoaded', function() {
    loadTheme();
});
