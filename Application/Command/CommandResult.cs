namespace Application
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public CommandResult(bool success, string? message, object? content)
        {
            Success = success;
            Message = message;
            Content = content;
        }

        public CommandResult(System.Collections.Generic.IReadOnlyCollection<CommandResult> notifications, object content)
        {
            Success = notifications.Count == 0;
            Message = string.Join(",", notifications.Select(item => item.Message).ToArray());
            Content = content;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Content { get; set; }
    }
}
