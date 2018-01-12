using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Validus_Code_Test;

namespace Validus_Web_Api2.Controllers
{
    public class SongController : ApiController
    {
        // GET: api/Song
        public IEnumerable<Song> Get()
        {
            var db = new MusicContext();

            return db.Song;
        }

        // GET: api/Song/5
        public Song Get(int id)
        {
            var db = new MusicContext();

            return db.Song.Where(s => s.Id == id).FirstOrDefault();

        }

        // POST: api/Song
        public void Post([FromBody]Song value)
        {
            var db = new MusicContext();

            value.Created = DateTime.Now;
            value.LastModified = DateTime.Now;

            db.Song.Add(value);
        }

        // PUT: api/Song/5
        public void Put(int id, [FromBody]Song value)
        {
            var db = new MusicContext();

            Song song =  db.Song.Where(s => s.Id == id).FirstOrDefault();

            song.LastModified = DateTime.Now;

            song.name = value.name;
            song.track  = value.track;

            db.SaveChanges();
        }

        // DELETE: api/Song/5
        public void Delete(int id)
        {
            var db = new MusicContext();

            Song song = db.Song.Where(s => s.Id == id).FirstOrDefault();

            db.Song.Remove(song);

            db.SaveChanges();
        }
    }
}
