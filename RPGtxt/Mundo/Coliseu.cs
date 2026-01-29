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
        while(jogador.VidaAtual > 0 && inimigo.VidaAtual > 0)
        {
         Console.Clear();
         Console.WriteLine("========================================");
         Console.WriteLine($"   COMBATE: {jogador.Nome} vs {inimigo.Nome}");
         Console.WriteLine("========================================");
         Console.WriteLine($"{jogador.Nome} HP: {jogador.VidaAtual}/{jogador.VidaMaxima}");
         Console.WriteLine($"{inimigo.Nome} HP: {inimigo.VidaAtual}/{inimigo.VidaMaxima}");
         Console.WriteLine("========================================");   

        Console.WriteLine("\nSua Vez! O que deseja fazer? ");
        Console.WriteLine("1. Atacar com sua arma");
        Console.Write("Escolha: ");

        string acao = Console.ReadLine() ?? "";

        if (acao == "1")
            {
                Console.WriteLine($"\n> Voce avanca para atacar!");
                int danoPlayer = jogador.CalcularDano();
                inimigo.ReceberDano(danoPlayer);

                if (inimigo.VidaAtual <= 0) break;

                Console.WriteLine($"\n> {inimigo.Nome} contra-ataca!");
            int danoInimigo = inimigo.CalcularDano();
            jogador.ReceberDano(danoInimigo);

            Console.WriteLine("\nPressione qualquer tecla para o proxino turno...");
            Console.ReadKey();
            }
        else
        {
            Console.WriteLine("\nOpcao invalida! voce hesitou e perdeu sua chance.");
            Thread.Sleep(1000);

            int danoInimigo = inimigo.CalcularDano();
            jogador.ReceberDano(danoInimigo);
            Console.ReadKey();
        }
            
        }
        
       
        if (jogador.VidaAtual > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n VITORIA! {inimigo.Nome} foi derrotado.");
            Console.ResetColor();

            jogador.GanharExp(inimigo.XpRecompensa);         
           
            int ouroGanho = inimigo.DropOuro();
            jogador.GanharOuro(ouroGanho);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Recompensa da partida: {ouroGanho}G recebidos!");
            Console.ResetColor();

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n DERROTA... {jogador.Nome} caiu diante de {inimigo.Nome}.");
            Console.WriteLine("A plateia silencia enquanto seu corpo e retirado da arena...");
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