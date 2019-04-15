using Stage.AlienTrick.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Stage.AlienTrick.Controllers
{
    public class SollicitatiesController : Controller
    {
        PortalEntities db = new PortalEntities();
        // GET: Sollicitaties
        [Rights(AllowAdmins = true)]
        public ActionResult Index(string searchString)
        {
            var jobsCollection = db.JobApplications.AsQueryable();

           if (!string.IsNullOrWhiteSpace(searchString))
            {
                jobsCollection = jobsCollection.Where(A => A.CandidateLastName.ToLower().Contains(searchString.ToLower()));
            }

            return View(jobsCollection.ToArray());
        }

        private void SentInvitation(bool Approved, int? UserId)
        {
            var usermail = db.JobApplications.Where(i => i.ID == UserId)
                .Select(d => d.CandidateMailadress)
                .FirstOrDefault();

            var gebruiker = db.JobApplications.Where(g => g.ID == UserId)
                .Select(g => g.CandidateName)
                .FirstOrDefault();


            SmtpClient client = new SmtpClient(WebConfigurationManager.AppSettings["SmtpHostname"])
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["MailUser"], WebConfigurationManager.AppSettings["MailPassword"])
            };

            MailMessage mailMessage = new MailMessage();
            MailAddress mailAddress = new MailAddress(WebConfigurationManager.AppSettings["MailNoreplyAddr"]);
            mailMessage.From = mailAddress;
            mailMessage.To.Add(usermail);
            
            if (Approved)
            {
                mailMessage.Subject = "Uitnodiging Gesprek bij AlienTrick ";
                mailMessage.Body = "Beste " + gebruiker + ",\n\n" +
                        "Hartelijk dank voor jou sollicitatie bij AlienTrick. We zijn erg benieuwd naar jou en willen je graag uitnodigen voor een gesprek! \n" +
                        "Om een gesprek in te plannen neem contact op met het onderstaande nummer! \n\n" +
                        "" +
                        " Telefoonnummer: 074 - 762 02 00\n" +
                        "\n" +
                        "\n" +
                        "Wij hopen snel van je te mogen horen!.\n\n\n" +
                        "" +
                        "" +
                        "Met vriendelijke groet,\n\n" +
                        "" +
                        "" +
                        "AlienTrick";
            } else
            {
                    mailMessage.Subject = "Stage sollicitatie bij AlienTrick ";
                    mailMessage.Body = "Beste " + gebruiker + ",\n\n" +
                        "Hartelijk dank voor jou sollicitatie bij AlienTrick. Tot onze spijt pas je niet bij een van onze vacatures! \n" +
                        "Wij wensen je veel succes in het vervolg van het zoeken naar een stageplaats! \n\n" +

                        "Met vriendelijke groet,\n\n" +
                        "" +
                        "" +
                        "AlienTrick";
            }

            client.Send(mailMessage);
        }
        [Rights(AllowAdmins = true)]
        public ActionResult SendInvitation(int? id, int? type)
        {

            if (type == 1)
            {
                SentInvitation(true, id);
                JobApplication jobApplication = db.JobApplications.Find(id);
                jobApplication.ApplyAnswered = 1;
                db.Entry(jobApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");

            }
            else
            {
                SentInvitation(false, id);
                JobApplication jobApplication = db.JobApplications.Find(id);
                db.JobApplications.Remove(jobApplication);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
        
        }
    }

