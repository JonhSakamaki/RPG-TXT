# ⚔️ Rise of the Warrior

**Rise of the Warrior** é um RPG de aventura baseado em texto (CLI) desenvolvido em C#. O projeto foca na aplicação rigorosa de conceitos de **Programação Orientada a Objetos (POO)** e arquitetura de sistemas escalável.

---

## 🎮 O Jogo
O jogador assume o papel de um gladiador em busca de glória. Através de um ciclo de jogabilidade (*game loop*) sólido, você deve:
1.  **Batalhar no Coliseu:** Enfrentar ondas progressivas de inimigos com atributos dinâmicos.
2.  **Gerenciar Economia:** Coletar ouro de inimigos derrotados para investir em equipamentos.
3.  **Customização:** Gerenciar um inventário complexo e equipar itens que alteram seus status em tempo real.

---

## 🛠️ Destaques Técnicos (ADS)

Este projeto foi construído utilizando as melhores práticas de desenvolvimento de software:

### 1. Pilares da POO
* **Encapsulamento:** Proteção de estados internos (como vida e ouro) através de propriedades com acessores controlados (`private set`).
* **Herança e Polimorfismo:** Implementação de uma árvore de classes para itens (`Arma`, `Armadura`, `Pocao`), permitindo o tratamento genérico em listas de inventário.
* **Abstração:** Métodos de cálculo de dano e defesa que isolam a complexidade matemática da interface principal.

### 2. Lógica e Algoritmos
* **Progressão Linear:** Sistema de ondas com multiplicador matemático de dificuldade: 
    * $status_{final} = status_{base} \times (1 + (onda \times 0.2))$
* **Type Safety:** Uso de *Generics* (`List<T>`) e tratamento de exceções de entrada com `int.TryParse`.

### 3. Interface de Usuário (UX/UI)
* **CLI Estilizada:** Uso de ASCII Art para o título e molduras decorativas para consistência visual.
* **Semântica de Cores:** Feedback visual padronizado (Ciano para ataque, Magenta para defesa, Vermelho para perigo e Amarelo para economia).
* **Dashboard em Tempo Real:** Interface que exibe o estado atual do jogador (HUD) em cada transição de menu.

## 🚀 Inicialização e Execução

O projeto pode ser iniciado de duas formas, dependendo do seu objetivo:

### 1. Ambiente de Desenvolvimento (Via Terminal)
Para compilar e rodar o código-fonte diretamente (requer .NET SDK instalado):

1.  Abra o terminal na pasta raiz do projeto.
2.  Execute o comando abaixo para restaurar dependências e iniciar o sistema:
    ```bash
    dotnet run
    ```
    *O ponto de entrada principal do software reside no arquivo `Program.cs`.*

### 2. Executável Final (Versão de Produção)
Se você deseja apenas jogar ou distribuir o sistema como um software independente:

1.  Navegue até o diretório de publicação:
    `bin/Release/netX.0/win-x64/publish/`
2.  Localize e execute o arquivo: 
    **`RiseOfTheWarrior.exe`** (ou o nome definido no seu .csproj).

---

## 🧭 Fluxo de Inicialização Técnica

Ao ser iniciado, o software segue a seguinte esteira de processamento:

1.  **Bootstrapping (`Program.cs`):** Limpeza do buffer do console e renderização da ASCII Art de abertura.
2.  **Instanciação de Objetos:** O sistema solicita o input do usuário para instanciar a classe `Jogador` e inicializa as classes motoras `Coliseu` (Arena) e `Loja` (Mercado).
3.  **Game Loop:** O controle é transferido para uma estrutura de repetição `while` baseada em uma máquina de estados simples, que aguarda as entradas do usuário para navegar entre os módulos do sistema.

---

## 🎮 Download Direto
Você pode baixar a versão pronta para jogar (sem precisar compilar o código) clicando aqui: 
[Baixar Rise of the Warrior v1.0](https://github.com/JonhSakamaki/RPG-TXT/releases/tag/V1.0.0)

---

## 🏗️ Estrutura do Projeto

```text
RPGtxt/
├── Personagens/    # Classes Jogador e Inimigo (Base e Lógica de Status)
├── Itens/          # Herança de Itens (Armas, Armaduras, Consumíveis)
├── Logica/         # Motores do jogo (Coliseu e Loja)
└── Program.cs      # Ponto de entrada e Game Loop principal


