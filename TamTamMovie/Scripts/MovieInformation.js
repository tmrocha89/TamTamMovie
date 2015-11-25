$(document).ready(function () {
   

    var url = "/Home/LoadInformation";

    $.get(url, null, function (data) {
        $('#MovieInformationContent').html(data);
    });

});