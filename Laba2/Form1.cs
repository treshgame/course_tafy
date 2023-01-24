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

namespace Laba1
{
    public partial class Form1 : Form
    { 
        public List<string> Chars = new List<string> { "&", "&&", "*", "$", ";", "-", "+", "/", "{", "}", "(", ")", "=", "<", ">", ",", "()"};
        public List<string> keyWords = new List<string> { "if", "else", "int", "string", "char", "main" };
        // Laba2 lists
        public List<string> L = new List<string>();
        public List<string> currentKeyWords = new List<string>();
        public List<string> R = new List<string>();
        public List<string> I = new List<string>();
        
        public Dictionary<string, string> tableDict = new Dictionary<string, string>();
        public Dictionary<int, int> newTableDict = new Dictionary<int, int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            L = new List<string>();
            R = new List<string>();
            I = new List<string>();
            this.dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            LexicalAnalysis();
            TokenClassification();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void LexicalAnalysis()
        {
            this.dataGridView1.Rows.Clear();
            string Code = CodeSource.Text;
            string lexem = "";
            for (int i = 0; i < Code.Length; i++)
            {
                if (!lexem.Equals(""))
                {
                    if (Convert.ToString(Code[i]).Trim().Equals(""))
                    {
                        addToDict(lexem);
                        lexem = "";
                    }
                    else if (Chars.Contains(Convert.ToString(Code[i])))
                    {
                        if (Chars.Contains(Convert.ToString(lexem) + Convert.ToString(Code[i])))
                        {
                            lexem += Code[i];
                        }
                        else
                        {
                            addToDict(lexem);
                            lexem = Convert.ToString(Code[i]);
                        }
                    }
                    else
                    {
                        if (Chars.Contains(Convert.ToString(lexem[0])))
                        {
                            addToDict(lexem);
                            lexem = "";
                        }
                        lexem += Code[i];
                    }
                }
                else
                {
                    lexem += Code[i];
                }

                if (i == Code.Length - 1)
                {
                    addToDict(lexem);
                }
            }

            foreach (KeyValuePair<string, string> dictItem in tableDict)
            {
                this.dataGridView1.Rows.Add(dictItem.Key, dictItem.Value);
            }
        }
        private void TokenClassification()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns[0].HeaderText = "Таблица";
            this.dataGridView1.Columns[1].HeaderText = "Строка";
            if (tableDict.Any())
            {
                foreach (KeyValuePair<string, string> dictItem in tableDict)
                {
                    switch (dictItem.Value)
                    {
                        case "R":
                            R.Add(dictItem.Key);

                            break;
                        case "L":
                            L.Add(dictItem.Key);
                            break;
                        case "I":
                            if (keyWords.Contains(dictItem.Key))
                                currentKeyWords.Add(dictItem.Key);
                            else
                                I.Add(dictItem.Key);
                            break;
                    }
                }
                foreach (KeyValuePair<string, string> dictItem in tableDict)
                {
                    int index;
                    switch (dictItem.Value)
                    {
                        case "R":
                            index = Chars.FindIndex(a => a == dictItem.Key);
                            this.dataGridView1.Rows.Add(2, index + 1);
                            break;
                        case "L":
                            index = L.FindIndex(a => a == dictItem.Key);
                            this.dataGridView1.Rows.Add(3, index + 1);
                            break;
                        case "I":
                            if (keyWords.Contains(dictItem.Key))
                            {
                                index = currentKeyWords.FindIndex(a => a == dictItem.Key);
                                this.dataGridView1.Rows.Add(1, index + 1);
                            }
                            else
                            {
                                index = I.FindIndex(a => a == dictItem.Key);
                                this.dataGridView1.Rows.Add(4, index + 1);
                            }
                            break;
                    }
                }
                addToDataGrid(keyWords, this.dataGridView2, 1);
                addToDataGrid(Chars, this.dataGridView3, 2);
                addToDataGrid(L, this.dataGridView4, 3);
                addToDataGrid(I, this.dataGridView5, 4);
            }
        }
        public void addToDict(string lexem)
        {
            if (tableDict.ContainsKey(lexem))
            {
                return;
            }
            if (int.TryParse(lexem, out int numericValue))
            {
                tableDict.Add(lexem, "L");
            }
            else
            {

                if (Chars.Contains(Convert.ToString(lexem)))
                {
                    tableDict.Add(lexem, "R");
                }
                else
                {
                    tableDict.Add(lexem, "I");
                }

            }
        }

        public void addToDataGrid(List<string> list, DataGridView dgv, int tableNum)
        {
            dgv.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dgv.Rows.Add(list[i]);
                this.dataGridView1.Rows.Add(tableNum, i+1);
                //label3.Text += $" {cellNumber} ";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
