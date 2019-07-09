import pika 

credentials = pika.PlainCredentials("guest", "guest")
conn_param = pika.connection.ConnectionParameters(credentials=credentials)

# establish tcp connection
connection = pika.BlockingConnection(conn_param)
# get channel
channel = connection.channel()

# use queue
queue_name = "pika_queue"
def consume_msg(channel, method, props, body):
    print(body)
    channel.basic_ack(method.delivery_tag)

channel.basic_consume(queue_name, consume_msg)

channel.start_consuming()