﻿namespace FreshUp.Application.Helpers;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        return hash;
    }

    public static bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}