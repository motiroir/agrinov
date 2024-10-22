let selectedRow = null;

function selectRow(row) {
    if (selectedRow) {
        selectedRow.classList.remove('selected');
    }

    selectedRow = row;
    selectedRow.classList.add('selected');

   
    const updateButton = document.getElementById('updateButton');
    updateButton.disabled = false;
}



document.getElementById('updateButton').onclick = function () {
    if (selectedRow) {
        const id = selectedRow.getAttribute('data-id');
        window.location.href = `UpdateProduction/${id}`;
    }
};

window.onload = function () {
    document.getElementById('updateButton').disabled = true;
};



