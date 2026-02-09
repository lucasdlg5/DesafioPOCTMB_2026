# DesafioPOCTMB_2026
Desenvolver um sistema simples de gestão de pedidos, onde será possível criar, listar e visualizar pedidos. Sempre que um pedido for criado, o sistema deve enviar uma mensagem para o Azure Service Bus, simulando um processamento assíncrono. Um worker será responsável por consumir as mensagens, processar o pedido e atualizar seu status.

## 🎯 Objetivo do Desafio
Desenvolver um sistema simples de gestão de pedidos, onde será possível criar, listar e
visualizar pedidos. Sempre que um pedido for criado, o sistema deve enviar uma mensagem
para o Azure Service Bus, simulando um processamento assíncrono. Um worker será
responsável por consumir as mensagens, processar o pedido e atualizar seu status.
## 🎯 Tecnologias Obrigatórias
- Backend: C# (.NET 7 ou superior) + Entity Framework + PostgreSQL
- Frontend: React + TailwindCSS
- Mensageria: Azure Service Bus
- Infraestrutura: Docker / Docker Compose (preferencial)

# 🎯 Requisitos do Sistema

## 1️⃣🎯 Backend (API em C#)
Criar uma API REST com os seguintes endpoints:
• POST /orders → Cria um novo pedido
• GET /orders → Lista todos os pedidos
• GET /orders/{id} → Obtém detalhes de um pedido
Regras de Negócio:
- Cada pedido deve conter os atributos: id, cliente, produto, valor, status, data_criacao.
- Status: Pendente, processando ou finalizado.
- Ao criar um pedido, persistir os dados no PostgreSQL usando EF e publicar uma mensagem no
Azure Service Bus.
- Criar um worker que consome mensagens, atualiza para 'Processando' e, após 5 segundos,
altera para 'Finalizado'.
Regras adicionais:
- Sequência de status obrigatória: Pendente → Processando → Finalizado.
- Consumidor do Service Bus deve ser idempotente.
- Incluir CorrelationId=OrderId e EventType=OrderCreated.
- Implementar health checks para API, banco e fila.

## 2️⃣🎯 Frontend (React + TailwindCSS)
Criar uma interface para:
- Listar pedidos em tabela responsiva.
- Criar novos pedidos via formulário.
- Visualizar detalhes do pedido.
- Exibir feedback visual para mudanças de status.

## 3️⃣🎯 Infraestrutura
- Criar Docker Compose com API, Worker, Frontend, PostgreSQL e PgAdmin.
- Usar .env para variáveis sensíveis.
- Configurar migrations automáticas.
- Implementar healthchecks no Compose.

## 4️⃣🎯 Módulo Opcional – Pergunte sobre os Pedidos (IA/Analytics) 🎯
Este módulo é opcional, mas vale pontos extras.
Permite que os usuários façam perguntas em linguagem natural sobre os pedidos, usando uma
LLM (OpenAI, Azure OpenAI, Gemini, etc.).
A LLM deve interpretar a pergunta, consultar o banco e responder de forma amigável, usando
dados reais.
Exemplos de perguntas:
- Quantos pedidos temos hoje?
- Qual o tempo médio para aprovar os pedidos?
- Quantos pedidos estão pendentes?
- Qual o valor total de pedidos finalizados este mês?

## 5️⃣🎯 Diferenciais Técnicos (Bônus)
- Outbox Pattern (mensageria transacional) [+3]
- Histórico de status do pedido [+3]
- SignalR/WebSockets com fallback [+3]
- Testcontainers [+1]
- Tracing ponta-a-ponta [+2]
- Golden Tests [+2]
- Módulo IA/Analytics com LLM [+5]

# 🎯 Critérios de Avaliação
- Qualidade do Código: 30%
- Mensageria & Confiabilidade: 20%
- Funcionalidade: 15%
- Documentação & DX: 15%
- Frontend & UX: 10%
- Testes Automatizados: 10%

# 🎯 Entrega
O candidato deve fornecer um repositório GitHub:
- Código-fonte completo
- README.md com instruções claras para rodar o projeto
- Exemplo de .env
- Diagramas simples de arquitetura (diferencial)



------


# TMB — Guia condensado de instalação e execução (do zero)

Este arquivo único descreve como clonar o repositório, instalar dependências (.NET 10 e React), executar a API REST, executar o frontend e validar endpoints (testes REST). Inclui também a correção imediata para o erro de materialização do EF Core.

> Substitua `<REPO_URL>` pela URL do repositório remoto.

---------------------
## Pré-requisitos
- Git
- .NET SDK 10 (https://dotnet.microsoft.com/)
- Node.js (LTS) + npm ou pnpm (https://nodejs.org/)
- (Opcional) Docker / Docker Compose
- Ferramenta HTTP: curl, httpie ou Postman

---------------------
## 1) Clonar repositório

git clone <REPO_URL> cd <nome-do-repositorio>


---------------------
## 2) Backend (.NET 10 — `TMB_REST`)
1. Entrar na pasta do backend:

cd TMB_REST/TMB_REST


2. Restaurar e compilar:

dotnet restore dotnet build


3. Configurar connection string
- Ajuste `appsettings.json` / `appsettings.Development.json` conforme seu banco de dados.

4. Migrations (se aplicável):

dotnet tool install --global dotnet-ef   # se necessário dotnet ef database update

criar migration (somente se precisar gerar esquema)
dotnet ef migrations add InitialCreate
dotnet ef database update


5. Executar API:

dotnet run

- Verifique `launchSettings.json` para as portas (ex.: `http://localhost:5000` e `https://localhost:5001`).

---------------------
## 3) Frontend (React / Next.js — `react-tmb`)
1. Entrar na pasta do frontend:

cd ../../react-tmb


2. Instalar dependências:

npm install

Ou

pnpm install


3. Rodar em desenvolvimento:

npm run dev

ou

pnpm dev

- Frontend: geralmente `http://localhost:3000`.

4. Build para produção:

npm run build npm start


---------------------
## 4) Testes REST API (automatizados e manuais)

Opção A — Testes automatizados (.NET):
- Se existirem testes no repositório:

dotnet test


Opção B — Testes manuais com curl (exemplos):
- GET todos

curl -s http://localhost:5000/api/OrderModels

- GET index

curl -s http://localhost:5000/api/OrderModels/index

- GET detalhe

curl -s http://localhost:5000/api/OrderModels/details/1

- POST criar

curl -X POST http://localhost:5000/api/OrderModels/create 
-H "Content-Type: application/json" 
-d '{ "Cliente":"Fulano", "Produto":"Produto X", "Valor":123.45, "Status":1, "Data_Criacao":"2026-02-01T12:00:00" }'

- POST editar

curl -X POST http://localhost:5000/api/OrderModels/edit/1 
-H "Content-Type: application/json" 
-d '{ "Id":1, "Cliente":"Fulano Atualizado", "Produto":"Produto X", "Valor":150.00, "Status":2, "Data_Criacao":"2026-02-01T12:00:00" }'

- POST deletar

curl -X POST http://localhost:5000/api/OrderModels/delete/1


Use Postman/Insomnia para testes interativos.

---------------------
## 5) Docker (opcional)
- Backend:

docker build -t tmb_rest ./TMB_REST/TMB_REST docker run -p 5000:80 tmb_rest

- Frontend:

docker build -t tmb_front ./react-tmb docker run -p 3000:3000 tmb_front


---------------------
