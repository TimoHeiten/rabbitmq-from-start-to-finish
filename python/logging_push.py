import ConnectLocal
import sys

args = sys.argv
exchange, routing = args[1], args[2]

body = "published to '{0}' with routing_key '{1}'".format(exchange, routing)

channel = ConnectLocal.do_connect()

channel.basic_publish(exchange, routing_key=routing, body=body)