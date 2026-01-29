using System;
using RPGtxt.Personagens;
using RPGtxt.Logica;
using RPGtxt.Itens;

Console.WriteLine("Digite seu nome Bravo Gladiador: ");
Jogador meuJogador = new Jogador(Console.ReadLine()!);

Coliseu arena = new Coliseu();
Loja mercante = new Loja();

Console.WriteLine("=== O DESPETAR DO GLADIADOR ===");
Console.WriteLine($"Bem-Vindo, {meuJogador.Nome}!");

bool jogoRun = true;

while (jogoRun)
{
    Console.Clear();
    Console.WriteLine("======================================");
    Console.Write("    SITUAÇÃO ATUAL DE  ");
    Console.ForegroundColor = ConsoleColor.Cyan; 
    Console.WriteLine(meuJogador.Nome.ToUpper());
    Console.ResetColor();

    Console.Write("    Vida: ");
    Console.ForegroundColor = ConsoleColor.Red; 
    Console.Write($"{meuJogador.VidaAtual}/{meuJogador.VidaMaxima}");
    Console.ResetColor();

    Console.Write(" | Ouro: ");
    Console.ForegroundColor = ConsoleColor.Yellow; 
    Console.Write($"{meuJogador.Ouro}G\n");
    Console.ResetColor();
    Console.WriteLine("======================================");
    
    Console.WriteLine("\nO QUE VOCE DESEJA FAZER AGORA?");
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
            Console.WriteLine("\nVoce caminha em direcao ao barulho da multidao na arena...");
            Console.ReadKey();
            arena.IniciarBatalha(meuJogador);           
            if (meuJogador.VidaAtual <= 0)
            {
                Console.Clear();
                Console.WriteLine("########################################");
                Console.WriteLine("#                                      #");
                Console.WriteLine("#           VOCÊ FOI DERROTADO!        #");
                Console.WriteLine("#      Sua lenda termina aqui...       #");
                Console.WriteLine("#                                      #");
                Console.WriteLine("########################################");
                Console.WriteLine("\nPressione qualquer tecla para fechar o jogo.");
                Console.ReadKey();
                jogoRun = false;
            }
             break;
        case "2":
            Console.WriteLine("\nVoce entra na tenda perfumada do mercador...");
            Console.ReadKey();
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
            Console.WriteLine("\nObrigado por jogar! Sua lenda sera lembrada.");
            break;
        default:
            Console.WriteLine("\nOpcao invalida! O gladiador esta confuso.");
            Thread.Sleep(1500);
            break;
    }
}