namespace Application.DTOs
{
    public class MessageDto
    {
        public string Type { get; set; }
        public List<string> Message { get; set; }

        public MessageDto()
        {
            Message = new List<string>();
        }

        public bool ExistsMessage => Message.Count > 0;

        public void AddMessage(string message)
        {
            Message.Add(message);
        }
    }
}