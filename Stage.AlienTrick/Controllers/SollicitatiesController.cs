using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Stage.AlienTrick.Controllers
{
    public class SollicitatiesController : Controller
    {
        PortalEntities db = new PortalEntities();
        // GET: Sollicitaties
        public ActionResult Index(string searchString)
        {
            var jobsCollection = db.JobApplications.AsQueryable();

           if (!string.IsNullOrWhiteSpace(searchString))
            {
                jobsCollection = jobsCollection.Where(A => A.CandidateLastName.ToLower().Contains(searchString.ToLower()));
            }

            return View(jobsCollection.ToArray());
        }



        public void Sendinvitation(int? id, int? type)
        {

            if (type == 1)
            {

                // verzend een email
                // string usermail = from i in db.JobApplications where i.ID == id select(i.CandidateMailadress).FirstOrDefault();

                string usermail = db.JobApplications.Where(i => i.ID == id).Select(d => d.CandidateMailadress).FirstOrDefault();


                try
                {
                    string gebruiker = db.JobApplications.Where(g => g.ID == id).Select(g => g.CandidateName).FirstOrDefault();



                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.EnableSsl = true;


                    //If you need to authenticate
                    client.Credentials = new NetworkCredential("alientrickstage@gmail.com", "Alientrick1234");
                    MailMessage mailMessage = new MailMessage();
                    MailAddress mailAddress = new MailAddress("noreply@kampementkunja.nl");
                    mailMessage.From = mailAddress;
                    mailMessage.To.Add(usermail);
                    mailMessage.Subject = "Uitnodiging Gesprek bij AlienTrick ";
                    mailMessage.Body = "Beste " + gebruiker.FirstOrDefault() + ",\n\n" +
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


                    client.Send(mailMessage);




                }
                catch
                {

                }




            }
            if(type == 2)
            {
                string usermail = db.JobApplications.Where(i => i.ID == id).Select(d => d.CandidateMailadress).FirstOrDefault();


                try
                {
                    string gebruiker = db.JobApplications.Where(g => g.ID == id).Select(g => g.CandidateName).FirstOrDefault();



                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.EnableSsl = true;


                    //If you need to authenticate
                    client.Credentials = new NetworkCredential("alientrickstage@gmail.com", "Alientrick1234");
                    MailMessage mailMessage = new MailMessage();
                    MailAddress mailAddress = new MailAddress("noreply@kampementkunja.nl");
                    mailMessage.From = mailAddress;
                    mailMessage.To.Add(usermail);
                    mailMessage.Subject = "Uitnodiging Gesprek bij AlienTrick ";
                    mailMessage.Body = "Beste " + gebruiker.FirstOrDefault() + ",\n\n" +
                        "Hartelijk dank voor jou sollicitatie bij AlienTrick. Tot onze spijt pas je niet bij een van onze vacatures! \n" +
                        "Wij wensen je veel succes in het vervolg van het zoeken naar een stageplaats! \n\n" +

                        "Met vriendelijke groet,\n\n" +
                        "" +
                        "" +
                        "AlienTrick";


                    client.Send(mailMessage);




                }
                catch
                {

                }
            }
        }
        
        }
    }

