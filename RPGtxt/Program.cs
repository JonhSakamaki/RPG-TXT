using System;
using RPGtxt.Personagens;
using RPGtxt.Logica;
using RPGtxt.Itens;

Console.Clear();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(@"
  ██████╗ ██╗███████╗███████╗     ██████╗ ███████╗
  ██╔══██╗██║██╔════╝██╔════╝    ██╔═══██╗██╔════╝
  ██████╔╝██║███████╗█████╗      ██║   ██║█████╗  
  ██╔══██╗██║╚════██║██╔══╝      ██║   ██║██╔══╝  
  ██║  ██║██║███████║███████╗    ╚██████╔╝██║     
  ╚═╝  ╚═╝╚═╝╚══════╝╚══════╝     ╚═════╝ ╚═╝     
                                                  
  ████████╗██╗  ██╗███████╗    ██╗    ██╗ █████╗ ██████╗ ██████╗ ██╗ ██████╗ ██████╗ 
  ╚══██╔══╝██║  ██║██╔════╝    ██║    ██║██╔══██╗██╔══██╗██╔══██╗██║██╔═══██╗██╔══██╗
     ██║   ███████║█████╗      ██║ █╗ ██║███████║██████╔╝██████╔╝██║██║   ██║██████╔╝
     ██║   ██╔══██║██╔══╝      ██║███╗██║██╔══██╗██╔══██╗██╔══██╗██║██║   ██║██╔══██╗
     ██║   ██║  ██║███████╗    ╚███╔███╔╝██║  ██║██║  ██║██║  ██║██║╚██████╔╝██║  ██║
     ╚═╝   ╚═╝  ╚═╝╚══════╝     ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═╝  ╚═╝
");

Console.ForegroundColor = ConsoleColor.DarkGray;
Console.WriteLine("          ~ O destino de um herói começa aqui ~          ");
Console.WriteLine("---------------------------------------------------------");
Console.ResetColor();

Console.Write("\nDigite seu nome, Bravo Guerreiro: ");
string nomeEntrada = Console.ReadLine()!;
if (string.IsNullOrWhiteSpace(nomeEntrada)) nomeEntrada = "Estrangeiro";

Jogador meuJogador = new Jogador(nomeEntrada);

Coliseu arena = new Coliseu();
Loja mercante = new Loja();

Console.WriteLine($"\nPrepare-se, {meuJogador.Nome}. A arena te espera!");
Console.WriteLine("Pressione qualquer tecla para começar...");
Console.ReadKey();

bool jogoRun = true;

while (jogoRun)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╔══════════════════════════════════════════════╗");
    Console.Write("║  STATUS: "); 
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(meuJogador.Nome.ToUpper().PadRight(12)); 
    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("                        ║");

    Console.Write("║  HP: "); 
    Console.ForegroundColor = ConsoleColor.Red; Console.Write($"{meuJogador.VidaAtual}/{meuJogador.VidaMaxima}".PadRight(10));
    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" | OURO: ");
    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"{meuJogador.Ouro}G".PadRight(10));
    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("           ║");

    Console.Write("║  ARMA: ");
    Console.ForegroundColor = ConsoleColor.Cyan; 
    string armaNome = meuJogador.ArmaEquipada?.Nome ?? "Punhos Nuus";
    Console.Write(armaNome.PadRight(30));
    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("        ║");

    Console.WriteLine("╚══════════════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine("\nO QUE VOCÊ DESEJA FAZER AGORA?");
    Console.WriteLine("1. Entrar no Coliseu (Batalhar)");
    Console.WriteLine("2. Visitar a Loja (Mercado)");
    Console.WriteLine("3. Ver Inventario (Equipar/Olhar)");
    Console.WriteLine("0. Sair do jogo");
    Console.WriteLine("--------------------------------------");
    Console.Write("Escolha uma opcao: ");

    string entrada = Console.ReadLine() ?? "";

    switch (entrada)
    {
        case "1":
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nVoce caminha em direcao ao barulho da multidao na arena...");
            Console.ResetColor();
            Console.ReadKey();
            arena.IniciarBatalha(meuJogador);  

            if (meuJogador.VidaAtual <= 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("########################################");
                Console.WriteLine("#                                      #");
                Console.WriteLine("#           VOCÊ FOI DERROTADO!        #");
                Console.WriteLine("#      Sua lenda termina aqui...       #");
                Console.WriteLine("#                                      #");
                Console.WriteLine("########################################");
                Console.ResetColor();
                Console.WriteLine("\nPressione qualquer tecla para fechar o jogo.");
                Console.ReadKey();
                jogoRun = false;
            }
             break;
        case "2":
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nVoce entra na tenda perfumada do mercador...");
            Thread.Sleep(1200);
            Console.ResetColor();
            mercante.Interagir(meuJogador);
            break;
        case "3":
            bool noInventario = true;
            while (noInventario)
            {
                Console.Clear();
                meuJogador.ExibirInventario();
                Console.WriteLine("\n--- STATUS ATUAIS ---");
                Console.WriteLine($"Ataque: {meuJogador.AtkMin}~{meuJogador.AtkMax} | Defesa Total: {meuJogador.DefesaTotal}");
                 Console.WriteLine("\n[Dica: Digite o numero do item para EQUIPAR ou '0' para voltar]");
                Console.Write("Escolha: ");

                string ent = Console.ReadLine() ?? "";

                if (ent == "0")
            {
                
                noInventario = false;
                
            }
            else if (int.TryParse(ent, out int indiceEquipar))
                {
                    meuJogador.EquiparItem(indiceEquipar - 1);
                }
                else
                {
                    Console.WriteLine("Opcao invalida!");
                    Thread.Sleep(800);
                }               
            }                    
            break;
        case "0":
            jogoRun = false;
            Console.WriteLine("\nObrigado por jogar Rise of the Warrior!");
            break;
        default:
            Console.WriteLine("\nOpcao invalida! O guerreiro esta confuso.");
            Thread.Sleep(1500);
            break;
    }
}