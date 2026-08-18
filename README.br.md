<img height="15px" src="https://em-content.zobj.net/thumbs/120/twitter/322/flag-united-states-of-america_1f1fa-1f1f8.png">[  Read this document in English](README.md)

# PIMDesk - Ecossistema de Gestão Corporativa de Chamados (Desktop & Mobile)

Bem-vindo ao repositório do **PIMDesk**, um projeto de conclusão de curso desenvolvido como parte da graduação em Análise e Desenvolvimento de Sistemas na Universidade Paulista (UNIP).

Este projeto é um ecossistema integrado e robusto — composto por uma aplicação de gestão Desktop e um aplicativo Mobile complementar — desenvolvido para otimizar a gestão corporativa de chamados (tickets), solicitações de suporte de TI e a visualização de dados operacionais.

> ⚠️ **Aviso Importante Sobre os Dados Ativos:**
> Como este projeto acadêmico foi concluído com sucesso, a hospedagem do banco de dados na nuvem e os serviços da API backend foram desativados. Você ainda pode explorar o código-fonte, compilar as aplicações e navegar pelas interfaces visuais (UI/UX) das plataformas Desktop e Mobile, mas o consumo e integração de dados em tempo real está atualmente offline.

## 🛠️ Tecnologias & Arquitetura

Esta aplicação foi arquitetada com foco em manutenibilidade e integração de sistemas entre diferentes plataformas, utilizando as seguintes tecnologias:

### Aplicação Desktop
* **Linguagem:** C#
* **Framework:** .NET
* **Interface / Apresentação:** Windows Forms (WinForms) com User Controls (controles de usuário) altamente customizados
* **ORM / Acesso a Dados:** Entity Framework

### Aplicação Mobile
* **Arquitetura:** Componente mobile projetado de forma fluida para permitir que os usuários finais criem chamados, acompanhem atualizações de status e interajam com o ecossistema de qualquer lugar.

### Infraestrutura & Operações
* **Banco de Dados:** SQL (Anteriormente hospedado na nuvem)
* **Controle de Versão:** Git & GitHub

## ✨ Principais Funcionalidades

Mesmo operando em um ambiente exclusivamente visual (UI-only) sem o banco de dados ativo, o código-fonte demonstra vários conceitos avançados de desenvolvimento full-stack:

* **📱 Acessibilidade Mobile:** Uma interface móvel projetada para interações rápidas e acompanhamento de status de chamados para os usuários finais.
* **📊 Dashboard Dinâmico Desktop:** Um painel analítico orientado a dados na aplicação desktop, projetado para renderizar gráficos e métricas sobre os status dos chamados e o desempenho da equipe.
* **💬 Fórum/Chat Integrado de Chamados:** Uma interface de mensagens simulando um ambiente de fórum para uma comunicação fluida sobre chamados de suporte específicos em ambas as plataformas.
* **🧩 User Controls Customizados:** Ampla customização dos componentes padrão do WinForms para criar uma experiência de usuário moderna e intuitiva.

## 📸 Capturas de Tela

| Dashboard Desktop | Chat de Chamados Desktop | Visão do App Mobile |
<p float="left">
  <img width="49%" alt="1765484397004" src="https://github.com/user-attachments/assets/4ee7e13f-1fc8-4731-9d40-e561cd3ff151" />
  <img width="49%" alt="1765484441977" src="https://github.com/user-attachments/assets/2e57c163-6df4-4316-bef4-104ac8b3ee70" />
  <img width="25%" height="25%" alt="1765484495278" src="https://github.com/user-attachments/assets/ed4ca93b-4f72-4527-ad79-3ea56238de90" />
</p>

## 🚀 Como Explorar o Código

Se você deseja explorar o código-fonte e as interfaces visuais interativas:

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/araujodanield/PIM-4-UNIP.git](https://github.com/araujodanield/PIM-4-UNIP.git)
