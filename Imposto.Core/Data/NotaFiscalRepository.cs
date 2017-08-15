using Dapper;
using Imposto.Core.Domain;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        private readonly string _strConn;

        public NotaFiscalRepository()
        {
            _strConn = ConfigurationManager.ConnectionStrings["DBTesteImposto"].ToString();
        }

        public int ProcNotaFiscal(NotaFiscal nf)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                var parametros = new {
                    pId = nf.Id,
                    pNumeroNotaFiscal = nf.NumeroNotaFiscal,
                    pSerie = nf.Serie,
                    pNomeCliente = nf.NomeCliente,
                    pEstadoDestino = nf.EstadoDestino,
                    pEstadoOrigem = nf.EstadoOrigem
                };

                return Convert.ToInt32(conn.ExecuteScalar("P_NOTA_FISCAL", parametros, commandType: CommandType.StoredProcedure));
            }
        }

        public void ProcNotaFiscalItem(int idNotaFiscal, ICollection<NotaFiscalItem> nfItens)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                var parametros = nfItens.Select(x => new {
                    pId = x.Id,
                    pIdNotaFiscal = idNotaFiscal,
                    pCfop = x.Cfop,
                    pTipoIcms = x.TipoIcms,
                    pBaseIcms = x.BaseIcms,
                    pAliquotaIcms = x.AliquotaIcms,
                    pValorIcms = x.ValorIcms,
                    pNomeProduto = x.NomeProduto,
                    pCodigoProduto = x.CodigoProduto,
                    pBaseIpi = x.BaseIpi,
                    pAliquotaIpi = x.AliquotaIpi,
                    pValorIpi = x.ValorIpi,
                    pDesconto = x.Desconto
                });

                var products = conn.Execute("P_NOTA_FISCAL_ITEM", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        public ICollection<ReportCfop> ProcCfop()
        {
            using (var conn = new SqlConnection(_strConn))
            {
                return conn.Query<ReportCfop>("P_REPORT_CFOP", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public string ProcCfopPorEstado(string origem, string destino)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                return conn.Query<string>("P_CFOP_POR_ESTADOS", new { @pEstadoDestino = destino, @pEstadoOrigem = origem }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public ICollection<Estados> ProcEstados()
        {
            using (var conn = new SqlConnection(_strConn))
            {
                return conn.Query<Estados>("P_ESTADOS", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public double ProcDescontoPorEstado(string origem)
        {
            using (var conn = new SqlConnection(_strConn))
            {
                return conn.Query<double>("P_DESCONTO_POR_ESTADOS", new { @pEstadoOrigem = origem }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}