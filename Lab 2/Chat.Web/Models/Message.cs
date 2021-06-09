using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Web.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        public String Text { get; set; }
        public DateTime Timestamp { get; set; }
        public String Username { get; set; }

        public Message() { }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Timestamp.Date < DateTime.Today)
            {
                sb.Append(this.Timestamp.Date);
                sb.Append('/');
                sb.Append(this.Timestamp.Month + 1);
                sb.Append('/');
                sb.Append(this.Timestamp.Year);
                sb.Append(' ');
            }

            sb.Append(this.Timestamp.Hour);
            sb.Append(':');
            sb.Append(this.Timestamp.Minute);
            sb.Append(':');
            sb.Append(this.Timestamp.Second);
            return sb.ToString();
        }
    }
}