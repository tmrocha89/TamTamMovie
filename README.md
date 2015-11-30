# TamTamMovie

See the result at https://tamtammovie.azurewebsites.net

I made two projects. The first one is the WebAPI and webpage. I made it all together, because that made it easier to import the project to the azure cloud. The webpage controller interacts with the WebAPI via Http requests. These two projects use the same DTO (MovieDTO).
The other project has all the business logic.

To get information about the movies, I get the basic information from IMDB’s website (eg. http://www.imdb.com/xml/find?json=1&tt=on&q=MOVIE_NAME&plot=full&r=json), and to get all the information about the movie I use OMDB with the IMDB movie id.
It is also possible to implement other online movie databases, as long as the classes that implement the logic to retrieve data from the databases implement the IMovieInformation interface.
The same thing for the videos. At this moment, I will retrieve a maximum of 10 trailers, and to support other websites we need to implement the IVideoProvider interface.

My web api is in the class MovieController, and it has 2 required parameters, “string movieName” and “bool isID”. This is also defined as the route “api/{controller}/{movieName}/{isID}/“.
I need the “isID” paremeter so I can tell when the “movieName” is the movie’s name or the movie’s id.
The other parameters are used to get the global information about the movie, or to get only the cover (this way the performance is maximized).

The cache is also implemented, through a class called “Model/MovieCache”, which serves as a repository to access it.

To search for movies, I use IMDB instead of OMDB, so I can get several movies from one search query. This way, if the user only knows part of the title, all possible options are presented. A smarter search was not implemented, but with more time I would have programmed regular expressions and maybe a grammar-based parser.

Also, you can share the trailer on Facebook. I used Facebook’s Javascript API.
As you can see in my (draft) class diagram, first I thought of the social networks as business logic, but on second thought  I decided to remove it and put it only in the “client application”.
Answering to Case TamTam

I made tow projects. The first one is the WebAPI and webpage. I made it all together, because that way was easy to import the project to the azure cloud. The webpage controller speaks with webAPI by Http requests. This two projects only use the same DTO Objects (MovieDTO).
The other project this where is all the business logic.

To get information about the movies, i will get the basic information to imdb website (eg. http://www.imdb.com/xml/find?json=1&tt=on&q=MOVIE_NAME&plot=full&r=json).
It is possible to implement also others online movie databases, the classes that will implement the logic to retrieve data from the website must implement the IMovieInformation interface.
The same thing to get videos, at this moment i will retrieve 10 trailers maximum, and to support others websites in our classes we need to implement IVideoProvider interface.

My web api is in the classe MovieController, and it has 2 require parameters, “string movieName” and “bool isID”. This is also defined a route as “api/{controller}/{movieName}/{isID}/“.
I need the “isID” to differentiate when the “movieName” is the movie name or is the movie’s id.
The others parameters are to get the global information about the movie and to get only the cover (this is built to assurance the performance)

The cache is also implemented, in a classe called “Model/MovieCache”. Is the repository that has access to it.

My smart search is the fact that i will get information to imdb, and didn’t have time to implement this.
My smart search is by the fact when you search, for example for “Fast and Furious” there are more than one movie, and you may not know all the names, so i present all movies with that string.
If i had more time, maybe i will implement this smart search with regular expressions and a grammar.

I also you can share the trailer in facebook. I used the Facebook’s Javascript API.
As you can see in my (draft) class diagram, at the first i put the social networks as business logic, but second thought  i decide to remove it and put only in the “client application”.




##My solution’s flow

I get the movie name and send it to imdb, and i get all the movies with that name, and then i go get the movie’s cover. To get the cover i use omdb api and i send the movie’s id, besides the cover i also get all the information (Stars, Writers, Directors,...) and the trailers.
When the user chooses a movie i show him all the information about that movie.
