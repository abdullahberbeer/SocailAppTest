using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerAppTwo.Models;

namespace ServerAppTwo.Data
{
    public class SocialAppContext:IdentityDbContext<User,Role,int>
    {
        public SocialAppContext(DbContextOptions<SocialAppContext> options):base(options)
        {
            
        }
       public DbSet<Image> Images { get; set; }
       public DbSet<Message> Messages { get; set; }
       public DbSet<UserToUser> UserToUser { get; set; }
       protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.Entity<UserToUser>().HasKey(x=> new {x.UserId,x.FollowerId});
            builder.Entity<UserToUser>().HasOne(x=>x.User).WithMany(x=>x.Followers).HasForeignKey(x=>x.UserId);
            builder.Entity<UserToUser>().HasOne(x=>x.Follewer).WithMany(x=>x.Followings).HasForeignKey(x=>x.FollowerId);
            builder.Entity<Message>().HasOne(x=>x.Sender).WithMany(x=>x.MessagesSents).HasForeignKey(x=>x.SenderId);
            builder.Entity<Message>().HasOne(x=>x.Recipient).WithMany(x=>x.MessagesRecipient).HasForeignKey(x=>x.RecipientId);
       }
      
    }
}