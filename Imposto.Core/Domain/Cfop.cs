namespace Imposto.Core.Domain
{
    public class ReportCfop
    {
        public string Cfop { get; set; }
        public double ValorTotalBaseICMS { get; set; }
        public double ValorTotalICMS { get; set; }
        public double ValorTotalBaseIPI { get; set; }
        public double ValorTotalIPI { get; set; }
    }
}
