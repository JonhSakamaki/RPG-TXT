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
        Console.WriteLine("\n╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║             VITRINE DO MERCADOR                      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");

        for (int i = 0; i < Estoque.Count; i++)
        {

            var item = Estoque[i];

            if (item is Arma) Console.ForegroundColor = ConsoleColor.Cyan;
            else if (item is Armadura) Console.ForegroundColor = ConsoleColor.Magenta;
            else Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write($"{i + 1}. {item.Nome.PadRight(25)}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"| Preco: {item.PrecoCompra}G");
            Console.ResetColor();   
        }

        Console.WriteLine("0. Sair da Loja");
        Console.WriteLine("════════════════════════════════════════════════════════");
    }

    public void ComprarItem(int escolha, Jogador jogador)
    {
        int indice = escolha - 1;

        if (indice < 0 || indice >= Estoque.Count)
        {
            Console.WriteLine("Opcao invalida! Escolha um item que esta na vitrine.");
            Thread.Sleep(1000);
            return;
        }

        Item itemDesejado = Estoque[indice];

        if(jogador.Ouro >= itemDesejado.PrecoCompra)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n [MERCADOR] Voce Comprou: {itemDesejado.Nome} por {itemDesejado.PrecoCompra}G.");
            Console.ResetColor();
            jogador.Pagar(itemDesejado.PrecoCompra);
            jogador.AddInventario(itemDesejado);
            
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[MERCADOR] Ouro insuficiente! Volte para o Coliseu e lute!");
            Console.ResetColor();
        }
        Console.WriteLine("Pressione uma tecla para continuar...");
        Console.ReadKey();

    }

    public void VenderItem(int indiceNoInventario, Jogador jogador)
    {
        if (indiceNoInventario < 0 || indiceNoInventario >= jogador.Inventario.Count)
        {
            Console.WriteLine("Voce nao tem este item!");
            return;
        }

        Item itemParaVender = jogador.Inventario[indiceNoInventario];
        int valorVenda = itemParaVender.PrecoCompra / 2;

        jogador.GanharOuro(valorVenda);
        jogador.Inventario.RemoveAt(indiceNoInventario);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n[MERCADOR] Voce vendeu {itemParaVender.Nome} por {valorVenda}G");
        Console.ResetColor();
    }

    public void Interagir(Jogador jogador)
    {
        bool saindo = false;

        while (!saindo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.Write("║  MERCADO | CLIENTE: ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(jogador.Nome.ToUpper().PadRight(8));
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("                        ║");
            Console.Write("║  SEU OURO: ");
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"{jogador.Ouro}G".PadRight(8));
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("                                ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();
            

            Console.WriteLine("1. Ver Itens para Comprar");
            Console.WriteLine("2. Vender Meus Itens (50% do valor)");
            Console.WriteLine("0. Sair da Loja");
            Console.Write("\nEscolha uma opcao: ");

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
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n[MERCADOR]: Boa sorte na arena, Guerreiro!");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    break;

                default:
                    Console.WriteLine("Opcao invalida!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}