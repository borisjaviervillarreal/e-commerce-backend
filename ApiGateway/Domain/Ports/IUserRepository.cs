﻿using ApiGateway.Domain.Entities;

namespace ApiGateway.Domain.Ports
{
    // Definimos las operaciones que manejaremos en el repositorio de usuarios
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email); // Buscar usuario por email
        Task AddUserAsync(User user); // Agregar un nuevo usuario
    }
}
