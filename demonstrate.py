import pika

credentials = pika.PlainCredentials("guest", "guest")

conn_param = pika.connection.ConnectionParameters(credentials=credentials)

connection = pika.BlockingConnection(conn_param)

channel = connection.channel()

queue_name = 'pika_queue'
def consume_msg(ch, meth, props, body):
    print(body.decode())
    channel.basic_ack(meth.delivery_tag)

channel.basic_consume(queue_name, consume_msg)
channel.start_consuming()