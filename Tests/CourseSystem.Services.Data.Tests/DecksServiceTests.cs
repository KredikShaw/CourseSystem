namespace CourseSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Data;
    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Data.Repositories;
    using CourseSystem.Services.Mapping;
    using CourseSystem.Web.ViewModels.Decks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class DecksServiceTests
    {
        [Fact]
        public async Task CreateDeckTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var decksRepository = new EfDeletableEntityRepository<Deck>(new ApplicationDbContext(options.Options));

            var mockCardsRepository = new Mock<IDeletableEntityRepository<Card>>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(decksRepository, mockCardsRepository.Object, mockUserManager.Object);

            await service.CreateDeck("Deck", "Pubilc", "User1", "NoThumbnail");
            await service.CreateDeck("Deck2", "Private", "User2", "NoThumbnail");

            var decks = decksRepository.All();

            Assert.Equal(2, decks.Count());
            Assert.True(decks.FirstOrDefault(x => x.Name == "Deck").IsPublic);
        }

        [Fact]
        public async Task GetDecksTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var decksRepository = new EfDeletableEntityRepository<Deck>(new ApplicationDbContext(options.Options));

            foreach (var deck in this.GetDecksData())
            {
                await decksRepository.AddAsync(deck);
                await decksRepository.SaveChangesAsync();
            }

            var mockCardsRepository = new Mock<IDeletableEntityRepository<Card>>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(decksRepository, mockCardsRepository.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(DeckViewModel).Assembly);

            var decks = service.GetDecks<DeckViewModel>("User1");

            Assert.Equal(3, decks.Count());
        }

        [Fact]
        public async Task DeleteDeckTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var decksRepository = new EfDeletableEntityRepository<Deck>(new ApplicationDbContext(options.Options));

            foreach (var deck in this.GetDecksData())
            {
                await decksRepository.AddAsync(deck);
                await decksRepository.SaveChangesAsync();
            }

            var mockCardsRepository = new Mock<IDeletableEntityRepository<Card>>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(decksRepository, mockCardsRepository.Object, mockUserManager.Object);
            await service.DeleteDeck("1", "User1");
            await service.DeleteDeck("2", "User2");

            var decks = decksRepository.All();

            Assert.Equal(2, decks.Count());
        }

        [Fact]
        public async Task CreateCardTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var cardsRepository = new EfDeletableEntityRepository<Card>(new ApplicationDbContext(options.Options));

            var mockDecksRepository = new Mock<IDeletableEntityRepository<Deck>>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(mockDecksRepository.Object, cardsRepository, mockUserManager.Object);
            await service.CreateCard("Front", "Back", "1");
            await service.CreateCard("Front2", "Back2", "1");
            await service.CreateCard("Front3", "Back3", "3");

            var cards = cardsRepository.All();

            Assert.Equal(3, cards.Count());
        }

        [Fact]
        public async Task GetCardsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var cardsRepository = new EfDeletableEntityRepository<Card>(new ApplicationDbContext(options.Options));

            foreach (var card in this.GetCardsData())
            {
                await cardsRepository.AddAsync(card);
                await cardsRepository.SaveChangesAsync();
            }

            var mockDecksRepository = new Mock<IDeletableEntityRepository<Deck>>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(mockDecksRepository.Object, cardsRepository, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(CardViewModel).Assembly);

            var cards = service.GetCards<CardViewModel>("deck1");
            Assert.Single(cards);
        }

        [Fact]
        public async Task GetCardTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var cardsRepository = new EfDeletableEntityRepository<Card>(new ApplicationDbContext(options.Options));

            foreach (var item in this.GetCardsData())
            {
                await cardsRepository.AddAsync(item);
                await cardsRepository.SaveChangesAsync();
            }

            var mockDecksRepository = new Mock<IDeletableEntityRepository<Deck>>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(mockDecksRepository.Object, cardsRepository, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(CardViewModel).Assembly);

            var card = service.GetCard<CardViewModel>("1");
            Assert.Equal("front1", card.FrontSide);
            Assert.Equal("back1", card.BackSide);
            Assert.Equal("deck1", card.DeckId);
        }

        [Fact]
        public async Task DeleteCardTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var cardsRepository = new EfDeletableEntityRepository<Card>(new ApplicationDbContext(options.Options));

            foreach (var item in this.GetCardsData())
            {
                await cardsRepository.AddAsync(item);
                await cardsRepository.SaveChangesAsync();
            }

            var decksRepository = new EfDeletableEntityRepository<Deck>(new ApplicationDbContext(options.Options));
            foreach (var deck in this.GetDecksData())
            {
                await decksRepository.AddAsync(deck);
                await decksRepository.SaveChangesAsync();
            }

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(decksRepository, cardsRepository, mockUserManager.Object);

            await service.DeleteCard("2", "User1");
            await service.DeleteCard("3", "User1");

            var cards = cardsRepository.All();
            Assert.Equal(3, cards.Count());
        }

        [Fact]
        public async Task UpdateCardTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var cardsRepository = new EfDeletableEntityRepository<Card>(new ApplicationDbContext(options.Options));

            foreach (var item in this.GetCardsData())
            {
                await cardsRepository.AddAsync(item);
                await cardsRepository.SaveChangesAsync();
            }

            var decksRepository = new EfDeletableEntityRepository<Deck>(new ApplicationDbContext(options.Options));
            foreach (var deck in this.GetDecksData())
            {
                await decksRepository.AddAsync(deck);
                await decksRepository.SaveChangesAsync();
            }

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new DecksService(decksRepository, cardsRepository, mockUserManager.Object);

            await service.UpdateCard("NEWFRONT", "NEWBACK", "2", "User1");
            await service.UpdateCard("NEWFRONT2", "NEWBACK2", "3", "User1");

            var card1 = cardsRepository.All().FirstOrDefault(x => x.Id == "2");
            var card2 = cardsRepository.All().FirstOrDefault(x => x.Id == "3");

            Assert.Equal("NEWFRONT", card1.FrontSide);
            Assert.Equal("NEWBACK", card1.BackSide);

            Assert.Equal("front3", card2.FrontSide);
            Assert.Equal("back3", card2.BackSide);
        }

        private IQueryable<Deck> GetDecksData()
        {
            return new List<Deck>
            {
                new Deck
                {
                    Id = "1",
                    IsPublic = true,
                    Name = "Deck1",
                    Thumbnail = "Image",
                    UserId = "User1",
                },
                new Deck
                {
                    Id = "2",
                    IsPublic = false,
                    Name = "Deck2",
                    Thumbnail = "asdfasdf",
                    UserId = "User1",
                },
                new Deck
                {
                    Id = "3",
                    IsPublic = true,
                    Name = "Deck3",
                    Thumbnail = "NoImg",
                    UserId = "User2",
                },
            }.AsQueryable();
        }

        private IQueryable<Card> GetCardsData()
        {
            return new List<Card>
            {
               new Card
               {
                   Id = "1",
                   FrontSide = "front1",
                   BackSide = "back1",
                   DeckId = "deck1",
               },
               new Card
               {
                   Id = "2",
                   FrontSide = "front2",
                   BackSide = "back2",
                   DeckId = "2",
               },
               new Card
               {
                   Id = "3",
                   FrontSide = "front3",
                   BackSide = "back3",
                   DeckId = "3",
               },
               new Card
               {
                   Id = "4",
                   FrontSide = "front4",
                   BackSide = "back4",
                   DeckId = "deck4",
               },
            }.AsQueryable();
        }
    }
}
