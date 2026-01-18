namespace RPGtxt.Personagens;

public abstract class PersonagemBase{
    
    protected static readonly Random _random = new Random();

    public string Nome {get; protected set;}
    public int VidaAtual {get; protected set;}
    public int VidaMaxima {get; protected set;}
    public int AtkMin {get; protected set;}
    public int AtkMax {get; protected set;}
    public int Defesa {get; protected set;}
   
    public PersonagemBase(string nome, int vida, int atkMin, int atkMax, int defesa)
    {
       Nome = nome;
       VidaMaxima = vida;
       VidaAtual = vida;
       AtkMin = atkMin;
       AtkMax = atkMax;
       Defesa = defesa; 
    }


    public void ReceberDano(int danoRecebido)
    {
        int danoFinal = danoRecebido - Defesa;

        if(danoFinal < 0)
        {
            danoFinal = 0;
        }

        VidaAtual -= danoFinal;

        if(VidaAtual < 0)
        {
            VidaAtual = 0;
        }

        Console.WriteLine($"{Nome} Recebeu {danoFinal} de dano! \n Vida restante: {VidaAtual}");
    }

    public virtual int CalcularDano()
    {
        return _random.Next(AtkMin, AtkMax + 1);
    }

}

