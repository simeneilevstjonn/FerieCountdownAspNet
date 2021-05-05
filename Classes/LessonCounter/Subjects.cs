using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.LessonCounter
{
    public static class Subjects
    {
        public enum Subject
        {
            Norwegian,
            SocialStudies,
            Mathematics,
            English,
            PhysicalEducation,
            Music,
            Language,
            ArtsAndCrafts,
            Science,
            Religion,
            Choice
        }

        /// <summary>
        /// Converts a enum subject value to a string in Norwegian.
        /// </summary>
        /// <param name="subject">The enum subject</param>
        /// <returns>The name of the subject in Norwegian</returns>
        public static string ToNbNoString(Subject subject) => subject switch
        {
            Subject.Norwegian => "Norsk",
            Subject.ArtsAndCrafts => "Kunst og Håndverk",
            Subject.English => "Engelsk",
            Subject.Language => "Fremmedspråk",
            Subject.Mathematics => "Matematikk",
            Subject.Music => "Musikk",
            Subject.PhysicalEducation => "Kroppsøving",
            Subject.Religion => "KRLE",
            Subject.Science => "Naturfag",
            Subject.SocialStudies => "Samfunnsfag",
            Subject.Choice => "Valgfag",
            _ => throw new ArgumentException()
        };
    }
}
