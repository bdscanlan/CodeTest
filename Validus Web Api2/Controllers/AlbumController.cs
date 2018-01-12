using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Validus_Code_Test;

namespace Validus_Web_Api2.Controllers
{
    public class AlbumController : ApiController
    {
        public IEnumerable<Album> Get()
        {
            var db = new MusicContext();

            var album = db.Album;
            
            return album;
        }

        // GET: api/Song/5
        public Album Get(int id)
        {
            var db = new MusicContext();

            return db.Album.Where(s => s.Id == id).FirstOrDefault();

        }

        // POST: api/Song
        public void Post([FromBody]Album value)
        {
            var db = new MusicContext();

            value.Created = DateTime.Now;
            value.LastModified = DateTime.Now;
           
            db.Album.Add(value);
        }

        // PUT: api/Song/5
        public void Put(int id, [FromBody]Album value)
        {
            var db = new MusicContext();

            Album album = db.Album.Where(s => s.Id == id).FirstOrDefault();

            if (album == null)
            {
                return;
            }

            album.LastModified = DateTime.Now;
            album.artist = value.artist;

            album.name = value.name;

            db.SaveChanges();
        }

        // DELETE: api/Song/5
        public void Delete(int id)
        {
            var db = new MusicContext();

            Album album = db.Album.Where(s => s.Id == id).FirstOrDefault();

            if (album == null)
            {
                return;
            }

            db.Album.Remove(album);

            db.SaveChanges();
        }
    }
}
