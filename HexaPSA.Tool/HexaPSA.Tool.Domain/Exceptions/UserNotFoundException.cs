namespace HexaPSA.Tool.Domain.Exceptions
{
    public sealed class ProjectTypeNotFoundException : NotFoundException
    {
        public ProjectTypeNotFoundException(Guid Id)
            : base($"The project type with the identifier {Id} was not found.")
        {
        }
    }
}
