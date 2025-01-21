namespace Kopibara.Models
{
    public class Accounts
    {
        public int Id { get; set; }
        public string GoogleId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
