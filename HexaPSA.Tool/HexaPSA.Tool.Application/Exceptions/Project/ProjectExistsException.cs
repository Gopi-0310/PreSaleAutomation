namespace HexaPSA.Tool.Application.Exceptions.Project
{

    public sealed class ProjectExistsException : Exception
    {
        public ProjectExistsException(string name) : base($"{name} is already exist.") { }
    }
}
