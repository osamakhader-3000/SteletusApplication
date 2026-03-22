namespace SteletusApplicationCore.Models
{
    public class User(Guid id, string? username, string email, string? password,UserAccountStatus accountStatus, UserEmailVerificationStatus emailVerificationStatus,byte securityLevel,bool localAccount)
    {
        public Guid Id { get; private set; } = id;
        public string? Username { get; private set; } = username;
        public string Email { get; private set; } = email;
        public string? Password { get; private set; } = password;

        public UserAccountStatus AccountStatus { get; private set; } = accountStatus;

        public UserEmailVerificationStatus EmailVerificationStatus { get; private set; } = emailVerificationStatus;

        public byte SecurityLevel { get; private set; } = securityLevel;


        public bool LocalAccount { get; private set; } = localAccount;

    }


    public enum UserAccountStatus
    {   
        Temporary,
        Active,
        Suspended,
        Deleted
    }

    public enum UserEmailVerificationStatus
    {
        Unverified,
        Verified
    }


}
