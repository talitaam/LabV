# Backend

API e WebSocket da aplicação. 
Ao executar, o Swagger da API é disponibilizado em http://localhost:5000/index.html, demonstrando as rotas disponíveis, assim como uma breve documentação.

## Tecnologias

* C#
* .NET Core 3.1
* SignalR

## Camadas

  - `AppService`: Representa as funcionalidades do sistema. Responsável por agrupar os métodos de cada domínio, construindo a funcionalidade. Permite a implementação de transações para realização de _rollback_ no banco de dados em cenários de erro.
  - `Domain`: Representa a camada de domínio da aplicação. Responsável por realizar as regras de negócio, como validações nos dados. Comunica-se com a camada "Data" para acessar os dados. Possui classes de serviço para cada domínio e classes DTO que refletem as entidades do sistema, utilizadas para transferir dados entre as camadas.
  - `Infra.CrossCutting.IoC`: Camada para configuração da injeção de dependência (inversão de controle). Responsável por mapear as instâncias de cada interface do sistema e construir o _container_ que disponibiliza cada instância nos momentos necessários.
  - `Infra.Data`: Representa a camada de acesso aos dados. Possui as queries de consultas, atualizações, inserções e deleções ao banco de dados, além das classes entidade que representam as tabelas. Comunica-se com o SGBD através do ORM Dapper.
  - `Shared`: Representa classes compartilhadas em todas camadas, possuindo classes úteis para formatação, exceções customizadas, validações e configurações da aplicação.
  - `WebApi`: Representa a camada de acesso externo ao backend. Disponibiliza as _controllers_ por domínio no padrão REST, as quais possuem rotas de acesso à cada funcionalidade. Também disponibliza um Hub para WebSocket, permitindo a conexão TCP através da biblioteca SignalR.

<a name="execucao"></a>

## Execução

Para inicializar a aplicação, recomenda-se o uso do [VisualStudio 2019](https://visualstudio.microsoft.com/pt-br/downloads/). É necessário possuir o SDK 3.1 instalado, o qual pode ser encontrado [aqui](https://dotnet.microsoft.com/download/dotnet/3.1).

É necessário incluir o usuário e senha do banco de dados local no arquivo `ReserveAqui.WebApi/appsettings.desenvolvimento.json`, substituindo-os no valor da chave `ConexaoMySql`. O arquivo `ReserveAqui.WebApi/appsettings.homologacao.json` possui uma Connection String para acesso ao banco de dados remoto, hospedado no Azure, sendo necessário incluir a senha para utilizá-lo.

Para definir qual _appsettings_ utilizar, deve-se informar o valor "Desenvolvimento" ou "Homologacao" para a variável de ambiente `ASPNETCORE_ENVIRONMENT`. Isso pode ser modificado no arquivo `ReserveAqui.WebApi/Properties/launchSettings.json`.

Ao instalar o SDK, é possível executar a aplicação através da IDE (VisualStudio 19) ou diretamente no compilador, utilizando o comando abaixo no diretório "ReserveAqui.WebApi":

```
dotnet run
```

Com isso, o backend será compilado e publicado em http://localhost:5000/index.html. A rota disponibilizada para o WebSocket é http://localhost:5000/chat.