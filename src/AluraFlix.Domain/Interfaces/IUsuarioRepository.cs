﻿using AluraFlix.Domain.Entities;

namespace AluraFlix.Domain.Interfaces;
public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);
    Task<bool> ExistEmailAsync(string email);
}
