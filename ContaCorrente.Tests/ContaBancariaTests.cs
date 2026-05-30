namespace ContaCorrente.Tests;

public class ContaBancariaTests
{
    [Fact]
    public void Constructor_trims_name()
    {
        var conta = new ContaBancaria("  Zé das Couves  ", 10m, 50m);

        Assert.Equal("Zé das Couves", conta.Nome);
    }

    [Fact]
    public void Constructor_rejects_blank_name()
    {
        var action = () => new ContaBancaria("   ", 10m, 50m);

        var exception = Assert.Throws<ArgumentException>(action);

        Assert.Equal("nome", exception.ParamName);
    }

    [Fact]
    public void Constructor_rejects_negative_initial_balance()
    {
        var action = () => new ContaBancaria("Maria", -0.01m, 50m);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(action);

        Assert.Equal("saldoInicial", exception.ParamName);
    }

    [Fact]
    public void Constructor_rejects_negative_overdraft_limit()
    {
        var action = () => new ContaBancaria("Maria", 0m, -0.01m);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(action);

        Assert.Equal("limiteChequeEspecial", exception.ParamName);
    }

    [Fact]
    public void Depositar_increases_balance_for_positive_values()
    {
        var conta = new ContaBancaria("Maria", 10m, 50m);

        conta.Depositar(15m);

        Assert.Equal(25m, conta.Saldo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Depositar_throws_for_zero_or_negative_values(decimal valor)
    {
        var conta = new ContaBancaria("Maria", 10m, 50m);

        var action = () => conta.Depositar(valor);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(action);

        Assert.Equal("valor", exception.ParamName);
        Assert.Equal(10m, conta.Saldo);
    }

    [Fact]
    public void TentarSacar_succeeds_within_balance()
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);

        var resultado = conta.TentarSacar(40m);

        Assert.True(resultado);
        Assert.Equal(60m, conta.Saldo);
    }

    [Fact]
    public void TentarSacar_allows_using_overdraft_up_to_configured_limit()
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);

        var resultado = conta.TentarSacar(125m);

        Assert.True(resultado);
        Assert.Equal(-25m, conta.Saldo);
    }

    [Fact]
    public void TentarSacar_fails_when_exceeding_overdraft_limit()
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);

        var resultado = conta.TentarSacar(151m);

        Assert.False(resultado);
        Assert.Equal(100m, conta.Saldo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TentarSacar_fails_for_zero_or_negative_values(decimal valor)
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);

        var resultado = conta.TentarSacar(valor);

        Assert.False(resultado);
        Assert.Equal(100m, conta.Saldo);
    }

    [Fact]
    public void TentarSacar_supports_withdrawing_to_exactly_zero()
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);

        var resultado = conta.TentarSacar(100m);

        Assert.True(resultado);
        Assert.Equal(0m, conta.Saldo);
    }

    [Fact]
    public void TentarSacar_supports_withdrawing_to_exactly_negative_overdraft_limit()
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);

        var resultado = conta.TentarSacar(150m);

        Assert.True(resultado);
        Assert.Equal(-50m, conta.Saldo);
    }

    [Fact]
    public void Deposit_after_negative_balance_updates_balance_correctly()
    {
        var conta = new ContaBancaria("Maria", 100m, 50m);
        conta.TentarSacar(125m);

        conta.Depositar(10m);

        Assert.Equal(-15m, conta.Saldo);
    }
}
