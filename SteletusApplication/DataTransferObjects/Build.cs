namespace SteletusApplicationPresentation.DataTransferObjects
{
    public class Build
    {
        public Guid? Id { get; set; }
        public string? Version { get; set; }
        public string? Link { get; set; }
        public List<Client>? Clients { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
