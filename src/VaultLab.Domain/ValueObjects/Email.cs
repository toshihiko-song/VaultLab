using System.Net.Mail;

namespace VaultLab.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email is required.", nameof(value));


            try
            {
                var address = new MailAddress(value);
                
                Value = address.Address.ToLowerInvariant();
            }
            catch(FormatException)
            {
                throw new ArgumentException("Invalid email address.", nameof(value));
            }
        }

        public override string ToString() => Value;
    }
}