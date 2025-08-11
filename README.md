# Micro-ondas

Este projeto é uma implementação simulada de um micro-ondas, com foco nas funcionalidades básicas e principalmente nas regras de negócio, concentradas no backend da aplicação.

> **Nível de dificuldade atendido:** 4 (de acordo com os requisitos propostos)

---

## Configurações

- Execute o ambiente com Docker:  
  ```bash
  docker-compose up
  Token: Frf-P?y+m9ma\"--Lw[y[Xa\\@cZS\\RBRE[+G?7fldl|e/D=G1rVFiie=2JKzY43.}
  ConnectionString: server=localhost;port=3306;database=microwave;user=user;password=123;

  schema do banco se encontra em: db.sql

# Tela Inicial:
<img width="3163" height="1131" alt="image" src="https://github.com/user-attachments/assets/97b855b8-ee97-41d0-8f52-54d4f801d66b" />

# Modelagem do Domínio:
Os modelos de domínio encapsulam regras e comportamentos relacionados.
Uso de Value Objects para representar conceitos imutáveis e garantir integridade dos dados.

> **Heating**: Modelo de aquecimento do microondas, representa as informações de aquecimento(duração, potência).
> **HeatingTimer**: Modelo que representa o timer do microondas, ou seja, tempo percorrido, tempo total, tempo que falta para acabar, se está parado ou não e etc.
> **Preset**: Modelo de aquecimentos customizados.

# Separação em Camadas:
> **Api**: REsponsável pela comunicação do servidor ao client, contem todos os endpoins devidamente autorizados e um socket para o timer do microondas.
> **DOmínio**: Contém as entidades, objetos de valor, enums e exceções que representam o core do negócio.
> **Aplicação**: Implementa os casos de uso, mantendo a lógica de negócio isolada.
> **Infraestrutura**: Persinstência e autorização.
> **Interface (UI)**: Interação com o usuário, foi utilizado Blazor para controle do micro-ondas.


# Programas de aquecimento pré-definidos:
Se encontram em HeatingPresets.Json

# Tratamento de exceções:
Exceções relacionadas ao dominio são tratadas em DOmainException e capturadas pelo middleware. 
Demais exceções são enviadas para um arquivo .log no diretório base da aplicação
