$(document).ready(function () {
    var url = "/Home/LoadInformation";
    $.get(url, null, function (data) {
        $('#MovieInformationContent').html(data);

    });
});


var setMovieInMainContent = function (movieName) {
    var url = "/Home/GetMovie?movieID=" + movieName;
    $.get(url, null, function (data) {
        $('#MovieDetailInformation').html(data);
    });
};