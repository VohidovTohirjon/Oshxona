using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OshXona.Extension;
using OshXona.Models;
using OshXona.Service;
using OshXona.IRepository;
using OshXona.UserRepository;

namespace OshXona
{
    internal class Program
    {
        public static void Main(string[] args)
        {
           Menu.FoodMenu();
        }
    }
}