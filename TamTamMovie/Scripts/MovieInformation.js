$(document).ready(function () {
    /*
    var url = "/Home/GetMoviesBasicInformation";
    $.get(url, null, function (data) {
        data = data + "<script>GetMoviesCover();</script>";
        $('#MovieInformationContent').html(data);        
    });
    */
    var AppID = "1644308279183059";
    var AppSecret = "f4f0d8646292bebd4e7bdeb474ea04a7";
    var url_autgh = "https://www.facebook.com/dialog/oauth?client_id=" + AppID + "&redirect_uri=http://localhost:51191/Home/Index";


});


var GetMoviesCover = function () {
    var IdArrays = $('#MovieInformationContent  img').map(function () {
        return this.id;
    }).get();
    LoadCover(IdArrays, 0);
};

var LoadCover = function (IdArray, position) {
    if (position < IdArray.length) {
        var url = "/Home/LoadCoverFor?movieID=" + IdArray[position];
        $.get(url, null, function (imageUrl) {
            if (imageUrl !== "N/A") {
                $('#' + IdArray[position]).attr('src', imageUrl);
            }
            LoadCover(IdArray, position + 1);
            LoadTrailersToCache(IdArray[position]);
        });
    }
};


var LoadTrailersToCache = function (movieID) {
    var url = "/Home/LoadDetailedData?movieID=" + movieID;
    $.get(url, null, null);
};



var setMovieInMainContent = function (movieName) {
    var url = "/Home/GetMovie?movieID=" + movieName;
    $.get(url, null, function (data) {
        $('#MovieDetailInformation').html(data);
    });
};