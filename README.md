# BlueBankApi

#### BackEnd de la aplicacion BlueBankApp

# Supuestos de Negocio
Blue Bank, requiere renovar su sistema financiero, core de gestión de cuentas de ahorro

Los requerimientos que debe soportar son los siguientes:

####	Crear una cuenta de ahorros
##### *-Nombre de la persona
##### *-Valor inicial de la cuenta
####	Consignar
##### *-Número de cuenta
##### *-Valor a consignar
####	Retirar dinero
##### *-Número de cuenta
##### *-Valor a retirar
####	Consultar saldo
##### *-Número de cuenta

# Arquitectura Utilizada

### Para el desarrollo del API, se aposto por una Arquitectura Limpia o arquitectura en Cebolla, debido a los beneficios que obtenemos:
#### Facilidad de Realizar Pruebas: debido a que cada capa es independiente, es más fácil el desarrollo y la ejecución de casos de pruebas unitarias y de integración.
#### Independencia de la interfaz de Usuario: actualmente el Frontend de la aplicación esta desarrollado en Angular, pero sin ningún problema puede cambiarse a una aplicación en React o View y esto será transparente para el API.
#### Independencia de la Base de datos: La base de datos funge como un repositorio de información y por ende realizar el cambio de motor de base de datos es transparente; actualmente la base de datos esta en SQL SERVER, pero sin ningún problema se puede cambiar a MySql o cualquier motor de Base de datos Relacional



# Tecnologias Utilizadas

#### .NetCore  5.0
#### EntityFramework Core 5.0
#### Json Web Token

## Url del Api
https://az-app-blue-bank-api.azurewebsites.net/WeatherForecast
