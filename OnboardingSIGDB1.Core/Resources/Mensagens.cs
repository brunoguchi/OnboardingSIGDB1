using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Core.Resources
{
    public static class Mensagens
    {
        public const string CampoDescricao = "Descrição";
        public const string CampoNome = "Nome";
        public const string CampoCNPJ = "CNPJ";
        public const string CampoDataFundacao = "Data Fundação";
        public const string CampoCPF = "CPF";
        public const string CampoDataContratacao = "Data Contratação";
        public const string CampoCargo = "Cargo";
        public const string CampoEmpresa = "Empresa";
        public const string CampoFuncionario = "Funcionário";

        public const string Tamanho11 = "11";
        public const string Tamanho14 = "14";
        public const string Tamanho150 = "150";
        public const string Tamanho250 = "250";

        public const string CampoObrigatorio = "O campo '{0}' é obrigatório e deve ser preenchido";
        public const string CampoComTamanhoMaximo = "Tamanho do campo '{0}' não deve ultrapassar {1} caracteres";
        public const string CampoDevePossuirTamanhoSuperior = "O campo '{0}' deve possuir valor superior a {1}";
        public const string CampoInvalido = "O campo '{0}' é inválido";
        public const string CampoNaoLocalizado = "O campo '{0}' não foi localizado";
        public const string CampoJaCadastradoParaEsteValor = "Já existe um(a) {0} cadastrada com o valor {1}";
        public const string FuncionarioJaVinculadoAoACargo = "Este funcionário já está vinculado a este cargo";
        public const string VincularFuncionarioAUmaEmpresa = "Necessário vincular o funcionário a uma empresa";
        public const string DadosInválidosParaVinculoDeFuncionarioAoCargo = "Dados inválidos para vincular funcionário a um cargo";
        public const string FuncionarioJaVinculadoAEmpresa = "Este funcionário já está vinculado a uma empresa";
    }
}
