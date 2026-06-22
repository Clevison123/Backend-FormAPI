namespace FormAPI.DTOs
{
    public class ResponseFormPersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
