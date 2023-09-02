namespace ShoppingTogether.API.Users.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sid { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
