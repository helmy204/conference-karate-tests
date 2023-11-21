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
                Name = "Tech Conference " + DateTime.Now.Year,
                Date = new DateTime(DateTime.Now.Year, 5, 15),
                Overview = "Overview for Tech Conference " + DateTime.Now.Year,
                Location = "Riyadh",
                UserId= 2
            },
            new Conference()
            {
                Id = 2,
                Name = "Data Summit",
                Date = new DateTime(DateTime.Now.Year, 8, 20),
                Overview = "Overview for Data Summit",
                Location = "Cairo",
                UserId= 2
            }
        };

        await conferenceContext.Conferences.AddRangeAsync(conferences);

        await conferenceContext.SaveChangesAsync();
        await Task.CompletedTask;
    }
}