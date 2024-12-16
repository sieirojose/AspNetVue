using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetVueApp.Domain.Interfaces;

namespace AspNetVueApp.Infrastructure.Jobs
{
    public class UserJob
    {
        private readonly IUserRepository _userRepository;

        public UserJob(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ProcessUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                Console.WriteLine($"Users Count: {users.Count()}");

                if (!users.Any())
                {
                    Console.WriteLine("No users to process.");
                    return;
                }
                else 
                { 
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Processing user: {user.FirstName} {user.LastName}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}