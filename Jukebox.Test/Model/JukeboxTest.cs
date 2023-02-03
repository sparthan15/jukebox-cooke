using DigitalJukebox.Domain.Port.Out;
using DigitalJukebox.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace DigitalJukebox.Test.Model
{
    public class JukeboxTest
    {

        private readonly Mock<ISongsLoader> _mockedSongsLoader;

        public JukeboxTest()
        {
            List<Song> storedSongs = new List<Song>() {
                Song.Create("Leonelda", "Leyend Maker", 4.56, "picture.jpg"),
                Song.Create("path to Glory", "Leyend Maker", 4.56, "picture.jpg"),
                Song.Create("La plata", "DIomedez", 3.30, "diopic.jpg"),
                Song.Create("Todo de cabeza", "Kaleth", 4.30, "Kalethpic.jpg"),
                Song.Create("Verdades afiladas", "Calamaro", 3.10, "diopic.jpg"),
                Song.Create("A million light years away", "Stratovarius", 6.30, "stratopic.jpg")
            };
            _mockedSongsLoader = new Mock<ISongsLoader>();
            _mockedSongsLoader.Setup(x => x.LoadSongs()).Returns(storedSongs);
        }

        [Fact]
        void canCreateJukeBox()
        {
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            _mockedSongsLoader.Verify(mock => mock.LoadSongs(), Times.Once);
            Assert.NotNull(jukebox);
        }

        [Fact]
        void jukeboxCanPauseSong()
        {
            List<Song> playList = SongList();
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            _mockedSongsLoader.Setup(mock => mock.LoadFiveSongsOrderedByTitle()).Returns(playList);
            jukebox.TurnOn();
            jukebox.Play();
            jukebox.Pause();
            Assert.True(jukebox.IsPaused());
        }

        [Fact]
        void jukeboxCanPlayCurrentSong()
        {
            List<Song> playList = SongList();
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            _mockedSongsLoader.Setup(mock => mock.LoadFiveSongsOrderedByTitle()).Returns(playList);
            jukebox.TurnOn();
            jukebox.Play();
            Assert.True(jukebox.IsPlayingSong());
            Assert.Equal(jukebox.CurrentSong, playList.First());
        }


        [Fact]
        void givenJukeBoxIsCreatedThenStatusIsOff()
        {
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            Assert.NotNull(jukebox);
            Assert.True(jukebox.IsOff());
        }

        [Fact]
        void givenJukeboxIsOffThenPlayListIsEmpty()
        {
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            Assert.Empty(jukebox.PlayList);
        }

        [Fact]
        void jukeboxHasStoredSongs()
        {
           Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            _mockedSongsLoader.Verify(mock => mock.LoadSongs(), Times.Once);
            Assert.NotEmpty(jukebox.Songs());    
        }

        [Fact]
        void givenJukeboxCanBeSwitchedToOnThenCurrentSongIsEmpty()
        {
            _mockedSongsLoader.Setup(mock => mock.LoadFiveSongsOrderedByTitle())
                .Returns(Enumerable.Empty<Song>().ToList());
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            jukebox.TurnOn();
            Assert.False(jukebox.IsOff());
        }

        [Fact]
        void givenJukeboxWasSwitchedToOnThenPlayListIsFilledWithFiveSongs()
        {
            List<Song> playList = SongList();
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            _mockedSongsLoader.Setup(mock => mock.LoadFiveSongsOrderedByTitle()).Returns(playList);
            jukebox.TurnOn();
            _mockedSongsLoader.Verify(mock => mock.LoadFiveSongsOrderedByTitle(), Times.Once());
            Assert.Equal(5, jukebox.PlayList.Count);
        }

      

        [Fact]
        void givenJukeBoxIsOffWhenPlayIsPressedThenThrowAnException()
        {
            List<Song> playList = SongList();
            Jukebox jukebox = Jukebox.Create(_mockedSongsLoader.Object);
            _mockedSongsLoader.Setup(mock => mock.LoadFiveSongsOrderedByTitle()).Returns(playList);
            Assert.Throws< InvalidOperationException>(()=>jukebox.Play());
        }

       

        private static List<Song> SongList()
        {
            return new List<Song>() {
                Song.Create("Leonelda", "Leyend Maker", 4.56, "picture.jpg"),
                Song.Create("path to Glory", "Leyend Maker", 4.56, "picture.jpg"),
                Song.Create("La plata", "DIomedez", 3.30, "diopic.jpg"),
                Song.Create("Todo de cabeza", "Kaleth", 4.30, "Kalethpic.jpg"),
                Song.Create("A million light years away", "Stratovarius", 6.30, "stratopic.jpg")
            };
        }

    }
}
