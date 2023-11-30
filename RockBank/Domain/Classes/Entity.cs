using Flunt.Notifications;

namespace RockBank.Domain.Classes
{
    public class Entity : Notifiable<Notification>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

    }
}
