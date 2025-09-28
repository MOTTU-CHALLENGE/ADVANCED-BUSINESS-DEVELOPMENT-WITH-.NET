Ai eu preciso também de 'Repositoris', 'Services', e 'Context' sendo 

'Repositoris' comunicação com o banco de dados
'Services' pra regra de negócio
'Contexts' para 'AppDbContext' e montar um banco de dados bem robusto

aaah eu não sei se isso se encaixa em .net ou é só em java, mas minha maior dúvida pe na parte do db contexts, isso existe mesmo? por que acho que acabei de inventar kkkk 

que eu tenho que usar agora um banco de dados SQL e um NoSQL, ai queria deixa bem bonito 


```bash
docker run -d \
  --name mysql \
  -e MYSQL_ROOT_PASSWORD=senha123 \
  -e MYSQL_DATABASE=mottuDB \
  -e MYSQL_USER=mottuser \
  -e MYSQL_PASSWORD=mottupass \
  -v mottu-vol:/var/lib/mysql \
  -p 3306:3306 \
  mysql:8.0
```

/api/PatioApi


## CLI da aplicação 

1. sql



docker exec -it mysql-dev mysql -u root -p

CREATE DATABASE mottuDB;

CREATE USER 'mottuser'@'%' IDENTIFIED BY 'mottupass'
GRANT CREATE, SELECT, INSERT, UPDATE, DELETE ON mottuDB.* TO 'mottuser'@'%';
FLUSH PRIVILEGES;

2. mongo

docker exec -it mongo-dev mongosh -u admin -p adminpass --authenticationDatabase admin

use mottuDB

db.createUser({
  user: "mottuser",
  pwd: "mottupass",
  roles: [
    {
      role: "readWrite",
      db: "mottuDB"
    }
  ]
})


usando de variaveis de ambiente 