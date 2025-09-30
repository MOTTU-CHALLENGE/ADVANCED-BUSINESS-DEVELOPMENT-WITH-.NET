# ADVANCED-BUSINESS-DEVELOPMENT-WITH-.NET

## **Integrantes**

|            NOME             |   RM   |
| :-------------------------: | :----: |
|  Francesco M Di Benedetto   | 557313 |
| Luiz Felipe Campos da Silva | 555591 |
|   Samuel Patrick Yariwake   | 556461 |

## **Objetivo**

### Rastrear uma moto no patio da MOTTU.

## **Solução**

### Utilizar IOT para rastrear uma moto no patio da MOTTU, triangulando a localização com o uso de WIFI.

A proposta é usar **dispositivos IoT (como o ESP32)** para captar a intensidade de sinal de redes WiFi no entorno, mesmo sem conexão ativa, e assim **triangular a localização da moto** de forma estimada.

Com um custo aproximado de **R$ 50,00 por dispositivo**, conseguimos montar um sistema inteligente e acessível para monitoramento interno.

## **Desenvolvimento**

### _Entidades_

Podemos definir 6 entidade prioritárias:

1. Filial
2. Patio
3. Wifi
4. Moto
5. IOT
6. Registro Sinal

Para se ter uma triangulação da moto, precisamos de uma `referencia` de localização, sendo necessário registro da filial, os patios que ela possui e onde estão localizados os receptores de WiFi.

Cada Moto terá um IOT que enviará os dados de `intensidade - Endereco MAC`.

### _Endpoints_

| Método | Rota                                            | Descrição                        |
| ------ | ----------------------------------------------- | -------------------------------- |
| GET    | **/api/[entidade]**                             | Retorna todos os registros       |
| GET    | **/api/[entidade]/paginado?pagina=[x]&qtd=[y]** | Retorna registros paginados      |
| GET    | **/api/[entidade]/{id}**                        | Retorna um único registro por ID |
| POST   | **/api/[entidade]**                             | Cria um novo registro            |
| PUT    | **/api/[entidade]/{id}**                        | Atualiza um registro existente   |
| DELETE | **/api/[entidade]/{id}**                        | Remove um registro existente     |

- `Registro Sinal` não tem PUT, já que não se pode adulterar um registro.

---

## **Video de exemplos de uso**

## **Repositório do GIT**

> [https://github.com/challenge-mottu/ADVANCED-BUSINESS-DEVELOPMENT-WITH-.NET](https://github.com/challenge-mottu/ADVANCED-BUSINESS-DEVELOPMENT-WITH-.NET)
=======
