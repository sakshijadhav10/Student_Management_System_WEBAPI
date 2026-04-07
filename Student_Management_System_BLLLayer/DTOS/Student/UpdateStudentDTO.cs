namespace Student_Management_System_BLL.DTOS.Student
{
    public class UpdateStudentDTO
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public int Age { get; set; }
        public string Course { get; set; } = "";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
