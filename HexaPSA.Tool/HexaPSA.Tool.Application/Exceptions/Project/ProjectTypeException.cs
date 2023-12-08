using HexaPSA.Tool.Domain.Exceptions;

namespace HexaPSA.Tool.Application.Exceptions.Project
{
    public sealed class ProjectTypeExistsException :Exception
    {
        public ProjectTypeExistsException(string name) : base($"{name} is already exist.") { }
    }

    public sealed class ProjectTypeNotFoundException : NotFoundException
    {
        public ProjectTypeNotFoundException(string name) : base($"{name} is already exist.") { }
    }
}
