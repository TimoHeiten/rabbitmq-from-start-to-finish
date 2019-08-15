import pika
import time

_creds = pika.PlainCredentials('guest', 'guest')
_conn = pika.connection.ConnectionParameters(credentials=_creds)
_connection = pika.BlockingConnection(parameters=_conn)

def client(corr_id, msg):
    print("start client with {0} and {1}".format(corr_id, msg))

    ch = _connection.channel()

    response_queue = 'response_queue'
    ch.queue_declare(response_queue) # queue for responses obv

    def _run():
        ch.basic_publish(exchange='', routing_key='request_queue',
                properties=pika.BasicProperties(reply_to=response_queue, correlation_id=corr_id),
                body=msg
        )

    def consume(ch, m, props, body):
        if corr_id == props.correlation_id:
            print("is correct id: {0}".format(corr_id))
            ch.basic_ack(delivery_tag=m.delivery_tag)
            time.sleep(1.5)
            _run() # for continous message sending
        else:
            print("expected {0} - got {1}".format(corr_id, props.correlation_id))
            ch.basic_reject(delivery_tag=m.delivery_tag, requeue=True)
    
    ch.basic_consume(response_queue, consume, auto_ack=False)

    _run()
    while True:
        _connection.process_data_events()


def server():
    def consume(ch, m, props, body):
        result = body.decode()
        ch.basic_publish(exchange='',
            routing_key=props.reply_to,
            properties=pika.BasicProperties(
                correlation_id=props.correlation_id),
                body = "response to : {0}".format(result)
            )
        ch.basic_ack(delivery_tag=m.delivery_tag)
    
    ch = _connection.channel()
    ch.basic_consume("request_queue", consume)
    ch.start_consuming()