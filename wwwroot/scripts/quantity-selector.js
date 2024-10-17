document.addEventListener('DOMContentLoaded', function () {
    const minusButton = document.querySelector('.quantity-selector button.minus');
    const plusButton = document.querySelector('.quantity-selector button.plus');
    const quantityInput = document.querySelector('.quantity-selector input');
    const minValue = parseInt(quantityInput.getAttribute('min')); 
    const maxValue = parseInt(quantityInput.getAttribute('max')); 

    minusButton.addEventListener('click', function () {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue > minValue) { 
            quantityInput.value = currentValue - 1;
        }
    });

    plusButton.addEventListener('click', function () {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue < maxValue) { 
            quantityInput.value = currentValue + 1;
        }
    });
});