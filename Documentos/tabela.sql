CREATE TABLE Atuacao (
	IDsetor int not null,
	setor varchar(100),
	PRIMARY KEY (IDsetor)
)

CREATE TABLE Funcionaria (
	IDfunc int not null IDENTITY,
	IDsetor int not null,
	nome varchar (100) not null,	
	senha varchar(20) not null,
	PRIMARY KEY (IDfunc),
	FOREIGN KEY (IDsetor) references Atuacao(IDsetor)
)

CREATE TABLE Resposta (
	IDresposta int not null IDENTITY,
	IDfuncAvaliadora int not null,
	IDsetorAvaliadora int not null,
	IDfundAvaliada int not null,
	IDsetorAvaliada int not null,
	pergunta varchar(max),
	resposta varchar(100),
	dataHoraResposta datetime,
	FOREIGN KEY (IDfuncAvaliadora) references Funcionaria(IDfunc),
	FOREIGN KEY (IDsetorAvaliadora) references Atuacao(IDsetor),
	FOREIGN KEY (IDfundAvaliada) references Funcionaria(IDfunc),
	FOREIGN KEY (IDsetorAvaliada) references Atuacao(IDsetor)
)

insert into Atuacao (IDsetor, setor) values (1, 'Cozinha')
insert into Atuacao (IDsetor, setor) values (2, 'Enfermagem')
insert into Atuacao (IDsetor, setor) values (3, 'Serviços gerais')
insert into Atuacao (IDsetor, setor) values (4, 'Técnica')
insert into Atuacao (IDsetor, setor) values (5, 'Outros')

insert into Funcionaria (nome, IDsetor, senha) values ('ALDENIRA PEREIRA MORAES', 1, '0')
insert into Funcionaria (nome, IDsetor, senha) values ('ANA LUCIA FERREIRA DOS SANTOS', 1, '1')
insert into Funcionaria (nome, IDsetor, senha) values ('CAROLINE MARCILENE BARREIRA DE PIERI', 2, '5')
insert into Funcionaria (nome, IDsetor, senha) values ('DAIANY RENATA THOMAZINI', 2, '6')
insert into Funcionaria (nome, IDsetor, senha) values ('FRANCISCA MARIA DE JESUS SANTOS', 3, '16')
insert into Funcionaria (nome, IDsetor, senha) values ('RENATA DOMINGUES', 3, '17')
insert into Funcionaria (nome, IDsetor, senha) values ('JULIANA PINARELLI DE CURTIS', 4, '23')
insert into Funcionaria (nome, IDsetor, senha) values ('MARCELA CRISTINA BARBERA', 4, '24')
insert into Funcionaria (nome, IDsetor, senha) values ('MARIANA SINICIATO HENRIQUES', 4, '25')
insert into Funcionaria (nome, IDsetor, senha) values ('TAMIRES DE ANGELO CANDIDO', 4, '27')
insert into Funcionaria (nome, IDsetor, senha) values ('FERNANDA DE OLIVEIRA VIEIRA', 5, '29')
insert into Funcionaria (nome, IDsetor, senha) values ('VANILDE MARTINELI DE OLIVEIRA CAMARGO', 5, '30')




