# Ecommerce Backend 
> Este projeto foi desenvolvido com fins educativos. Para acessar o frontend, visite: https://github.com/luisgomes2002/Ecommerce-Client

## Descrição do projeto
O projeto é um sistema de e-commerce voltado para compra e venda de produtos. Ele inclui funcionalidades como login, autenticação, controle de contas, criação e exclusão de produtos, além da integração com APIs externas para conexão com contas Google e processamento de pagamentos.

## Manual de uso
- Para o funcionamento do projeto, é necessário instalar as seguintes dependências:
  ![Screenshot 2024-06-30 194033](https://github.com/luisgomes2002/Ecommerce-Server/assets/85139913/f625e36e-fddf-45fb-91e5-6670412591b0)
- No arquivo appsettings.json, adicione a configuração de conexão com um banco de dados SQL e a chave JWT da seguinte forma:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu_servidor;Database=sua_base_de_dados;User=seu_usuario;Password=sua_senha;"
  },
  "Jwt": {
    "Key": "sua_chave_secreta_para_jwt",
    "Issuer": "seu_issuer",
    "Audience": "sua_audience"
  }
}
```
- Certifique-se de ter a versão mais recente do .NET instalada em seu computador para evitar problemas.
- Recomenda-se o uso do Postman ou outro API Client para acessar e testar as rotas da aplicação.
- Ao receber o token ao fazer login ou criar uma conta, adicione um cabeçalho com a chave "Authorization" e o valor "Bearer \<token\>".
