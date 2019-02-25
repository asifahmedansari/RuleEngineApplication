using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RuleEngineApplication
{
    class RuleEngine
    {
        using (StreamReader r = new StreamReader("C://Users//SayedMohammadHaider//Desktop//RuleEngineApplication//RuleEngineApplication//json1.json"))
            {
                string json = r.ReadToEnd();
    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (var list in items)
                {
                    Console.WriteLine(list.signal);
                    Console.WriteLine(list.value_type);
                    Console.WriteLine(list.value);
                }
Console.ReadLine();
            }
    public class Item
{
    public string signal;
    public string value_type;
    public string value;
}
    }
}
