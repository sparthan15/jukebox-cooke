using DigitalJukebox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace DigitalJukebox.Test.Model
{
    public class SongTest
    {
        [Fact]
        void canCreateSong()
        {
            Song song = Song.Create("Leonelda", "LeyendMaker",4.56,  "picture.jpg");
            Assert.NotNull(song);
        }

        [Fact]
        void canCreateEmptySong()
        {
            Song song = Song.Empty();
            Assert.NotNull(song);
            
        }

        [Fact]
        void validProperties()
        {
            Song song = Song.Create("Leonelda", "LeyendMaker", 4.56, "picture.jpg");
            Assert.Equal("Leonelda", song.Title);
            Assert.Equal("LeyendMaker", song.Artist);
            Assert.Equal(4.56, song.Duration);
            Assert.Equal("picture.jpg", song.Picture);
        }

      
    }
}
