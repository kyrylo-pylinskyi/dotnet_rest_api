namespace My_Rest_API.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Mail  { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
