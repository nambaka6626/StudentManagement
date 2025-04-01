namespace StudentManagement.Models
{
    public interface IUser
    {
        string Name { get; set; }
        string Role { get; }
        void Register();
    }
}
