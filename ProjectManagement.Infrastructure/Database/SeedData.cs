using MongoDB.Driver;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enum;

namespace ProjectManagement.Infrastructure.Database
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IMongoDatabase database)
        {
            var users = database.GetCollection<User>("Users");
            var projects = database.GetCollection<Project>("Projects");
            var tasks = database.GetCollection<TaskItem>("Tasks");

            // Seed Users
            if (!await users.Find(_ => true).AnyAsync())
            {
                var defaultUsers = new List<User>
                {
                    new User { FullName = "Chandan", Email = "chandan@gmail.com", PasswordHash = "Chandan@123", Role = Domain.Enum.Role.Admin, CreatedAt = DateTime.UtcNow },            
                };

                await users.InsertManyAsync(defaultUsers);
                Console.WriteLine("seeded 20 default users successfully.");
            }

            // Seed Projects
            if (!await projects.Find(_ => true).AnyAsync())
            {
                var sampleProject = new Project
                {
                    Name = "Project Management Dashboard",
                    Description = "Internal dashboard for managing projects, tasks, and teams.",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(2),
                    Status = "In Progress",
                    TeamMembers = new List<string> { "Chandan", "Nandan", "Guruji", "Asha", "Ravi" }
                };

                await projects.InsertOneAsync(sampleProject);
                Console.WriteLine("Default project seeded.");
            }

            // Seed Tasks
            if (!await tasks.Find(_ => true).AnyAsync())
            {
                var project = await projects.Find(_ => true).FirstOrDefaultAsync();
                if (project != null)
                {
                    var sampleTasks = new List<TaskItem>
                    {
                        new TaskItem
                        {
                            ProjectId = project.Id!,
                            Title = "Setup MongoDB Connection",
                            Description = "Configure MongoDB and create collections.",
                            AssignedTo = "Ravi",
                            CreatedAt = DateTime.UtcNow,
                            DueDate = DateTime.UtcNow.AddDays(5),
                            Status = "ToDo",
                            Priority = 1
                        },
                        new TaskItem
                        {
                            ProjectId = project.Id!,
                            Title = "Implement JWT Authentication",
                            Description = "Setup secure authentication using JWT tokens.",
                            AssignedTo = "Nandan",
                            CreatedAt = DateTime.UtcNow,
                            DueDate = DateTime.UtcNow.AddDays(7),
                            Status = "InProgress",
                            Priority = 2
                        },
                        new TaskItem
                        {
                            ProjectId = project.Id!,
                            Title = "Integrate Angular Frontend",
                            Description = "Connect Angular UI to backend APIs.",
                            AssignedTo = "Guruji",
                            CreatedAt = DateTime.UtcNow,
                            DueDate = DateTime.UtcNow.AddDays(10),
                            Status = "ToDo",
                            Priority = 3
                        }
                    };

                    await tasks.InsertManyAsync(sampleTasks);
                    Console.WriteLine(" Default tasks seeded.");
                }
            }
        }
    }
}
