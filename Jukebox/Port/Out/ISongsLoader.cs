using DigitalJukebox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalJukebox.Domain.Port.Out
{
    public interface ISongsLoader
    {
        List<Song> LoadFiveSongsOrderedByTitle();
        List<Song> LoadSongs();
    }
}
