using System.Diagnostics;

string opcao = "", nome = "";
decimal saldo = 0, chequeEspecial = 0;
bool isContaCriada = false;

while (true)
{
    Console.Clear();
    
    ExibirTitulo();   
    ExibirMenu();

    opcao = Console.ReadLine()!.Trim().Substring(0,1);

    Console.WriteLine($"Opção informada: {opcao}");

    if (opcao == "0")
    {
        //1
        Console.Clear();
        ExibirTitulo(); 
        Console.Write("Nome: ");
        nome = Console.ReadLine()!;

        Console.Write("Depósito Inicial: ");
        //2
        saldo = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Cheque especial: ");
        chequeEspecial = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine($"\nBem vindo, {nome}.");
        Console.WriteLine($"Seu saldo é de {saldo:C2}");
        Console.WriteLine($"Seu limite de cheque especial é de {chequeEspecial:C2}\n");

        isContaCriada = true;
    }
    else if (!isContaCriada)
    {
        Console.WriteLine("A abertura da conta é obrigatória!!");

        Console.Write("Pressione uma tecla para continuar");
        Console.ReadKey(true);
        continue;
    }
    else 
    {
        Console.Clear();
        ExibirTitulo();
        Console.WriteLine($"Cliente: {nome}");

        //6
        if (opcao == "1"){ 
            //7
            Console.WriteLine($"Seu saldo é de: {saldo:C2}");
            Console.WriteLine($"Limite de cheque especial: {chequeEspecial:C2}");
        }
        else if (opcao == "2")
        {
            //8
            Console.Write("Informe um valor para saque: ");
            decimal valorSaque = Convert.ToDecimal(Console.ReadLine());

            bool valorEhValido = valorSaque > 0;
            bool limiteSuficiente = saldo + chequeEspecial >= valorSaque;
            bool estaUsandoLimite = valorSaque > saldo;

            if(!valorEhValido)
            {
                Console.WriteLine("-> Não foi possível realizar sua operação. Tente novamente");
            }
            else if(!limiteSuficiente)
            {
                Console.WriteLine("-> Seu limite atual não permite esta operação!");
            }
            else
            {
                if(estaUsandoLimite)
                {
                    Console.WriteLine("-> Você está utilizando seu cheque especial");
                }
                saldo -= valorSaque;
                Console.WriteLine("Saque efetuado com sucesso.");
                Console.WriteLine($"-> Seu saldo é de {saldo:C2}");
            }
        }
        else if (opcao == "3")
        {
            //9
            Console.Write("Informe um valor para depósito: ");
            decimal valorDeposito = Convert.ToDecimal(Console.ReadLine());

            bool valorEhValido = valorDeposito > 0;
            if(!valorEhValido)
            {
                Console.WriteLine("-> Não foi possível realizar sua operação. Tente novamente");
            }
            else
            {
                saldo += valorDeposito;
                Console.WriteLine("Depósito efetuado com sucesso.");
                Console.WriteLine($"-> Seu saldo é de {saldo:C2}");
            }
        }
        else if (opcao == "4")
        {
            Console.WriteLine("Obrigado por utilizar nossos serviços");
            Console.WriteLine($"-> Valor a receber: {saldo:C2}");
            break;
        }
        else
        {
            Console.WriteLine("Opção Inválida!!!");
        }

    }

    Console.Write("Pressione uma tecla para continuar");
    Console.ReadKey(true);
}

void ExibirTitulo()
{
    string TITULO = @"---- MongaBank - Seu dinheiro 
    rende mais! ----\n";   
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.White;
    Console.WriteLine(TITULO);   
    Console.ResetColor();
}

void ExibirMenu()
{
    Console.WriteLine("Selecione uma opção abaixo:\n");
    Console.BackgroundColor = ConsoleColor.White;

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("0 - Abrir Conta Corrente");

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("1 - Saldo");

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("2 - Saque");

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("3 - Depósito");
    
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("4 - Encerrar conta e sair");

    Console.ResetColor();
    Console.Write("\nOpção: ");
}


