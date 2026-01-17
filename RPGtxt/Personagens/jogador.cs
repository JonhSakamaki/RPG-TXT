namespace RPGtxt.Personagens;

public class Jogador : PersonagemBase
{
    public Jogador(string nome) : base(nome,150, 10, 20, 10)
    {
        Nivel = 1;
        ExpAtual = 0;
        ExpNecessaria = 100;
    }
    public int Nivel {get; private set;}
    public int ExpAtual{get; private set;}
    public int ExpNecessaria{get; private set;}

    public void GanharExp(int quantidade)
    {
        ExpAtual += quantidade;
        Console.WriteLine($"{Nome} ganhou {quantidade} de Experiencia! Falta {ExpNecessaria} para subir de nivel!");

        while(ExpAtual >= ExpNecessaria)
        {
            SubirDeNivel();
        }
    }

    private void SubirDeNivel()
    {
        ExpAtual -= ExpNecessaria;
        Nivel++;

        VidaMaxima += 25;
        VidaAtual = VidaMaxima;
        AtkMin += 3;
        AtkMax += 5;
        Defesa += 3;


        ExpNecessaria = (int)(ExpNecessaria * 1.7);

        Console.WriteLine($"****LEVEL UP!****");
        Console.WriteLine($"{Nome} agora esta no Nivel {Nivel}!");
        Console.WriteLine($"Status aumentados! \n Vida: {VidaMaxima} \n Ataque: {AtkMin}~{AtkMax} \n Defesa: {Defesa}");
    }

}

  

