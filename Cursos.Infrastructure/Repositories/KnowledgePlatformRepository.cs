﻿using Cursos.CrossCutting;
using Cursos.Domain.Domain.Models;
using Cursos.Domain.Interfaces.Repositories;
using Cursos.Infrastructure.Context;

namespace Cursos.Infrastructure.Repositories
{
    public class KnowledgePlatformRepository : LoggingBase, IKnowledgePlatformRepository
    {
        private readonly DataContext _context;
        public KnowledgePlatformRepository(DataContext context)
        {
            _context = context;
        }
        public void Register(KnowledgePlatform knowledgePlatform)
        {
            Log.Info($"Persistencia de plataforma de ensino, Id: {knowledgePlatform.Id}");
            _context.KnowledgePlatforms.Add(knowledgePlatform);
        }

        public KnowledgePlatform Get(string name)
        {
            Log.Info($"Obtenção de plataforma de ensino pelo nome \"{name}\"");
            return _context.KnowledgePlatforms.Where(e => e.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower()).FirstOrDefault();
        }
    }
}