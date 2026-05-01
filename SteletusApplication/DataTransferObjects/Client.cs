namespace SteletusApplicationPresentation.DataTransferObjects
{
    public class Client
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public IEnumerable<Build>? Builds { get; set; }

        public string? CurrentChangeRequest { get; set; }

        public string? LastChangeRequest { get; set; }
    }
}
