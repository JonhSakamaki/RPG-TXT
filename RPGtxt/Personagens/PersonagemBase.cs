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
       AtkMin = atkMin;
       AtkMax = atkMax;
       Defesa = defesa; 
    }

}

