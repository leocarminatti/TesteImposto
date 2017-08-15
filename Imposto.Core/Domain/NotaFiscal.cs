using Imposto.Core.Common;
using Imposto.Core.Data;
using System;
using System.Collections.Generic;

namespace Imposto.Core.Domain
{
    public class NotaFiscal
    {
        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }
        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }
        
        public void EmitirNotaFiscal(Pedido pedido)
        {
            NumeroNotaFiscal = 99999;
            Serie = new Random().Next(Int32.MaxValue);
            NomeCliente = pedido.NomeCliente;
            EstadoDestino = pedido.EstadoDestino;
            EstadoOrigem = pedido.EstadoOrigem;

            foreach (var itemPedido in pedido.ItensDoPedido)
            {
                var notaFiscalItem = new NotaFiscalItem()
                {
                    Cfop = new NotaFiscalRepository().ProcCfopPorEstado(EstadoOrigem, EstadoDestino),
                    Desconto = Calculos.DescontoPorOrigem(EstadoOrigem)
                };

                if (EstadoDestino == EstadoOrigem)
                {
                    notaFiscalItem.AtribuirTipoIcms("60");
                    notaFiscalItem.AtribuirAliquotaIcms(0.18);
                }
                else
                {
                    notaFiscalItem.AtribuirTipoIcms("10");
                    notaFiscalItem.AtribuirAliquotaIcms(0.17);
                }

                if (notaFiscalItem.Cfop == "6.009")
                    notaFiscalItem.CalcularBaseIcms(itemPedido.ValorItemPedido, 0.90); //redução de base
                else
                    notaFiscalItem.AtribuirBaseIcms(itemPedido.ValorItemPedido);

                notaFiscalItem.CalcularValorIcms(notaFiscalItem.BaseIcms, notaFiscalItem.AliquotaIcms);

                if (itemPedido.Brinde)
                    notaFiscalItem.CalcularAliquotaIpi(0.10);
                else
                    notaFiscalItem.CalcularAliquotaIpi(0);

                notaFiscalItem.AtribuirBaseIpi(notaFiscalItem.BaseIcms);
                notaFiscalItem.CalcularValorIpi(notaFiscalItem.BaseIpi, notaFiscalItem.AliquotaIpi);
                notaFiscalItem.AtribuirNomeProduto(itemPedido.NomeProduto);
                notaFiscalItem.AtribuirCodigoProduto(itemPedido.CodigoProduto);

                ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        }
    }
}
