// Site-wide JavaScript

document.addEventListener('click', function(e) {
    if (e.target.tagName === 'BUTTON' && e.target.type === 'submit') {
        const form = e.target.closest('form');
        if (form) {
            let hasEmpty = false;
            // Check elements that have the HTML5 required attribute or ASP.NET data-val-required attribute
            const requiredElements = form.querySelectorAll('input[required], textarea[required], select[required], input[data-val-required], textarea[data-val-required], select[data-val-required]');
            
            requiredElements.forEach(el => {
                // If it's visible and empty
                if (el.type !== 'hidden' && !el.value.trim()) {
                    hasEmpty = true;
                }
            });

            if (hasEmpty) {
                alert('Please fill in all mandatory fields.');
            }
        }
    }
});
