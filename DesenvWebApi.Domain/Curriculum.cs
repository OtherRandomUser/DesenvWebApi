using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DesenvWebApi.Domain
{
    public class Curriculum : Entity
    {
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public ICollection<SubjectCurriculum> Subjects { get; protected set; }

        private Curriculum()
        {
        }

        public Curriculum(string code, string name)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException("Curriculum code can't be null");

            Code = code;

            SetName(name);

            Subjects = new Collection<SubjectCurriculum>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Curriculum name can't be null");

            Name = name;
        }
    }
}