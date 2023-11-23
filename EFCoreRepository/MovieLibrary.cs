using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFCoreRepository
{
    public class MovieLibrary: IEnumerable<string> 
    {
        private Dictionary<int, string> _ordinaryMovies = new();
        //{ 
        //{10, "Movie1"},
        //{20, "Movie2"},
        //{30, "Movie3"},
        //{40, "Movie4"},
        //{50, "Movie5"},
        //};

        private Dictionary<int, string> _adultMovies = new();
        //{
        //{6, "AdultMovie6"},
        //{7, "AdultMovie7"},
        //{8, "AdultMovie8"},
        //{9, "AdultMovie9"},
        //};
        public MovieLibrary(MlDbContext db)
        {
            foreach (var item in db.Movies) 
            
            {
                if (item.IsAdult == true) 
                {
                    _adultMovies.Add(item.Articul, item.Name);
                }
                else 
                { 
                    _ordinaryMovies.Add(item.Articul, item.Name);
                }
            }
            
        }
        public string this[int article]
        {
            get  
            {
                return GetMovie(article);
            }
            set
            {
               throw new NotImplementedException();
            }
        }
        private string GetMovie(int article)
        {
            if (!IsAdultTime())
            {
                if (_ordinaryMovies.ContainsKey(article))
                {
                    return _ordinaryMovies[article];
                }
            }
            else
            {
                if (_ordinaryMovies.ContainsKey(article))
                {
                    return _ordinaryMovies[article];
                }
                if (_adultMovies.ContainsKey(article))
                {
                    return _adultMovies[article];
                }
            }

            return "No such key";
        }
        private bool IsAdultTime()
        { 
            return DateTime.Now.Hour < 23 || DateTime.Now.Hour > 7;
        }
        public IEnumerator<string> GetEnumerator() 
        {
            if (!IsAdultTime())
            {
                return new MovieEnumerator(_ordinaryMovies.Values.ToList());
            }
            else
            {
                var result = _ordinaryMovies.Values.ToList();
                result.AddRange(_adultMovies.Values.ToList());
                return new MovieEnumerator(result);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        
        { 
            return GetEnumerator(); 
        }
}
    public class MovieEnumerator: IEnumerator<string>
    {
        private int _position = -1;
        private List<string> _movies = new();

        public MovieEnumerator (List<string> movies)
        {
            _movies = movies;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < _movies.Count;
        }
        public void Reset()
        {
            _position = -1;
        }
        public string Current
        {
            get 
            
            { 
              return _movies.ElementAt(_position);
            }
        }
        object IEnumerator.Current => Current;

        public void Dispose()

        { 
        }
    }
}
