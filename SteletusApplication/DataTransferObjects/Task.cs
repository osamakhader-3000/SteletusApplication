namespace SteletusApplicationPresentation.DataTransferObjects
{
    public class Task
    {
        public Guid? Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid? AssignedTo { get; set; }
        public Guid? AssignedBy { get; set; }
    }
}
