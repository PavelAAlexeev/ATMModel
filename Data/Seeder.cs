using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ATMModel.Models;
using System;
using System.Linq;

namespace ATMModel.DataAccess.EF.DBSeeder
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ATMModelContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ATMModelContext>>()))
            {
                if (context.User.Any())
                {
                    return;
                }

                var user1 = new User
                    {
                        FirstName = "Pavel",
                        LastName = "Alexeev",
                    };
                var user2 = new User
                    {
                        FirstName = "Русская",
                        LastName = "Фамилия",
                    };
                context.User.AddRange(user1, user2);

                var card1 = new Card
                {
                    Balance = 1000.11M,
                    Blocked = false,
                    CardNumber = "1111111111111111",
                    CountOfWrongTry = 0,
                    HashedPin = "",
                    User = user1,
                };

                var card2 = new Card
                {
                    Balance = 100,
                    Blocked = true,
                    CardNumber = "1111111111111112",
                    CountOfWrongTry = 0,
                    HashedPin = "",
                    User = user1,
                };

                var card3 = new Card
                {
                    Balance = 0,
                    Blocked = false,
                    CardNumber = "1111111111111113",
                    CountOfWrongTry = 3,
                    HashedPin = "",
                    User = user1,
                };

                var card4 = new Card
                {
                    Balance = 0,
                    Blocked = false,
                    CardNumber = "1111111111111114",
                    CountOfWrongTry = 0,
                    HashedPin = "",
                    User = user2,
                };

                context.Card.AddRange(card1, card2, card3, card4);

                context.SaveChanges();
            }
        }
    }
}