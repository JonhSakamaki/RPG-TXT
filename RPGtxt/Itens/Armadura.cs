namespace RPGtxt.Itens;

public class Armadura : Item
{
    public int DefesaExtra {get; private set;}

    public double VidaPercentual {get; private set;}

    public Armadura(string nome, int preco, int defesaExtra, double porcentagemVida, TipoItem tipoSlot) : base(nome, preco, tipoSlot)
    {
        DefesaExtra = defesaExtra;
        VidaPercentual = porcentagemVida / 100.0;
    }

    public override void ExibirDescricao()
    {
        base.ExibirDescricao();

        Console.WriteLine($"Atributos: +{DefesaExtra} DEF | +{VidaPercentual * 100}% HP | Compra: {PrecoCompra}G | Venda: {PrecoVenda}G");
    }
}