using OshXona.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OshXona.IRepository
{
    internal interface IFoodRepository
    {
            User Create();
            User LogIn(string username, string password);
            void Search(string foodname);
            bool Delete(string foodnname);
            void PrintAll();
           public void Update(string LogIn);
            void AddFood(User food);
            byte[] HashingCode(User user);
            IList<User> GetAll();
        }
}