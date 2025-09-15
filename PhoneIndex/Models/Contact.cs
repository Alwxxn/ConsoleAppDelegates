namespace PhoneIndex.Models
{
    public class Contact
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public Contact(string name, string phoneNumber, string email = "", string address = "")
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Phone: {PhoneNumber}, Email: {Email}, Address: {Address}";
        }
    }
}
