using NUnit.Framework;
using WebService.DTO;

namespace TestProject
{
    public class Tests
    {
        private ResponsavelDTO _responsavel;
        private AlunoDTO _aluno;

        [SetUp]
        public void Setup()
        {
            _responsavel = new ResponsavelDTO();
            _aluno = new AlunoDTO();
        }

        #region Testes de Responsaveis
        [Test]
        public void ResponsavelContemEmail_ReturnTrue()
        {
            _responsavel.Email = "testeEmail@gmail.com";

            var result = _responsavel.VerificaObrigatoriedadeEmail();

            Assert.IsTrue(result, "O responsavel contem e-mail");
        }

        [Test]
        public void ResponsavelNaoContemEmail_ReturnFalse()
        {
            _responsavel.Email = null;

            var result = _responsavel.VerificaObrigatoriedadeEmail();

            Assert.IsFalse(result, "O responsavel não contem e-mail");
        }
        #endregion

        #region Testes de Alunos
        // Alunos de ensino fundamental precisam ter e-mail
        [Test]
        public void AlunoFContemEmail_ReturnTrue()
        {
            _aluno.Segmento = "Fundamental";
            _aluno.Email = "testeEmail@gmail.com";

            var result = _aluno.VerificaObrigatoriedadeEmail();

            Assert.IsTrue(result, "O aluno do ensino fundamental contem e-mail");
        }

        // Alunos de ensino fundamental precisam ter e-mail
        [Test]
        public void AlunoFNaoContemEmail_ReturnFalse()
        {
            _aluno.Segmento = "Fundamental";
            _aluno.Email = ""; 

            var result = _aluno.VerificaObrigatoriedadeEmail();

            Assert.IsFalse(result, "O aluno do ensino fundamental não contem e-mail");
        }
        #endregion

    }
}