import pika
from ConnectLocal import do_connect

def consuming_callback(ch, method, body):
   msg = body.decode()
   if "reject" in msg:
       ch.basic_nack(delivery_tag=method.delivery_tag, requeue=True)
       print("N-acked the message")
   else:
        ch.basic_ack(delivery_tag=method.delivery_tag)
        print("acked message: {0}".format(msg))
  

with do_connect() as channel:
    value = ""
    while (value != "q"):
        (method, props, body) = channel.basic_get("pika_queue", auto_ack=False)
        if body:
            consuming_callback(channel, method, body)
            print("any key to continue, q to stop", end="")
        value = input()

print ("done")