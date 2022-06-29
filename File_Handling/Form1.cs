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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //TextBOX -> textBox1(Name of textbox)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListDirectory(treeView1, textBox1.Text);
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDinfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDinfo));
        }
        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var Dnode = new TreeNode(directoryInfo.Name);
            foreach (var d in directoryInfo.GetDirectories())
                Dnode.Nodes.Add(CreateDirectoryNode(d));
            foreach (var file in directoryInfo.GetFiles())
                Dnode.Nodes.Add(new TreeNode(file.Name));
            return Dnode;
        }

        List<TreeNode> checkedNodes = new List<TreeNode>();
        //List<ListBox> listBoxes = new List<ListBox>();  
        //List<string> selectedFilesContent = new List<string>();

        //TreeView -> treeView1(Name) used to show the folder and to add the checked treeview into listBox 
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            givecheckedNodes(treeView1.Nodes);
            listBox1.Items.Clear();
            foreach (TreeNode node in checkedNodes)
            {
                string file = node.Text;
                listBox1.Items.Add(file);
            }
        }
        //void GetTextFromSelectedFiles()
        //{
           
        //    for (int i = 0; i < listBox1.SelectedItems.Count; i++)
        //    {
        //        selectedFilesContent.Add(ReadFileContent(listBox1.SelectedItems.ToString()));
        //    }
        //    //when the loop is done, the list<T> holds all the text from selected files!
        //}
        //private string ReadFileContent(string path)
        //{
        //    return File.ReadAllText(path);
        //}
        void givecheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);
                }
                else
                {
                    givecheckedNodes(node.Nodes);
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
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

            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OKCancel);
        }

        //Btn (search files)-> used to send the items of listbox1 into form2 listbox1.
        //And to show the dialog box.
        private void button3_Click(object sender, EventArgs e)
        {
                //this.Hide();
                Form2 f2 = new Form2( listBox1.Items);
                f2.CheckNodes1 = checkedNodes;
                f2.path= textBox1.Text;
                f2.Show();             
        }

        //CheckedListBox -> checkedListBox1(name) used to check the type of file and get highlighted in Listbox1.
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (int s in checkedListBox1.CheckedIndices)
            {
                if (checkedListBox1.GetItemChecked(s) == true)
                {
                    listBox1.SetSelected(s, true);

                }
            }
            //listBox1.ClearSelected();
            //string curItem = checkedListBox1.CheckedItems.ToString();
            //int index1 = listBox1.Items.IndexOf(curItem);
            //if (index1 != -1)
            //{
            //    listBox1.SetSelected(index1, true);
            //    //listBox1.Items[index1] = true;
            //}

            //foreach(CheckedListBox item in checkedListBox1.CheckedItems.AsQueryable())
            //{
            //    listBox1.SetSelected(1,true);
            //}

            //foreach (CheckBox item in checkedListBox1.SelectedItems)
            //{
            //    listView2.Items[item.Index].Selected = true;
            //}
        }
        //check button
        private void button4_Click(object sender, EventArgs e)
        {

            foreach (int s in checkedListBox1.CheckedIndices)
            {
                if (checkedListBox1.GetItemChecked(s) == true)
                {
                    listBox1.SetSelected(s, true);
                    
                }
            }
            //listBox1.ClearSelected();
        }  
    }
}
