using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerAppTwo.DTO;
using ServerAppTwo.Models;

namespace ServerAppTwo.Data
{
    public class SocialRepository : ISocialRepository
    {
        private readonly SocialAppContext _context;
   
        public SocialRepository(SocialAppContext context)
        {
            _context=context;
            
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public  void Delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user= await _context.Users.Include(x=>x.Images).FirstOrDefaultAsync(x=>x.Id==id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(UserQueryParams userParams)
        {
           var users = _context.Users.Where(x=>x.Id!=userParams.UserId).Include(x=>x.Images).AsQueryable();
           
           if(userParams.Followers){
               var result= await GetFollows(userParams.UserId,false);
               users=users.Where(u=>result.Contains(u.Id));
           }
           if(userParams.Followings){
               var result = await GetFollows(userParams.UserId,true);
                users=users.Where(u=>result.Contains(u.Id));
           }
           if(!string.IsNullOrEmpty(userParams.Gender)){
               users=users.Where(i=>i.Gender==userParams.Gender);
           }
           if(userParams.minAge !=18||userParams.maxAge!=100){
               var today = DateTime.Now;
               var min = today.AddYears(-(userParams.maxAge+1));
               var max = today.AddYears(-(userParams.minAge));
               users= users.Where(i=>i.DateOfBirth>=min&&i.DateOfBirth<=max);
           }
           if(!string.IsNullOrEmpty(userParams.Country)){
               users = users.Where(c=>c.Country.ToLower()==userParams.Country.ToLower());
           }
           if(!string.IsNullOrEmpty(userParams.City)){
               users = users.Where(c=>c.City.ToLower()==userParams.City.ToLower());
           }
           if(!string.IsNullOrEmpty(userParams.OrderBy)){
               if(userParams.OrderBy=="age"){
                   users=users.OrderBy(x=>x.DateOfBirth);
               }
               else if(userParams.OrderBy=="created"){
                   users=users.OrderByDescending(x=>x.Created);
               }
           }

           return await users.ToListAsync(); 
        }

        private async Task<IEnumerable<int>> GetFollows(int userId, bool IsFollowings)
        {
            var user = await _context.Users.Include(i=>i.Followers).Include(i=>i.Followings).FirstOrDefaultAsync(i=>i.Id==userId);
            if(IsFollowings){
                return user.Followers.Where(i=>i.FollowerId==userId).Select(i=>i.UserId);
            }
           else{
               return user.Followings.Where(i=>i.UserId==userId).Select(i=>i.FollowerId);
           }
        }

        public async Task<bool> IsAllReadyFollowed(int followerUserId, int userId)
        {
            return await _context.UserToUser.AnyAsync(x=>x.FollowerId==followerUserId&&x.UserId==userId);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}