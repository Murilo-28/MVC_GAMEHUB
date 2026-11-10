
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory]
            PRIMARY KEY ([MigrationId])
    );
END;

BEGIN TRANSACTION;

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Senha] nvarchar(max) NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Perfil] nvarchar(max) NOT NULL,
    [Telefone] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

CREATE TABLE [Jogos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Categoria] nvarchar(max) NULL,
    [ImagemUrl] nvarchar(max) NULL,
    [Preco] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Jogos] PRIMARY KEY ([Id])
);

CREATE TABLE [Pedidos] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioNome] nvarchar(max) NOT NULL,
    [FormaPagamento] nvarchar(max) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [DataPedido] datetime2 NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id])
);

CREATE TABLE [PedidoItens] (
    [Id] int NOT NULL IDENTITY,
    [PedidoId] int NOT NULL,
    [JogoNome] nvarchar(max) NOT NULL,
    [JogoImagemUrl] nvarchar(max) NULL,
    [Preco] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_PedidoItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItens_Pedidos_PedidoId]
        FOREIGN KEY ([PedidoId])
        REFERENCES [Pedidos] ([Id])
        ON DELETE CASCADE
);

CREATE INDEX [IX_PedidoItens_PedidoId]
ON [PedidoItens] ([PedidoId]);

INSERT INTO [__EFMigrationsHistory]
    ([MigrationId], [ProductVersion])
VALUES
    (N'20260618235320_Cadastro', N'9.0.15'),
    (N'20260619004620_Inicial', N'9.0.15'),
    (N'20260622225309_InitialCreate', N'9.0.15'),
    (N'20260623000530_atulizacaoPrincipal', N'9.0.15'),
    (N'20260623230022_mudancaNaModelDosJogos', N'9.0.15'),
    (N'20260623230338_mundacasDNV', N'9.0.15'),
    (N'20260623234755_MudancasDeNovo', N'9.0.15'),
    (N'20260630000859_LojaQuaseCompleta', N'9.0.15'),
    (N'20260630221346_mudancasNaLojaTWO', N'9.0.15'),
    (N'20260630222115_AdicionarPedidos', N'9.0.15'),
    (N'20260702223859_AdicionarUsuarioIdNoPedido', N'9.0.15');

COMMIT;