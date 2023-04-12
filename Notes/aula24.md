# Estrutura de backend

## HTTP / HTTPS

Procotolo a ser seguido na comunicação cliente-servidor.

## API, Rest API e Restful API

Uma API ela disponibiliza recursos sem que tenhamos que nos preocupar com a maneira que estes recursos foram implementados.

## ORM - Object Relational Mapper

Ela ajuda a gente a trocar do modelo Orientado a Objetos para o modelo relacional.

```cs
public class Pessoa {

  [key]
  [Required]
  public int Id {get; set;}

  [Required]
  public string Name {get; set;}

  public int Age {get; set;}

}
```

vai converter para (não será feito pela gente):

```sql
create table if not exists pessoa (
  id int primary key,
  name varchar(255) not null,
  age int
);
```

## Migrations

Só funciona se o meu `Program.cs` e Contexto de persistência estiverem configurados corretamente.

- EF Core
- EF Core Tools

> Trocas estruturais nos modelos do projeto geralmente se relacionam com uma novas migrations.

### Comandos no Nuget Console Manager

1. `Add-Migration`: Cria um arquivo responsável pela migration.

```bash
Add-Migration "Adding Pessoa table"
```

2. `Update-Database`: Executa no meu banco todas as migrations pendentes.

3. `Remove-Migration`: Desfaz a última migration executada.

4. `Script-Migration`: Gera um script SQL a partir das mudanças pendentes.

## Estrutura do Entity Framework Core

### Contexto de Persistência (DbContext)

O banco e esquema com o qual iremos nos comunicar.

```json
"ConnectionStrings": {
    "PessoaConnString" : "server=localhost;database=pessoa_db;user=root;password=admin123"
}
```

```cs
using Microsoft.EntityFrameworkCore;

public class PessoaContext : DbContext {

    public HeroContext(DbContextOptions options)
        : base(options) {
    }

}
```

### Conjunto de Persistência (DbSet)

```cs
public class PessoaContext : DbContext {

  public HeroContext(DbContextOptions options)
    : base(options) {
  }

  public DbSet<Pessoa> Pessoas {get; set; }
  public DbSet<Endereco> Enderecos {get; set; }
  public DbSet<Carro> Carros {get; set; }

}
```

## Swagger

Swagger fornece um cardápio do que podemos consumir da nossa API.

Existem outras além do Swagger e recomenda-se sempre entregar projetos com API's documentadas.

## Wrapping

```cs
public class ListaEspelhada {

  public List<int> list;

  public void Add(int element){
    list.Add(element);
    list.Insert(0, element);
  }

}
```
