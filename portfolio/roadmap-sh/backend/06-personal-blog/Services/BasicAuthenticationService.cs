using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Services;

public class BasicAuthenticationService
{
    private static List<UserData> _users;
    static BasicAuthenticationService()
    {
        _users = 
        [
            new UserData
            {
                Username = "admin",
                Email = "admin@example.com",
                Password = "admin1234",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"admin"),
                    new Claim("IsAdmin","true")
                }
            },
            new UserData
            {
                Username = "user",
                Email = "user@example.com", 
                Password = "user1234",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "user")
                }
            }
        ];
    }

    public static UserData AuthenticateUser(UserLogin user)
    {
        return _users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
    }
}