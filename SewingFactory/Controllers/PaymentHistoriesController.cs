using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class PaymentHistoriesController : ApiController
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: api/PaymentHistories
        public IQueryable<PaymentHistory> GetPaymentHistory()
        {
            return db.PaymentHistory;
        }

        // GET: api/PaymentHistories/5
        [ResponseType(typeof(PaymentHistory))]
        public IHttpActionResult GetPaymentHistory(int id)
        {
            PaymentHistory paymentHistory = db.PaymentHistory.Find(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            return Ok(paymentHistory);
        }

        // PUT: api/PaymentHistories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaymentHistory(int id, PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentHistory.Id)
            {
                return BadRequest();
            }

            db.Entry(paymentHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentHistoryExists(id))
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

        // POST: api/PaymentHistories
        [ResponseType(typeof(PaymentHistory))]
        public IHttpActionResult PostPaymentHistory(PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentHistory.Add(paymentHistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paymentHistory.Id }, paymentHistory);
        }

        // DELETE: api/PaymentHistories/5
        [ResponseType(typeof(PaymentHistory))]
        public IHttpActionResult DeletePaymentHistory(int id)
        {
            PaymentHistory paymentHistory = db.PaymentHistory.Find(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            db.PaymentHistory.Remove(paymentHistory);
            db.SaveChanges();

            return Ok(paymentHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentHistoryExists(int id)
        {
            return db.PaymentHistory.Count(e => e.Id == id) > 0;
        }
    }
}