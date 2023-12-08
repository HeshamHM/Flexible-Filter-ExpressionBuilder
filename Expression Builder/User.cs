
namespace Expression_Builder
{
    public sealed class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Country { get; set; } = "";
        public bool IsActive { get; set; }=true;
    }
}
