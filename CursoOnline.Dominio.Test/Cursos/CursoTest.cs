using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
  public class CursoTest : IDisposable
  {
    private readonly ITestOutputHelper _outputHelper;
    private readonly string _nome;
    private readonly double _cargaHoraria;
    private readonly PublicoAlvo _publicoAlvo;
    private readonly double _valor;
    private readonly string _descricao;

    public CursoTest(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
      _outputHelper.WriteLine("Construtor sendo executado!");

      var faker = new Faker();

      _nome = faker.Random.Word();
      _cargaHoraria = faker.Random.Double(50, 1000);
      _publicoAlvo = PublicoAlvo.Estudante;
      _valor = faker.Random.Double(100, 1000); ;
      _descricao = faker.Lorem.Paragraph();

    }

    public void Dispose()
    {
      _outputHelper.WriteLine("Dispose sendo executado!");
    }

    [Fact]
    public void DeveCriarCurso()
    {
      var cursoEsperado = new
      {
        Nome = _nome,
        CargaHoraria = _cargaHoraria,
        PublicoAlvo = _publicoAlvo,
        Valor = _valor,
        Descricao = "Uma descrição qualquer"
      };

      var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria,
      cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

      cursoEsperado.ToExpectedObject().ShouldMatch(curso);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NãoDeveCursoTerUmNomeInvalido(string nomeInvalido)
    {
      Assert.Throws<ArgumentException>(() =>
        CursoBuilder.Novo().ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido!");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    [InlineData(-10)]
    public void NãoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
    {
      Assert.Throws<ArgumentException>(() =>
        CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem("Carga Horária inválida!");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    [InlineData(-10)]
    public void NãoDeveCursoTerUmaValorMenorQue1(double valorInvalido)
    {
      Assert.Throws<ArgumentException>(() =>
        CursoBuilder.Novo().ComValor(valorInvalido).Build()).ComMensagem("Valor inválido!");
    }
  }
}
