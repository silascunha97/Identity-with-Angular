# Biblioteca API
API desenvolvida em ASP.NET Core, utilizando Identity + JWT Bearer Authentication para autenticação e autorização baseada em Roles. A aplicação permite gerenciar livros, usuários (Aluno, Docente, Bibliotecário) e o fluxo de empréstimos.

## Tecnologias Utilizadas
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core
- IdentityCore
- JWT Bearer Token
- SQL Server
- Swagger/OpenAPI

## Objetivo da API
Fornecer um sistema simples para:
- Gerenciar livros e estoque
- Emitir e registrar empréstimos
- Controlar usuários com papéis distintos
- Restringir acesso às rotas com base nos papéis do Identity

## Perfis de Usuário (Roles)
| Role | Permissões |
|------|------------|
| Aluno | Buscar livros, solicitar empréstimo |
| Docente | Igual ao aluno, mas vinculado a múltiplas turmas |
| Bibliotecario | Realizar empréstimos e gerenciar livros |

## Entidades Principais
### Livro
- QuantidadeTotal
- QuantidadeDisponivel
- Título
- Status automático baseado no estoque

### Emprestimo
- LivroId
- UserId
- DataEmprestimo
- DataDevolucao

### Aluno / Docente / Bibliotecario
Mapeados ao IdentityUser via UserId.

## Autenticação
Token JWT enviado no header:

Authorization: Bearer <token>

## Rotas Principais
### Autenticação
- POST /auth/register-aluno
- POST /auth/register-docente
- POST /auth/register-bibliotecario
- POST /auth/login

### Livros
- GET /livros
- POST /livros (Bibliotecario)
- PUT /livros/{id} (Bibliotecario)
- DELETE /livros/{id} (Bibliotecario)

### Empréstimos
- POST /emprestimos/solicitar (Aluno/Docente)
- POST /emprestimos/realizar (Bibliotecario)
- GET /emprestimos/minhas
- GET /emprestimos
- PUT /emprestimos/devolver/{id} (Bibliotecario)

## Como Rodar
1. git clone <repo>
2. Configure o appsettings.json
3. dotnet ef database update
4. dotnet run

## Licença
MIT
