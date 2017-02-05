using Brain.Node;
using Brain.Train.Models;
using DesktopPresentation.Nodes;
using System;
using System.Diagnostics;
using System.Security;
using System.Windows.Forms;

namespace DesktopPresentation
{
    public partial class frmIABuilder : Form
    {
        private readonly Timer t = new Timer();

        public frmIABuilder()
        {
            InitializeComponent();
            t.Tick += timer1_Tick;
            t.Start();
        }

        private void btEntrada_Click(object sender, EventArgs e)
        {
            rnaDashBoard1.Componente = 0;
        }

        private void btSaida_Click(object sender, EventArgs e)
        {
            rnaDashBoard1.Componente = 1;
        }

        private void btProcessamento_Click(object sender, EventArgs e)
        {
            rnaDashBoard1.Componente = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(rnaDashBoard1.GerarEstruturaTexto());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rnaDashBoard1.ObjSelecionado == null) return;
            txnome.DataBindings.Clear();
            txvalor.DataBindings.Clear();
            txmax.DataBindings.Clear();
            txmin.DataBindings.Clear();
            txCamada.DataBindings.Clear();
            txnome.DataBindings.Add(new Binding("Text", rnaDashBoard1.ObjSelecionado.Tag as BaseNode, "Nome", false,
                DataSourceUpdateMode.OnPropertyChanged));
            txvalor.DataBindings.Add(new Binding("Text", rnaDashBoard1.ObjSelecionado.Tag as BaseNode, "Valor", false,
                DataSourceUpdateMode.OnPropertyChanged));
            txmax.DataBindings.Add(new Binding("Text", rnaDashBoard1.ObjSelecionado.Tag as BaseNode, "LimMax", false,
                DataSourceUpdateMode.OnPropertyChanged));
            txmin.DataBindings.Add(new Binding("Text", rnaDashBoard1.ObjSelecionado.Tag as BaseNode, "LimMin", false,
                DataSourceUpdateMode.OnPropertyChanged));
            txCamada.DataBindings.Add(new Binding("Text", rnaDashBoard1.ObjSelecionado.Tag as BaseNode, "Camada", false,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rnaDashBoard1.MontarRede();
            AtualizarArvore();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbStatus.Text = rnaDashBoard1.Estado;
            lblno.Text = rnaDashBoard1._obj;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            rnaDashBoard1.Componente = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //define as propriedades do controle 
            var ofd1 = new OpenFileDialog();
            ofd1.Multiselect = true;
            ofd1.Title = "Selecionar Arquivo de Dados";
            ofd1.InitialDirectory = @"C:\";

            ofd1.Filter = "Texto (*.txt)|*.TXT|" + "All files (*.*)|*.*";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.FilterIndex = 0;
            ofd1.RestoreDirectory = true;
            ofd1.ReadOnlyChecked = true;
            ofd1.ShowReadOnly = true;

            var dr = ofd1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                foreach (var arquivo in ofd1.FileNames)
                {
                    try
                    {
                        rnaDashBoard1.Enabled = false;
                        rnaDashBoard1.treinarRedeErro(float.Parse(textBox1.Text), arquivo);
                        rnaDashBoard1.Enabled = true;
                        AtualizarArvore();
                    }
                    catch (SecurityException ex)
                    {
                        // O usuário  não possui permissão para ler arquivos
                        MessageBox.Show("Erro de segurança Contate o administrador de segurança da rede.\n\n" +
                                        "Mensagem : " + ex.Message + "\n\n" +
                                        "Detalhes (enviar ao suporte):\n\n" + ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        // Não pode carregar a imagem (problemas de permissão)
                        MessageBox.Show("Não é possível exibir a imagem" + ex.Message);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var dt = new InputTrainning();
            for (var i = 0; i < rnaDashBoard1.Treinamento.inputs.Length; i++)
            {
                //var frm = new frmInputValue("Digite o Nome da Entrada + ';' + Valor para Entrada");
                //frm.ShowDialog();
                //var dados = frm.retorno.Split(';');
                //dt.Values.Add(new TrainningValues {InputName = dados[0], Value = float.Parse(dados[1])});
            }

            MessageBox.Show(rnaDashBoard1.testarValorRede(dt));
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            var tt = new ToolTip();
            tt.SetToolTip((sender as PictureBox), (sender as PictureBox).Name);
        }

        private void AtualizarArvore()
        {
            treeView1.Nodes.Clear();
            rnaDashBoard1.Rede.ForEach(w =>
            {
                var tn = new TreeNode();
                tn.Text = w.GetType() == typeof (SensorNeuron)
                    ? "Input->" + w.Name
                    : (w.GetType() == typeof (HiddenNode)
                        ? "Hidden->" + w.Name
                        : (w.GetType() == typeof (BiasNeuron) ? "Bias->" + w.Name : "Output->" + w.Name));

                for (var i = 0; i < w.ConnectionsTo.Count; i++)
                {
                    var subtn = new TreeNode();
                    subtn.Text = w.ConnectionsTo[i].ConnectedNode.GetType() == typeof (SensorNeuron)
                        ? "Input->" + w.ConnectionsTo[i].ConnectedNode.Name + ", Weight: " + w.ConnectionsTo[i].Weight
                        : (w.ConnectionsTo[i].ConnectedNode.GetType() == typeof (HiddenNode)
                            ? "Hidden->" + w.ConnectionsTo[i].ConnectedNode.Name + ", Weight: " + w.ConnectionsTo[i].Weight
                            : (w.ConnectionsTo[i].ConnectedNode.GetType() == typeof (BiasNeuron)
                                ? "Bias->" + w.ConnectionsTo[i].ConnectedNode.Name + ", Weight: " + w.ConnectionsTo[i].Weight
                                : "Output->" + w.ConnectionsTo[i].ConnectedNode.Name + ", Weight: " + w.ConnectionsTo[i].Weight));
                    tn.Nodes.Add(subtn);
                }
                treeView1.Nodes.Add(tn);
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBox3.Text = (rnaDashBoard1.Treinamento.ExtrairRede());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rnaDashBoard1.CriarLigacoes();
        }

        private void rnaDashBoard1_Load(object sender, EventArgs e)
        {
        }

        private void rnaDashBoard1_Click(object sender, EventArgs e)
        {
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void modo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            rnaDashBoard1.Componente = 3;
        }

        private void treinamentoNãoSupervisionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}