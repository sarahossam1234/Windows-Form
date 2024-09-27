using System.Collections.Generic;

public class MessageData
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public List<string> ToEmails { get; set; }
    public List<string> CcEmails { get; set; }

    public MessageData(string subject, string content, List<string> toEmails, List<string> ccEmails)
    {
        Subject = subject;
        Content = content;
        ToEmails = toEmails ?? new List<string>();
        CcEmails = ccEmails ?? new List<string>();
    }

    public MessageData() // Default constructor
    {
        ToEmails = new List<string>();
        CcEmails = new List<string>();
    }
}
