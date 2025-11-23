# 📚 IA-Learning

Uma plataforma inteligente criada para analisar e demonstrar, na prática, o impacto da Inteligência Artificial no futuro do trabalho.

## 🧠 Ideia do Projeto

O IA-Learning foi desenvolvido dentro do tema “O Futuro do Trabalho”, buscando responder à pergunta:
Como a Inteligência Artificial pode prejudicar, transformar ou substituir profissões nos próximos anos?
Nosso objetivo foi criar uma IA amigável, educativa e responsável, capaz de:

Auxiliar no entendimento de conteúdos

Sugerir materiais de estudo

Avaliar tarefas e respostas

Explicar temas complexos em diferentes níveis de profundidade

A proposta é mostrar como a IA pode ser utilizada como ferramenta de aprendizado, e não como ameaça.

---
## 👨‍💻 Integrantes

- Caroline Assis Silva - RM 557596  
- Enzo de Moura Silva - RM 556532  
- Luis Henrique Gomes Cardoso - RM 558883  

---

## 📎 Funcionalidades Implementadas

✔️ Versionamento de API

✔️ CRUD com HATEOAS

✔️ Paginação

✔️ Integração com OpenAI

✔️ Oracle + Migrations

✔️ Testes unitários com xUnit

✔️ Estrutura

## ✅ Versão 1 – Recursos Principais

A v1 contém todos os módulos base da aplicação:

Usuários – CRUD e relacionamentos

IA – Cadastro e gerenciamento das inteligências artificiais

Tarefas – CRUD, vinculação ao usuário e IA

Avaliações – Registro e consulta de avaliações

Recomendações – Sugestões usando OpenAI com base na tarefa

Habilidades – Cadastro de habilidades criada

## 🚀 Versão 2 – O que foi adicionado

A v2 possui tudo da v1, mais:

📊 Dashboard

/ias-mais-usadas

/media-avaliacoes

/tarefas-por-usuario

❤️ HealthCheck

/api/v{version}/health

## 🧪 Como Executar o Projeto

1. Clone o repositório:

 ```bash
    git clone https://github.com/codecrazes/ia-learning.NET.git
    cd ia-learning.NET
```
2. Criar a variável de ambiente da OpenAI
  
 ```bash
  setx OPENAI_API_KEY ""
 ```

3. Restaure as dependências e execute o projeto:

```bash
dotnet restore

dotnet run
```

### 🧪 Como Rodar os Testes (xUnit)

```bash
cd ia-learning.Tests
```
```bash
dotnet test
```

## 🌐 Documentação da API

Swagger disponível em:

[http://localhost:5056/swagger/index.html](http://localhost:5056/swagger/index.html)

## 🔄 Exemplos de Requisições (JSON para Teste)

### Usuario
```json
{
  "nome": "Jose bezerra",
  "email": "jose@example.com"
}
```

### Tarefa
```bash
{
  "nome": "IA de analises Profissionais",
  "provedor": "OpenAI",
  "descricao": "IA voltada para estudos de carreira e desenvolvimento profissional.",
  "custo": 0,
  "tipo": "Educação"
}
```
### Tarefa com Paginação

| Nome     | Tipo | Exemplo |
|----------|------|---------|
| page     | int  | `1`     |
| pageSize | int  | `5`     |

### Recomendação
```bash
{
  "titulo": "Aprender conceitos de Machine Learning supervisionado",
  "dificuldade": 4,
  "tempoDisponivelMin": 45,
  "descricao": "Estudar classificadores como Regressão Logística e Decision Tree, entendendo suas aplicações no mercado de trabalho.",
  "usuarioId": 2,
  "iaId": 2
}
```


