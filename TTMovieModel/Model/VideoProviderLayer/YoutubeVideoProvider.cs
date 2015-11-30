using System;
using System.Collections.Generic;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Diagnostics;

namespace TTMovieModel.Model
{

    //   Created with base in
    //
    // https://github.com/youtube/api-samples/blob/master/dotnet/Search.cs
    //
    //

    public class YoutubeVideoProvider : IVideoProvider
    {
        private const string API_KEY = "AIzaSyCh7VANgjDUfuLsH-nMOAzwC04c8CL8Fwo";
        private const string EMBEBED_BASE_URL = "https://www.youtube.com/embed/";
        private const int MAX_RESULTS = 10;
        private const string PREFIX = "Trailer ";


        public IList<Video> GetVideoFor(string videoName)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = API_KEY,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = PREFIX+videoName;
            searchListRequest.MaxResults = MAX_RESULTS;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            IList<Video> videos = new List<Video>();

            foreach (var searchResult in searchListResponse.Items)
            {

                if (searchResult.Id.Kind == "youtube#video")
                {
                    videos.Add(new Video( EMBEBED_BASE_URL + searchResult.Id.VideoId , new Image(searchResult.Snippet.Thumbnails.Default__.Url)) );
                }
            }
            return videos;
        }

    }
}
