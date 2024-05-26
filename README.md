# StockAlert

O objetivo do sistema é avisar, via e-mail, caso a cotação de um ativo da B3 caia mais do que certo nível, ou suba acima de outro.
## Configurações

Para executar o programa corretamente, é necessário primeiro configurar o arquivo ```Configuracao.json```.  
Navague até a pasta ```/StockAlert/Configuration```, abra o json e coloque suas credenciais no arquivo.  

Exemplo:
```sh
{
  "EmailSettings": {
    "SmtpServer": "smtp.office365.com",
    "Port": 587,
    "SenderEmail": "email_example@live.com",
    "SenderPassword": "yourpassword",
    "ReceiverEmail": "receiverEmail@gmail.com"
  },
  "ApiSettings": {
    "ApiKey": "YOUR_API_KEY",
    "ApiUrl": "https://brapi.dev/api/quote/"
  }
}
```
Acesse o site : https://brapi.dev/ para conseguir uma chave da API.

## Como executar o programa

Clone o repositório na pasta que desejar.

Navegue até a pasta StockAlert.

Rode o seguinte comando:
```sh
dotnet run --project .\StockAlert\ PETR4 22.67 22.59
```
