function LoadCascade(url, dataFind, controlCarga) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        data: dataFind,
        success: function (selectItems) {
            controlCarga.html(""); 
            $.each(selectItems, function (i, selectItem) {
                controlCarga.append(
                    $('<option></option>').val(selectItem.Value).html(selectItem.Text));
            });
        }
    });
}