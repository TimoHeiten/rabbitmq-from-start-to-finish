import pika
import ConnectLocal

channel = ConnectLocal.do_connect()

def consumer(ch, method, props, body):
    print(body.decode())

    ch.basic_ack(delivery_tag=method.delivery_tag)

channel.basic_consume("dead_end", consumer)

channel.start_consuming()