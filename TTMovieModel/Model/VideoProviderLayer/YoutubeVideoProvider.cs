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
        private static string ApiKey = "AIzaSyCh7VANgjDUfuLsH-nMOAzwC04c8CL8Fwo";
        private static int MaxResults = 10;


        public void /*async Task*/ Run()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                
                ApiKey = ApiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "Minions"; // Replace with your search term.
            searchListRequest.MaxResults = MaxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = /*await*/ searchListRequest.Execute(); // ExecuteAsync();
            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();
            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            Debug.WriteLine(" A ESCREVER A ASDASDASDASD");
            foreach (var searchResult in searchListResponse.Items)
            {

                if (searchResult.Id.Kind == "youtube#video")
                {
                    videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                }
            }

            Debug.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
        }



    }
}
