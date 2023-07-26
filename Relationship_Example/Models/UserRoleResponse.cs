namespace Relationship_Example.Models
{
    public class UserRoleResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public UserResponse User { get; set; }
        public RoleResponse Role { get; set; }
    }
}
