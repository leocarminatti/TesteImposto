namespace Imposto.Core.Domain
{
    public class NotaFiscalItem
    {
        public int Id { get; set; }
        public int IdNotaFiscal { get; set; }
        public string Cfop { get; set; }
        public string TipoIcms { get; set; }
        public double BaseIcms { get; set; }
        public double AliquotaIcms { get; set; }
        public double ValorIcms { get; set; }
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }
        public double BaseIpi { get; set; }
        public double AliquotaIpi { get; set; }
        public double ValorIpi { get; set; }
        public double Desconto { get; set; }

        public void AtribuirTipoIcms(string value)
        {
            TipoIcms = value;
        }

        public void AtribuirNomeProduto(string value)
        {
            NomeProduto = value;
        }

        public void AtribuirCodigoProduto(string value)
        {
            CodigoProduto = value;
        }

        public void AtribuirAliquotaIcms(double value)
        {
            AliquotaIcms = value;
        }

        public void AtribuirBaseIcms(double value)
        {
            BaseIcms = value;
        }

        public void AtribuirBaseIpi(double value)
        {
            BaseIpi = value;
        }

        public void AtribuirValorIcms(double value)
        {
            ValorIcms = value;
        }

        public void CalcularBaseIcms(double valorPedido, double value)
        {
            AliquotaIpi = valorPedido * value;
        }

        public void CalcularValorIcms(double baseIcms, double aliquotaIcms)
        {
            ValorIcms = baseIcms * aliquotaIcms;
        }

        public void CalcularAliquotaIpi(double aliquota)
        {
            AliquotaIpi = aliquota;
        }

        public void CalcularValorIpi(double baseIpi, double aliquotaIpi)
        {
            ValorIpi = baseIpi * aliquotaIpi; 
        }
    }
}
