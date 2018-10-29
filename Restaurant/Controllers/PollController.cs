using System;
using System.Collections.Generic;
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
    public class PollController : ApiController
    {
        private SqlBdContext db = new SqlBdContext();

        // GET: poll
        public IQueryable<Survey> GetSurveys()
        {
            return db.Surveys;
        }

        // GET: poll/{id}
        [ResponseType(typeof(Survey))]
        public async Task<IHttpActionResult> GetSurvey(int id)
        {
            Survey survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // PUT: poll/{id}
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSurvey(int id, Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.Id)
            {
                return BadRequest();
            }

            db.Entry(survey).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: poll/{id}/vote
        [ResponseType(typeof(void))]
        [Route("poll/{id}/vote")]
        public async Task<IHttpActionResult> PostVoteForSurvey(int id, Vote vote)
        {
            Survey survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            Boolean existe = false;
            foreach (Resto resto in survey.Restaurants)
            {
                if (resto.Id == vote.RestoId)
                {
                    existe = true;
                }
            }

            if (!existe)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            existe = false;
            foreach (User user in survey.Users)
            {
                if (user.Id == vote.UserId)
                {
                    existe = true;
                }
            }

            if(!existe)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            survey.Votes.Add(vote);
            db.Entry(survey).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: poll
        [ResponseType(typeof(Survey))]
        public async Task<IHttpActionResult> PostSurvey(Survey survey)
        {
            if (survey.StartDate < DateTime.Now)
            {
                survey.StartDate = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(survey.EndDate < survey.StartDate || survey.Users.Count == 0 || survey.Restaurants.Count == 0)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            List<Resto> restos = new List<Resto>();
            foreach(Resto resto in survey.Restaurants)
            {
                Resto restaurantFound = await db.Restaurants.FindAsync(resto.Id);
                if (restaurantFound == null)
                {
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
                }
                else
                {
                    restos.Add(restaurantFound);
                }
            }

            List<User> users = new List<User>();
            foreach (User user in survey.Users)
            {
                User userFound = await db.Users.FindAsync(user.Id);
                if (user == null)
                {
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
                }
                else
                {
                    users.Add(userFound);
                }
            }

            survey.Restaurants = restos;
            survey.Users = users;
            db.Surveys.Add(survey);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = survey.Id }, survey);
        }

        // DELETE: poll/{id}
        [ResponseType(typeof(Survey))]
        public async Task<IHttpActionResult> DeleteSurvey(int id)
        {
            Survey survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            db.Surveys.Remove(survey);
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

        private bool SurveyExists(int id)
        {
            return db.Surveys.Count(e => e.Id == id) > 0;
        }
    }
}