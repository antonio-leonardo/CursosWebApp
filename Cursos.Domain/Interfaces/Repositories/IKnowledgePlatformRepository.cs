using Cursos.Domain.Domain.Models;

namespace Cursos.Domain.Interfaces.Repositories
{
    public interface IKnowledgePlatformRepository
    {
        KnowledgePlatform Get(string name);
        void Register(KnowledgePlatform knowledgePlatform);
    }
}
