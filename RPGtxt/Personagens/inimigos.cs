namespace RPGtxt.Personagens;

public class Inimigo : PersonagemBase
{
    public int XpRecompensa{ get; private set;}
    public int OuroMin{get; private set;}
    public int OuroMax {get; private set;}

    public Inimigo(string nome, int vida, int atkMin, int atkMax, int defesa, int xp,int ouMin, int ouMax) : base (nome, vida, atkMin, atkMax, defesa)
    {
        XpRecompensa = xp;
        OuroMin = ouMin;
        OuroMax = ouMax;
    }

    public int DropOuro()
    {
        return _random.Next(OuroMin, OuroMax + 1);
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine($"\nO inimigo {Nome} foi derrotado!");
        Console.WriteLine($"Xp Ganho: {XpRecompensa} ");
    }

    public void Loot()
    {
        int Sorteio = _random.Next(1, 101);

        if (Sorteio >= 97)
        {
            Console.WriteLine($"ITEM EPICO! Voce encontrou um item Lendario!");
        }
        else if (Sorteio >= 81)
        {
            Console.WriteLine($"ITEM RARO! Voce encontrou um item Azul.");
        }
        else if(Sorteio >= 51)
        {
            Console.WriteLine($"ITEM INCOMUN: Voce encontrou um item Verde.");
        }
        else
        {
            Console.WriteLine($"ITEM COMUN: Voce encontrou apenas materiais comuns");
        }

    }

}