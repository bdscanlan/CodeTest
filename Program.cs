using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validus_Code_Test
{
    class Program 
    {
        static void Main(string[] args)
        {
            using (var db = new MusicContext())
            {
                //db.Song.Add(new Song { name = "Another song2", Created = DateTime.Now, LastModified = DateTime.Now });
               // db.SaveChanges();

                foreach (var song in db.Song)
                {
                    Console.WriteLine(song.name);
                }

                // db.Artist.Add(new Artist { name = "Another artist", Created = DateTime.Now, LastModified = DateTime.Now });
                //db.SaveChanges();

                foreach (var song in db.Artist)
                {
                    Console.WriteLine(song.name);
                }

                //db.Album.Add(new Album { name = "Another album", Created = DateTime.Now , LastModified = DateTime.Now });
               // db.SaveChanges();

                foreach (var album in db.Album)
                {
                    Console.WriteLine(album.name);
                }

            }

            Console.ReadLine();
        }
    }
}
