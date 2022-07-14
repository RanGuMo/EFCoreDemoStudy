using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDemo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Api.Factorys
{
    public class DbContextFactory
    {
        private static MyBBSContext _dbContext = null;
        private DbContextFactory()
        {
            
        }
       public static MyBBSContext GetMyBBSContext(){
           if(_dbContext==null){
               _dbContext = new MyBBSContext();
               _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
           }
           return _dbContext;
       }


    }
}