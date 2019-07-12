import pika 
import ConnectLocal as conn

queue_name = "auto_delete"
queue_name2 = "non_durable"
# queue_name3 = "exclusive"
def callback(ch, met, body, prop):
    pass

with conn.do_connect() as channel:
    queue_ok_result = channel.queue_declare(queue_name, auto_delete=True)
    channel.queue_declare(queue_name2, durable=False)
    print("declared both, lets check the UI")
    channel.basic_consume(queue_name, callback)
