using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuleEngineApplication
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            fetchjson();
        }

        //Json function logic codes for streaming data//
        private void fetchjson()
        {
            using (RuleDBEntities1 mde = new RuleDBEntities1())
            {
                var dataFromTable = mde.Rules.ToList();
                using (StreamReader r = new StreamReader("C://Users//ASIF AHMED//documents//visual studio 2015//Projects//RuleEngineApplication//RuleEngineApplication//signal.json"))
                {
                    string json = r.ReadToEnd();
                    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                    //var uniqueData = from dataBaseData in dataFromTable
                    //                 join jsonData in items
                    //                    on new { A = dataBaseData.SignalId, B = dataBaseData.ValueType } equals new { A = jsonData.signal, B = jsonData.value_type } into details
                    //                 from d in details
                    //                 select new { dataBaseData.SignalId, dataBaseData.ValueType, dataBaseData.Condition, dataBaseData.Value };
                    //dataGridView1.DataSource = uniqueData.ToList();
                    List<matchItem> matListFinal = new List<matchItem>();
                    List<matchItem> unMatchListFinal = new List<matchItem>();
                    dataGridView1.DataSource = matListFinal.ToList();
                    foreach (var list in items)
                    {
                        var matchData = dataFromTable.Where(x => x.SignalID == list.signal && x.valuetype == list.value_type).ToList();
                        if (matchData.Count == 0)
                        {
                            unMatchListFinal.Add(new matchItem() { signal = list.signal, value_type = list.value_type, value = list.value });
                            dataGridView2.DataSource = unMatchListFinal;
                        }
                        foreach (var matList in matchData)
                        {
                            if (matList.valuetype == "String")
                            {
                                if (matList.condition == "=")
                                {
                                    if (matList.value.Contains(list.value))
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                            }
                            else if (matList.valuetype == "Datetime")
                            {
                                var jsonValue = Convert.ToDateTime(list.value);
                                var databaseValue = Convert.ToDateTime(matList.value);
                                if (matList.condition == "=")
                                {
                                    if (databaseValue == jsonValue)
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                                else if (matList.condition == ">")
                                {
                                    if (databaseValue > jsonValue)
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                                else if (matList.condition == "<")
                                {
                                    if (databaseValue < jsonValue)
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                            }
                            else if (matList.valuetype == "Integer")
                            {
                                var jsonValue = Convert.ToDecimal(list.value);
                                var databaseValue = Convert.ToDecimal(matList.value);
                                if (matList.condition == "=")
                                {
                                    if (databaseValue == jsonValue)
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                                else if (matList.condition == ">")
                                {
                                    if (databaseValue > jsonValue)
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                                else if (matList.condition == "<")
                                {
                                    if (databaseValue < jsonValue)
                                    {
                                        matListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView1.DataSource = matListFinal;
                                        break;
                                    }
                                    else
                                    {
                                        unMatchListFinal.Add(new matchItem() { signal = matList.SignalID, value_type = matList.valuetype, value = matList.value });
                                        dataGridView2.DataSource = unMatchListFinal;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public class Item
        {
            public string signal;
            public string value_type;
            public string value;

        }

        public class matchItem
        {
            public string signal { get; set; }
            public string value_type { get; set; }
            public string value { get; set; }
        }
    }
}
