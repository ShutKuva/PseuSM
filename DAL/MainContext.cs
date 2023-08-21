using Bogus;
using Core;
using Core.Enums;
using DAL.Entities;
using DAL.Entities.Cloudinary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tools.Abstractions;

namespace DAL
{
    public class MainContext : DbContext
    {
        private readonly IHasher _hasher;
        private readonly ITimeService _timeService;

        public MainContext(
            IHasher hasher,
            ITimeService timeService,
            DbContextOptions<MainContext> options) : base(options)
        {
            _hasher = hasher;
            _timeService = timeService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImagePlaceholder> ImagePlaceholders { get; set; }
        public DbSet<CloudinaryEntity> CloudinaryImages { get; set; }
        public DbSet<FollowList> FollowLists { get; set; }
        public DbSet<FriendList> FriendLists { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasOne(user => user.FollowList)
                .WithOne(followList => followList.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<User>()
                .HasOne(user => user.FriendList)
                .WithOne(friendList => friendList.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<FollowList>()
                .HasMany(followList => followList.FollowUsers)
                .WithOne(followUser => followUser.FollowList)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<FriendList>()
                .HasMany(friendList => friendList.FriendUsers)
                .WithOne(friendUser => friendUser.FriendList)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ImagePlaceholder>()
                .HasOne(imagePlaceholder => imagePlaceholder.Image)
                .WithOne(image => image.ImagePlaceholder)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<FollowUser>()
                .HasOne(followUser => followUser.User)
                .WithMany(user => user.FollowUsers)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<FriendUser>()
                .HasOne(friendUser => friendUser.User)
                .WithMany(user => user.FriendUsers)
                .OnDelete(DeleteBehavior.NoAction);

            //Fake data
            var configurationsForFakeData = new ConfigurationsForFakeData()
            {
                CloudianryImageUrl = "https://res.cloudinary.com/dxj2p22uz/image/upload/",
                CloudianryVideoUrl = "https://res.cloudinary.com/dxj2p22uz/video/upload/",

                AvatarPublicIds = new List<string>
                {
                    "9ef5bab003c591231fee210d0af6072d.675x675x1_sb41tb",
                    "brent-faiyaz-pretty-boy-flow_elq6xi",
                    "prettyboy-dra-1_fkh4l8",
                    "images_1_hkq9hu",
                    "images_re64ko",
                    "rob_lowe_mxbg4x",
                    "1c299c93367f9476b857f73529db61c9_h9zuik",
                },
                BackgroundPublicIds = new List<string>
                {
                    "at_art_design_2021-02_Greendigs-plants_zoom_backgrounds_zoombackground-2_t9vmlv",
                    "pexels-eberhard-grossgasteiger-1287142_utjp4n",
                    "sa_pixar_virtualbg_coco_16x9_9ccd7110_a8o1t1",
                    "aesthetic-cloud-background-05bsl7m2e5bh01ki_wbjssy",
                    "wallpaper2you_57798_cfongt",
                    "photo-1432847712612-926caafaa802_wx7dmd",
                    "загружено_zx0eym",
                },
                ImagePublicIds = new List<string>
                {
                    "at_art_design_2021-02_Greendigs-plants_zoom_backgrounds_zoombackground-2_t9vmlv",
                    "pexels-eberhard-grossgasteiger-1287142_utjp4n",
                    "sa_pixar_virtualbg_coco_16x9_9ccd7110_a8o1t1",
                    "aesthetic-cloud-background-05bsl7m2e5bh01ki_wbjssy",
                    "wallpaper2you_57798_cfongt",
                    "photo-1432847712612-926caafaa802_wx7dmd",
                    "загружено_zx0eym",
                    "9ef5bab003c591231fee210d0af6072d.675x675x1_sb41tb",
                    "brent-faiyaz-pretty-boy-flow_elq6xi",
                    "prettyboy-dra-1_fkh4l8",
                    "images_1_hkq9hu",
                    "images_re64ko",
                    "rob_lowe_mxbg4x",
                    "1c299c93367f9476b857f73529db61c9_h9zuik",
                },
                VideoPublicIds = new List<string>
                {
                    "Among_Us_Drip_Theme_Song_Original_Among_Us_Trap_Remix___Amogus_Meme_Music_dcu7mg",
                },
                Password = "string"
            };

            int cloudinaryEntityId = 0;
            var avatarCloudinaryEntities = new List<CloudinaryEntity>();
            foreach (string publicId in configurationsForFakeData.AvatarPublicIds)
            {
                avatarCloudinaryEntities.Add(new CloudinaryEntity()
                {
                    Id = ++cloudinaryEntityId,
                    PublicId = publicId,
                });
            }
            var backgroundCloudinaryEntities = new List<CloudinaryEntity>();
            foreach (string publicId in configurationsForFakeData.BackgroundPublicIds)
            {
                backgroundCloudinaryEntities.Add(new CloudinaryEntity()
                {
                    Id = ++cloudinaryEntityId,
                    PublicId = publicId,
                });
            }
            var imageCloudinaryEntities = new List<CloudinaryEntity>();
            foreach (string publicId in configurationsForFakeData.ImagePublicIds)
            {
                imageCloudinaryEntities.Add(new CloudinaryEntity()
                {
                    Id = ++cloudinaryEntityId,
                    PublicId = publicId,
                });
            }
            var videoCloudinaryEntities = new List<CloudinaryEntity>();
            foreach (string publicId in configurationsForFakeData.VideoPublicIds)
            {
                videoCloudinaryEntities.Add(new CloudinaryEntity()
                {
                    Id = ++cloudinaryEntityId,
                    PublicId = publicId,
                });
            }

            int imageId = 0;
            var imageFaker = new Faker<Image>()
                .RuleFor(i => i.Id, () => ++imageId);
            var images = new List<Image>();

            int videoId = 0;
            var videoFaker = new Faker<Video>()
                .RuleFor(v => v.Id, () => ++videoId)
                .RuleFor(v => v.Name, f => f.Lorem.Word());
            var videos = new List<Video>();

            int followListsId = 0;
            var followListFaker = new Faker<FollowList>()
                .RuleFor(fl => fl.Id, () => ++followListsId);
            var followLists = new List<FollowList>();

            int friendListsId = 0;
            var friendListFaker = new Faker<FriendList>()
                .RuleFor(fl => fl.Id, () => ++friendListsId);
            var friendLists = new List<FriendList>();

            int postId = 0;
            var postFaker = new Faker<Post>()
                .RuleFor(p => p.Id, () => ++postId)
                .RuleFor(p => p.CreationDate, () => _timeService.GetCurrentDateTime().GetAwaiter().GetResult())
                .RuleFor(p => p.UpdateDate, () => _timeService.GetCurrentDateTime().GetAwaiter().GetResult())
                .RuleFor(p => p.Description, f => f.Lorem.Text())
                .RuleFor(p => p.Images, (f, p) =>
                {
                    imageFaker = imageFaker
                        .RuleFor(i => i.PostId, () => p.Id)
                        .RuleFor(i => i.CloudinaryEntityId, f => f.Random.CollectionItem(imageCloudinaryEntities).Id)
                        .RuleFor(i => i.Reference, (f, i) => configurationsForFakeData.CloudianryImageUrl + imageCloudinaryEntities.First(ci => ci.Id == i.CloudinaryEntityId).PublicId)
                        .RuleFor(i => i.Type, () => ImageTypes.Regular);
                    
                    images.AddRange(imageFaker.GenerateBetween(0, 10));
                    return new List<Image>();
                })
                .RuleFor(p => p.Videos, (f, p) =>
                {
                    videoFaker = videoFaker
                        .RuleFor(v => v.PostId, () => p.Id)
                        .RuleFor(v => v.CloudinaryEntityId, f => f.Random.CollectionItem(videoCloudinaryEntities).Id)
                        .RuleFor(i => i.Reference, (f, i) => configurationsForFakeData.CloudianryVideoUrl + videoCloudinaryEntities.First(ci => ci.Id == i.CloudinaryEntityId).PublicId);

                    videos.AddRange(videoFaker.GenerateBetween(0, 10));
                    return new List<Video>();
                });
            var posts = new List<Post>();

            int userId = 0;
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, () => ++userId)
                .RuleFor(u => u.Status, f => f.Random.Enum<UserStatuses>())
                .RuleFor(u => u.Description, f => f.Lorem.Text())
                .RuleFor(u => u.Login, f => f.Internet.UserName())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Password, f => _hasher.Hash(configurationsForFakeData.Password))
                .RuleFor(u => u.Images, (f, u) =>
                {
                    imageFaker = imageFaker
                        .RuleFor(i => i.UserId, () => u.Id)
                        .RuleFor(i => i.CloudinaryEntityId, f => f.Random.CollectionItem(avatarCloudinaryEntities).Id)
                        .RuleFor(i => i.Reference, (f, i) => configurationsForFakeData.CloudianryImageUrl + avatarCloudinaryEntities.First(ci => ci.Id == i.CloudinaryEntityId).PublicId)
                        .RuleFor(i => i.Type, () => ImageTypes.Avatar);

                    images.AddRange(imageFaker.GenerateBetween(1, 10));

                    imageFaker = imageFaker.RuleFor(i => i.Type, () => ImageTypes.Background);

                    images.AddRange(imageFaker.GenerateBetween(0, 10));

                    return new List<Image>();
                })
                .RuleFor(u => u.Posts, (f, u) =>
                {
                    postFaker = postFaker
                        .RuleFor(p => p.UserId, () => u.Id);

                    posts.AddRange(postFaker.GenerateBetween(0, 10));

                    return new List<Post>();
                })
                .RuleFor(u => u.FollowList, (f, u) =>
                {
                    followListFaker = followListFaker
                        .RuleFor(fl => fl.UserId, () => u.Id);

                    FollowList fl = followListFaker.Generate();

                    followLists.Add(fl);

                    return fl;
                })
                .RuleFor(u => u.FriendList, (f, u) =>
                {
                    friendListFaker = friendListFaker
                        .RuleFor(fl => fl.UserId, () => u.Id);

                    FriendList fl = friendListFaker.Generate();

                    friendLists.Add(fl);

                    return fl;
                });

            Console.WriteLine("Start generating users");
            var users = userFaker.Generate(10);
            Console.WriteLine("End generating users");

            int followUserId = 0;
            var followUserFaker = new Faker<FollowUser>()
                .RuleFor(fu => fu.Id, () => ++followUserId);
            var followUsers = new List<FollowUser>();

            int friendUserId = 0;
            var friendUsers = new List<FriendUser>();
            var friendUserFaker = new Faker<FriendUser>()
                .RuleFor(fu => fu.Id, () => ++friendUserId)
                .RuleFor(fu => fu.FriendListId, f => f.Random.CollectionItem(friendLists).Id)
                .RuleFor(fu => fu.UserId, (f, fu) =>
                {
                    User rUser;
                    do
                    {
                        rUser = f.Random.CollectionItem(users);
                    } while (rUser.FriendList.Id == fu.FriendListId);

                    User ownerOfFriendList = users.First(u => u.FriendList.Id == fu.FriendListId);

                    followUserFaker = followUserFaker
                        .RuleFor(fu => fu.UserId, () => rUser.Id)
                        .RuleFor(fu => fu.FollowListId, () => ownerOfFriendList.FollowList.Id);

                    followUsers.Add(followUserFaker.Generate());

                    followUserFaker = followUserFaker
                        .RuleFor(fu => fu.UserId, () => ownerOfFriendList.Id)
                        .RuleFor(fu => fu.FollowListId, () => rUser.FollowList.Id);

                    followUsers.Add(followUserFaker.Generate());

                    friendUsers.Add(new FriendUser
                    {
                        Id = ++friendUserId,
                        FriendListId = rUser.FriendList.Id,
                        UserId = ownerOfFriendList.Id
                    });

                    return rUser.Id;
                });
            Console.WriteLine("Start generating FriendUsers");
            friendUsers.AddRange(friendUserFaker.Generate(20));
            Console.WriteLine("End generating FriendUsers");

            users.ForEach(user =>
            {
                user.FriendList = null!;
                user.FollowList = null!;
            });

            Console.WriteLine("Adding to db Users");
            modelBuilder.Entity<User>().HasData(users);
            Console.WriteLine("Adding to db friedn lists");
            modelBuilder.Entity<FriendList>().HasData(friendLists);
            Console.WriteLine("Adding to db follow lists");
            modelBuilder.Entity<FollowList>().HasData(followLists);
            Console.WriteLine("Adding to db cloudinary entities");
            modelBuilder.Entity<CloudinaryEntity>().HasData(avatarCloudinaryEntities);
            modelBuilder.Entity<CloudinaryEntity>().HasData(backgroundCloudinaryEntities);
            modelBuilder.Entity<CloudinaryEntity>().HasData(imageCloudinaryEntities);
            modelBuilder.Entity<CloudinaryEntity>().HasData(videoCloudinaryEntities);
            Console.WriteLine("Adding to db images");
            modelBuilder.Entity<Image>().HasData(images);
            Console.WriteLine("Adding to db videos");
            modelBuilder.Entity<Video>().HasData(videos);
            Console.WriteLine("Adding to db friend users");
            modelBuilder.Entity<FriendUser>().HasData(friendUsers);
            Console.WriteLine("Adding to db follow users");
            modelBuilder.Entity<FollowUser>().HasData(followUsers);

            Console.WriteLine("Ending");
        }
    }
}
