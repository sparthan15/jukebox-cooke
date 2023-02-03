using DigitalJukebox.Domain.Port.Out;
using System.Collections;
using System.Collections.Generic;

namespace DigitalJukebox.Model
{
    public class Jukebox
    {
        private List<Song> _songList { get; set; }

        public List<Song> PlayList { get; }

        private  State _state= State.OFF;

        private readonly  ISongsLoader _songsLoader;

        public Song CurrentSong { get; set; }

        private Jukebox(ISongsLoader songsLoader) {

            PlayList = new List<Song>();
            _songList = songsLoader.LoadSongs();
            _songsLoader = songsLoader;
        }

        public static Jukebox Create(ISongsLoader songsLoader)
        {
           return new Jukebox(songsLoader);
        }

        public List<Song> Songs()
        {
            return _songList;
        }

        public void TurnOn()
        {
            _state = State.ON;
            PlayList.AddRange(_songsLoader.LoadFiveSongsOrderedByTitle());
        }

        public void Play()
        {
            _state = State.PLAYING;
            CurrentSong = PlayList.First();
        }

        public void Pause()
        {
            _state = State.PAUSE;
        }

        public bool IsPaused()
        {
            return _state.Equals(State.PAUSE);
        }

        public Boolean IsOff()
        {
            return _state.Equals(State.OFF);
        }

        public bool IsPlayingSong()
        {
            return _state.Equals(State.PLAYING);
        }
    }

    public enum State
    {
        OFF,ON,PLAYING,PAUSE
    }
}