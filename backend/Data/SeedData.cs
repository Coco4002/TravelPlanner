using Microsoft.AspNetCore.Identity;
using TravelPlanner.API.Models;

namespace TravelPlanner.API.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            // Creare roluri
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Creare cont admin
            var adminEmail = "admin@travelplanner.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin TravelPlanner"
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed destinații
            if (!context.Destinations.Any())
            {
                var destinations = new List<Destination>
                {
                    new Destination
                    {
                        Name = "Paris",
                        Country = "Franța",
                        Description = "Orașul luminilor, capitala modei și a romantismului. Vizitează Turnul Eiffel, Luvru și Champs-Élysées.",
                        ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800",
                        Category = "City Break",
                        AveragePrice = 150,
                        Rating = 4.8
                    },
                    new Destination
                    {
                        Name = "Santorini",
                        Country = "Grecia",
                        Description = "Insulă vulcanică cu case albe și acoperișuri albastre, apusuri spectaculoase și plaje unice.",
                        ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800",
                        Category = "Beach",
                        AveragePrice = 120,
                        Rating = 4.9
                    },
                    new Destination
                    {
                        Name = "Tokyo",
                        Country = "Japonia",
                        Description = "O combinație fascinantă de tradiție și tehnologie ultramodernă. Temple antice lângă zgârie-nori futuriști.",
                        ImageUrl = "https://images.unsplash.com/photo-1540959733332-eab4deabeeaf?w=800",
                        Category = "City Break",
                        AveragePrice = 200,
                        Rating = 4.7
                    },
                    new Destination
                    {
                        Name = "Bali",
                        Country = "Indonezia",
                        Description = "Insula zeilor cu temple sacre, orezării în terase, plaje tropicale și o cultură vibrantă.",
                        ImageUrl = "https://images.unsplash.com/photo-1537996194471-e657df975ab4?w=800",
                        Category = "Beach",
                        AveragePrice = 80,
                        Rating = 4.6
                    },
                    new Destination
                    {
                        Name = "Roma",
                        Country = "Italia",
                        Description = "Orașul etern cu Colosseum, Vatican, Fontana di Trevi și cea mai bună mâncare italiană.",
                        ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800",
                        Category = "City Break",
                        AveragePrice = 130,
                        Rating = 4.8
                    },
                    new Destination
                    {
                        Name = "Interlaken",
                        Country = "Elveția",
                        Description = "Paradisul sporturilor de aventură înconjurat de Alpii elvețieni, lacuri cristale și peisaje de vis.",
                        ImageUrl = "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800",
                        Category = "Adventure",
                        AveragePrice = 250,
                        Rating = 4.7
                    },
                    new Destination
                    {
                        Name = "Barcelona",
                        Country = "Spania",
                        Description = "Arhitectura lui Gaudí, plaje mediteraneene, tapas delicioase și viață de noapte vibrantă.",
                        ImageUrl = "https://images.unsplash.com/photo-1583422409516-2895a77efded?w=800",
                        Category = "City Break",
                        AveragePrice = 110,
                        Rating = 4.7
                    },
                    new Destination
                    {
                        Name = "Machu Picchu",
                        Country = "Peru",
                        Description = "Cetatea pierdută a incașilor, una dintre cele 7 minuni ale lumii moderne, la 2430m altitudine.",
                        ImageUrl = "https://images.unsplash.com/photo-1587595431973-160d0d94add1?w=800",
                        Category = "Adventure",
                        AveragePrice = 100,
                        Rating = 4.9
                    },
                    new Destination
                    {
                        Name = "Maldive",
                        Country = "Maldive",
                        Description = "Vilele pe apă, plaje cu nisip alb, ape turcoaz cristale - destinația supremă pentru relaxare.",
                        ImageUrl = "https://images.unsplash.com/photo-1514282401047-d79a71a590e8?w=800",
                        Category = "Beach",
                        AveragePrice = 300,
                        Rating = 4.9
                    },
                    new Destination
                    {
                        Name = "Praga",
                        Country = "Cehia",
                        Description = "Orașul celor 100 de turnuri cu centrul medieval, Podul Carol și cea mai bună bere din lume.",
                        ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800",
                        Category = "City Break",
                        AveragePrice = 70,
                        Rating = 4.6
                    }
                };

                context.Destinations.AddRange(destinations);
                await context.SaveChangesAsync();
            }
        }
    }
}