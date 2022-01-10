using System;
using System.Collections.Generic;
using OshXona.Models;
using OshXona.UserRepository;

namespace OshXona
{
    public class Menu
    {
        public static void FoodMenu()
        {
            FoodRepository yengi = new FoodRepository();
            User abc = new User();
            int number = 0;
            int a = 0;
            xex:
            Console.WriteLine("If you wanna change the menu tap 1, or wanna look tap 2\n" +
                              "1.Admin\t 2.Consumer");
            Console.Write(">>>");
            try
            {
                number = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Write only 1 or 2, nor string neither other data types are accepted");
                goto xex;
            }

            if (number == 1)
            {
                Console.WriteLine("Welcome to the Admin Panel, now you've to log in to earn rights!");
                Console.WriteLine("Username: default : respi");
                Console.Write(">>>");
                string username = Console.ReadLine();
                Console.WriteLine("Password: default : 456");
                Console.Write(">>>");
                string password = Console.ReadLine();
                FoodRepository mm = new FoodRepository();
                User r = mm.LogIn(username, password);
                if (r == null)
                {
                    goto xex;
                }
                if (r != null)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Xush kelibsiz, bu dasturdan foydalanishingiz mumkin");
                    alfs:
                    Console.WriteLine(
                        "1.Printall 📄\t 2.AddFood 🍽\t 3.Update 📥📤\t 4.Delete 🗑\t 5.Search 🔍\t 6.Clear🏳\t 0.Exit 🏳");
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(">>>");
                        a = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Formatni int shaklida kiriting!");
                        goto alfs;
                    }

                    if (a == 1)
                    {
                        yengi.PrintAll();
                        goto alfs;
                    }
                    else if (a == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Taom nomini kiriting: ");
                        abc.FoodName = Console.ReadLine();
                        count:
                        Console.WriteLine("Uning nechta donaligini ham yozing: ");
                        try
                        {
                            abc.Count = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Butun son shaklda yozing! ");
                            goto count;
                        }

                        count1:
                        Console.WriteLine("O'zbek somidagi narxini yozing, elsatma ',' o'rniga . ni ishlating: ");
                        try
                        {
                            abc.Costage = double.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Son shaklda yozing! ");
                            goto count1;
                        }

                        Console.WriteLine("Taom qaysi ovqatlanish kafesida bor: ");
                        abc.Location = Console.ReadLine();

                        yengi.AddFood(abc);
                        goto alfs;
                    }
                    else if (a == 3)
                    {
                        Console.WriteLine("Taom faqatgina o'zining nomi bilan yangilanadi:");
                        Console.Write(">>>");
                        string upd = Console.ReadLine();
                        yengi.Update(upd);
                        goto alfs;
                    }
                    else if (a == 4)
                    {
                        Console.WriteLine("Ovqatni menyudan faqatgina uning nomini chaqiribgina o'chirish mumkin");
                        Console.Write(">>>");
                        string dt = Console.ReadLine();
                        yengi.Delete(dt);
                        goto alfs;
                    }
                    else if (a == 5)
                    {
                        Console.WriteLine(
                            "Taomning nomi,miqdori,narxi va joylashuviga ko'ra sizga ma'lumotni taqdim etamiz");
                        Console.Write(">>>");
                        string qidirish = Console.ReadLine();
                        yengi.Search(qidirish);
                        goto alfs;
                    }
                    else if (a == 6)
                    {
                        Console.Clear();
                        goto alfs;
                    }
                    else if (a == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Foydalanganingiz uchun tashakkur!" +
                                          "Tugadi");
                    }
                    else
                    {
                        Console.WriteLine("Berilgan menyudagi sonlardan birini tanlang");
                        goto xex;

                    }
                }
             }
            else if (number == 2)
            {
                pam:
                int b = 0;
                Console.WriteLine("1.Printall 📄\t 2.Search 🔍\t 3.Clear🏳\t 0.Exit 🏳");
                b = int.Parse(Console.ReadLine());
                if (b == 1)
                {
                    yengi.PrintAll();
                    goto pam;
                }
                else if (b == 2)
                {
                    Console.WriteLine(
                        "Taomning nomi,miqdori,narxi va joylashuviga ko'ra sizga ma'lumotni taqdim etamiz");
                    Console.Write(">>>");
                    string qidirish = Console.ReadLine();
                    yengi.Search(qidirish);
                    goto pam;
                }
                else if (b == 3)
                {
                    Console.Clear();
                    goto pam;
                }
                else if(b==0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Foydalanganingiz uchun tashakkur!" +
                                      "\nTugadi");
                }
                else
                {
                    Console.WriteLine("Berilgan menyudagi sonlardan birini tanlang");
                    goto pam;
                }
            }
            else
            {
                Console.WriteLine("Butun son kiriting!");
                goto xex;
            }
        }
    }
}
