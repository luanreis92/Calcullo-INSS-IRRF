CREATE TABLE [dbo].[tab_irrf](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fx_sl_inicial] [decimal](18, 2) NOT NULL,
	[fx_sl_final] [decimal](18, 2) NOT NULL,
	[aliquota] [decimal](18, 2) NOT NULL,
	[deducao] [decimal](18, 2) NOT NULL,
	[ano] [int] NOT NULL,
 CONSTRAINT [PK_tab_irrf] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[tab_inss](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fx_sl_inicial] [decimal](18, 2) NOT NULL,
	[fx_sl_final] [decimal](18, 2) NOT NULL,
	[aliquota] [decimal](18, 2) NOT NULL,
	[ano] [int] default getdate('yyyy') NOT NULL,
 CONSTRAINT [PK_tab_inss] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[tab_salarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](80) NOT NULL,
	[sl_bruto] [money] NOT NULL,
	[desc_outros] [money] NULL,
	[quant_dep] [int] NULL,
	[inss] [money] NOT NULL,
	[irrf] [money] NULL,
	[sl_liq] [money] NOT NULL,
 CONSTRAINT [PK_tab_salarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]






