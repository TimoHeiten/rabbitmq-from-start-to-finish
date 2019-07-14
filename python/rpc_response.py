import pika
import ConnectLocal as locale

# connect
with locale.do_connect() as channel:
# declare queue
    channel.queue_declare("request_queue", durable=True)
# bind queue to amq.direct
    channel.queue_bind("request_queue", "amq.direct", routing_key="request")

    def callback(ch, method, props, body):
        print("now doing the response!")
#   in consumer publish to reply_to tag
        ch.basic_publish(routing_key=props.reply_to, exchange=""
                , body="response to the request: '{0}'".format(body))
        ch.basic_ack(delivery_tag=method.delivery_tag)

# basic.consume
    channel.basic_consume("request_queue",callback)
    channel.start_consuming()
# close