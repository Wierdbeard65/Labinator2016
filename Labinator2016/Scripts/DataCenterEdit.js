
$(document).on("change", '#Region', function () {
    RefreshGateways();
});

function RefreshGateways() {
    var region = $("#Region").val();
    var currentGW = ($("#oldGW").val());
    $.ajax({
        type: "POST",
        data: { region: region },
        url: '../../DataCenters/ConfigurationAjax',
        dataType: 'json',
        success: function (result) {
            //var result = jQuery.parseJSON(data);
            var dropdown = $("#GateWayId");
            dropdown.html('');
            dropdown.append('<option value="">Please Select.... XYZ</option>');
            if (result != '') {
                // Loop through each of the results and append the option to the dropdown
                $.each(result, function (k, v) {
                    if (k == currentGW)
                    {
                        dropdown.append('<option value="' + k + '" selected>' + v + '</option>');
                    }
                    else
                    {
                        dropdown.append('<option value="' + k + '">' + v + '</option>');
                    }
                });
            }
        }
    });
}

$(document).load(RefreshGateways());