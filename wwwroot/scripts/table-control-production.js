let selectedRow = null;

function selectRow(row) {
    if (selectedRow) {
        selectedRow.classList.remove('selected');
    }

    selectedRow = row;
    selectedRow.classList.add('selected');

   
    // Récupération du statut de validation
    let validationStatus = selectedRow.getAttribute('data-validation-status');
    

    const updateButton = document.getElementById('updateButton');

    // Vérifier si le statut est "REFUSED" ou "APPROVED"
    if (validationStatus === 'REFUSED' || validationStatus === 'APPROVED')
    {
        updateButton.disabled = true;
        
    }
    else 
    {
        updateButton.disabled = false;
        
    }
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



