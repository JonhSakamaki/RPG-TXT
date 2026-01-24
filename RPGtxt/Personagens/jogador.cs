using RPGtxt.Itens;

namespace RPGtxt.Personagens;

public class Jogador : PersonagemBase
{
    public Jogador(string nome) : base(nome,150, 10, 20, 10)
    {
        Nivel = 1;
        ExpAtual = 0;
        ExpNecessaria = 100;
        Ouro = 100;
    }
    public int Nivel {get; private set;}
    public int ExpAtual{get; private set;}
    public int ExpNecessaria{get; private set;}

    public List<Item> Inventario {get; private set;} = new List<Item>();

    public Arma? ArmaEquipada {get; private set;}
    public Armadura? Capacete {get; private set;}
    public Armadura? Peitoral {get; private set;}
    public Armadura? Luva {get; private set;}
    public Armadura? Pernas {get; private set;}
    public Armadura? Escudo {get; private set;}

    public int Ouro {get; private set;}


    public void GanharExp(int quantidade)
    {
        ExpAtual += quantidade;
        Console.WriteLine($"{Nome} ganhou {quantidade} de Experiencia! Falta {ExpNecessaria} para subir de nivel!");

        while(ExpAtual >= ExpNecessaria)
        {
            SubirDeNivel();
        }
    }

    public void AddInventario(Item itemRecebido)
    {
        if(itemRecebido.Tipo == TipoItem.Consumivel)
        {
            Item? itemExistente = Inventario.Find(i => i.Nome == itemRecebido.Nome);

            if(itemExistente != null)
            {
                itemExistente.Quantidade += 1;
                Console.WriteLine($"Voce ja tinha {itemRecebido.Nome}. Agora tem {itemExistente.Quantidade}!");
                return;
            }
        }
        Inventario.Add(itemRecebido);
        Console.WriteLine($"{itemRecebido.Nome} adicionado ao inventario!");
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

    public override int CalcularDano()
    {
        int minFInal = AtkMin;
        int maxFinal = AtkMax;

        if (ArmaEquipada != null)
        {
            minFInal += ArmaEquipada.DanoMIn;
            maxFinal += ArmaEquipada.DanoMax;
        }
        return _random.Next(minFInal, maxFinal + 1);
    }

    public int DefesaTotal
    {
        get
        {
            int total = Defesa;
            if (Capacete != null) total += Capacete.DefesaExtra;
            if(Peitoral != null) total += Peitoral.DefesaExtra;
            if(Pernas != null) total += Pernas.DefesaExtra;
            if(Luva != null) total += Luva.DefesaExtra;
            if(Escudo != null) total += Escudo.DefesaExtra;
            return total;
        }
    }

public int VidaMaxItens
    {
        get
        {
            double bTotal = 0;
            if(Capacete != null) bTotal += Capacete.VidaPercentual;
            if(Peitoral != null) bTotal += Peitoral.VidaPercentual;
            if(Pernas != null) bTotal += Pernas.VidaPercentual;
            if(Luva != null) bTotal += Luva.VidaPercentual;
            return(int)(VidaMaxima * (1 + bTotal));
        }
    }

    public override void ReceberDano(int danoInimigo)
    {
        int danoFinal = danoInimigo - DefesaTotal;

        if(danoFinal <0) danoFinal = 0;

        VidaAtual -= danoFinal;

        Console.WriteLine($"{Nome} absorveu o impacto! Dano Recebido: {danoFinal}.");
    }

}