using ContaCorrente;

const string TITULO = "---- MongaBank - Seu dinheiro rende mais! ----\n";

const string MENU = @"Selecione uma opção abaixo:

0 - Abrir Conta Corrente
1 - Saldo
2 - Saque
3 - Depósito
4 - Encerrar conta e sair

Opção: ";

ContaBancaria? conta = null;

while (true)
{
    Console.Clear();
    Console.WriteLine(TITULO);
    Console.Write(MENU);

    var opcaoInformada = (Console.ReadLine() ?? string.Empty).Trim();
    var opcao = opcaoInformada.Length == 0 ? string.Empty : opcaoInformada[..1];

    Console.WriteLine($"Opção informada: {opcao}");

    if (opcao == "0")
    {
        Console.Clear();
        Console.WriteLine(TITULO);

        try
        {
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Depósito inicial: ");
            var saldoInicial = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Limite de cheque especial: ");
            var chequeEspecial = Convert.ToDecimal(Console.ReadLine());

            conta = new ContaBancaria(nome, saldoInicial, chequeEspecial);

            Console.WriteLine($"\nBem vindo, {conta.Nome}.");
            Console.WriteLine($"Seu saldo é de {conta.Saldo:C2}");
            Console.WriteLine($"Seu limite de cheque especial é de {conta.LimiteChequeEspecial:C2}\n");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\n-> {ex.Message}");
        }
    }
    else if (conta is null)
    {
        Console.WriteLine("A abertura da conta é obrigatória!!");
    }
    else if (opcao == "1")
    {
        Console.Clear();
        Console.WriteLine(TITULO);
        Console.WriteLine($"Seu saldo é de: {conta.Saldo:C2}");
        Console.WriteLine($"Limite de cheque especial: {conta.LimiteChequeEspecial:C2}\n");
    }
    else if (opcao == "2")
    {
        Console.Clear();
        Console.WriteLine(TITULO);
        Console.WriteLine($"Cliente: {conta.Nome}.");
        Console.Write("Informe um valor para saque: ");

        var valorSaque = Convert.ToDecimal(Console.ReadLine());

        if (!conta.TentarSacar(valorSaque))
        {
            Console.WriteLine("\n-> Seu limite atual não permite esta operação!");
        }
        else
        {
            if (conta.Saldo < 0)
            {
                Console.WriteLine("\n-> Você está utilizando seu cheque especial");
            }

            Console.WriteLine($"-> Seu saldo é de {conta.Saldo:C2}");
        }
    }
    else if (opcao == "3")
    {
        Console.Clear();
        Console.WriteLine(TITULO);
        Console.WriteLine($"Cliente: {conta.Nome}.");
        Console.Write("Informe um valor para depósito: ");

        try
        {
            var valorDeposito = Convert.ToDecimal(Console.ReadLine());
            conta.Depositar(valorDeposito);
            Console.WriteLine($"\n-> Seu saldo atual é de {conta.Saldo:C2}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"\n-> {ex.Message}");
        }
    }
    else if (opcao == "4")
    {
        Console.Clear();
        Console.WriteLine(TITULO);
        Console.WriteLine("Obrigado por utilizar nossos serviços\n");
        Console.WriteLine($"Valor a receber: {conta.Saldo:C2}\n");
        break;
    }
    else
    {
        Console.WriteLine("Opção inválida!");
    }

    AguardarContinuacao();
}

static void AguardarContinuacao()
{
    Console.Write("Pressione uma tecla para continuar");

    if (!Console.IsInputRedirected)
    {
        Console.ReadKey(true);
    }
}
