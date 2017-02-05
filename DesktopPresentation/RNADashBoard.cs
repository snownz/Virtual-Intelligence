using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Brain.Train.Models;
using Brain.Node;
using DesktopPresentation.Nodes;
using Brain.Train;

namespace DesktopPresentation
{
    public partial class RNADashBoard : UserControl
    {
        private static int X;
        private static int Y;
        private readonly List<Ligacoes> lg = new List<Ligacoes>();
        public Boolean Alteracao = true;
        public String Estado = "";
        private Ligacoes NovaLigacao = new Ligacoes();
        public UserControl ObjSelecionado = new UserControl {Tag = new SensorNeuron()};
        public List<BaseNode> Rede = new List<BaseNode>();
        public BackPropagationTrain Treinamento;

        public RNADashBoard()
        {
            InitializeComponent();
        }

        public int wobj { get; set; }
        public int hobj { get; set; }

        public int Componente
        {
            get { return _Componente; }
            set
            {
                _Componente = value;
                switch (value)
                {
                    case -1:
                        Estado = "";
                        break;
                    case 0:
                        Estado = "Clique no Painel para Adicionar uma Entrada";
                        break;
                    case 1:
                        Estado = "Clique no Painel para Adicionar um No Hidden";
                        break;
                    case 2:
                        Estado = "Clique no Painel para Adicionar uma Saida";
                        break;
                }
            }
        }

        private int _Componente { get; set; }

        public string _obj
        {
            get { return "Nó Selecionado: " + (ObjSelecionado.Tag as BaseNode).Name; }
        }

        public BaseNode NoSelecionado { get; set; }

        public void MontarRede()
        {
            Rede.Clear();
            var numE = 0;
            var numS = 0;
            if (Controls.Count >= 2)
            {
                foreach (
                    var ct in
                        Controls.Cast<object>()
                            .Where(ct => ct.GetType() == typeof (RNAHidden) || ct.GetType() == typeof (RNASaida)))
                {
                    ((ct as UserControl).Tag as BaseNode).ConnectionsTo.Clear();
                }
                foreach (var ct in Controls)
                {
                    if (ct.GetType() != typeof (RNAEntrada)) continue;
                    var nd = (ct as UserControl).Tag as BaseNode;
                    nd.Layer = -1;
                    Rede.Add(nd);
                    numE++;
                }
                foreach (var ct in Controls)
                {
                    if (ct.GetType() != typeof (BiasNeuron)) continue;
                    var nd = (ct as UserControl).Tag as BaseNode;
                    Rede.Add(nd);
                }
                foreach (var ct in Controls)
                {
                    if (ct.GetType() != typeof (RNAHidden)) continue;
                    var nd = (ct as UserControl).Tag as BaseNode;
                    var ligacoes = lg.Where(w => w.Entrada == (ct as UserControl).Name).ToList();
                    ligacoes.ForEach(w =>
                    {
                        var _ct = Controls.Find(w.Saida, true).FirstOrDefault() as UserControl;
                        nd.Synapse(_ct.Tag as BaseNode);
                    });
                    Rede.Add(nd);
                }
                var mx = Rede.Max(w => w.Layer);
                foreach (var ct in Controls)
                {
                    if (ct.GetType() == typeof (RNASaida))
                    {
                        var nd = (ct as UserControl).Tag as BaseNode;
                        nd.Layer = mx + 1;
                        var ligacoes = lg.Where(w => w.Entrada == (ct as UserControl).Name).ToList();
                        ligacoes.ForEach(w =>
                        {
                            var _ct = Controls.Find(w.Saida, true).FirstOrDefault() as UserControl;
                            nd.Synapse(_ct.Tag as BaseNode);
                        });
                        Rede.Add(nd);
                        numS++;
                    }
                }
                Alteracao = false;
                Treinamento = new BackPropagationTrain(Rede.Where(x => x is SensorNeuron).ToArray(), Rede.Where(x => x is OutpuNode).ToArray());
            }
            else
            {
                Estado = "Erro ao Montar Rede";
            }
        }

        public void treinarRedeErro(float erro, string File)
        {
            if (!Alteracao)
            {
                var let = new List<InputTrainning>();

                var Dados =
                    System.IO.File.ReadAllText(File)
                        .Replace("\n", "")
                        .Replace("\r", "")
                        .Replace("\t", "");
                var _dt = Dados.Split(Convert.ToChar(";"));

                var Nomes = new string[int.Parse(_dt[0])];
                for (var i = 0; i < int.Parse(_dt[0]); i++)
                {
                    Nomes[i] = _dt[i + 1];
                }


                for (var i = int.Parse(_dt[0]) + 1; i < _dt.Count(); i++)
                {
                    var vet = _dt[i].Split(Convert.ToChar(","));
                    var pos = 0;
                    var aux = new InputTrainning();
                    for (var j = 0; j < Treinamento.inputs.Length; j++)
                    {
                        aux.Values.Add(new TrainningValues
                        {
                            Value = float.Parse(vet[pos].Replace('.', ',')),
                            InputName = Nomes[pos].Replace(";", "")
                        });
                        pos++;
                    }
                    for (var j = 0; j < Treinamento.outputs.Length; j++)
                    {
                        aux.DesiredValues.Add(new Desired
                        {
                            Value = float.Parse(vet[pos].Replace('.', ',')),
                            Neuron = Rede.FirstOrDefault(x => x.Name == Nomes[pos].Replace(";", "")).GetHashCode().ToString()
                        });
                        pos++;
                    }
                    let.Add(aux);
                }
                Treinamento.RunEpoch(let.ToArray());
            }
            else
            {
                Estado = "Alterações Foram Feitas na Rede!";
            }
        }

        public string testarValorRede(InputTrainning dado)
        {
            if (!Alteracao)
            {
                var d = Treinamento.Test(dado.Values);
                var _out = Rede.Where(x => x is OutpuNode).ToList();
                return string.Join("\n", _out.Select(x => $"{x.Name}: {d[_out.IndexOf(x)]}"));
            }

            Estado = "Alterações Foram Feitas na Rede!";
            return "";
        }

        private void RNADashBoard_Click(object sender, EventArgs e)
        {
            switch (Componente)
            {
                case -1:
                    break;
                case 0:
                    CriarEntrada();
                    Alteracao = true;
                    break;
                case 1:
                    CriarHidden();
                    Alteracao = true;
                    break;
                case 2:
                    CriarSaida();
                    Alteracao = true;
                    break;
                case 3:
                    CriarBias();
                    Alteracao = true;
                    break;
            }
        }

        private void ClickObj(object sender, EventArgs e)
        {
            ObjSelecionado = (sender as UserControl);
        }

        private void CriarEntrada()
        {
            var rd = new Random();
            var no = new SensorNeuron();
            var cp = new RNAEntrada
            {
                Width = wobj,
                Height = hobj,
                Location = new Point(X - wobj/2, Y - hobj/2),
                Parent = this
            };
            cp.Click += ClickObj;
            cp.MouseDown += MouseDownEfect;
            cp.MouseUp += MouseUpEfect;
            cp.MouseMove += MouseMoveEfect;
            cp.ContextMenuStrip = contextMenuStrip1;
            cp.Name = "e_" + rd.Next(0, 999999);
            cp.KeyPress += RNADashBoard_KeyPress;
            no.Name = cp.Name;
            cp.Tag = no;
            Controls.Add(cp);
            cp.BringToFront();
        }

        private void CriarBias()
        {
            var rd = new Random();
            var no = new BiasNeuron();
            var cp = new RNABias
            {
                Width = wobj,
                Height = hobj,
                Location = new Point(X - wobj/2, Y - hobj/2),
                Parent = this
            };
            cp.Click += ClickObj;
            cp.MouseDown += MouseDownEfect;
            cp.MouseUp += MouseUpEfect;
            cp.MouseMove += MouseMoveEfect;
            cp.ContextMenuStrip = contextMenuStrip1;
            cp.Name = "b_" + rd.Next(0, 999999);
            cp.KeyPress += RNADashBoard_KeyPress;
            no.Name = cp.Name;
            no.Value = (float) (rd.Next(100)/100.0);
            cp.Tag = no;
            Controls.Add(cp);
            cp.BringToFront();
        }

        private void CriarHidden()
        {
            var rd = new Random();
            var no = new HiddenNode();
            var cp = new RNAHidden
            {
                Width = wobj,
                Height = hobj,
                Location = new Point(X - wobj/2, Y - hobj/2),
                Parent = this
            };
            cp.Click += ClickObj;
            cp.MouseDown += MouseDownEfect;
            cp.MouseUp += MouseUpEfect;
            cp.MouseMove += MouseMoveEfect;
            cp.ContextMenuStrip = contextMenuStrip1;
            cp.Name = "h_" + rd.Next(0, 999999);
            cp.KeyPress += RNADashBoard_KeyPress;
            no.Name = cp.Name;
            cp.Tag = no;
            Controls.Add(cp);
            cp.BringToFront();
        }

        private void CriarSaida()
        {
            var rd = new Random();
            var no = new OutpuNode();
            var cp = new RNASaida
            {
                Width = wobj,
                Height = hobj,
                Location = new Point(X - wobj/2, Y - hobj/2),
                Parent = this
            };
            cp.Click += ClickObj;
            cp.MouseDown += MouseDownEfect;
            cp.MouseUp += MouseUpEfect;
            cp.MouseMove += MouseMoveEfect;
            cp.ContextMenuStrip = contextMenuStrip1;
            cp.Name = "s_" + rd.Next(0, 999999);
            cp.KeyPress += RNADashBoard_KeyPress;
            no.Name = cp.Name;
            cp.Tag = no;
            Controls.Add(cp);
            cp.BringToFront();
        }

        public String GerarEstruturaTexto()
        {
            var ne = 0;
            var ns = 0;
            var Header = "";
            var Body = "";
            foreach (var ct in Controls.Cast<object>().Where(ct => ct.GetType() == typeof (RNAEntrada)))
            {
                Body += ((ct as UserControl).Tag as BaseNode).Name + ";\n";
                ne++;
            }
            foreach (var ct in Controls.Cast<object>().Where(ct => ct.GetType() == typeof (RNASaida)))
            {
                Body += ((ct as UserControl).Tag as BaseNode).Name + ";\n";
                ns++;
            }

            Header = (ne + ns) + ";";
            return Header + "\n" + Body;
        }

        public void CriarLigacoes()
        {
            if (Alteracao)
            {
                var auxRede =
                    (from object ct in Controls
                        where !(ct.GetType() == typeof (PictureBox))
                        select (ct as UserControl).Tag as BaseNode).ToList();
                foreach (var no in auxRede.OrderByDescending(w => w.Layer))
                {
                    auxRede.Where(w => w.Layer == no.Layer - 1)
                        .ToList()
                        .ForEach(w => lg.Add(new Ligacoes {Entrada = no.Name, Saida = w.Name }));
                }
                RePintar();
            }
        }

        private void RNADashBoard_MouseMove(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(ObjSelecionado);
            lg.RemoveAll(w => w.Entrada == ObjSelecionado.Name || w.Saida == ObjSelecionado.Name);
            RePintar();
        }

        private void RePintar()
        {
            if (lg.Count > 0)
            {
                pictureBox1.Image = null;
                var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                var gr = Graphics.FromImage(bmp);
               
                lg.ForEach(w =>
                {
                    var Entrada = Controls.Find(w.Entrada, true).FirstOrDefault() as UserControl;
                    var Saida = Controls.Find(w.Saida, true).FirstOrDefault() as UserControl;

                    gr.DrawLine(new Pen(Color.Black), Entrada.Location.X,
                        Entrada.Location.Y + hobj/2,
                        Saida.Location.X + wobj, Saida.Location.Y + hobj/2);

                    gr.DrawLine(new Pen(Color.Black), Entrada.Location.X,
                        Entrada.Location.Y + hobj/2,
                        Entrada.Location.X - 5, (Entrada.Location.Y + hobj/2) - 5);
                    gr.DrawLine(new Pen(Color.Black), Entrada.Location.X,
                        Entrada.Location.Y + hobj/2,
                        Entrada.Location.X - 5, (Entrada.Location.Y + hobj/2) + 5);
                });

                pictureBox1.Image = bmp;
            }
        }

        private void ligarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NovaLigacao = new Ligacoes();
            NovaLigacao.Entrada = ObjSelecionado.Name;
            Estado = "Selecione o No a Ser Ligado";
            foreach (var ct in Controls.Cast<object>().Where(ct => !(ct.GetType() == typeof (PictureBox))))
            {
                RemoveClickEvent((ct as UserControl));
                (ct as UserControl).Click += LigarNo;
            }
        }

        private void LigarNo(object sender, EventArgs e)
        {
            NovaLigacao.Saida = (sender as UserControl).Name;
            lg.Add(NovaLigacao);
            RePintar();
            foreach (var ct in Controls.Cast<object>().Where(ct => !(ct.GetType() == typeof (PictureBox))))
            {
                RemoveClickEvent((ct as UserControl));
                (ct as UserControl).Click += ClickObj;
            }
            Estado = "";
        }

        private void RemoveClickEvent(UserControl b)
        {
            var f1 = typeof (Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            var obj = f1.GetValue(b);
            var pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var list = (EventHandlerList) pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }

        private void RNADashBoard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (decimal) Keys.Escape) return;
            NovaLigacao = null;
            foreach (var ct in Controls)
            {
                if (!(ct.GetType() == typeof (PictureBox)))
                {
                    RemoveClickEvent((ct as UserControl));
                    (ct as UserControl).Click += ClickObj;
                }
            }
            Estado = "";
        }

        #region Eventos Para mover a Janela

        private bool mover;
        private bool movido;

        private Point inicial;
        private Point inicio;

        private void MouseDownEfect(object sender, MouseEventArgs e)
        {
            X = e.Location.X;
            Y = e.Location.Y;
            mover = true;
        }

        private void MouseUpEfect(object sender, MouseEventArgs e)
        {
            mover = false;

            if (!movido) return;
            if ((sender as UserControl).Left < 0 || (sender as UserControl).Top < 0 ||
                (sender as UserControl).Left + (sender as UserControl).Width > Width ||
                (sender as UserControl).Top + (sender as UserControl).Height > Height)
                (sender as UserControl).Location = inicio;
            else
            {
                (sender as UserControl).Location = new Point((sender as UserControl).Left + (e.X - inicial.X),
                    (sender as UserControl).Top + (e.Y - inicial.Y));
            }

            movido = false;
            //RePintar();
        }

        private void MouseMoveEfect(object sender, MouseEventArgs e)
        {
            if (!mover) return;
            (sender as UserControl).Location = new Point(
                (sender as UserControl).Left + ((e.X - wobj/2) - inicial.X),
                (sender as UserControl).Top + ((e.Y - hobj/2) - inicial.Y));
            RePintar();
        }

        #endregion
    }

    public class Ligacoes
    {
        public string Entrada { get; set; }
        public string Saida { get; set; }
    }
}