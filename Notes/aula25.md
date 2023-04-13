# Models em APIs

## Model

user(model):

- username
- email
- password
- phone (1:1)
- adress (1:1)
- followers (1:n)
- following (1:n)
- posts (1:n)
- isactive

## DTO's

userRegister

- username
- email
- password
- phone (1:1)

userLogin

- username
- password

userProfile

- username
- followers (1:n)
- following (1:n)
- posts (1:n)
