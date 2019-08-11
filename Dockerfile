# needed to append .txt because Udemy does not allow for Dockerfile extension on files 

FROM rabbitmq:3-management

# Define environment variables.
ENV RABBITMQ_ERLANG_COOKIE="i love cookies"
ENV RABBITMQ_NODENAME="rabbit1@docker1"

# no hostname here, because you need to specify this with the docker container run command. (call it --hostname docker1)
EXPOSE 15672
EXPOSE 5672