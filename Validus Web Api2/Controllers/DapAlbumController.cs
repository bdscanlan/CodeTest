using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Dapper;

using Validus_Code_Test;

namespace Validus_Web_Api2.Controllers
{
    public class DapAlbumController : ApiController
    {
       
        public IEnumerable<Album> Get()
        {

            using (var conn = new SqlConnection("Server=.;Database=scratch;Integrated Security=True;"))
            {
                conn.Open();

                string sql = "";

                IList<Album> allAlbums = new List<Album>();

                IList<Artist> allArtists = new List<Artist>();

                sql = " SELECT A.Id, A.Name, A.Created, A.LastModified, A.YearReleased, S.Id, S.Name, S.Created, S.LastModified, S.Album_Id, S.Track, R.Id,  R.Name ,R.Created, R.LastModified    " +
                        " FROM          Album A             " +
                        " INNER JOIN    Song  S             " +
                        "   ON A.Id = S.album_id            " +
                        " INNER JOIN artist_albums AA       " +
                        "   on S.Album_id = AA.albums_id    " +
                        " INNER JOIN Artist R               " +
                        "   on AA.artist_Id = R.ID          " +
                        " ORDER BY A.Id, S.Id ";

                var ax = conn.Query<Album,Song, Artist, Album>(
                    sql,
                    (album, song, artist) =>
                    {
                        var thisSongsArtist = (allArtists.Where(a => a.Id == artist.Id).FirstOrDefault());

                        if (thisSongsArtist == null)
                        {
                            allArtists.Add(artist);
                            thisSongsArtist = artist;
                        }

                        //if( thisSongsArtist.Albums == null)
                        //{

                        //    thisSongsArtist.Albums = new List<Album>();
                        //    thisSongsArtist.Albums.Add(album);
                        //}

                        var thisSongsAlbum = (allAlbums.Where(a => a.Id == album.Id).FirstOrDefault());

                        if (thisSongsAlbum == null)
                        {
                            thisSongsAlbum = album;

                            album.artist = artist;

                            album.Songs = new List<Song>();
                            
                            allAlbums.Add(album);
                        }

                        thisSongsAlbum.Songs.Add(song);

                        return album;
                    }
                    //,splitOn: "YearReleased, Track"
                ).ToList();

                return allAlbums;
            }


        }

        // GET: api/Song/5
        public Album Get(int id)
        {
            IList<Album> thisAlbum = Get().Where(Album => Album.Id == id).ToList();

            return thisAlbum.FirstOrDefault();

        }

        // POST: api/Song
        public void Post([FromBody]Album value)
        {
            #region Fiddler Test Script
            
            //POST http://localhost:65529/DapAlbum/1 HTTP/1.1
            //User-Agent: Fiddler
            //Host: localhost:65529
            //Content-Type: application/json
            //Content-Length: 76

            //{
            //name:'Boston 2',
            //yearReleased: '1977',
            //Artist :
            //{
            //name:'Boston'
            //}
            //}

            #endregion
            try
            {
                using (var conn = new SqlConnection("Server=.;Database=scratch;Integrated Security=True;"))
                {
                    conn.Open();

                    if (value.artist.name != null && value.name != null )
                    {
                        // INSERT previously unknown artists
                        string sqlArtist = " INSERT INTO Artist (Name, Created, LastModified)                      " +
                                            " SELECT Data.Name, Data.Created, Data.LastModified FROM (              " +
                                            " SELECT @Name Name, GetDate() Created, GetDate() LastModified ) Data   " +
                                            " LEFT OUTER JOIN Artist R                                              " +
                                            "   ON Data.Name = R.Name                                               " +
                                            " WHERE R.Name IS NULL                                                  ";

                        var artistQuery = conn.Execute(sqlArtist, new { @Name = value.artist.name });
                    }

                    string sqlAlbum =   " INSERT INTO Album (Name, YearReleased, Created, LastModified, Artist_Id)                                  " +
                                        " SELECT AA.*, A.Id FROM (                                                                                  " +
                                        " SELECT @Name Name, @YearReleased yearReleased, @Created Created, @LastModified LastModified   )    AA     " +
                                        " LEFT OUTER JOIN Artist A   ON     A.Name = @AName                                                         " +
                                        " LEFT OUTER JOIN Album  P   ON P.Name = @Name  AND P.YearReleased = @YearReleased                          " +
                                        " WHERE P.Name IS NULL                                                                                      ";
                                    
                    
                    var ax = conn.Execute(
                        sqlAlbum, new { @Name = value.name, @YearReleased = value.yearReleased, @Created = DateTime.Now, @LastModified = DateTime.Now, @AName = value.artist.name }
                    );

                    if (value.artist.name != null && value.name != null)
                    {
                        string sqlAlbumArtist = " INSERT INTO artist_albums (artist_id, albums_id)                          " +
                                                " SELECT DATA.artist_Id, Data.album_Id      FROM (                          " +
                                                "   SELECT  AL.Artist_Id,  AL.ID album_Id                                   " +
                                                "   FROM    Album AL                                                        " +
                                                "   WHERE AL.Name = @Name and AL.YearReleased = @YearReleased ) Data        " +
                                                " LEFT OUTER JOIN artist_albums AA                                          " + 
                                                "   ON  DATA.Album_Id   = AA.albums_Id                                      " + 
                                                "   AND DATA.artist_Id  = AA.Artist_Id                                      " +
                                                " WHERE AA.artist_Id IS NULL                                                ";

                        var aa = conn.Execute(sqlAlbumArtist, new { @Name = value.name, @YearReleased = value.yearReleased });
                    
                    }
                }
            }
            catch(Exception  ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // PUT: api/Song/5
        public void Put(int id, [FromBody]Album value)
        {
            
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
