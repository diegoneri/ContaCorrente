using System.Globalization;

var culturaBrasileira = new CultureInfo("pt-BR");
var conta = (ContaBancaria?)null;
var executando = true;

while (executando)
{
    ExibirCabecalho();
    Console.WriteLine("Selecione uma opção abaixo:");
    Console.WriteLine();
    Console.WriteLine("0 - Abrir Conta Corrente");
    Console.WriteLine("1 - Saldo");
    Console.WriteLine("2 - Saque");
    Console.WriteLine("3 - Depósito");
    Console.WriteLine("4 - Encerrar conta e sair");
    Console.WriteLine();

    switch (LerOpcao())
    {
        case 0:
            ExibirCabecalho();
            conta = new ContaBancaria(
                LerTexto("Nome: "),
                LerDecimal("Depósito inicial: ", 0m),
                LerDecimal("Limite de cheque especial: ", 0m));

            Console.WriteLine();
            Console.WriteLine("-> Conta corrente aberta com sucesso!");
            break;
        case 1:
            if (!ContaDisponivel(conta))
            {
                break;
            }

            var contaSaldo = conta!;
            ExibirCabecalho();
            Console.WriteLine($"Cliente: {contaSaldo.Nome}.");
            Console.WriteLine($"-> Seu saldo atual é de {FormatarMoeda(contaSaldo.Saldo)}");
            break;
        case 2:
            if (!ContaDisponivel(conta))
            {
                break;
            }

            var contaSaque = conta!;
            ExibirCabecalho();
            Console.WriteLine($"Cliente: {contaSaque.Nome}.");

            var valorSaque = LerDecimal("Informe um valor para saque: ", 0.01m);
            if (!contaSaque.TentarSacar(valorSaque))
            {
                Console.WriteLine();
                Console.WriteLine("-> Seu limite atual não permite esta operação!");
                break;
            }

            Console.WriteLine();
            if (contaSaque.Saldo < 0)
            {
                Console.WriteLine("-> Você está utilizando seu cheque especial");
            }

            Console.WriteLine($"-> Seu saldo é de {FormatarMoeda(contaSaque.Saldo)}");
            break;
        case 3:
            if (!ContaDisponivel(conta))
            {
                break;
            }

            var contaDeposito = conta!;
            ExibirCabecalho();
            Console.WriteLine($"Cliente: {contaDeposito.Nome}.");
            contaDeposito.Depositar(LerDecimal("Informe um valor para depósito: ", 0.01m));
            Console.WriteLine();
            Console.WriteLine($"-> Seu saldo atual é de {FormatarMoeda(contaDeposito.Saldo)}");
            break;
        case 4:
            executando = false;
            break;
        default:
            Console.WriteLine();
            Console.WriteLine("-> Opção inválida!");
            break;
    }

    if (executando)
    {
        Console.WriteLine();
    }
}

return;

void ExibirCabecalho()
{
    Console.WriteLine("---- MongaBank - Seu dinheiro rende mais! ----");
    Console.WriteLine();
}

bool ContaDisponivel(ContaBancaria? contaAtual)
{
    if (contaAtual is not null)
    {
        return true;
    }

    Console.WriteLine("-> Abra uma conta corrente primeiro.");
    return false;
}

int LerOpcao()
{
    Console.Write("Opção: ");

    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out var opcao))
        {
            Console.WriteLine();
            return opcao;
        }

        Console.Write("Informe uma opção válida: ");
    }
}

string LerTexto(string mensagem)
{
    Console.Write(mensagem);

    while (true)
    {
        var texto = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(texto))
        {
            return texto.Trim();
        }

        Console.Write("Informe um valor válido: ");
    }
}

decimal LerDecimal(string mensagem, decimal valorMinimo)
{
    Console.Write(mensagem);

    while (true)
    {
        var entrada = Console.ReadLine();
        if (decimal.TryParse(entrada, NumberStyles.Number, culturaBrasileira, out var valor) ||
            decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out valor))
        {
            if (valor >= valorMinimo)
            {
                return valor;
            }
        }

        Console.Write("Informe um valor válido: ");
    }
}

string FormatarMoeda(decimal valor)
{
    var valorFormatado = Math.Abs(valor).ToString("C", culturaBrasileira);
    return valor < 0 ? $"- {valorFormatado}" : valorFormatado;
}

sealed class ContaBancaria
{
    public ContaBancaria(string nome, decimal saldoInicial, decimal limiteChequeEspecial)
    {
        Nome = nome;
        Saldo = saldoInicial;
        LimiteChequeEspecial = limiteChequeEspecial;
    }

    public string Nome { get; }

    public decimal Saldo { get; private set; }

    public decimal LimiteChequeEspecial { get; }

    public void Depositar(decimal valor)
    {
        Saldo += valor;
    }

    public bool TentarSacar(decimal valor)
    {
        var saldoAposSaque = Saldo - valor;
        if (saldoAposSaque < -LimiteChequeEspecial)
        {
            return false;
        }

        Saldo = saldoAposSaque;
        return true;
    }
}
