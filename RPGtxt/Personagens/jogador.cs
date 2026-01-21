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

    
}