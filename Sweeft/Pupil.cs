namespace Sweeft
{
    public class Pupil
    {
        public int PupilId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Grade { get; set; }

        public ICollection<TeacherPupil> TeacherPupils { get; set; }
    }
}
