import pika
import ConnectLocal

channel = ConnectLocal.do_connect()


body = "from tracing published!"
while (True):
    channel.basic_publish("amq.direct", "pika_queue", body)
    v = input("press ctrl+c to stop or any key to continue publishing")