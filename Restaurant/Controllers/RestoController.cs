using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class RestoController : ApiController
    {
        private SqlBdContext db = new SqlBdContext();

        // GET: api/Resto
        public IQueryable<Resto> GetRestaurants()
        {
            return db.Restaurants;
        }

        // GET: api/Resto/5
        [ResponseType(typeof(Resto))]
        public async Task<IHttpActionResult> GetRestaurant(int id)
        {
            Resto restaurant = await db.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Resto/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRestaurant(int id, Resto restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            db.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Resto
        [ResponseType(typeof(Resto))]
        public async Task<IHttpActionResult> PostRestaurant(Resto restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurant);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Resto/5
        [ResponseType(typeof(Resto))]
        public async Task<IHttpActionResult> DeleteRestaurant(int id)
        {
            Resto restaurant = await db.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurant);
            await db.SaveChangesAsync();

            return Ok(restaurant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantExists(int id)
        {
            return db.Restaurants.Count(e => e.Id == id) > 0;
        }
    }
}