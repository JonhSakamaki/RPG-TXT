using System.Threading;
using RPGtxt.Personagens;

namespace RPGtxt.Logica;

public class Coliseu
{
    public int OndaAtual {get; private set;} = 1;

    public Inimigo GerarInimigoParaOnda()
    {
        string[] tipos = {"SLime", "Goblins", "Esqueletos", "Kobolds", "Orc", "treant"};
        string tipoSorteado = tipos[new Random().Next(tipos.Length)];
        string nome = $"{tipoSorteado} da Onda {OndaAtual}";
        int vidaBase = 50;
        int atkMinBase = 8; 
        int atkMaxBase = 12;
        int defesaBase = 6;

        double multiplicador = 1 + (OndaAtual * 0.2);

        int vida = (int)(vidaBase * multiplicador);
        int atkMin = (int)(atkMinBase * multiplicador);
        int atkMax = (int)(atkMaxBase * multiplicador);
        int defesa = (int)(defesaBase * multiplicador);


        int xp = 20 + (OndaAtual * 10);
        int ouMin = 10 + (OndaAtual * 5);
        int ouMax = 20 + (OndaAtual * 8);

        return new Inimigo(nome, vida, atkMin, atkMax, defesa, xp, ouMin, ouMax);

    }

    public void IncrementarOnda()
    {
        OndaAtual++;
    }

    public void RealizarDuelo(Jogador jogador, Inimigo inimigo)
    {   
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"\n*** Os portoes se abrem... {inimigo.Nome} surge das sombras! ***");
        Console.ResetColor();
        Thread.Sleep(1000);

        while(jogador.VidaAtual > 0 && inimigo.VidaAtual > 0)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.Write("║  JOGADOR:");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(jogador.Nome.PadRight(10)); Console.ResetColor();
            Console.Write("vs  INIMIGO:");
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(inimigo.Nome.PadRight(10) + "║"); Console.ResetColor();
            Console.WriteLine($"║  HP: {jogador.VidaAtual}/{jogador.VidaMaxima}".PadRight(21) + $"| HP: {inimigo.VidaAtual}/{inimigo.VidaMaxima}".PadRight(20) + "║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine("\nSUA VEZ! [1]ATACAR");
            Console.Write("Escolha: ");
            string acao = Console.ReadLine() ?? "";

            if (acao == "1")
            {
                int danoPlayer = jogador.CalcularDano();
                inimigo.ReceberDano(danoPlayer);
                Console.Write("\n> Voce golpeia com precisao: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"-{danoPlayer} HP no inimigo");
                Console.ResetColor();

                if(inimigo.VidaAtual <= 0) break;

                Thread.Sleep(500);
                int danoInimigo = inimigo.CalcularDano();
                jogador.ReceberDano(danoInimigo);

                Console.Write($"> {inimigo.Nome} revida ferozmente: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"-{danoInimigo} de sua Vida!");
                Console.ResetColor();

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor =            ConsoleColor.Yellow;
                Console.WriteLine("\n[AVISO]: Opção inválida! O gladiador hesita no combate e perde sua chance...");
                Console.ResetColor();
                Thread.Sleep(1000); 

                int danoInimigo = inimigo.CalcularDano();
                jogador.ReceberDano(danoInimigo);
                Console.ReadKey();
            }
        }
        
        if (jogador.VidaAtual > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nVITORIA! O publico vai ao delirio com a queda do {inimigo.Nome}!");
            Console.ResetColor();

            jogador.GanharExp(inimigo.XpRecompensa);
            int ouroGanho = inimigo.DropOuro();
            jogador.GanharOuro(ouroGanho);

            Console.Write("Recompensa: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ouroGanho}G coletados do chao da arena.");
            Console.ResetColor();
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nDERROTA... {jogador.Nome} caiu. o Silencio toma conta do Coliseu.");
            Console.ResetColor();
            Console.ReadKey();
        }
         
    }

    public void IniciarBatalha(Jogador jogador)
    {
        Inimigo inimigoDaVez = GerarInimigoParaOnda();
        RealizarDuelo(jogador, inimigoDaVez);

        if (jogador.VidaAtual > 0)
        {
            IncrementarOnda();
            Console.WriteLine($"\nPrepare-se! A proxima onda sera a de numero {OndaAtual}.");
            Console.ReadKey();
        }
    }
}