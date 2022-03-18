# HelloWorldLambda function com .Net core 6

Neste exemplo vamos criar uma HellowWorldLambda através do SAM CLI e executar localmente.

# Criação do lamdba com AWS CLI
- Abra o windows terminal e vá para a pasta onde deseja criar função e execute o comando abaixo
```bash
sam init
```
- Escolha a opção 1 - AWS Quick Start Template.
- Escolha o template 1 - Hello World Example
- Você será questionado se quer usar python, apenas informe "N" e dê um enter.
- Escolha a opção 1 para selecionar o runtime dotnet6
- Escolha a opção 2 - Image. Pois vamos rodar o nosso lambda dentro de um container.
- Por último informe o nome do seu projeto.

# Executando o lambda localmente

Ainda no terminal, navegue para a pasta do seu projeto recem criado e execute o comando abaixo:
```bash
sam local invoke "HelloWorldFunction"
```
- Note que ocorrerá o seguinte erro:
```bash
Error: ImageUri not provided for Function: HelloWorldFunction of PackageType: Image
```
- Este erro ocorre pois é preciso informar a URI da imagem docker que utilizaremos como base para rodar nossa função. No arquivo template.yaml na sessão resources acrescente o ImagemUri conforme abaixo: 
- ImageUri: 123456789012.dkr.ecr.eu-west-1.amazonaws.com/function:latest
- Agora faça o build através do comando abaixo:
```bash
sam build
```
- Agora é só executar novamente o comando abaixo para rodar a função
```bash
sam local invoke "HelloWorldFunction"
```
