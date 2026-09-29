# Prueba Técnica - Desarrollador .NET

## 📋 Información General

| Concepto              | Detalle                                      |
| --------------------- | -------------------------------------------- |
| **Tiempo límite**     | 2 días calendario desde la recepción         |
| **Stack tecnológico** | .NET 8, Entity Framework Core, SQL Server    |
| **Arquitectura base** | Clean Architecture                           |
| **Entrega**           | Repositorio público en GitHub                |

### Repositorio Git

```bash
https://github.com/joseluisplataglez-ctrl/calculadora-divisas-test.git
```

> ⚠️ **Importante:** El aplicativo al momento de levantar aplicará las migraciones pendientes en la base de datos(ContextDb y AuditContextDB) por lo que solo es necesario cambiar la cadena de conexión
---

# Decisiones de Diseño

## 1. Arquitectura y Datos
[Dominio]
```bash
#  La capa de DOMINIO esta preparada para poder escalar y mantenerse de acuerdo a un enfoque DDD por lo que contiene 
# lo necesario para ajustar campos, aplicar reglas de negocio y extenderse, además de que se utilizó una clase abstracta 
# para reutilizar campos de auditoria

```
[Application]
```bash
# La capa de APPLICATION se integra por contratos de aplicación, DTOs de transferencia tanto a las solicitudes del backend
# como para peticiones con servicios externos, manejo de excepciones globales, perfiles de mapeo y los casos de uso.
# La capa esta preparada para extenderse y aunque se llena de varios espacios de nombre es más fácil realizar cualquier mantenimiento

```
[Infrastructure]
```bash
# La capa de INFRASTRUCTURE contempla el acceso a datos (SQL Server), configuraciones de migración, servicios externos, de localizción
# y la configuración para los logs (bitacoras)

```

[Presentation]
```bash
# La capa de PRESENTATION consolida las solicitudes mediante CQRS a los casos de uso mediante MediatR dentro de los controllers
# , además dentro de los middlewares se agregaron los filtros personalizados para las peticiones Http, excepciones globales y LogAction 
# que registran la actividad de las peticiones al backend. Contiene el patron OperationResult para homologar las respuestas hacia el
# frontend, las vistas (todo dentro de Index) y manejo de errores.

```

```mermaid
erDiagram
    Currencies ||--o{ Rates : "defines base/quote"
    Providers ||--o{ Rates : "provides"

    Currencies {
        int CurrencyId PK
        string CurrencyIsoCode
        string CurrencyIsoNumber
        string CurrencyName
        string CurrencySymbol
        datetime CurrencyStartDate
        datetime CreatedAt
        string CreatedBy
        datetime UpdatedAt
        string UpdatedBy
        int IsActive
    }

    Rates {
        int RateId PK
        datetime CurrencyDate
        decimal RateValue
        int ProviderId FK
        int CurrencyBaseId FK
        int QuoteCurrencyId FK
        datetime CreatedAt
        string CreatedBy
        datetime UpdatedAt
        string UpdatedBy
        int IsActive
    }

    Providers {
        int ProviderId PK
        string ProviderKey
        string ProviderName
        string ProviderCountryCode
        string ProviderRateType
        string ProviderPivotCurrency
        datetime CreatedAt
        string CreatedBy
        datetime UpdatedAt
        string UpdatedBy
        int IsActive
    }

    FavouriteCurrencies {
        int FavouriteCurrencyId PK
        string FavouriteCurrency
        datetime CreatedAt
        string CreatedBy
        datetime UpdatedAt
        string UpdatedBy
        int IsActive
    }

    AuditLogs {
        int Id PK
        string Action
        datetime Timestamp
    }

```


## 2. Ubicación de Reglas de Negocio  
[Validaciones de datos]
```bash
# Validaciones de Entrada - [Application - FLuentValidation] (apoyado de la IPipelineBehaivor para cada campo)
# Reglas de Estado e Invariantes [Domain - Entities] (validaciones internas con excepcionDomain)
# Validaciones de exitencia [Application - Handlers] (Orquestción de existencia de objetos en BD con UoW)
```

```mermaid
sequenceDiagram
    autonumber
    participant C as Cliente (Presentation)
    participant P as Pipeline (FluentValidation)
    participant H as Handler (Application)
    participant E as Entidad (Domain)
    participant DB as Base de Datos / FrankfurterService

    Note over C, P: Capa 1: Validación de Entrada
    C->>P: Enviar Command/Query
    alt Datos Inválidos
        P-->>C: throw ValidationException (400)
    else Datos Correctos
        P->>H: Ejecutar Handle()
    end

    Note over H, DB: Capa 2: Orquestación y Existencia
    H->>DB: Consultar existencia (UoW)
    alt No Existe
        DB-->>H: null / empty
        H-->>C: return OperationResult.Failure (404)
    else Existe
        H->>E: Invocar Regla de Negocio
    end

    Note over E, E: Capa 3: Invariantes de Dominio
    alt Regla Violada (Estado ilegal)
        E-->>H: throw DomainException
        H-->>C: Catch & Map to Backend Response (400)
    else Regla Exitosa
        E->>E: Actualizar Estado Interno
        E-->>H: Retornar Éxito
    end

    H->>DB: UnitOfWork.CompleteAsync()
    DB-->>H: Commit OK
    H-->>C: return OperationResult.Success (200)
```


## 3. Patrones Utilizados
[Razonamiento de patrones de diseño utilizados]
```bash
#  1. Repository .- Estructura las operaciones genericas para CRUD de datos adenmas de crear una estructura que prepara el UoW

#  2. CQRS .- Separación de las operaciones de lectura y escritura

#  3. Mediator .- Desacopla los controladores de la lógica de negocio ( sustituye a la capa se servicios)

#  4. UnitOfWork .- Permite la atomicidad para mantener varias operaciones en la BD simulando incluso COMMITS, ROLLBACKS

#  5. Result Pattern .- Estructura el flujo normal del programa para la estenderización entre la capa de aplicación y de presentación

#  6. Strategy Pattern .- Para el manejo global de excepciones se provee y contemplan los diferentes tipos de excepciones a nivel
# dominio, aplicación o lógica de negocio

# 7. FactoryPattern.- Para el caso de los errores / excepciones de respuesta de errors a ProblemDetailFactory para que construya esa respuesta

```


```mermaid
graph TD
    %% Capa de Presentación
    CTRL[Controller]

    %% Patrón Mediator
    subgraph "Patrón Mediator (MediatR)"
        CTRL -->|1. Envía Request| MED[Mediator]
        MED -->|2. Despacha al| HAND[Handler específico]
        style MED fill:#f9f,stroke:#333,stroke-width:2px
    end

    %% Patrón CQRS
    subgraph "Patrón CQRS"
        HAND -->|Escritura| CMD[Command Handler]
        HAND -->|Lectura| QRY[Query Handler]
        style HAND fill:#bbf,stroke:#333
    end

    %% Servicios Externos
    subgraph "Servicios Externos (External Services)"
        CMD -->|3b. Invoca Abstracción| EXT_INF[IExternalService / API Client]
        EXT_INF -->|4b. Petición HTTP / | EXT_API((API Externa / Frankfurter))
        style EXT_INF fill:#fdd,stroke:#333,stroke-width:2px
        style EXT_API fill:#f9f,stroke:#333,stroke-width:1px
    end

    %% Patrón Unit of Work & Repository
    subgraph "Persistencia (UoW & Repository)"
        CMD -->|3. Coordina| UOW[Unit of Work]
        UOW -->|4. Provee| REPO[Repository Genérico / Específico]
        REPO -->|5. Operaciones CRUD| DB[(SQL Server)]
        UOW -->|6. Commit / Rollback| DB
        style UOW fill:#dfd,stroke:#333,stroke-width:2px
    end

    %% Patrón Result Pattern
    subgraph "Patrón Result (OperationResult)"
        QRY -.->|7. Envuelve| RES[Result Pattern]
        CMD -.->|7. Envuelve| RES
        RES -.->|8. Respuesta Estandarizada| CTRL
        style RES fill:#fff4dd,stroke:#d4a017,stroke-width:2px
    end

    %% Notas Técnicas
    classDef note font-style:italic,font-size:10px;
    N1[Sustituye Capa de Servicios] -.-> MED
    N2[Atomicidad de Operaciones] -.-> UOW
    N3[Estandariza Flujo App-Backend] -.-> RES
```
## 4. Frontend
[Resumen]
```bash
#  Se respeta la estructura MVC otorgada por la plantila de ASP.NET Core MVC que además cuenta con Boostrap para los estilos y jQuery
#  para el dinamismo de la página, se agregan iconos FontAwesome para mejorar el diseño y se trata de manejar clean code para no abusar
#  de las funciones engorrosas de lado del frontend

```


## 5. Trade-offs y Limitaciones
[Pendientes]
```bash
#  Agregar gráficas para una mejor lectura de las comparaciones / conversiones de las divisas
#  Dockerizar el aplicativo para poderse ejecutar en cualquier entorno
#  Despliegue dentro de Azure

```
## 6. Usabilidad

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Carga de datos desde Frakfurter Service
</b></h4>
  </div>
  <img src="images/CargaDatos.png" alt="Avatar" style="width:100%">

</div>

------

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Panel Principal
</b></h4>
  </div>
  <img src="images/PanelPrincipal.png" alt="Avatar" style="width:100%">

</div>

-------

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Seleccion Divisas
</b></h4>
  </div>
  <img src="images/SeleccionDivisas.png" alt="Avatar" style="width:100%">

</div>

-------

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Visualizacion Divisas
</b></h4>
  </div>
  <img src="images/visualizacionDivisas.png" alt="Avatar" style="width:100%">

</div>

-------

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Calculos
</b></h4>
  </div>
  <img src="images/Calculos.png" alt="Avatar" style="width:100%">

</div>

-------

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Estructura Base de Datos
</b></h4>
  </div>
  <img src="images/basedatos.png" alt="Avatar" style="width:100%">

</div>

-------

<div style="box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;">
  <div style="text-align:center;font-weight: bold; background-color: #A7CFDB;">
    <h4 ><b style="color: white">
    
## Bitacora - Logs
</b></h4>
  </div>
  <img src="images/logs.png" alt="Avatar" style="width:100%">

</div>
