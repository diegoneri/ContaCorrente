namespace ContaCorrente;

public sealed class ContaBancaria
{
    public ContaBancaria(string nome, decimal saldoInicial, decimal limiteChequeEspecial)
    {
        Nome = (nome ?? string.Empty).Trim();

        if (Nome.Length == 0)
        {
            throw new ArgumentException("O nome do cliente é obrigatório.", nameof(nome));
        }

        if (saldoInicial < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(saldoInicial), "O depósito inicial não pode ser negativo.");
        }

        if (limiteChequeEspecial < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(limiteChequeEspecial), "O limite de cheque especial não pode ser negativo.");
        }

        Saldo = saldoInicial;
        LimiteChequeEspecial = limiteChequeEspecial;
    }

    public string Nome { get; }

    public decimal Saldo { get; private set; }

    public decimal LimiteChequeEspecial { get; }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor do depósito deve ser positivo.");
        }

        Saldo += valor;
    }

    public bool TentarSacar(decimal valor)
    {
        if (valor <= 0)
        {
            return false;
        }

        if (Saldo - valor < -LimiteChequeEspecial)
        {
            return false;
        }

        Saldo -= valor;
        return true;
    }
}
