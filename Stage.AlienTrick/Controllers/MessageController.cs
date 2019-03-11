using Stage.AlienTrick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage.AlienTrick.Controllers
{
    public class MessageController : Controller
    {
        private PortalEntities db = new PortalEntities();
        // GET: Message
        public ActionResult Index(Messagemodel messagemodel, string searchString)
        {
            var messages = db.Messages.ToList();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                messages = messages.Where(m => m.SenderName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(messages);
        }
        public ActionResult Create()
        {
            return View();
        }
        //create
        public ActionResult CreateMessage(Messagemodel messagemodel)
        {
            if (ModelState.IsValid)
            {

                if (messagemodel.Message == null)
                    throw new Exception("Heb je niks gemaakt?");


                var CreateMessage = new Message();
                CreateMessage.ReceiverName = messagemodel.ReceiverName;
                CreateMessage.SenderName = messagemodel.SenderName;
                CreateMessage.MessageText = messagemodel.MessageText;
                CreateMessage.TimeSent = DateTime.Now;
                db.Messages.Add(CreateMessage);
                db.SaveChanges();
                return View("index");
            }
            return View("index");
        }
    }
}
