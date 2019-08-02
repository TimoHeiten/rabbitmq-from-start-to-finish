import pika
import ConnectLocal

channel = ConnectLocal.do_connect()

body = "dead valley message - {0}"

_input = ""
counter = 0
while (_input != "q"):
    counter += 1
    default_exchange = ""
    channel.basic_publish(default_exchange, routing_key="dead_letter", body=body.format(counter))

    print("sent to dead letter!, press q to quit or any key to send more messages")
    _input = input()
    print()
