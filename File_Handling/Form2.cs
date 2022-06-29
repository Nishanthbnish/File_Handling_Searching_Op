using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace File_Handling
{
    public partial class Form2 : Form
    {
        FileInfo[] files;
        public string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public Form2(ListBox.ObjectCollection items)
        {
            InitializeComponent();
            
            listBox1.Items.AddRange(items);
            //string paths = path;
            //files = new DirectoryInfo(paths).GetFiles("*.txt", SearchOption.TopDirectoryOnly); //no need to define any parameters
            //for (int i = 0; i < files.Length; i++)
            //    listBox1.Items.Add(paths.(files[i].FullName));

        }

        
        
        public List<TreeNode> Node1;
        public List<TreeNode> CheckNodes1
        {
            get { return Node1; }   
            set { Node1 = value; }
        }

        

        public List<string> item1;
        public List<string>checkListItems
        { get { return item1; }
          set { item1 = value; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stream mystream;
            OpenFileDialog openFileDialog = new OpenFileDialog();   
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((mystream=openFileDialog.OpenFile()) !=null)
                {            
                    string strfilename=openFileDialog.FileName;//path
                    string filetext=File.ReadAllText(strfilename);//content
                    richTextBox1.Text = filetext;
                    textBox2.Text = strfilename;
                    //MessageBox.Show(strfilename);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = 0;
            string temp = richTextBox1.Text;
            richTextBox1.Text="";
            richTextBox1.Text = temp;
            while(index<richTextBox1.Text.LastIndexOf(textBox1.Text))
            {
                //searches the text in a RichtextBox control for a string within a range of text within the 
                richTextBox1.Find(textBox1.Text, index, richTextBox1.TextLength, RichTextBoxFinds.None);
                //selection color.This is added automatically when it's selected
                richTextBox1.SelectionBackColor = Color.Yellow;
                //After a match is found the index is increased so the search won't stop at the same
                index = richTextBox1.Text.IndexOf(textBox1.Text, index) + 1;
            }
            this.timer1.Start();
        }
        //To read selected listbox1 file into richtextbox
        //richtextbox1.Text = File.ReadAllText(listbox1.SelectedItem.ToString());
        private void button2_Click(object sender, EventArgs e)
        {

            //string[] lines = File.ReadAllLines("C:\\Users\\nish\\Documents\\Text_document\\Program.cs");
            //richTextBox1.Text = lines.ToString();
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                List<string> lines = new List<string>();
                using (StreamReader r = new StreamReader(f.OpenFile()))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);

                    }
                }
            }
            //var items = checkListItems;
            //foreach (var item in items)
            //{
            //    string fileName = listBox1.GetItemText(item);
            //    string fileContents = System.IO.File.ReadAllText(fileName);
            //    //Do something with the file contents
            //    listBox1.Items.Add(fileName);
            //    listBox1.Items.Add(fileContents);
            //}
            //foreach (string value in listBox1.SelectedItems)
            //{
            //    StreamReader[] sr = new StreamReader(value);
            //    string[] fileName = listBox1.GetItemText(sr);
            //    string[] fileContents = File.ReadAllText(fileName);
            //    //string strfilename = openFileDialog.FileName;
            //    string filetext = File.ReadAllText(strfilename);
            //    richTextBox1.Text = fileContents;
            //}

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.FileName = listBox1.SelectedItem.ToString();
                openFileDialog.InitialDirectory = textBox1.Text;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*;";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

           richTextBox1.Text=fileContent;
            textBox2.Text=filePath; ;
        }
    }
}
