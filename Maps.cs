using System;
using System.Collections.Generic;
using System.Linq;

namespace Instalock
{
    internal class Maps
    {
        public static Dictionary<string, string> MapUUIDs = new Dictionary<string, string>()
        {
            ["ascent"] = "7eaecc1b-4337-bbf6-6ab9-04b8f06b3319",
            ["bind"] = "2c9d57ec-4431-9c5e-2939-8f9ef6dd5cba",
            ["split"] = "d960549e-485c-e861-8d71-aa9d1aed12a2",
            ["fracture"] = "b529448b-4d60-346e-e89e-00a4c527a405",
            ["haven"] = "2bee0dc9-4ffe-519b-1cbd-7fbe763a6047",
            ["breeze"] = "2fb9a4fd-47b8-4e7d-a969-74b4046ebd53",
            ["icebox"] = "e2ad5c54-4114-a870-9641-8ea21279579a",
            ["pearl"] = "fd267378-4d1d-484f-ff52-77821ed10dc2",
            ["lotus"] = "2fe4ed3a-450a-948b-6d6b-e89a78e680a9",
        };

        public static Dictionary<string, string> MapCodeNames = new Dictionary<string, string>()
        {
            ["ascent"] = "ascent",
            ["bind"] = "duality",
            ["split"] = "bonsai",
            ["fracture"] = "canyon",
            ["haven"] = "triad",
            ["breeze"] = "foxtrot",
            ["icebox"] = "port",
            ["pearl"] = "pitt",
            ["lotus"] = "jam",
        };

        public static string GetUUIDFromName(string name)
        {
            try
            {
                name = name.ToLower();
                var uuid = MapUUIDs.First(x => x.Key == name).Value;
                return uuid;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string GetNameFromUUID(string uuid)
        {
            try
            {
                uuid = uuid.ToLower();
                var name = MapUUIDs.FirstOrDefault(x => x.Value == uuid).Key;
                return name;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string GetIDFromName(string name)
        {
            try
            {
                name = name.ToLower();
                var uuid = MapCodeNames.First(x => x.Key == name).Value;
                return uuid;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string GetNameFromID(string id)
        {
            try
            {
                id = id.ToLower();
                var name = MapCodeNames.FirstOrDefault(x => x.Value == id).Key;
                return name;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}