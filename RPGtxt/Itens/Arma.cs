using System.Diagnostics.Contracts;

namespace RPGtxt.Itens;

public class Arma : Item
{
    public int DanoMIn{get; private set; }
    public int DanoMax{get; private set;}

    public Arma(string nome, int precoCompra, int danoMin, int danoMax) : base(nome, precoCompra, TipoItem.Arma)
    {
        DanoMIn = danoMin;
        DanoMax = danoMax;
    }

    public override void ExibirDescricao()
    {
        base.ExibirDescricao();
        Console.WriteLine($"Atributo: {DanoMIn}~{DanoMax} ATK | Compra: {PrecoCompra}G | Venda: {PrecoVenda}G ");

    }
}