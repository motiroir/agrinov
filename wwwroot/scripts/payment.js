window.onload = function () {
    const name = document.getElementById('name');
    const cardnumber = document.getElementById('cardnumber');
    const expirationdate = document.getElementById('expirationdate');
    const securitycode = document.getElementById('securitycode');
    const creditCard = document.querySelector('.creditcard');

    // Format card number
    cardnumber.addEventListener('input', function () {
        let value = cardnumber.value.replace(/\D/g, '');
        let formattedValue = value.replace(/(\d{4})(?=\d)/g, '$1 ');

        cardnumber.value = formattedValue.substring(0, 19);
        document.getElementById('svgnumber').textContent = cardnumber.value || '0123 4567 8910 1112';
    });

    // Format expiration date
    expirationdate.addEventListener('input', function () {
        let value = expirationdate.value.replace(/\D/g, '');
        if (value.length >= 2) {
            expirationdate.value = value.substring(0, 2) + '/' + value.substring(2, 4);
        }
        document.getElementById('svgexpire').textContent = expirationdate.value || '01/23';
    });

    // Format CSV
    securitycode.addEventListener('input', function () {
        let value = securitycode.value.replace(/\D/g, '');
        securitycode.value = value.substring(0, 3);
        document.getElementById('svgsecurity').textContent = securitycode.value || '985';
    });

    // Get Name
    name.addEventListener('input', function () {
        document.getElementById('svgname').textContent = name.value || 'Jean Dupont';
        document.getElementById('svgnameback').textContent = name.value || 'Jean Dupont';
    });

    // Card flip event on click
    creditCard.addEventListener('click', function () {
        this.classList.toggle('flipped');
    });

    // Focus events
    name.addEventListener('focus', function () {
        creditCard.classList.remove('flipped');
    });

    cardnumber.addEventListener('focus', function () {
        creditCard.classList.remove('flipped');
    });

    expirationdate.addEventListener('focus', function () {
        creditCard.classList.remove('flipped');
    });

    securitycode.addEventListener('focus', function () {
        creditCard.classList.add('flipped');
    });
};



document.getElementById('online-button').addEventListener('click', function () {
    this.classList.add('active');
    document.getElementById('offline-button').classList.remove('active');
    document.getElementById('online-payment').style.opacity = '1';
    document.getElementById('online-payment').style.transform = 'translate(300px, -60px)';
    document.getElementById('offline-payment').style.opacity = '0';
    document.getElementById('offline-payment').style.transform = 'translate(500px,20px)';

});

document.getElementById('offline-button').addEventListener('click', function () {
    this.classList.add('active');
    document.getElementById('online-button').classList.remove('active');
    document.getElementById('offline-payment').style.opacity = '1';
    document.getElementById('offline-payment').style.transform = 'translate(-200px, 20px)';
    document.getElementById('online-payment').style.opacity = '0';
    document.getElementById('online-payment').style.transform = 'translate(-300px, -60px)';
 
});

