namespace SteletusApplicationPresentation.DataTransferObjects
{
    public class Build
    {
        public Guid? Id { get; set; }
        public string? Version { get; set; }
        public string? Link { get; set; }
        public List<string>? Clients { get; set; }
    }
}
