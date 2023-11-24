namespace Conferences.Api.DAL;
using Conferences.Api.Entities;

public class ConferenceContextSeed 
{
    public static async Task SeedAsync(ConferenceContext conferenceContext)
    {
        if(conferenceContext.Conferences.Any()) 
            await Task.CompletedTask;

        var users = new List<User>()
        {
            new User()
            {
                Id=1,
                Username="admin",
                Password="P@ssw0rd"
            },
            new User()
            {
                Id=2,
                Username="user",
                Password="P@ssw0rd"
            }
        };

        await conferenceContext.Users.AddRangeAsync(users);

        var conferences = new List<Conference>()
        {
            new Conference()
            {
                Id = 1,
                Name = "XP Days " + DateTime.Now.Year,
                Date = new DateTime(DateTime.Now.Year, 3, 14),
                Overview = "Overview for XP Days " + DateTime.Now.Year,
                Location = "Cairo",
                IsPublished = true,
                UserId= 2
            },
            new Conference()
            {
                Id = 2,
                Name = "Tech Conference " + DateTime.Now.Year,
                Date = new DateTime(DateTime.Now.Year, 5, 15),
                Overview = "Overview for Tech Conference " + DateTime.Now.Year,
                Location = "Riyadh",
                IsPublished = true,
                UserId= 2
            },
            new Conference()
            {
                Id = 3,
                Name = "Data Summit",
                Date = new DateTime(DateTime.Now.Year, 8, 20),
                Overview = "Overview for Data Summit",
                Location = "Cairo",
                IsPublished = false,
                UserId= 2
            },
            new Conference()
            {
                Id = 4,
                Name = "AI and Chat GPT",
                Date = new DateTime(DateTime.Now.Year, 2, 16),
                Overview = "Overview for AI and Chat GPT",
                Location = "Berlin",
                IsPublished = false,
                UserId= 2
            },
            new Conference()
            {
                Id = 5,
                Name = "The Machine Learning Conference",
                Date = new DateTime(DateTime.Now.Year, 1, 23),
                Overview = "Overview for The Machine Learning Conference",
                Location = "London",
                IsPublished = false,
                UserId= 2
            }
        };

        await conferenceContext.Conferences.AddRangeAsync(conferences);

        await conferenceContext.SaveChangesAsync();
        await Task.CompletedTask;
    }
}