using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class UserController : ApiController
    {
        private SqlBdContext db = new SqlBdContext();

        // GET: /user
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: /user/{id}
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = "";
            return Ok(user);
        }

        // PUT: /user/{id}
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: /user
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User usernameExiste = db.Users.SingleOrDefault(x => x.Username == user.Username);

            if (usernameExiste != null)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            db.Users.Add(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // POST: /user/login
        [ResponseType(typeof(User))]
        [Route("user/login")]
        public IHttpActionResult LoginUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User usernameExiste = db.Users.SingleOrDefault(x => x.Username == user.Username);

            if (usernameExiste == null)
            {
                return NotFound();
            }

            if (usernameExiste.Password != user.Password)
            {
                return Unauthorized();
            }

            usernameExiste.Password = "";
            usernameExiste.Username = "";
            return Ok(usernameExiste);
        }

        // DELETE: users/{id}
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}