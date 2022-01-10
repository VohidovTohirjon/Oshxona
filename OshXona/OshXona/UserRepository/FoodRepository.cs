using OshXona.IRepository;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OshXona.Extension;
using OshXona.Models;
using OshXona.Service;

namespace OshXona.UserRepository
{
    public class FoodRepository : IFoodRepository
    {
        public User Create()
        {
            while (true)
            {
                try
                {
                    var user = new User();
                    
                    Console.WriteLine("Input Login: ");
                    user.Login = Console.ReadLine();    

                    Console.WriteLine("Input Password: ");
                    user.Password = Console.ReadLine();

                    Console.WriteLine("Succeded");
                    return user;
                }
                catch
                {
                    Console.WriteLine("Iltimos, qayta urinib ko'ring ");
                    continue;
                }
            }
        }

        public void AddFood(User food)
        {
            int scanner = 0;
            string json = File.ReadAllText(Constants.UserJsonPath);
            IList<User> Foods = JsonConvert.DeserializeObject<List<User>>(json);


            var lists = (Foods.Select(ovqat => new User()
            {
                FoodName = ovqat.FoodName, Count = ovqat.Count,
                Costage = ovqat.Costage, Location = ovqat.Location, Password = ovqat.Password
            })).ToList();

            foreach (var item in lists)
            {
                if (item.FoodName == food.FoodName)
                {
                    scanner = 1;
                }
            }

            if (scanner == 0)
            {
                Foods.Add(new User
                    { FoodName = food.FoodName, Count = food.Count, Costage = food.Costage, Location = food.Location });

                string res = JsonConvert.SerializeObject(Foods);
                File.WriteAllText(Constants.UserJsonPath, res);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Menyuga taom muvaffaqiyatli tarzda qo'shildi! ✅");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bunday taom menyuda mavjudligi aniqland ❗❓");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public byte[] HashingCode(User user)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(user.Password);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return tmpHash;
        }

        public bool Delete(string nameoffood)
        {
            bool result = false;

            try
            {
                string json = File.ReadAllText(Constants.UserJsonPath);
                IList<User> foodlar = JsonConvert.DeserializeObject<List<User>>(json);

                var Quantity = foodlar.Where(x => x.FoodName == nameoffood.Trim().ToLower().UpperCase()).ToList();
                if (Quantity.Count > 0)
                {
                    foreach (var item in Quantity)
                    {
                        foodlar.Remove(item);
                    }

                    string res = JsonConvert.SerializeObject(foodlar);
                    File.WriteAllText(Constants.UserJsonPath, res);


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ovqat menyudan muvaffaqiyatli tarzda o'chirildi ✅");
                    result = true;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bizning menyudan bunday ovqat topilmadi X");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return result;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Qaytadan urinib ko'rishingizni so'raymiz...");
                Console.ForegroundColor = ConsoleColor.White;
            }

            return result;
        }
        
        public IList<User> GetAll()
        {
            string json = File.ReadAllText(Constants.UserJsonPath);
            return JsonConvert.DeserializeObject<IList<User>>(json);
        }

        public User LogIn(string login, string password)
        {
            string[] files = Directory.GetFiles(Constants.UserDbPath);

            foreach (string file in files)
            {
                string[] userDetails = File.ReadAllLines(file);

                string userLogin = userDetails[4];
                string userPassword = userDetails[5];
                
                if (login == userLogin && password == userPassword)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login muvaffaqiyatli amalga oshirildi! 👍");
                    return new User
                    {
                        Id = "123",
                        Login = userDetails[4],
                        Password = userDetails[5]
                    };
                }

            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bunday login va parol ma'lumotlar omborida mavjud emas 👎");
            return null;
        }

        public void PrintAll()
        {
            IList<User> goods = GetAll();

            foreach (User good in goods)
            {
                Console.WriteLine("| Taom Nomi: {0,15} | Narxi (UZS): {1, 10} | Hozirda Mavjud:  {2,5} ta dona | Joylashuvi: {3,10} shaxobchasi |", good.FoodName, good.Costage,
                    good.Count, good.Location);
            }
        }

        public void Update(string kirish)
        {
            string readdedFile = File.ReadAllText(Constants.UserJsonPath);
            int s = 0;
            IList<User> products = JsonConvert.DeserializeObject<IList<User>>(readdedFile);

            foreach (var product in products)
            {
                if (product.FoodName == kirish.Trim().ToLower().UpperCase())
                {
                    s = 1;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Kiritilgan ovqat menyuda mavjud, ma'lumotlarni yangilash mumkin! ");
                    Console.WriteLine("Taomning yangi nomini kiriting: ");
                    string newname = Console.ReadLine();
                    product.FoodName = newname;
                    arr:
                    try
                    {
                        Console.WriteLine("Uning sonini kiriting: ");
                        int count = int.Parse(Console.ReadLine());
                        product.Count = count;
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine(ex);
                        goto arr;
                    }
                    Console.WriteLine("Uning joylashuvini yozing: ");
                    string located = Console.ReadLine();
                    product.Location = located;
                    err:
                    Console.WriteLine("Uning narxini yozing (UZS): ");
                    try
                    {
                        double costed = Double.Parse(Console.ReadLine());
                        product.Costage = costed;
                    }
                    catch (SystemException excep)
                    {
                        Console.WriteLine(excep);
                        goto err;
                    }
                }
            }
            if (s == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bunday ovqat menyuda mavjud emas :(");
            }
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(Constants.UserJsonPath, json);
        }
        public void Search(string foodnames)
        {
            int x = 0;
            string json = File.ReadAllText(Constants.UserJsonPath);
            IList<User> foodlar = JsonConvert.DeserializeObject<List<User>>(json);
            foreach (var foods in foodlar)
            {
                IEnumerable<User> enumerable = foodlar.Where(x => x.ToString().Contains(foodnames)).ToList();
                if (foods.ToString().ToLower().Trim().Contains(foodnames.Trim().ToLower())
                    || enumerable.Equals(true))
                {
                    x = 1;
                    Console.WriteLine("\t\tThat's what you have searched: 😇 \t\t");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("| FoodName: {0,15} | Narxi (UZS): {1, 10} | Mavjud:  {2,5} ta dona | Joylashuvi: {3,15} shaxobchasi |", foods.FoodName, foods.Costage,
                        foods.Count, foods.Location);
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                }
            }

            if (x == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bunday ma'lumotni o'z ichiga olgan type bizning menyuda yo'q 😕");
            }
            /*  int xexe = 0;
    
              var Way = foodlar.Where(x => x.FoodName == foodnames).ToList();
              var Way1 = foodlar.Where(x => x.Count == int.Parse(foodnames)).ToList();
              var Way2 = foodlar.Where(x => x.Costage == double.Parse(foodnames)).ToList();
              var way3 = foodlar.Where(x => x.Location == foodnames).ToList();
              
              
              if (Way.Count > 0)
              {
                  foreach (var foods in Way) 
                  {
                      if (foods.FoodName == foodnames)
                      {
                          Console.WriteLine("That's what you have searched: ");
                          Console.WriteLine("| {0,20} | {1, 20} | {2,20} | {3,20} |\n", foods.FoodName, foods.Costage, foods.Count, foods.Location);
                      }
                  }
              } 
           else  if (Way1.Count > 0)
              {
                  foreach (var foods in Way1) 
                  {
                      if (foods.Count == int.Parse(foodnames))
                      {
                          Console.WriteLine("That's what you have searched: ");
                          Console.WriteLine("| {0,20} | {1, 20} | {2,20} | {3,20} |\n", foods.FoodName, foods.Costage, foods.Count, foods.Location);
                      }
                  }
              } 
            else  if (Way2.Count > 0)
              {
                  foreach (var foods in Way2) 
                  {
                      if (foods.Costage == double.Parse(foodnames))
                      {
                          Console.WriteLine("That's what you have searched: ");
                          Console.WriteLine("| {0,20} | {1, 20} | {2,20} | {3,20} |\n", foods.FoodName, foods.Costage, foods.Count, foods.Location);
                      }
                  }
              } 
             else if (way3.Count > 0)
              {
                  foreach (var foods in way3) 
                  {
                      if (foods.Location == foodnames)
                      {
                          Console.WriteLine("That's what you have searched: ");
                          Console.WriteLine("| {0,20} | {1, 20} | {2,20} | {3,20} |\n", foods.FoodName, foods.Costage, foods.Count, foods.Location);
                      }
                  }
              }*/
        }
    }
}