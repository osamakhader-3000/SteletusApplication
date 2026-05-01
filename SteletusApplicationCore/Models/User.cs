namespace SteletusApplicationCore.Models
{    
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

    public enum UserRole
    {
        Employee = 1,
        TechnicalSupport = 2,
        Administrator = 3
    }

}
