# Desafio-API

Sistema de gerenciamento de contas a pagar com cálculo automático de multas.

## Tecnologias
- .NET Core 3.0
- Entity Framework
- xUnit
- SQL Server

## Funcionalidades
- Cadastro de contas
- Cálculo de multas e juros:
  - Até 3 dias: 2% multa + 0,1% juros/dia
  - 4-5 dias: 3% multa + 0,2% juros/dia
  - Mais de 5 dias: 5% multa + 0,3% juros/dia
- Listagem com valores corrigidos
