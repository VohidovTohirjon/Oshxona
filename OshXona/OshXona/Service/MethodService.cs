using System;
using System.IO;

namespace OshXona.Service
{
    public class MethodService
    {
       
            public static string GetUserPath(Guid Id)
            {
                return Path.Combine(Constants.UserDbPath, Id.ToString() + ".txt");
            }
        }
    }