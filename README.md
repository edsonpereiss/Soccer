# Iniciar do Zero

# // comments command docker compose
# command: [--auth]

# docker stop $(docker ps -aq)
# docker system prune --all --force --volumes
# docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
# docker exec -it soccerDb bash

# mongo

# use admin
# db.createUser(
#   {
#     user: "root",
#     pwd: "Mongo2021", // or cleartext password
#     roles: [ { role: "userAdminAnyDatabase", db: "admin" }, "readWriteAnyDatabase" ]
#   }
# )


# use soccerDb

# db.createUser(
#   {
#     user: "soccer",
#     pwd:  "Mongo2021",   // or cleartext password
#     roles: [ { role: "readWrite", db: "soccerDb" } ]
#   }
# )

# db.adminCommand( { shutdown: 1 } )

# docker stop soccerDb

# // Add command docker compose
# command: [--auth]

# docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

# mongodb://soccer:Mongo2021@localhost:27017

