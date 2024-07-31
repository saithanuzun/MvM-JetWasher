
public class ContactForm
{
    public ContactForm(string name, string mobileNumber, string service, string comment)
    {
        Name = name;
        MobileNumber = mobileNumber;
        Service = service;
        Comment = comment;
    }

    public string Name { get; set; }
    public string MobileNumber { get; set; }
    public string Service { get; set; }
    public string Comment { get; set; }
}