namespace Domain.Errors
{
    public sealed record  Error
    {
        public string Message { get; }
       
        public string Code { get; }
        
        internal Error(string message,string code)
        {
            Message = message;
            Code = code;
        }

        public static  Error None = new (string.Empty, string.Empty);

        public static Error Presistance = new("Failed to save changes to database", "Presistance.Failure");

        public static Error Validation(string message) => new(message, "Validation");
    
    }
}
