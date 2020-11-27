using System;

namespace DesenvWebApi.Domain
{
    public class SubjectCurriculum : Entity
    {
        public Guid SubjectId { get; protected set; }
        public Subject Subject { get; protected set; }

        public Guid CurriculumId { get; protected set; }
        public Curriculum Curriculum { get; protected set; }

        private SubjectCurriculum()
        {
        }

        public SubjectCurriculum(Subject subject, Curriculum curriculum)
        {
            SubjectId = subject?.Id
                ?? throw new ArgumentNullException("Subject can't be null");
            Subject = subject;

            CurriculumId = curriculum?.Id
                ?? throw new ArgumentNullException("Curriculum can't be null");
            Curriculum = curriculum;
        }
    }
}