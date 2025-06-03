# Plataforma de Monitoramento de Máquinas Pesadas - Telemetria

Sistema completo para monitoramento em tempo real de máquinas pesadas, com backend em .NET 8 e frontend em Angular. A aplicação simula dados de telemetria atualizados via WebSockets para exibir status e localização das máquinas.

---

## 📋 Descrição do Projeto

Esta plataforma permite o **cadastro, monitoramento e visualização em tempo real** do status e da localização de máquinas pesadas. Utiliza **WebSockets** para comunicação instantânea entre backend e frontend, garantindo atualizações dinâmicas e contínuas da telemetria.

---

## ✅ Funcionalidades Implementadas

### 🔧 Backend (.NET 8)
- API RESTful para cadastro, listagem e filtro de máquinas.
- Endpoints para atualizar status e localização das máquinas.
- WebSocket para envio em tempo real dos dados atualizados.
- Simulador de telemetria automática para testes e demonstrações.
- Containerização com Docker.

### 🎨 Frontend (Angular)
- Dashboard com lista de máquinas e status em tempo real.
- Cadastro de novas máquinas com validação de campos.
- Página de detalhes com informações completas de cada máquina.
- Integração com API REST e WebSocket.
- (Futuro) Suporte à visualização geográfica via mapas.

---

## 🚀 Tecnologias Utilizadas

- **Backend:** .NET 8, C#, Entity Framework Core
- **Frontend:** Angular, TypeScript, RxJS, WebSockets
- **Banco de Dados:** MySQL (containerizado)
- **DevOps:** Docker, Docker Compose

---

## 🔧 Pré-requisitos

Para executar o projeto, você precisará de:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- (Opcional para desenvolvimento frontend local) [Node.js](https://nodejs.org/) e [Angular CLI](https://angular.io/cli)

---

## 🐳 Como Rodar o Projeto com Docker (Recomendado)

1. Clone este repositório:

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio

Inicie os containers com Docker Compose:

docker-compose up --build

Acesse a aplicação:

Frontend Angular: http://localhost:4200

Backend API (.NET): http://localhost:5000/swagger

MySQL: Rodando internamente no container, porta padrão 3306
