# Quick Order - Tech Challenge FIAP

Projeto do Tech Challenge da FIAP - Fase 5

**INTEGRANTES DO GRUPO 74**

* Moisés Barboza de Figueiredo Junior
moises.figueiredo@gmail.com

* Gabriela da Silva Lopes
gabrieladslopes@gmail.com

* Francisco Tadeu da Silva e Souza
fsouza.thadeu@gmail.com

<br />

<br />

## Microserviço - Pedido
Microserviço de pedido é usuado como para organizar os pedidos do cliente em uma sacola até que o cliente finalize 
seu pedido e submenta-o para pagamento. O pagamento do pedido é realizado no microserviço de pagamento, por comunicação sincrona 
para gerar o QRCode de pagamento, e comunicação assíncrona para receber o resultado do pagamento.
Pagamento aprovado gera comunicação assíncrona para o microserviço de atendimento. Pagamento recusado gera o cancelamento do pedido.


<br />

## Pré-Requisitos

Antes de executar este projeto, os seguintes itens deverão estar instalados no computador:

* Docker
* Kubernetes


<br />

Passo a passo - Instalação:

* Obter os scripts de instalação do kubernetes no repositório https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Infra-MicroServices-Kubernetes

* Abrir alguma interface de linha de comando como, por exemplo, o **PowerShell** e digitar o comando `kubectl config get-contexts`. O resultado deverá ser conforme abaixo, com o contexto do **docker-desktop** selecionado:
  
<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-backend/assets/44347862/ce7f5145-2ae7-44a0-82d5-fecf3c593589)


* Caso o contexto do **docker-desktop** não esteja selecionado, selecionar o mesmo através do comando `kubectl config set-context docker-desktop`
* Executar o comando `kubectl get all` para garantir que o cluster esteja limpo e assim prevenir que ocorram conflitos de portas nos passos posteriores. O resultado deverá ser esse:

<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-backend/assets/44347862/01637947-6284-4dd3-a148-d1cc039603f4)


<br />

* Caso haja algum **pod, svc ou deployment** etc listado no passo anterior, limpar o cluster através do comando `kubectl delete all --all` e `kubectl delete pvc --all`
* Baixe o projeto QuickOrder-Infra-Kubernetes do repositório https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Infra-MicroServices-Kubernetes.git
* Verifique os sprits do repositório digitando `ls` 

<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Produto/assets/19378661/83153e7a-811c-4eb0-9f7b-da3590a2e99a)



<br />

* Aplicar os **scripts yml** dos **PersistentVolumeClaim** através do comando `kubect apply -f .\01-pvc\`:

<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Produto/assets/19378661/3f417c40-7978-4801-8910-20d3ce8b3f44)



<br />

* Aplicar os **scripts yml** dos **Deployments** através do comando `kubectl apply -f .\02-deployments\`:

<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Produto/assets/19378661/ed24558f-fdf2-43ff-812f-17f0c0efea6e)


<br />

* Aplicar os **scripts yml** dos **Services** através do comando `kubectl apply -f .\03-services\`:

<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Produto/assets/19378661/9966598c-45a5-45a7-8c4e-2c576c8a327e)



<br />

* Executar comando `kubectl get all` para verificar a criação dos itens dos passos anteriores. O resultado deverá ser similar ao listado abaixo:

<br />

![image](https://github.com/TechChallenge-4SOAT-G74/QuickOrder-Produto/assets/19378661/5c9c3b53-6a22-4996-a433-34e20cbd9376)



<br />

* Abrir o browser e digitar o seguinte endereço **http://localhost:30000/swagger/**. O swagger da Api deverá ser exibido, indicando que a subida da aplicação ocorreu com sucesso:

<br />

![image](https://github.com/user-attachments/assets/3218e012-b918-4a21-9714-1f42e2e7b089)

