$(document).ready(function () {
    var url = "/Home/GetMoviesBasicInformation";
    $.get(url, null, function (data) {
        data = data + "<script>GetMoviesCover();</script>";
        $('#MovieInformationContent').html(data);        
    });

    

});


var GetMoviesCover = function () {
    var IdArrays = $('#MovieInformationContent  img').map(function () {
        return this.id;
    }).get();
    LoadCover(IdArrays, 0);
    /*
    for (var i = 0; i < IdArrays.length; i++) {
        var url = "/Home/LoadCoverFor?movieID=" + IdArrays[i];
        $.get(url, null, function (imageUrl) {         
            if (imageUrl != "N/A") {
                alert(imageUrl);
                $('#'+IdArrays[i]).attr('src',imageUrl);
            }
        });
    }
    */
};

var LoadCover = function (IdArray, position) {
    if (position < IdArray.length) {
        var url = "/Home/LoadCoverFor?movieID=" + IdArray[position];
        $.get(url, null, function (imageUrl) {
            if (imageUrl !== "N/A") {
                alert(imageUrl);
                $('#' + IdArray[position]).attr('src', imageUrl);
            }
            LoadCover(IdArray, position + 1);
        });
    }
};

/*
$('#MovieInformationContent').bind('DOMNodeInserted', function (event) {
    
    if (this.innerHTML != "") {
        var movieID = this.getElementsByTagName("img")[0].id;
        //alert(movieID);
    }

});
*/


$('img').load(function () {
    alert(this.id);
});

var setMovieInMainContent = function (movieName) {
    var url = "/Home/GetMovie?movieID=" + movieName;
    $.get(url, null, function (data) {
        $('#MovieDetailInformation').html(data);
    });
};