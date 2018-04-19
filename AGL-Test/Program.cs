using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;


namespace AGL_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://agl-developer-test.azurewebsites.net/people.json";

            string jsonStr = string.Empty;
 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonStr = reader.ReadToEnd();
            }

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic jsonObj = jsonSerializer.Deserialize<dynamic>(jsonStr);
            
            List<string> maleCatList = new List<string>();
            List<string> femaleCatList = new List<string>();

            foreach (var item in jsonObj)
            {
                dynamic eachOwnersPets = item["pets"];
                
                if (eachOwnersPets != null)
                {
                    string ownerGender = item["gender"];
                    if (ownerGender.ToUpper() == "MALE")
                    {
                        foreach (var pet in eachOwnersPets)
                        {
                            string maleOwnerPetType = pet["type"];
                            if (maleOwnerPetType.ToUpper() == "CAT")
                            {
                                maleCatList.Add(pet["name"]);
                            }
                        }
                    }
                    else if (ownerGender.ToUpper() == "FEMALE")
                    {
                        foreach (var pet in eachOwnersPets)
                        {
                            string femaleOwnerPetType = pet["type"];
                            if (femaleOwnerPetType.ToUpper() == "CAT")
                            {
                                femaleCatList.Add(pet["name"]);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Male Cats Names");

            maleCatList.Sort();
            maleCatList.ToList().ForEach(Console.WriteLine);

            Console.Write("\r\n");
            femaleCatList.Sort();
            Console.WriteLine("Female Cat Names");
            femaleCatList.ToList().ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
