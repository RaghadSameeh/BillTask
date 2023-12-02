function TotalPrice(id) {
    //get inputs values to get the total for each item
    var input1 = document.querySelector(".quantity" + id);
    var input2 = document.querySelector(".unitPrice" + id);
    var result = document.querySelector(".result" + id);
    result.innerHTML = Number(input1.value) * Number(input2.value);
    updateTotal();
    
}

function updateTotal() {
    let total = 0;
    let table = document.getElementById("tbody");

    for (let i = 1; i <= table.rows.length; i++) {
        var result = Number(document.querySelector(".result" + i).innerHTML);
        total += result;
    }

    document.getElementById('total').innerHTML = total.toFixed(2);
}



function deleteSelectedRows() {
    var checkboxes = document.getElementsByClassName('deleteCheckbox');
    var table = document.getElementsByClassName('displayTable')[0];
    var rowsToDelete = [];

    // Convert the collection to an array and use forEach
    Array.from(checkboxes).forEach(function (chkDelete, index) {
        if (chkDelete.checked) {
            // If the checkbox is checked, add the row index to the array
            rowsToDelete.push(index);
        }
    });

    // Loop through the array of row indices to delete
    for (var j = 0; j < rowsToDelete.length; j++) {
        // Remove the row from the client-side table
        table.deleteRow(rowsToDelete[j]);
    }
}




