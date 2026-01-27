using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class identity_users
    {
        public identity_users()
        {
            chat_identity_users_timestamp = new HashSet<chat_identity_users_timestamp>();
            chat_messages = new HashSet<chat_messages>();
            chat_room_participants = new HashSet<chat_room_participants>();
            identity_images = new HashSet<identity_images>();
            identity_users_rol = new HashSet<identity_users_rol>();
            staff = new HashSet<staff>();
            tasks_state_history = new HashSet<tasks_state_history>();
            time_tracking_auto = new HashSet<time_tracking_auto>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Face { get; set; }
        public long IdfImg { get; set; }
        public string IdOneSignal { get; set; }
        public string IdOneSignalWeb { get; set; }
        public int GeoTrackingEvery { get; set; }
        public string FaceStamp { get; set; }
        public string TFASecret { get; set; }

        public virtual ICollection<chat_identity_users_timestamp> chat_identity_users_timestamp { get; set; }
        public virtual ICollection<chat_messages> chat_messages { get; set; }
        public virtual ICollection<chat_room_participants> chat_room_participants { get; set; }
        public virtual ICollection<identity_images> identity_images { get; set; }
        public virtual ICollection<identity_users_rol> identity_users_rol { get; set; }
        public virtual ICollection<staff> staff { get; set; }
        public virtual ICollection<tasks_state_history> tasks_state_history { get; set; }
        public virtual ICollection<time_tracking_auto> time_tracking_auto { get; set; }
    }
}
