using System.Collections.Generic;

namespace ReserveAqui.WebApi.Model
{
    public class MensagemWhatsappModel
    {
        public List<Contact> Contacts { get; set; }
        public List<Message> Messages { get; set; }

        public class Contact
        {
            public Profile Profile { get; set; }
            public string Wa_id { get; set; }
        }

        public class Profile
        {
            public string Name { get; set; }
        }

        public class Message
        {
            public string From { get; set; }
            public string Id { get; set; }
            public string Timestamp { get; set; }
            public MessageText Text { get; set; }
            public string Type { get; set; }
        }

        public class MessageText
        {
            public string Body { get; set; }
        }
    }
}
