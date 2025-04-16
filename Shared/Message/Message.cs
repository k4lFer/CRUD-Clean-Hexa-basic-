namespace Shared.Message
{
    public class Message
    {
        public string Type { get; set; }
        public List<string> ListMessage { get; set; }

        public Message()
        {
            ListMessage = new List<string>();
        }

        public bool ExistsMessage() => ListMessage.Count > 0;

        public void AddMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                ListMessage.Add(message);
            }
        }
        public bool IsSuccess() => Type == "success" || Type == "created" || Type == "updated" || Type == "deleted";
        public bool IsError() => Type == "error" || Type == "warning" || Type == "not-found" || Type == "exception" || Type == "timeout" || Type == "conflict";

        // Métodos de respuesta según la operación
        public void Success() => Type = "success";
        public void Created() => Type = "created";
        public void Updated() => Type = "updated";
        public void Deleted() => Type = "deleted";
        public void Warning() => Type = "warning";
        public void Error() => Type = "error";
        public void Conflict() => Type = "conflict";
        public void Exception() => Type = "exception";
        public void NotFound() => Type = "not-found";
        public void Timeout() => Type = "timeout";
    }
}
