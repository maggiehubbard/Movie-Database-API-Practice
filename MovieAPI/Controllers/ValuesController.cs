
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace MovieAPI.Controllers
{
    public class ValuesController : ApiController
    {
        static 

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        //#1 Get a list of all movies
        public JsonResult<List<Movy>> GetMovieList()
        {
            MovieEntities db = new MovieEntities();
            List<Movy> movies = db.Movies.ToList();
            return Json(movies);
        }

        //#2 Search movie catalog by genre
        public JsonResult<List<Movy>> GetMoviesByGenre(string genre)
        {
            MovieEntities db = new MovieEntities();
            List<Movy> movies = (from m in db.Movies
                                 where m.Genre == genre
                                 select m).ToList();
            return Json(movies);
        } 

        //#3 Get a random movie pick
        public JsonResult<Movy> GetRandomMoviePick()
        {
            Random rnd = new Random();
            MovieEntities db = new MovieEntities();
            List<Movy> movies = db.Movies.ToList();
            int upperLimit = movies.Count() + 1;
            int moviepick = rnd.Next(0, upperLimit);
            return Json(movies[moviepick]);
        }

        //#4 Random movie by Category
        public JsonResult<Movy> GetRandomMoviePickByGenre(string genre)
        {
            Random rnd = new Random();
            MovieEntities db = new MovieEntities();
            List<Movy> movies = (from m in db.Movies
                                 where m.Genre == genre
                                 select m).ToList();
            int upperLimit = movies.Count();
            int moviepick = rnd.Next(0, upperLimit);
            return Json(movies[moviepick]);
        }

        //#5 Get a list of random movie picks - user specifies quantity
        public JsonResult<List<Movy>> GetRandomMovieList(int quantity)
        {
            MovieEntities db = new MovieEntities();
            Random rnd = new Random();
            List<Movy> movies = db.Movies.ToList();
            List<Movy> randomMovies = new List<Movy>();
            int upperLimit = movies.Count();
            

            for (int i = 0; i < quantity; i++)
            {
                int moviepick = rnd.Next(0, upperLimit);
                randomMovies.Add(movies[moviepick]);
            }          

            return Json(randomMovies);
        }

        //#6 Get a list of all movie categories

        public JsonResult<List<string>> GetCategoryList()
        {
            MovieEntities db = new MovieEntities();
            List<Movy> movies = db.Movies.ToList();
            List<string> categoryList = new List<string>();

            for (int i = 0; i < movies.Count(); i++)
            {
                categoryList.Add(movies[i].Genre);  
            }
            return Json(categoryList);
        }

        //#7 Get info about a specific movie - user specifies title as parameter query
        public JsonResult<Movy> GetMoviesAboutOneMovie(string title)
        {
            MovieEntities db = new MovieEntities();
            Movy movie = (from m in db.Movies
                                 where m.Title == title
                                 select m).Single();
            return Json(movie);
        }

        //#8 Get a list  of movies w/ keywrd in the title - user specifies title as parameter query
        public JsonResult<List<Movy>> GetSearchMoviesByKeyWordInTitle(string search)
        {
            MovieEntities db = new MovieEntities();
            List<Movy> movies = (from m in db.Movies
                          where m.Title.Contains(search)
                          select m).ToList();
            return Json(movies);
        }
    }
}
