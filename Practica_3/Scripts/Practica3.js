function ConsultarSaldoCompras() {
    let compra = $("#Compra").val();

    if (compra.length >= 1) {
        $.ajax({
            type: 'GET',
            url: '', // agregar direccion
            dataType: 'json',
            success: function (data) {
                $("#Saldo").val(data.saldo); // Modificar el "data.saldo" por el valor que sea necesario
            }
        });
    }
    else {
        $("#Saldo").val("");
    }
}