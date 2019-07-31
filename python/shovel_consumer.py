import pika 
import ConnectLocal as cL

channel = cL.do_connect()

def consumer(ch, method, props, body):
    msg = body.decode()
    print(msg)
    print("from shovel mate")
    ch.basic_ack(delivery_tag=method.delivery_tag)

channel.basic_consume("shovel_queue", consumer)

channel.start_consuming()