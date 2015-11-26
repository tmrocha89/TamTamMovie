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
        private const string EMBEBED_BASE_URL = "//youtube.com/v/"; //"https://youtu.be/";
        private const int MAX_RESULTS = 10;


        public Video /*async Task*/ getVideo(string videoName)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = API_KEY,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = videoName;
            searchListRequest.MaxResults = MAX_RESULTS;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = /*await*/ searchListRequest.Execute(); // ExecuteAsync();
            List<string> videos = new List<string>();

            Debug.WriteLine(" A ESCREVER A ASDASDASDASD");
            foreach (var searchResult in searchListResponse.Items)
            {

                if (searchResult.Id.Kind == "youtube#video")
                {
                    return new Video( EMBEBED_BASE_URL + searchResult.Id.VideoId);
                    //videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                }
            }

            // Debug.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            return null;
        }



    }
}
