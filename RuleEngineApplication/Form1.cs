using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RuleEngineApplication;

namespace RuleEngineApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ruleDBDataSet.Rules' table. You can move, or remove it, as needed.
            this.rulesTableAdapter.Fill(this.ruleDBDataSet.Rules);
            fillcombobox();
        }

        //Variables
        string signal;
        string valuetype;
        string symbolvalue;
        int intvalue;
        string stringvalue;
        int switchvalue;
        //Function to fill Dropdown combobox//
        private void fillcombobox()
        {
            cb_valuetype.Items.Insert(0, "Select Value Type");
            cb_valuetype.Items.Insert(1, "Integer");
            cb_valuetype.Items.Insert(2, "Date Time");
            cb_valuetype.Items.Insert(3, "String");
            cb_valuetype.SelectedIndex = 0;
        }
        private void cleartextbox()
        {
            txt_signal.Text = "";
            txt_conditionvalue.Text = "";
            cb_valuetype.SelectedIndex = 0;
        }
        //behaviour on dropdown value change//
        private void cb_valuetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_valuetype.SelectedIndex > 0)
            {
                groupBox1.Visible = true;
                switchvalue = Convert.ToInt32(cb_valuetype.SelectedIndex);
                switch (switchvalue)
                {
                    case 1:
                        radioButton2.Visible = true;
                        radioButton3.Visible = true;
                        radioButton4.Visible = true;
                        radioButton5.Visible = true;
                        radioButton6.Visible = true;
                        txt_conditionvalue.Visible = true;
                        dateTimePicker1.Visible = false;
                        radioButton1.Text = "=(Equal To)";
                        radioButton2.Text = "≠(Not Equal To)";
                        radioButton3.Text = ">=(Greater Than or Equal To)";
                        radioButton4.Text = "<=(Less Than or Equal To)";
                        radioButton5.Text = ">(Greater Than)";
                        radioButton6.Text = "<(Less Than)";
                        break;
                    case 2:
                        radioButton1.Text = "=(Equal To)";
                        radioButton4.Text = "<=(Less Than or Equal To)";
                        dateTimePicker1.Visible = true;
                        txt_conditionvalue.Visible = false;
                        radioButton3.Visible = false;
                        radioButton2.Visible = false;
                        radioButton5.Visible = false;
                        radioButton6.Visible = false;
                        break;
                    case 3:
                        radioButton1.Text = "=(Equal To)";
                        radioButton2.Text = "≠(Not Equal To)";
                        dateTimePicker1.Visible = false;
                        txt_conditionvalue.Visible = true;
                        radioButton3.Visible = false;
                        radioButton4.Visible = false;
                        radioButton5.Visible = false;
                        radioButton6.Visible = false;
                        break;
                }
            }
            else
            {
                groupBox1.Visible = false;
            }
        }
        //Save button click event//
        private void btn_savedata_Click(object sender, EventArgs e)
        {

            if (txt_signal.Text != "")
            {
                signal = txt_signal.Text.Trim().ToUpper();
                if (cb_valuetype.SelectedIndex > 0)
                {
                    valuetype = cb_valuetype.SelectedItem.ToString();
                    switch (switchvalue)
                    {
                        case 1:
                            if (txt_conditionvalue.Text != "")
                            {
                                stringvalue = txt_conditionvalue.Text.Trim();
                            }
                            else
                            {
                                MessageBox.Show("Enter Condition Value!");
                            }
                            break;
                        case 2:
                            stringvalue = dateTimePicker1.Value.ToString();
                            break;
                        case 3:
                            if (txt_conditionvalue.Text != "")
                            {
                                stringvalue = txt_conditionvalue.Text.Trim();
                            }
                            else
                            {
                                MessageBox.Show("Enter Condition Value!");
                            }
                            break;
                    }
                    if (radioButton1.Checked == true)
                    {
                        symbolvalue = "=";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        symbolvalue = "!=";
                    }
                    else if (radioButton3.Checked == true)
                    {
                        symbolvalue = ">=";
                    }
                    else if (radioButton4.Checked == true)
                    {
                        symbolvalue = "<=";
                    }
                    else if (radioButton5.Checked == true)
                    {
                        symbolvalue = ">";
                    }
                    else if (radioButton6.Checked == true)
                    {
                        symbolvalue = "<";
                    }
                    else
                    {
                        MessageBox.Show("Select One Condition!");
                    }
                    using (RuleDBEntities1 mde = new RuleDBEntities1())
                    {
                        int Result = 0;
                        Rule re = new Rule(); //Create Object
                        re.SignalID = signal;
                        re.valuetype = valuetype;
                        re.condition = symbolvalue;
                        re.value = stringvalue;
                        mde.Rules.Add(re);
                        Result = mde.SaveChanges();
                        if (Result > 0)
                        {
                            MessageBox.Show("Submitted Successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Submission failed");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select Value Type!");
                }
            }
            else
            {

                MessageBox.Show("Enter Signal Name");
            }

        }
        // Clearfuntion on clear button click
        private void btn_clear_Click(object sender, EventArgs e)
        {
            cleartextbox();
        }
        //Key press function on value textbox//
        private void txt_conditionvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (switchvalue)
            {
                case 1:
                    if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)Keys.Back)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 Data = new Form2();
            Data.ShowDialog();
        }
    }
}
