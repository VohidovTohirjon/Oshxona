using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OshXona.Enums;
using OshXona.Extension;

namespace OshXona.Models
{
    public class User
    {
        public string Id { get; set; } 
        public string FoodName { get; set; }
        public double Costage { get; set; } = 120050.343;
        public int Count { get; set; } = 0;
        public string Location { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public FoodType TypeFood { get; set; } = FoodType.Neutral;
        public Frequency Frequent { get; set; } = Frequency.Normal;
        public override string ToString()
        {
            return $"{Id}\n{FoodName}\n{Costage}\n{Count}\n{TypeFood}\n{Frequent}\n{Login}\n{Password}";
        }
    }
    }