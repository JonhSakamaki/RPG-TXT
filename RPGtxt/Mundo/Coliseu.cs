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
        Console.WriteLine($"\n *** INICIANDO COMBATE: {inimigo.Nome} ***");

        while (jogador.VidaAtual > 0  && inimigo.VidaAtual > 0)
        {
            int danoPlayer = jogador.CalcularDano();
            inimigo.ReceberDano(danoPlayer);

            if(inimigo.VidaAtual <= 0) break;

            int danoInimigo = inimigo.CalcularDano();
            jogador.ReceberDano(danoInimigo);

            Thread.Sleep(600);
        }
        if (jogador.VidaAtual > 0)
        {
            Console.WriteLine($"\n VITORIA! {inimigo.Nome} foi derrotado.");
            jogador.GanharExp(inimigo.XpRecompensa);
           
            int ouroGanho = inimigo.DropOuro();

            Console.WriteLine($"Recompensa da partida: {ouroGanho}G recebidos!");
        }
    }
}