function TotalPrice(id) {
    var input1 = document.querySelector(".quantity" + id);
    var input2 = document.querySelector(".unitPrice" + id);
    var result = document.querySelector(".result" + id);

    result.innerHTML = Number(input1.value) * Number(input2.value);
    updateTotal();
}

function updateTotal() {
    var result1 = Number(document.querySelector(".result1").innerHTML);
    var result2 = Number(document.querySelector(".result2").innerHTML);
    var result3 = Number(document.querySelector(".result3").innerHTML);

    var total = result1 + result2 + result3;
    document.getElementById('total').innerHTML = total.toFixed(2);
}
