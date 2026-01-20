namespace RPGtxt.Itens;

public abstract class Item
{
    public string Nome {get; protected set;}
    public int PrecoCompra {get; protected set;}

    public int PrecoVenda => PrecoCompra/2;

    public Item(string nome, int precoCompra)
    {
        Nome = nome;
        PrecoCompra = precoCompra;
    }

    public virtual void ExibirDescricao()
    {
        Console.Write($"{Nome.PadRight(20)} | ");
    }

}