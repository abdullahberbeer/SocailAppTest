using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerAppTwo.DTO;
using ServerAppTwo.Models;

namespace ServerAppTwo.Data
{
    public interface ISocialRepository
    {
     void Add<T>(T entity) where T:class;
     void Delete<T>(T entity) where T:class;
     Task<bool> SaveChanges();
     Task<User> GetUser(int id);
     Task<IEnumerable<User>> GetUsers(UserQueryParams userParams);
     Task<bool> IsAllReadyFollowed(int followerUserId,int userId);   
    }
}