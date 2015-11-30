
$(document).ready(function () {
    document.getElementById('srch-term').value = "Fast and Furious";
    ajaxRequestAsync(drawAllMovies);
    clearSearchBox();
});

var clearSearchBox = function () {
    var searchBox = document.getElementById('srch-term').value = "";
}

var drawAllMovies = function (data) {
    var div = document.getElementById('MovieInformationContent').innerHTML = data;
    clearSearchBox();
    GetMoviesCover();
};

var ajaxRequestAsync = function (callBack) {
    var movieName = document.getElementById('srch-term').value;
    var url = "/Home/GetMoviesBasicInformation?movieName=" + movieName;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url, true);
    xhr.onload = function (e) {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                callBack(xhr.responseText);
                console.log(xhr.responseText);
            } else {
                console.error(xhr.statusText);
            }
        }
    };
    xhr.onerror = function (e) {
        console.error(xhr.statusText);
    };
    xhr.send(null);
};


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
            if (imageUrl && imageUrl !== "N/A" ) {
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