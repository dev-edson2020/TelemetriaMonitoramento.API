# Plataforma de Monitoramento de Máquinas Pesadas - Telemetria

Sistema básico para monitoramento em tempo real de máquinas pesadas com backend em .NET 8 e frontend em Angular. A aplicação simula dados de telemetria atualizados via WebSockets para exibir status e localização das máquinas.

---

## Descrição do Projeto

Este projeto visa desenvolver uma plataforma que permita o cadastro, monitoramento e visualização em tempo real do status e localização de máquinas pesadas. A comunicação em tempo real é feita através de WebSockets, permitindo que o frontend seja atualizado instantaneamente com as informações enviadas pelo backend.

---

## Funcionalidades Implementadas

### Backend (.NET 8)

- API RESTful para cadastro, listagem e filtro de máquinas.
- Endpoints para atualizar localização e status das máquinas.
- Envio do status atualizado em tempo real via WebSockets.
- Simulação automática de dados de telemetria para máquinas específicas.
- Containerização com Docker para facilitar o deploy e testes.

### Frontend (Angular)

- Dashboard com lista de máquinas e atualização do status em tempo real.
- Formulário de cadastro de novas máquinas com validações básicas.
- Página de detalhes que mostra as informações completas de cada máquina.
- Consumo da API RESTful e integração com WebSockets para atualização dinâmica.
- (Opcional) Possibilidade de integração futura com mapas para visualização geográfica.

---

## Tecnologias Utilizadas

- Backend: .NET 8.0, C#, Entity Framework Core, MySQL
- Frontend: Angular, TypeScript, RxJS, WebSockets
- Containerização: Docker, Docker Compose
- Banco de Dados: MySQL (containerizado)

---

## Pré-requisitos

Para rodar a aplicação, você precisa ter instalado:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- (Opcional para frontend local) [Node.js](https://nodejs.org/) e [Angular CLI](https://angular.io/cli)

---

## Como Rodar o Projeto

### Usando Docker Compose (recomendado)

1. Clone o repositório:
   ```bash
   git clone https://seu-repositorio.git
   cd seu-repositorio
