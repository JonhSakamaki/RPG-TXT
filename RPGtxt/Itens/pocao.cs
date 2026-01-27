namespace RPGtxt.Itens;

public class Pocao : Item
{
    public int PoderCura{get; private set;}

    public Pocao(string nome, int preco, int poderCura, TipoItem tipo) : base(nome, preco, TipoItem.Consumivel)
    {
        PoderCura = poderCura;
    }

    public override void ExibirDescricao()
    {
        base.ExibirDescricao();
        Console.WriteLine($"Efeito: Restaura {PoderCura} HP | Compra: {PrecoCompra}G | Venda: {PrecoVenda}G");
    }
}