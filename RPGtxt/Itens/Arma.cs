using System.Diagnostics.Contracts;

namespace RPGtxt.Itens;

public class Arma : Item
{
    public int DanoExtra{get; private set; }

    public Arma(string nome, int precoCompra, int danoExtra) : base(nome, precoCompra)
    {
        DanoExtra = danoExtra;
    }

    public override void ExibirDescricao()
    {
        base.ExibirDescricao();
        Console.WriteLine($"Atributo: +{DanoExtra} ATK | Compra: {PrecoCompra}G | Venda: {PrecoVenda}G ");

    }
}