using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imposto.Teste
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalcularValorIpi()
        {
            var baseIpi = 50;
            var aliquotaIpi = 0.2;

            var result = baseIpi * aliquotaIpi;

            Assert.AreEqual(result, 10);
        }
    }
}
