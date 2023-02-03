using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalJukebox.Model
{
    public class Song
    {
        public string Title { get;  }
        public string Artist { get; }
        public double Duration { get; }
        public string Picture { get; }
       
        private Song(string title, string artist, double duration, string picture) {
        
            Title = title;
            Artist = artist;
            Duration = duration;
            Picture = picture;
        }

        public static Song Create(string title, string artist, double duration, string picture)
        {
            return new Song(title, artist, duration, picture);
        }

        public static Song Empty()
        {
            return new Song("", "", 0.0, "");
        }
    }
}
