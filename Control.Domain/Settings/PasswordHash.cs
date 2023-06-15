namespace Control.Domain.Settings
{
    public class PasswordHash
    {
        public string Hash { get; set; }
        public byte[] Sal { get; set; }
    }
}