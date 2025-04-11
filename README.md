# 📖 Clube da Leitura 📖

## Demostração

![](https://i.imgur.com/EENrUU1.gif)

## Introdução

Aplicativo para gerenciamento de um **Clube de Leitura**, com controle de amigos, caixas, revistas e registros de empréstimos.

## Funcionalidades

### 🧑‍🤝‍🧑 Amigos

- **Cadastro:** Nome, nome do responsável e telefone.
- **Edição:** Atualização de dados de amigos.
- **Remoção:** Permitida apenas se não houver empréstimos vinculados.
- **Visualização:** Lista de amigos e se contém algum empréstimo.

- **Regras de Negócio:**
	- Nome: 3–100 caracteres.
	- Nome do responsável: 3–100 caracteres.
	- Telefone: Formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.
	- Não pode haver outro amigo com o mesmo nome e telefone.

### 📦 Caixas

- **Cadastro:** Etiqueta única, cor e dias de empréstimo.
- **Edição:** Atualização de dados da caixa.
- **Remoção:** Permitida apenas se não houver revistas vinculadas.
- **Visualização:** Lista completa de caixas.

- **Regras de Negócio:**
	- Etiqueta: Única, até 50 caracteres.
	- Cor: Seleção baseada em uma tabela.
	- Cada caixa define o prazo máximo para empréstimo de suas revistas.

### 📚 Revistas

- **Cadastro:** Título, número da edição, ano de publicação e caixa associada.
- **Edição:** Atualização das informações.
- **Remoção:** Exclusão de revistas do sistema.
- **Visualização:** Lista com status (Disponível, Emprestada, Reservada).

- **Regras de Negócio:**
	- Título: 2–100 caracteres.
	- Número da edição: Tem que ser um número positivo.
	- Ano de publicação: Não pode ser uma data no futuro.
	- Caixa: Obrigatória uma caixa válida.
	- Título + Num. Edição devem ser únicos.
	- Status inicial: Disponível.


### 🔄 Empréstimos

- **Cadastro:** Amigo, revista disponível, data de empréstimo automática e data de devolução baseada na caixa do livro.
- **Devolução:** Registro de devolução de revista.
- **Visualização:** Empréstimos abertos, concluídos e atrasados.

- **Regras de Negócio:**
	- Apenas um empréstimo ativo por amigo.
	- Status: Aberto, Concluído, Atrasado.
	- Empréstimos atrasados são destacados.
	- Data de devolução = data do empréstimo + dias da caixa.

## Como Utilizar

#### Clone o Repositório
```
git clone https://github.com/Carlos-Henrique-Rafaeli/clube-da-leitura.git
```

#### Navegue até a pasta raiz da solução
```
cd clube-da-leitura
```

#### Restaure as dependências
```
dotnet restore
```

#### Navegue até a pasta do projeto
```
cd ClubeDaLeitura.ConsoleApp
```

#### Execute o projeto
```
dotnet run
```