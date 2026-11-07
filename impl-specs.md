# Especificação do Projeto: Gerenciador de Biblioteca (Library Management System)

## Objetivo
Desenvolver o backend de um sistema de gerenciamento de biblioteca em C# e .NET 10. O projeto é uma atividade prática para fixação de **Programação Orientada a Objetos (POO)**, estruturado para evoluir progressivamente para conceitos de **Arquitetura de Software em Camadas** e **Bancos de Dados Relacionais**.

---

## Convenção de Idioma (Código em Inglês)
Todo o código fonte do projeto (nomes de classes, interfaces, propriedades, métodos, parâmetros, variáveis e namespaces) deve ser obrigatoriamente escrito em **Inglês**. A documentação e menus de terminal podem utilizar o Português para fins didáticos.

---

## Arquitetura do Sistema
O sistema adota uma arquitetura em camadas para garantir a separação de responsabilidades, facilitando a manutenção, a testabilidade e a evolução incremental:

1. **Camada de Apresentação / CLI (`Library.Presentation`)**
   - Responsável pela interação direta com o usuário via terminal Console CLI (`ConsoleMenu`), exibindo menus e capturando opções.
   - Não contém regras de negócio nem acessa o repositório diretamente; consome exclusivamente a **Camada de Serviços (`IBookService`)**.

2. **Camada de Serviços (`Library.Services` e `Library.Services.Interfaces`)**
   - Concentra as interfaces de serviço (`IBookService`) e sua implementação concreta (`BookService`).
   - Orquestra o fluxo de dados, executa validações de regras de negócio e media as chamadas para a persistência.

3. **Camada de Domínio (`Library.Domain.Entities` e `Library.Domain.Interfaces`)**
   - Contém a entidade principal (`Book`) com suas propriedades e regras de domínio encapsuladas.
   - Define o contrato abstrato de persistência (`IBookRepository`), mantendo as regras de negócio totalmente desacopladas da infraestrutura de armazenamento.

4. **Camada de Infraestrutura (`Library.Infrastructure.Repositories`)**
   - Responsável pelas implementações técnicas concretas de armazenamento e comunicação externa.
   - Implementa a classe `InMemoryBookRepository` para manipular o acervo em memória (fase inicial de POO), permitindo a evolução transparente para Bancos de Dados Relacionais (SQL/ORM) sem alterar as camadas superiores.

---

## Requisitos Funcionais

### 1. CRUD Padrão
- **Create (Add):** Registrar um novo livro no acervo aplicando validações de domínio.
- **Read (GetById / GetAll):** Listar todos os livros cadastrados ou obter um livro específico pelo seu identificador único (`Id`).
- **Update:** Permitir alteração de dados cadastrais e ajuste controlado de estoque por meio de métodos de domínio.
- **Delete (Remove):** Remover o registro de uma obra do acervo por `Id`.

### 2. Consultas e Buscas
- **Busca por Nome do Livro (`SearchByTitle`):** Filtrar obras cujo título contenha o termo pesquisado.
- **Busca por ISBN (`GetByIsbn`):** Obter a publicação exata pelo código ISBN.
- **Busca por Nome do Autor (`SearchByAuthor`):** Listar obras escritas por um determinado autor.
- **Busca por Nome da Editora (`SearchByPublisher`):** Listar obras publicadas por uma determinada editora (*publisher*).

### 3. Carga em Massa (Bulk Load / Data Seeding)
- **Carga de Dados via JSON (`LoadBooksFromJson`):** Ler o arquivo estático `books_dataset.json`, desserializar os registros JSON em objetos da entidade `Book` e realizar a inserção em lote no repositório (`BulkLoad`).

---

## O Conceito de ISBN no Domínio
O **ISBN** (*International Standard Book Number*) é o identificador único padronizado internacionalmente para livros. No sistema:
- **Chave de Negócio:** Atua como chave natural que impede a duplicação da mesma publicação no acervo.
- **Validação e Encapsulamento:** A atribuição deve garantir a higienização da entrada (remoção de formatação/hífens) e validação de tamanho (ISBN-10 ou ISBN-13).
- **Integração Externa:** Serve como ponto de integração para consulta automatizada de metadados em serviços externos (ex: Open Library API ou Google Books API).

---

## Requisitos Técnicos e Orientação a Objetos
- **Encapsulamento:** Manter o estado interno das entidades protegido com modificadores de acesso restritos (`private set`), permitindo mutação apenas via métodos com intenções explícitas de negócio.
- **Abstração (Interfaces):** Interface `IBookRepository` isola a regra de negócio da infraestrutura de armazenamento.
- **Injeção de Dependência:** Registrar abstrações no contêiner nativo do .NET 10.

---

## Carga Inicial de Dados (Data Seeding)
- **Dataset Estático (`books_dataset.json`):** Inicializar o acervo a partir do arquivo JSON fornecido para exercitar a desserialização e carga inicial via repositório.


