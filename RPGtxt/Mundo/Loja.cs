using RPGtxt.Itens;
using RPGtxt.Personagens;

namespace RPGtxt.Logica;

public class Loja
{
    public List<Item> Estoque {get; private set;}

    public Loja()
    {
        Estoque = new List<Item>();
        InicializarEstoque();
    }

    private void InicializarEstoque()
    {
        Estoque.Add(new Arma("Espada de Ferro", 25, 4, 10));
        Estoque.Add(new Arma("Lanca de ferro", 20, 3, 8));
        Estoque.Add(new Arma("Bastao de Madeira", 9, 1, 6));

        Estoque.Add(new Armadura("Capacete de Couro", 15, 3, 0.02, TipoItem.Capacete));
        Estoque.Add(new Armadura("Peitoral de Couro", 20, 6, 0.02, TipoItem.Peitoral));
        Estoque.Add(new Armadura("Calca de Couro", 15, 4, 0.02, TipoItem.Pernas));
        Estoque.Add(new Armadura("Luvas de Couro", 10, 2, 0.02, TipoItem.Luva));
        

        Estoque.Add(new Pocao("Pocao de cura", 30, 40,  TipoItem.Consumivel));
    }

    public void MostrarMenu()
    {
        Console.WriteLine("\n========== BEM VINDO A LOJA DO COLISEU ==========");
        Console.WriteLine("O que voce deseja comprar?");

        for (int i = 0; i < Estoque.Count; i++)
        {
            Console.Write($"{i + 1}.");
            Estoque[i].ExibirDescricao();
        }

        Console.WriteLine("0. Sair da Loja");
        Console.WriteLine("=================================================");
    }

    public void ComprarItem(int escolha, Jogador jogador)
    {
        int indice = escolha - 1;

        if (indice < 0 || indice >= Estoque.Count)
        {
            Console.WriteLine("Opcao invalida! Escolha um item que esta na vitrine.");
            return;
        }

        Item itemDesejado = Estoque[indice];

        if(jogador.Ouro >= itemDesejado.PrecoCompra)
        {
            Console.WriteLine($"\n [LOJA] Voce Comprou: {itemDesejado.Nome} por {itemDesejado.PrecoCompra}G.");
            jogador.Pagar(itemDesejado.PrecoCompra);
            jogador.AddInventario(itemDesejado);
        }
        else
        {
            Console.WriteLine($"\n[Loja] Ouro insuficiente! Volte para o Coliseu e lute!");
        }

    }

    public void VenderItem(int indiceNoInventario, Jogador jogador)
    {
        if (indiceNoInventario < 0 || indiceNoInventario >= jogador.Inventario.Count)
        {
            Console.WriteLine("Voce nao tem este item no seu inventario!");
            return;
        }

        Item itemParaVender = jogador.Inventario[indiceNoInventario];

        int valorVenda = itemParaVender.PrecoCompra / 2;

        jogador.GanharOuro(valorVenda);
        Console.WriteLine($"\n[LOJA] Voce vendeu {itemParaVender.Nome} por {valorVenda}G");
    }

    public void Interagir(Jogador jogador)
    {
        bool saindo = false;

        while (!saindo)
        {
            Console.Clear();
            Console.WriteLine($"-_-_-_-_- LOJA DE EQUIPAMENTOS -_-_-_-_-");
            Console.WriteLine($"Seu Ouro: {jogador.Ouro}G");
            Console.WriteLine("1. Ver Itens para Comorar");
            Console.WriteLine("2. Vender Meus Itens (50% do valor)");
            Console.WriteLine("0. Sair da Loja");
            Console.Write("Escolha uma opcao: ");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    this.MostrarMenu();
                    Console.Write("Digite o numero do item para comprar (ou 0 para voltar): ");
                    if (int.TryParse(Console.ReadLine(), out int escolhaCompra)&& escolhaCompra != 0)
                    {
                        this.ComprarItem(escolhaCompra, jogador);
                        Console.ReadKey();                       
                    }
                    break;
                case "2":
                    jogador.ExibirInventario();
                    Console.Write("Digite o numero do item para vender (ou 0 para voltar): ");
                    if(int.TryParse(Console.ReadLine(), out int escoolhaVenda) && escoolhaVenda != 0)
                    {
                        this.VenderItem(escoolhaVenda - 1, jogador);
                        Console.ReadKey();
                    } 
                    break;
                case "0":
                    saindo = true;
                    Console.WriteLine("Obrigado pela visita! Volte sempre.");
                    break;

                default:
                    Console.WriteLine("Opcao invalida!");
                    Thread.Sleep(1000);
                    break;

            }

        }
    }

}