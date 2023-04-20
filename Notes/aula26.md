# Relacionamentos de entidades

## 1:n (um pra muitos)

```cs
public class User : Entity {

  public string Username {get; set;}
  public string Email {get; set;}
  public string Password {get; set;}
  public virtual ICollection<Post> Posts {get; set;}

}

public class Post : Entity {

  public string Title {get; set;}
  public string Text {get; set;}
  public DateTime moment {get; set;}

  public int UserId {get; set;}
  public virtual User User {get; set;}
}
```

- Dependencia sobre `Proxies`

program.cs

```cs
builder.Services.AddDbContext<FilmeContext>(opts =>
    opts.UseLazyLoadingProxies().UseMySql(
      connectionString,
      ServerVersion.AutoDetect(connectionString))
);
```

### Automapper

sintaxe:

```cs
  ForMember(dest => dest.member, opt => opt.MapFrom(src => src.member) )
```

```cs
public class UserProfile : Profile {
  CreateMap<User, UserViewDto>()
    .ForMember(userViewDto => userViewDto.PostsViewDto,
     opt => opt.MapFrom(user => user.posts));
}
```

## n:n ()

- has one
- has many

user <---> group

```cs
public class User : Entity {

  public string Username {get; set;}
  public string Email {get; set;}
  public string Password {get; set;}
  public virtual ICollection<Group> Groups {get; set;}

}

public class Group : Entity {

  public string Name {get; set;}
  public string Email {get; set;}
  public string Password {get; set;}
  public virtual ICollection<User> Users {get; set;}

}
```

```cs
public class User : Entity {
  public string Username {get; set;}
  public string Email {get; set;}
  public string Password {get; set;}
  public virtual ICollection<UserGroup> UserGroups {get; set;}
}

public class Group : Entity {
  public string Name {get; set;}
  public string Email {get; set;}
  public string Password {get; set;}
  public virtual ICollection<UserGroup> UserGroups {get; set;}
}

public class UserGroup {
  public int UserId {get; set;}
  public virtual User User {get; set;}
  public int GroupId {get; set;}
  public virtual Group Group {get; set;}
}
```

dbContext:

```cs
public class UserContext : DbContext {
  public DbSet<User> Users {get; set;}
  public DbSet<Group> Groups {get; set;}
  public DbSet<UserGroup> UserGroups {get; set;}

  protected override void OnModelCreating(ModelBuilder builder) {

    builder.Entity<UserGroup>()
      .HasKey(userGroup => new {userGroup.UserId, userGroup.GroupId})

    builder.Entity<UserGroup>()
      .HasOne(userGroup => userGroup.User)
      .WithMany(user => user.UserGroups)
      .HasForeignKey(userGroup => userGroup.UserId);

    builder.Entity<UserGroup>()
      .HasOne(userGroup => userGroup.Group)
      .WithMany(group => group.UserGroups)
      .HasForeignKey(userGroup => userGroup.GroupId);
  }

}
```

### Mapeamentos um pouco além

```cs
public class Place : Entity {

  public string Name {get; set;}
  public DateTime foundation {get; set;}
  public int maximumCapacity {get; set;}

}

public class Content : Entity {

  public string Theme {get; set;}
  public TimeSpan Duration {get; set;}
  public virtual ICollection<Event> Events  {get; set;}

}

public class Event : Entity {

  public virtual ICollection<Place> Places  {get; set;}
}
```
