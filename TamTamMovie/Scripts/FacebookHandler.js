var btnFbLogin = "<button onclick='login()'>Facebook Login</button>";

// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {
    if (response.status === 'connected') {
        drawFbShareButton();
    } else {
        login();
    }
}


var drawFbShareButton = function () {
    var btnFbShareMovie = "<button onclick='shareVideo()'>Share Video on Facebook</button>";
    document.getElementById('SocialNetworkButtons').innerHTML = btnFbShareMovie;
};

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '1644308279183059',
        cookie: true,
        xfbml: true,  // parse social plugins on this page
        version: 'v2.2'
    });

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function shareVideo() {
    var body = InformationToShare();

    FB.api('/me/feed', 'post', { message: body }, function (response) {
        if (!response || response.error) {
            alert("Error post");
            console.log(response.error);
        } else {
            alert('Post Successuful');
        }
    });
}

var InformationToShare = function () {
    var mainDiv = document.getElementById('MovieDetailInformation');
    var movieTitle = mainDiv.getElementsByTagName('h2')[0].innerText;
    var movieTrailer = mainDiv.getElementsByTagName('iframe')[0].getAttribute('src');

    return {
        message: movieTitle,
        source: movieTrailer
    };
    //return movieTitle + "\n\n" + movieTrailer;
};

var login = function (data) {
    FB.login(function (response) {
       // alert(response);
    }, { scope: 'email,publish_actions' });
}