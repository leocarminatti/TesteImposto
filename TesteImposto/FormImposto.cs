using Imposto.Core.Service;
using System;
using System.Data;
using System.Windows.Forms;
using Imposto.Core.Domain;
using System.Linq;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            PreparaTablePedidos();
            CarregarCombos();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            var table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void PreparaTablePedidos()
        {
            dataGridViewPedidos.AutoGenerateColumns = true;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void CarregarCombos()
        {
            var estados = new NotaFiscalService().CallProcEstados();

            cboOrigem.Items.AddRange(estados.Select(x => x.EstadoOrigem).Distinct().ToArray());
            cboDestino.Items.AddRange(estados.Select(x => x.EstadoDestino).Distinct().ToArray());
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    pedido.EstadoOrigem = cboOrigem.SelectedItem.ToString();
                    pedido.EstadoDestino = cboDestino.SelectedItem.ToString();
                    pedido.NomeCliente = textBoxNomeCliente.Text;

                    var table = (DataTable)dataGridViewPedidos.DataSource;

                    foreach (DataRow row in table.Rows)
                    {
                        pedido.ItensDoPedido.Add(
                            new PedidoItem()
                            {
                                Brinde = string.IsNullOrEmpty(row["Brinde"].ToString()) ? false : Convert.ToBoolean(row["Brinde"]),
                                CodigoProduto = row["Codigo do produto"].ToString(),
                                NomeProduto = row["Nome do produto"].ToString(),
                                ValorItemPedido = string.IsNullOrEmpty(row["Valor"].ToString()) ? 0 : Convert.ToDouble(row["Valor"].ToString())
                            });
                    }

                    new NotaFiscalService().GerarNotaFiscal(pedido);

                    LimparCampos();

                    MessageBox.Show("Operação efetuada com sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Operação cancelada, pelo motivo: {0}", ex.Message));
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(textBoxNomeCliente.Text))
            {
                MessageBox.Show("Campo Nome do Cliente dever ser preenchido!");
                return false;
            }
            else if(cboOrigem.SelectedItem == null)
            {
                MessageBox.Show("Campo Estado Origem dever ser preenchido!");
                return false;
            }
            else if (cboDestino.SelectedItem == null)
            {
                MessageBox.Show("Campo Estado Destino dever ser preenchido!");
                return false;
            }
            else if (dataGridViewPedidos.RowCount == 1)
            {
                var row = dataGridViewPedidos.Rows[0];

                if (row.Cells[0].Value == null && row.Cells[1].Value == null && row.Cells[2].Value == null)
                {
                    MessageBox.Show("Deve-se preencher pelo menos um pedido!");
                    return false;
                }
            }

            return true;
        }

        private void LimparCampos()
        {
            cboOrigem.SelectedIndex = -1;
            cboDestino.SelectedIndex = -1;
            textBoxNomeCliente.Text = string.Empty;
            PreparaTablePedidos();
        }
    }
}
