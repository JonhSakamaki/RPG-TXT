namespace RPGtxt.Itens;

public abstract class Item
{
    public string Nome {get; protected set;}
    public int PrecoCompra {get; protected set;}

    public int PrecoVenda => PrecoCompra/2;

    public TipoItem Tipo {get; private set;}

    public Item(string nome, int precoCompra, TipoItem tipo)
    {
        Nome = nome;
        PrecoCompra = precoCompra;
        Tipo = tipo;
    }

    public virtual void ExibirDescricao()
    {
        Console.Write($"[{Tipo}]{Nome.PadRight(20)} | ");
    }

}