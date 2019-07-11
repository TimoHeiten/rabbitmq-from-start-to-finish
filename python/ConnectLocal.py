import pika

def do_connect():
    credentials = pika.PlainCredentials("guest", "guest")
    conn_param = pika.connection.ConnectionParameters(credentials=credentials)

    # establish tcp connection
    connection = pika.BlockingConnection(conn_param)
    # get channel
    channel = connection.channel()
    return channel