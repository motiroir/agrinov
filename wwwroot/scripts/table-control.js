let selectedRow = null;

function selectRow(row, isForSale) {
    if (selectedRow) {
        selectedRow.classList.remove('selected');
    }

    selectedRow = row;
    selectedRow.classList.add('selected');

    const deleteButton = document.getElementById('deleteButton');
    const updateButton = document.getElementById('updateButton');

    if (!isForSale) {
        deleteButton.disabled = false;
        deleteButton.classList.remove('disabled');
        updateButton.disabled = false;
        updateButton.classList.remove('disabled');
    } else {
        deleteButton.disabled = true;
        deleteButton.classList.add('disabled');
        updateButton.disabled = true;
        updateButton.classList.add('disabled');
    }
}



document.getElementById('updateButton').onclick = function () {
    if (selectedRow) {
        const id = selectedRow.getAttribute('data-id');
        window.location.href = `UpdateBoxContract/${id}`;
    }
};

document.getElementById('deleteButton').onclick = function () {
    if (selectedRow) {
        const id = selectedRow.getAttribute('data-id');
        showPopup(id); 
    }
};



function showPopup(id) {
    const popup = document.getElementById('popup');
    popup.style.display = 'block';

    var closeButton = document.querySelector('.close-button');
    closeButton.onclick = function () {
        popup.style.display = 'none';
    };

    window.onclick = function (event) {
        if (event.target === popup) {
            popup.style.display = 'none';
        }
    };

    var confirmDelete = document.getElementById('confirmDelete');
    confirmDelete.onclick = function () {
        popup.style.display = 'none';
        fetch(`DeleteBoxContract/${id}`, {
            method: 'DELETE' 
        })
            .then(response => {
                if (response.ok) {
                    location.reload(); 
                } else {
                    alert("Erreur lors de la suppression du contrat.");
                }
            })
            .catch(error => {
                console.error('Erreur:', error);
                alert("Une erreur est survenue lors de la suppression du contrat.");
            });
    };
}

function updateForSaleStatus(checkbox) {
    const form = checkbox.closest('form');
    if (form) {
        form.submit();
    }
}