using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Core.Extensions
{
    public static class FormatadoresDeStringExtension
    {
        public static string RemoverFormatacaoDocumento(this string valor)
        {
            return !string.IsNullOrEmpty(valor) ? valor.Replace(".", "").Replace("-", "").Replace("/", "") : string.Empty;
        }

        public static string AplicarFormatacaoCNPJ(this string documento)
        {
            return !string.IsNullOrEmpty(documento) ? Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00") : string.Empty;
        }

        public static string AplicarFormatacaoCPF(this string documento)
        {
            return !string.IsNullOrEmpty(documento) ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : string.Empty;
        }
    }
}
