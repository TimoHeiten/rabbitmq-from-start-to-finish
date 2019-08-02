import ConnectLocal
import requests
from requests.auth import HTTPBasicAuth as auth

def get_exchange_names():
    url = "http://localhost:15672/api/exchanges"
    response = requests.get(url, auth=auth('guest', 'guest'))

    array = response.json()

    return [exchange["name"] for exchange in array if exchange['vhost'] == '/']

all_exchanges = get_exchange_names()

def consumer(ch, method, props, body):
    msg = body.decode()
    print('logged: \n "{0}"'.format(msg))

channel = ConnectLocal.do_connect()
log_queue = "logging_queue"
channel.queue_declare(log_queue, durable=True)

for name in all_exchanges:
    if name != "":
        channel.queue_bind(log_queue, name, routing_key='#') # binds to all matches!

channel.basic_consume(log_queue, consumer, auto_ack=True)

print("start consuming")
channel.start_consuming()
