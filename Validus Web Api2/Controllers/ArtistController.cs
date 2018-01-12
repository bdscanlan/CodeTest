using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Validus_Code_Test;

namespace Validus_Web_Api2.Controllers
{
    public class ArtistController : ApiController
    {
        public IEnumerable<Artist> Get()
        {
            var db = new MusicContext();

            return db.Artist;
        }

        // GET: api/Song/5
        public Artist Get(int id)
        {
            var db = new MusicContext();

            return db.Artist.Where(s => s.Id == id).FirstOrDefault();

        }

        // POST: api/Song
        public void Post([FromBody]Artist value)
        {
            var db = new MusicContext();

            value.Created = DateTime.Now;
            value.LastModified = DateTime.Now;

            db.Artist.Add(value);
        }

        // PUT: api/Song/5
        public void Put(int id, [FromBody]Artist value)
        {
            var db = new MusicContext();

            Artist artist = db.Artist.Where(s => s.Id == id).FirstOrDefault();

            artist.LastModified = DateTime.Now;

            artist.name = value.name;
            

            db.SaveChanges();
        }

        // DELETE: api/Song/5
        public void Delete(int id)
        {
            var db = new MusicContext();

            Artist artist = db.Artist.Where(s => s.Id == id).FirstOrDefault();

            db.Artist.Remove(artist);

            db.SaveChanges();
        }
    }
}
