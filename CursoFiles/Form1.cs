using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursoFiles
{
    public partial class Form1 : Form
    {

        private string Caminho;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Caminho = "C:\\Users\\joaog\\Desktop";

            Carregar();

            
        }

        private void Carregar()
        {
            trvPastas.Nodes.Clear();
            TreeNode node = new TreeNode(Caminho);
            node.Tag = Caminho;
            trvPastas.Nodes.Add(node);

            CarregarDiretorios(Caminho, node);
        }

        private void CarregarDiretorios(string path, TreeNode nodePai)
        {
            string[] diretorios = Directory.GetDirectories(path);

            foreach (var diretorio in diretorios)
            {
                TreeNode node = new TreeNode(Path.GetFileName(diretorio));
                node.Tag = diretorio;

                nodePai.Nodes.Add(node);

                CarregarDiretorios(diretorio, node);
            }
        }

        private void trvPastas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lbxArquivos.Items.Clear();

            string path = (string) trvPastas.SelectedNode.Tag;

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                lbxArquivos.Items.Add(Path.GetFileName(file));
            }
        }

        private void lbxArquivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diretorio = (string)trvPastas.SelectedNode.Tag;
            string arquivo = (string)lbxArquivos.SelectedItem;

            string path = Path.Combine(diretorio, arquivo);

            string extensao = Path.GetExtension(path);
            if (extensao == ".jpg" || extensao == ".png" || extensao == ".bmp")
            {
                pbxVisualizar.Image = Image.FromFile(path);
            }
        }

        private void lbxArquivos_DoubleClick(object sender, EventArgs e)
        {
            string path = Path.Combine((string)trvPastas.SelectedNode.Tag, (string)lbxArquivos.SelectedItem);

            Process.Start(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Caminho = dialog.SelectedPath;
                Carregar();
            }
        }
    }
}
