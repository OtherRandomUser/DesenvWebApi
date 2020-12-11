using System;

namespace DesenvWebApi.Domain
{
    public class User : Entity
    {
        public string Email { get; protected set; }
        public string Name { get; protected set; }

        public Guid? CurriculumId { get; protected set; }
        public Curriculum Curriculum { get; protected set; }

        private User()
        {
        }

        public User(string email, string name)
        {
            //if (string.IsNullOrWhiteSpace(email))
                //throw new ArgumentNullException("Email can't be null");

            //Email = email;

            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name can't be null");

            Name = name;
        }

        public void SetCurriculum(Curriculum curriculum)
        {
            CurriculumId = curriculum?.Id;
            Curriculum = curriculum;
        }
    }
}