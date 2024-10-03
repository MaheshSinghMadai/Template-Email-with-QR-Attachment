using System.Reflection;

namespace FluentEmailDemo.Entity
{
    public class AppointmentRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime AppointmentDateTime { get; set; }

        public AppointmentRequest(string name, string email, DateTime appointmentDateTime)
        {
            Name = name;
            Email = email;
            AppointmentDateTime = appointmentDateTime;       
        }
    }
}
