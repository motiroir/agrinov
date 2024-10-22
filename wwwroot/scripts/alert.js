window.onload = function () {
    console.log(contractExists);
    if (contractExists === 'true') {
        var popup = document.getElementById('popup');
        popup.style.display = 'block';
    }
    var closeButton = document.querySelector('.close-button');
    closeButton.onclick = function () {
        var popup = document.getElementById('popup');
        popup.style.display = 'none';
    };
    window.onclick = function (event) {
        var popup = document.getElementById('popup');
        if (event.target === popup) {
            popup.style.display = 'none';
        }
    };
};
