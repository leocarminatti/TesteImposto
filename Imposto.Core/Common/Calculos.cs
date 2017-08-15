using Imposto.Core.Service;

namespace Imposto.Core.Common
{
    public class Calculos
    {
        public static double DescontoPorOrigem(string origem)
        {
            return new NotaFiscalService().CallProcCfopPorEstado(origem);
        }
    }
}
