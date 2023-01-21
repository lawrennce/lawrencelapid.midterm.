namespace lawrencelapid.Midterm_.Infrastructure.Domain.Models
{
    public class Researchlogin
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Type { get; set; } // Qualititative, Quantitative
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

}
