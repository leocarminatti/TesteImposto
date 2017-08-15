using Imposto.Core.Common;
using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        private readonly NotaFiscalRepository _rep;

        public NotaFiscalService()
        {
            _rep = new NotaFiscalRepository();
        }

        public void GerarNotaFiscal(Pedido pedido)
        {
            var notaFiscal = new NotaFiscal();

            try
            {
                notaFiscal.EmitirNotaFiscal(pedido);

                new XML().Save<NotaFiscal>(notaFiscal);
            }
            catch (System.Exception)
            {
                throw new Exception("Xml não gerado");
            }

            CallProcNotaFiscalItem(CallProcNotaFiscal(notaFiscal), notaFiscal.ItensDaNotaFiscal);
        }

        public int CallProcNotaFiscal(NotaFiscal nf)
        {
            return _rep.ProcNotaFiscal(nf);
        }

        public void CallProcNotaFiscalItem(int idNotaFiscal, ICollection<NotaFiscalItem> nfItens)
        {
            _rep.ProcNotaFiscalItem(idNotaFiscal, nfItens);
        }

        public string CallProcCfopPorEstado(string origem, string destino)
        {
            return _rep.ProcCfopPorEstado(origem, destino);
        }

        public ICollection<Estados> CallProcEstados()
        {
            return _rep.ProcEstados();
        }

        public double CallProcCfopPorEstado(string origem)
        {
            return _rep.ProcDescontoPorEstado(origem);
        }
    }
}
